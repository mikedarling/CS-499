using AnimalRescue.Data.Models.Entities;
using AnimalRescue.Data.Repositories.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;

namespace AnimalRescue.Security.Authentication
{
    public class UserService : IUserService
    {

        #region Constructors

        public UserService(AnimalRescueContext dbContext, IOwinContext owinContext)
        {
            this._owinContext = owinContext;

            if (_userMgr == null)
            {
                var userStore = new UserStore<ApplicationUser>(dbContext);
                _userMgr = new UserManager<ApplicationUser>(userStore);
            }
        }

        #endregion

        #region Local Variables

        private IOwinContext _owinContext;

        private static UserManager<ApplicationUser> _userMgr;

        #endregion

        #region Methods

        public async Task<bool> AttemptLogin(string username, string password)
        {
            var user = await _userMgr.FindAsync(username, password);

            if (user == null)
            {
                return false;
            }

            this._owinContext.Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            var identity = await _userMgr.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            if (!identity.IsAuthenticated)
            {
                return false;
            }

            this._owinContext.Authentication.SignIn(new AuthenticationProperties() { IsPersistent = false }, identity);

            return true;
        }

        public bool Logout()
        {
            this._owinContext.Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return true;
        }

        #endregion

    }
}
