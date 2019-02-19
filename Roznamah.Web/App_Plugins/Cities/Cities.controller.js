angular.module("umbraco").controller("Cities", function ($scope, $http) {

    //get all city list to bind data for custom city dropdown
    $http.get("/umbraco/api/CustomProperties/GetAllCities").then(function (response) {
    $scope.Items = response.data;
    });
});
