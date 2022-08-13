graziosoApp.controller('DetailsController', ['$scope' , '$rootScope', 'AnimalService', 'AnimalTypeService', 'BreedService', function DetailsController($scope, $rootScope, AnimalService, AnimalTypeService, BreedService) {
    var breeds;
    var clean;
    
    /****************************\
    | ** Root Scope Listeners ** | 
    \****************************/

    $rootScope.$on('selectedAnimalUpdated', function() {
        AnimalService
            .getAnimalDetails()
            .then(function(data) {
                $scope.animal = data;
                clean = angular.copy(data);
                $scope.filterBreeds();
                if ($scope.animalTypes) {
                    $scope.currentAnimalType = $scope.animalTypes.find(animalType => animalType.Id == data.AnimalTypeId).Name
                }
                if (breeds) {
                     var currentBreeds = breeds
                        .filter(breed => $scope.animal.BreedIds.indexOf(breed.Id) > -1)
                     $scope.currentBreeds = currentBreeds
                        .map(breed => breed.Name)
                        .join('/');
                }               
            });
    });

    /***************\
    | ** Helpers ** | 
    \***************/

    $scope.filterBreeds = function() {
        if (!breeds) {
            $scope.breeds = null;
            return;
        }

        if (!($scope.animalTypes)) {
            $scope.breeds = breeds;
            return;
        }

        if (!($scope.animal) || !($scope.animal.AnimalTypeId)) {
            $scope.breeds = breeds;
            return;
        }

        $scope.breeds = breeds
            .filter(breed => breed.AnimalTypeId == $scope.animal.AnimalTypeId);

    };

    /**************\
    | ** Events ** | 
    \**************/

    $scope.toggleEdit = function(event) {
        event.preventDefault();
        $scope.editMode = !($scope.editMode);
    };

    $scope.update = function(event) {
        event.preventDefault();
        if (this.editForm.$pristine) {
            return;
        }
        AnimalService
            .updateAnimal($scope.animal)
            .then(function(data) {
                clean = angular.copy(data);
                $rootScope.$emit('selectedAnimalDetailsUpdated')
                $scope.editMode = false;
            })
    };

    $scope.cancel = function(event) {
        event.preventDefault();
        $scope.editMode = false;
        if (this.editForm.$pristine) {
            return;
        }
        this.editForm.$setPristine(true);
        $scope.animal = angular.copy(clean);
    }
    
    /************\
    | ** Init ** | 
    \************/

    function init() {
        AnimalTypeService
            .getAll()
            .then(function(data) {
                $scope.animalTypes = data;
                $scope.filterBreeds();
            });
        
        BreedService
            .getAll()
            .then(function(data) {
                breeds = data;
                $scope.filterBreeds();
            });
    };
   
    init();
}]);
 