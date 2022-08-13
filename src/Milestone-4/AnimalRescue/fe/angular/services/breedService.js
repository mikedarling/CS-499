graziosoApp.service('BreedService', ['HttpRequestService', function BreedService(HttpRequestService) {
    var breeds;
    
    var getBreeds = function() {
        return breeds || (
            HttpRequestService
                .get('/breeds')
                .then(function(response) {
                    if (!response || !response.models) {
                        return;
                    }
                    breeds = response.models;
                    return breeds;
                })
        );
    };

    return {
        getAll: getBreeds
    };
}]);