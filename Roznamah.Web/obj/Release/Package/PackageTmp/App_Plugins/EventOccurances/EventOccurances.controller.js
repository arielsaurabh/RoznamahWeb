angular.module("umbraco").controller("EventOccurances", function ($scope, $http, $window, editorState) {

    debugger;
    var pageId = $window.sessionStorage.getItem("pageId");
    if (editorState.current.id != pageId) {
        $window.sessionStorage.clear();
    }

    //get all cities to bind city dropdown
    $http.get("/umbraco/api/CustomProperties/GetAllCities").then(function (response) {
        $scope.Items = response.data;
    });

    $("button.btn.umb-button__button.btn-success").click(function () {
        $window.sessionStorage.clear();
    })

    var currentContentNameIndex = "";

    var getCurrentPropertyHtml = document.querySelectorAll("[data-element='property-eventOccurance']");
    if (getCurrentPropertyHtml.length) {
        var currentSelectedIndexData = getCurrentPropertyHtml[0].querySelector(".umb-nested-content__item.ng-scope.umb-nested-content__item--active .umb-nested-content__item-name");
        if (currentSelectedIndexData) {
            currentContentNameIndex = currentSelectedIndexData.innerHTML;
        }
    }


    //custom plugin data model
    $scope.modelData = {
        city: 0,
        venue: 0,
        date: {
            from: "",
            to: ""
        },
        time: {
            from: "",
            to: ""
        },
        location: ""
    };

    //error messages
    $scope.fromDateError = false;
    $scope.toDateError = false;

    //venue special date and venue opening days for validating event time and date
    $scope.venueSpecialDate = [];
    $scope.venueOpeningDays;

    //get venues for selected city
    $scope.getVenuesByCity = function () {

        $http.get("/umbraco/api/CustomProperties/GetAllVelues?cityId=" + $scope.modelData.city).then(function (response) {
            $scope.Items2 = response.data;
        });
    }

    $scope.getModelValue = function (tempData) {
        var data = $scope.modelData;
        if (tempData == "fdate") {
            var filtereData = $scope.venueSpecialDate.filter((item => item.isHoliday == true && item.occasionDate == data.date.from));
            if (filtereData.length) {
                $scope.fromDateError = true;
            }
            else {
                $scope.fromDateError = false;
            }
        }
        else if (tempData == "tdate") {
            var filtereData = $scope.venueSpecialDate.filter((item => item.isHoliday == true && item.occasionDate == data.date.to));
            if (filtereData.length) {
                $scope.toDateError = true;
            }
            else {
                $scope.toDateError = false;
            }
        }
        else if (tempData == "time") {

        }
        $scope.model.value = JSON.stringify($scope.modelData);
        $window.sessionStorage.setItem(currentContentNameIndex, $scope.model.value);
        $window.sessionStorage.setItem("pageId", editorState.current.id);
    }


    if (editorState.current.id) {
        //var currentContentNameIndex = getCurrentItemName();

        //var getCurrentPropertyHtml = document.querySelectorAll("[data-element='property-eventOccurance']");
        //if (getCurrentPropertyHtml.length) {
        //    var currentSelectedIndexData = getCurrentPropertyHtml[0].querySelector(".umb-nested-content__item.ng-scope.umb-nested-content__item--active .umb-nested-content__item-name");
        //    if (currentSelectedIndexData) {
        //        currentContentNameIndex = currentSelectedIndexData.innerHTML;
        //    }
        //}
        var sessionStorage = $window.sessionStorage.getItem(currentContentNameIndex);

        if (currentContentNameIndex != "") {
            if (sessionStorage) {
                $scope.modelData = JSON.parse(sessionStorage);
                $scope.getModelValue();
                $scope.getVenuesByCity();
            }
            else {
                $http.get("/umbraco/api/CustomProperties/GetEventOccurance?pageId=" + editorState.current.id).then(function (response) {
                    if (response.data != "null") {
                        var responseData = JSON.parse(JSON.parse(response.data))
                        var currentTabData = responseData.filter(item => item.name == currentContentNameIndex);
                        if (currentTabData.length) {

                            $scope.modelData = JSON.parse(currentTabData[0].eventOccurance);
                            $scope.getModelValue();
                            $scope.getVenuesByCity();
                        }
                    }
                });
            }

        }
    }

    $scope.getLocationForVenue = function () {

        $http.get("/umbraco/api/CustomProperties/GetLocationForVenue?venueId=" + $scope.modelData.venue).then(function (response) {
            var data = response.data;
            $scope.modelData.location = data[0].Value;
            var specialDate = JSON.parse(data[1].Value);
            $scope.venueOpeningDays = JSON.parse(data[2].Value);
            $scope.venueSpecialDate = [];
            specialDate.forEach(function (item) {
                $scope.venueSpecialDate.push(JSON.parse(item.specialOccasion));
            });

            $scope.getModelValue();
        });
    }

});