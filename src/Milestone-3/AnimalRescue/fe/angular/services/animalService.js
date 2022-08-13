var animalService = graziosoApp.service('AnimalService', ['HttpRequestService', function AnimalService(HttpRequestService) {
    var animals;
    var selectedAnimal;
    
    var getAnimals = function() {
        return animals || (
            HttpRequestService
                .get('/animals')
                .then(function(response) {
                    if (!response || !response.models) {
                        return;
                    }
                    animals = response.models;
                    return animals;
                })
        );
    };
    
    var getSelectedAnimal = function() {
        return selectedAnimal;
    };

    var setSelectedAnimal = function(id) {
        if (!animals) {
            return;
        }

        selectedAnimal = animals.find(x => x.Id == id);
    };

    return {
        getAll: getAnimals,
        getSelectedAnimal: getSelectedAnimal,
        setSelectedAnimal: setSelectedAnimal
    };
}]);