angular.module("umbraco").controller("VenueSpecialOccasion", function ($scope, $http, editorState) {

    $scope.ramdan = false;
    $scope.adha = false;
    $scope.iftar = false;
    $scope.nDay = false;
    $scope.isHoliday = false;

    //set empty model if event is new
    $scope.modelData = {
        occasionDate: "",
        isHoliday: false,
        ocassionName: "",
        openingHours: "",
        closingHours: ""
    };

    if (editorState.current.id) {
        var currentContentNamendex = "";
        var currentTabIndexHtml = $(".umb-nested-content__item.ng-scope.umb-nested-content__item--active .umb-nested-content__item-name").html();

        var getCurrentPropertyHtml = document.querySelectorAll("[data-element='property-venueSpecialOccasion']");
        if (getCurrentPropertyHtml.length) {
            var currentSelectedIndexData = getCurrentPropertyHtml[0].querySelector(".umb-nested-content__item.ng-scope.umb-nested-content__item--active .umb-nested-content__item-name");
            if (currentSelectedIndexData) {
                currentContentNamendex = currentSelectedIndexData.innerHTML;
            }
        }
        
        if (currentContentNamendex != "") {
            $http.get("/umbraco/api/CustomProperties/GetSpecialOccasionsForEvent?pageId=" + editorState.current.id + "&key=" + currentContentNamendex).then(function (response) {
                if (response.data != "null") {
                    var responseData = JSON.parse(JSON.parse(response.data))
                    var currentTabData = responseData.filter(item => item.name == currentContentNamendex);
                    if (currentTabData.length) {
                        debugger;
                        $scope.modelData = JSON.parse(currentTabData[0].specialOccasion);
                        $scope.isHoliday = $scope.modelData.isHoliday;
                        switch ($scope.modelData.ocassionName) {
                            case "Adha Eid":
                                $scope.adha = true;
                                break;
                            case "Iftar Eid":
                                $scope.iftar = true;
                                break;

                            case "Ramdan":
                                $scope.ramdan = true;
                                break;

                            case "National Day":
                                $scope.nDay = true;
                                break;
                        }
                    }

                }

            });
        }
        //get opening time for event if event is in edit mode
    }

    //update model on field value update
    $scope.getModelValue = function (data, previousValue) {

        if (data == "ramdan") {
            $scope.ramdan = true;
            $scope.adha = false;
            $scope.iftar = false;
            $scope.nDay = false;
            $scope.modelData.ocassionName = "Ramdan";
            $scope.modelData.isHoliday = false;
            $scope.isHoliday = false;
        }
        else if (data == "adha") {
            $scope.ramdan = false;
            $scope.adha = true;
            $scope.iftar = false;
            $scope.nDay = false;
            $scope.modelData.ocassionName = "Adha Eid";
            $scope.modelData.isHoliday = false;
            $scope.isHoliday = false;
        }
        else if (data == "iftar") {
            $scope.ramdan = false;
            $scope.adha = false;
            $scope.iftar = true;
            $scope.nDay = false;
            $scope.modelData.ocassionName = "Iftar Eid";
            $scope.modelData.isHoliday = false;
            $scope.isHoliday = false;
        }
        else if (data == "nDay") {
            $scope.ramdan = false;
            $scope.adha = false;
            $scope.iftar = false;
            $scope.nDay = true;
            $scope.modelData.ocassionName = "National Day";
            $scope.modelData.isHoliday = false;
            $scope.isHoliday = false;
        }
        else if (data == "isHoliday") {
            $scope.isHoliday = true;
            $scope.ramdan = false;
            $scope.adha = false;
            $scope.iftar = false;
            $scope.nDay = false;
            $scope.modelData.ocassionName = "";
            $scope.modelData.isHoliday = true;
        }

        var data = $scope.modelData;
        $scope.model.value = JSON.stringify($scope.modelData);
    }

});