graziosoApp.service('AnimalService', ['HttpRequestService', function AnimalService(HttpRequestService) {
    var selectedAnimal;

    var getAnimals = function() {
        return HttpRequestService
            .get('/animals')
            .then(function(response) {
                if (!response || !response.models) {
                    return;
                }
                animals = response.models;
                return animals;
            });
    };

    var getAnimalDetails = function() {
        return HttpRequestService
            .get(`/animals/details/${selectedAnimal.AnimalId}`)
            .then(function(response) {
                if (!response || !response.model) {
                    return;
                }
                return response.model;
            })
    };

    var updateAnimal = function(animal) {
        return HttpRequestService
            .post('/animals/details', animal )
            .then(function(response) {
                if (!response || !response.model) {
                    return;
                }
                selectedAnimal = response.model;
                return response.model;
            })
    }
    
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
        setSelectedAnimal: setSelectedAnimal,
        getAnimalDetails: getAnimalDetails,
        updateAnimal: updateAnimal
    };
}]);