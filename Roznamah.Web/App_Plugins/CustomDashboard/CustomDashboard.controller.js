angular.module("umbraco").controller("CustomDashboardController", function ($scope, $http) {
    //get dashboard data for custom dashboard
    $scope.DashboardData = {
        EventCount: 0,
        VenueCount: 0,
        OrganizerCount: 0,
        EntityCount: 0,
        UpcomingEventCount: 0,
        PastEventCount: 0,
        RegisteredUserCount: 0
    }

    var month = new Array();
    month[0] = "Jan";
    month[1] = "Feb";
    month[2] = "Mar";
    month[3] = "Apr";
    month[4] = "May";
    month[5] = "Jun";
    month[6] = "Jul";
    month[7] = "Aug";
    month[8] = "Sep";
    month[9] = "Oct";
    month[10] = "Nov";
    month[11] = "Dec";

    function getUpcoming_pastEvents(eventData, callback){
        var upcomingEvents = 0;
        var pastEvents = 0;
        var event_detailData = [];

        eventData.forEach(function (item) {
            var eventDetail = {
                id: "",
                name: "",
                isUpcoming: false,
                isPast: false,
                date: "",
                month: "",
                year:""
            }
            eventDetail.id = item.Id;
            eventDetail.name = item.Name;

            var occurrences = JSON.parse(item.Occurrences);
            occurrences.forEach(function (item) {
                var eventOccuranceDate = JSON.parse(item.eventOccurance).date;
                var fromDate = eventOccuranceDate.from;
                var toDate = eventOccuranceDate.to;
                eventDetail.date = fromDate;
                eventDetail.month = month[new Date(fromDate).getMonth()];
                eventDetail.year = new Date(fromDate).getFullYear();
                
                if (new Date(fromDate) > new Date()) {
                    eventDetail.isUpcoming = true;
                }
                if (new Date(toDate) < new Date()) {
                    eventDetail.isPast = true;
                }
            })
            event_detailData.push(eventDetail);
        });
        callback(event_detailData);
    }

    function getNumberOfEvent_EachMonth(EventData, callback) {
        var eventPerMonth = [];
        month.forEach(function (data) {
            eventPerMonth.push(EventData.filter(item => item.month == data).length)
        })
        callback(eventPerMonth);
    }

    $http.get("/umbraco/api/CustomProperties/GetDashboardCountsData").then(function (response) {
        debugger;
        if (response.data) {
            $scope.DashboardData.EventCount = response.data.EventData.length;
            $scope.DashboardData.VenueCount = response.data.VenueCount;
            $scope.DashboardData.OrganizerCount = response.data.OrganizerCount;
            $scope.DashboardData.EntityCount = response.data.EntityCount;
            getUpcoming_pastEvents(response.data.EventData, function (data){
                var d = data;
                if (data.length) {
                    $scope.DashboardData.UpcomingEventCount = data.filter(s => s.isUpcoming == true).length;
                    $scope.DashboardData.PastEventCount = data.filter(s => s.isPast == true).length;
                    getNumberOfEvent_EachMonth(data, function (graphData) {
                        console.log(graphData)
                        config.data.datasets[0].data = graphData;
                    });
                }
            });
        }
    });

});
