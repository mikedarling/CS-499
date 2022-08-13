graziosoApp.service('HttpRequestService', ['$http', '$q', function HttpRequestService($http, $q) {
    var get = function (url, qs, headers) {
        if (qs && qs.length > 0) {
            url = url + '?';
            for (x in qs) {   
                url = url + qs[x].name + '=' + qs[x].value;
                if (x != (qs.length - 1)) {
                    url = url + '&';
                }
            }  
        }
        return $http({
            method: 'GET',
            url: url,
            headers: headers
        })
        .then(function (response) {
            if (typeof response.data === 'object' || typeof response.data === 'string') {
                if (response && response.data && response.data.exception) {
                    alert('There was an error processing your request.');
                }
                return response.data;
            } else {
                console.log(typeof response.data);
                return $q.reject(response.data);
            }
        }, function (response) {
            return $q.reject(response.data);
        });
    };

    var post = function (url, data, headers) {
        return $http({
            method: 'POST',
            url: url,
            data: data,
            headers: headers
        })
        .then(function (response) {
            if (typeof response.data === 'object' || typeof response.data === 'string') {
                if (response && response.data && response.data.exception) {
                    alert('There was an error processing your request.');
                }
                return response.data;
            } else {
                console.log(typeof response.data)
                return $q.reject(response.data);
            }
        }, function (response) {
            return $q.reject(response.data)
        });
    };

    return {
        get: get,

        post: post
    }
}]);