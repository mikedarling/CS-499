graziosoApp.service('AnimalTypeService', ['HttpRequestService', function AnimalTypeService(HttpRequestService) {
    var animalTypes;
    
    var getAnimalTypes = function() {
        return animalTypes || (
            HttpRequestService
                .get('/animaltypes')
                .then(function(response) {
                    if (!response || !response.models) {
                        return;
                    }
                    animalTypes = response.models;
                    return animalTypes;
                })
        );
    };

    return {
        getAll: getAnimalTypes
    };
}]);