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
      var coords = new google.maps.LatLng($scope.selectedAnimal.LocationLat, $scope.selectedAnimal.LocationLong);

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
