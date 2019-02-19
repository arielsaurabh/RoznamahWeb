angular.module("umbraco").controller("Tickets", function ($scope, $http, editorState) {

    $scope.modelData = {
        "isFreeTickets": false,
        "ticketHasPrice": false,
        "singleTicketPrice": 0,
        "ticketPriceRange": {
            "priceStart": 0,
            "priceUpTo": 0
        },
        "onlineTickets": false,
        "onlineTicketUrl": "",
        "offlineTickets": false,
        "isAvailableAtEventGate": false,
        "isAvailableAtShops": false,
        "shopName": ""
    };

    $scope.getModelValue = function (data) {
        if (data == "free" && $scope.modelData.isFreeTickets) {
            $scope.modelData.ticketHasPrice = false;
            $scope.modelData.onlineTickets = false;
            $scope.modelData.offlineTickets = false;
            $scope.modelData.isAvailableAtEventGate = false;
            $scope.modelData.isAvailableAtShops = false;
        }
        else if (data == "hasPrice" && $scope.modelData.ticketHasPrice) {
            $scope.modelData.isFreeTickets = false;
        }
        else if (data == "online" && $scope.modelData.onlineTickets) {
            $scope.modelData.offlineTickets = false;
        }
        else if (data == "offline" && $scope.modelData.offlineTickets) {
            $scope.modelData.onlineTickets = false;
        }
        else if (data == "gate" && $scope.modelData.isAvailableAtEventGate) {
            $scope.modelData.isAvailableAtShops = false;
        }
        else if (data == "shop" && $scope.modelData.isAvailableAtShops) {
            $scope.modelData.isAvailableAtEventGate = false;
        }
        $scope.model.value = JSON.stringify($scope.modelData);
    }

    $scope.changeTicketType = function () {
        debugger;
        var v = $scope.model.value;
        if ($scope.isFreeTicket) {
            $scope.TicketHasPrice = false;
        }
    }

    $http.get("/umbraco/api/CustomProperties/GetAllShops").then(function (response) {
        $scope.shopNames = response.data;
        $scope.getModelValue();
    });

    //get all shops if page is in edit mode
    if (editorState.current.id) {

        //get opening time for event if event is in edit mode
        $http.get("/umbraco/api/CustomProperties/GetTickets?pageId=" + editorState.current.id).then(function (response) {
            var responseData = JSON.parse(response.data);
            if (responseData) {
                $scope.modelData = JSON.parse(responseData);
            }

        });
    }
});