graziosoApp.controller('TableController', ['$scope' , '$rootScope', 'AnimalService', function TableController($scope, $rootScope, AnimalService) {
    function parseDate(date) {
        var value = date.replace(/\/Date\((.*?)\)\//, "$1");
        return new Date(parseInt(value));
    }
    
    $scope.isOnCurrentPage = function(rowNumber) {

        firstRow = ($scope.currentPage - 1) * $scope.pageSize;
        if (rowNumber < firstRow) {
            return false;
        }

        lastRow = $scope.currentPage * $scope.pageSize;
        if (rowNumber > lastRow) {
            return false;
        }
        
        return true;
    };

    $scope.updateLastPage = function() {
        var lastPage = Math.trunc($scope.animalCount / $scope.pageSize);
        
        if ($scope.animalCount % $scope.pageSize != 0) {
            lastPage++;
        }

        $scope.lastPage = lastPage;
    }

    $scope.resetPage = function() {
        $scope.currentPage = 1;
    }

    $scope.setSelectedAnimal = function(id) {
        AnimalService.setSelectedAnimal(id);
        $rootScope.$emit("selectedAnimalUpdated");
    }

    $scope.round = function(value, places) {
        var coefficient = Math.pow(10, places);
        return (Math.round(value * coefficient) / coefficient);
    }

    $scope.setSort = function(key) {
        $scope.currentPage = 1;

        if ($scope.sortKey == key) {
            $scope.sortKey = "-" + key;
            return;
        }

        if ($scope.sortKey == "-" + key) {
            $scope.sortKey = key;
            return;
        }

        $scope.sortKey = key;
    }

    $scope.formatDate = function(date) {
        
        return parseDate(date).toLocaleDateString();
    }

    $scope.animalFilter = function() {
        return function(animal) {
            if ($scope.animalId && !animal.AnimalId.toLowerCase().startsWith($scope.animalId.toLowerCase())) {
                return false;
            }

            if ($scope.animalName && !animal.Name.toLowerCase().startsWith($scope.animalName.toLowerCase())) {
                return false;
            }

            if ($scope.animalBreed && !animal.Breed.toLowerCase().startsWith($scope.animalBreed.toLowerCase())) {
                return false;
            }

            if ($scope.animalType && animal.AnimalType != $scope.animalType) {
                return false;
            }

            if ($scope.dobMin && parseDate(animal.DateOfBirth) < $scope.dobMin) {
                return false;
            }

            if ($scope.dobMax && parseDate(animal.DateOfBirth) > $scope.dobMax) {
                return false;
            }

            if ($scope.dooMin && parseDate(animal.Monthyear) < $scope.dooMin) {
                return false;
            }

            if ($scope.dooMax && parseDate(animal.Monthyear) > $scope.dooMax) {
                return false;
            }

            return true;
        }
    }

    $scope.init = function() {
        var now = new Date();
        var offset = now.getTimezoneOffset();

        $scope.today = new Date(now).toISOString().substring(0, 10);
        $scope.currentPage = 1;
        $scope.pageSize = 10;
        $scope.pageSizes = [10, 15, 25, 50];
        $scope.animalTypes = [
            { value: '', text: 'Any'},
            { value: 'Other', text: 'Other'},
            { value: 'Dog', text: 'Dog'},
            { value: 'Cat', text: 'Cat'},
            { value: 'Bird', text: 'Bird' }
        ];
        $scope.animalType = '';
        $scope.sortKey = 'AnimalId';
        AnimalService
            .getAll()
            .then(function(data) {
                $scope.animals = data;
                $scope.animalCount = data.length;
                $scope.updateLastPage();
                $scope.setSelectedAnimal($scope.animals[0].Id);
            });
    }

    $scope.init();
}]);
