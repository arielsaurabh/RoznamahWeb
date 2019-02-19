angular.module("umbraco").controller("CascadingDropdown", function ($scope, $http) {

    //get all cities list to bind with city dropdown
    $http.get("/umbraco/api/CustomProperties/GetAllCities").then(function (response) {
        $scope.Items = response.data;
    });

    $scope.cityId = "";
    $scope.valueId = "";

    //function to get all venues for events
    $scope.getAllVenues = function () {
       
        $http.get("/umbraco/api/CustomProperties/GetAllVelues?cityId=" + $scope.cityId).then(function (response) {
            $scope.Items2 = response.data;
        });
    }


});