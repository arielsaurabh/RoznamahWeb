angular.module("umbraco").controller("MyDropdown", function ($scope, $http) {
    $http.get("/umbraco/api/CustomProperties/GetAllEvents")
        .then(function (response) {

            $scope.Items = response.data;

        });
});
