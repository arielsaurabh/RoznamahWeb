angular.module("umbraco").controller("VenueLocation", function ($scope, $http, editorState) {

    //get twitter images if page is in edit mode
    if (editorState.current.id) {
        $http.get("/umbraco/api/CustomProperties/GetVenueLocation?pageId=" + editorState.current.id).then(function (response) {
            debugger;
            var responseData = JSON.parse(response.data);
            if (responseData) {
                $scope.modelData.shortLink = responseData;
            }
        });
    }

    $scope.modelData = {
        shortLink: "",
        lat: "",
        long:""
    };

    $scope.showMap = false;

    $scope.getModelValue = function () {
        $scope.model.value = $scope.modelData.shortLink;
    }
});