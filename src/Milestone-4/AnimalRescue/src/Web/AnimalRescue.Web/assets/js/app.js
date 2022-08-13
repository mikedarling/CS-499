var graziosoApp = angular.module('graziosoApp', []);
graziosoApp.controller('DetailsController', ['$scope' , '$rootScope', 'AnimalService', 'AnimalTypeService', 'BreedService', function DetailsController($scope, $rootScope, AnimalService, AnimalTypeService, BreedService) {
    var breeds;
    var clean;
    
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

    $scope.toggleEdit = function(event) {
        event.preventDefault();
        $scope.editMode = !($scope.editMode);
    };

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
 
graziosoApp.controller('MapController', ['$scope' , '$rootScope', 'AnimalService', function MapController($scope, $rootScope, AnimalService) {
   var marker; 
   var map;
   
   $scope.initialize = function() {
      map = new google.maps.Map(document.getElementById('map'));
   };
   
   $rootScope.$on('selectedAnimalUpdated', function() {
      $scope.selectedAnimal = AnimalService.getSelectedAnimal();
      if (marker) {
         marker.setMap(null);
      }
      var coords = new google.maps.LatLng($scope.selectedAnimal.Latitude, $scope.selectedAnimal.Longitude);

      marker = new google.maps.Marker({
         position: coords,
         title: $scope.selectedAnimal.Name
      });

      marker.setMap(map)
      map.panTo(coords);
      map.setZoom(8);
   });
     
   google.maps.event.addDomListener(window, 'load', $scope.initialize);   
}]);

graziosoApp.controller('TableController', ['$scope' , '$rootScope', 'AnimalService', 'AnimalTypeService', 'BreedService', function TableController($scope, $rootScope, AnimalService, AnimalTypeService, BreedService) {
    $rootScope.$on('selectedAnimalDetailsUpdated', function() {
        var updatedAnimal = AnimalService.getSelectedAnimal();
        var idx = $scope.animals
            .findIndex(animal => animal.Id == updatedAnimal.Id);
        $scope.animals[idx] = updatedAnimal;
        console.log($scope.animals[idx]);
    })
    
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
    }

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

            if ($scope.dooMin && parseDate(animal.DateOfOutcome) < $scope.dooMin) {
                return false;
            }

            if ($scope.dooMax && parseDate(animal.DateOfOutcome) > $scope.dooMax) {
                return false;
            }

            return true;
        }
    }

    $scope.filterBreeds = function () {

    }

    function init() {
        var now = new Date();
        var offset = now.getTimezoneOffset();

        $scope.today = new Date(now).toISOString().substring(0, 10);
        $scope.currentPage = 1;
        $scope.pageSize = 10;
        $scope.pageSizes = [10, 15, 25, 50];
        
        AnimalTypeService
            .getAll()
            .then(function(data) {
                $scope.animalTypes = data;
            });
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

    init();
}]);

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