angular.module("umbraco").controller("VenueOpeningTime", function ($scope, $http, editorState) {

    if (editorState.current.id) {
        //get opening time for event if event is in edit mode
        $http.get("/umbraco/api/CustomProperties/GetOpeningTimeForEvent?pageId=" + editorState.current.id).then(function (response) {
            debugger;
            $scope.modelData = JSON.parse(JSON.parse(response.data));
        });
    }
    else {
        //set empty model if event is new
        $scope.modelData = {
            "isAlwaysOpen": false,
            "isRestrictedVenue": false,
            "openingDays": {
                "s": false,
                "m": false,
                "t": false,
                "w": false,
                "th": false,
                "f": false,
                "sat": false
            },
            "closingDays": {
                "s": false,
                "m": false,
                "t": false,
                "w": false,
                "th": false,
                "f": false,
                "sat": false
            }
        };
    }
    //update model on field value update
    $scope.getModelValue = function (data, isOpenning) {

        if (data == "open" && $scope.modelData.isAlwaysOpen) {
            $scope.modelData.isRestrictedVenue = false;
        }
        else if (data == "restricted" && $scope.modelData.isRestrictedVenue) {
            $scope.modelData.isAlwaysOpen = false;
        }
        else if (isOpenning) {

            if ($scope.modelData.openingDays[data]) {
                $scope.modelData.closingDays[data] = false;
            }
            else if ($scope.modelData.closingDays[data]) {
                $scope.modelData.openingDays[data] = false;
            }
        }
        else {
            if ($scope.modelData.closingDays[data]) {
                $scope.modelData.openingDays[data] = false;
            }
            else if ($scope.modelData.openingDays[data]) {
                $scope.modelData.closingDays[data] = false;
            }
        }
        $scope.model.value = JSON.stringify($scope.modelData);
    }

});