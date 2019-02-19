angular.module("umbraco")
.controller("Diplo.DateRangePicker",
//inject umbracos assetsService
function ($scope, $timeout, assetsService) {

    assetsService
        .load([
            "/App_Plugins/DateRangePicker/lib/moment.min.js",
            "/App_Plugins/DateRangePicker/lib/daterangepicker.js"
        ])
        .then(function () {

            var uFormat = "YYYY-MM-DDTHH:mm:ss"
            var timePicker = $scope.model.config.timePicker != 1 ? false : true;
            var enableEditing = $scope.model.config.enableEditing == 1 ? true : false;
            var initialiseCurrent = $scope.model.config.initialiseCurrent == 1 ? true : false;

            var dateFormat;

            var hasDateFormat = $scope.model.config.dateFormat != null && $scope.model.config.dateFormat.length > 0;

            if (timePicker) {
                dateFormat = !hasDateFormat ? uFormat : $scope.model.config.dateFormat;
            }
            else {
                dateFormat = !hasDateFormat ? "YYYY-MM-DD" : $scope.model.config.dateFormat;
            }

            var showDropdowns = $scope.model.config.showDropdowns != 1 ? false : true;
            
            var timePickerIncrement = $scope.model.config.timePickerIncrement == null ? 15 : parseInt($scope.model.config.timePickerIncrement);
            var timePicker12Hour = $scope.model.config.timePicker12Hour == null ? false : true;

            $timeout(function () {

                var dateBox = $('#diplo-drp-' + $scope.model.alias);

                var current = $scope.model.value;

                if (current) {
                    var parts = current.split(",");
                    var startDate = moment(parts[0])
                    var endDate = moment(parts[1]);
                    $(dateBox).val(startDate.format(dateFormat) + " - " + endDate.format(dateFormat));
                }
                else if (initialiseCurrent) {
                    var startDate = moment();
                    var endDate = moment().add('days', 1);
                    $(dateBox).val(startDate.format(dateFormat) + " - " + endDate.format(dateFormat));
                    $("#diplo-drp-json-" + $scope.model.alias).val(startDate.format(uFormat) + "," + endDate.format(uFormat)).trigger('input');
                }

                if (enableEditing) {
                    $(dateBox).removeAttr("readonly");
                }

                $(dateBox).daterangepicker({ 
                    format: dateFormat,
                    showDropdowns: showDropdowns,
                    timePicker: timePicker,
                    timePickerIncrement: timePickerIncrement,
                    timePicker12Hour: timePicker12Hour
                },
                function (start, end) {
                    $("#diplo-drp-json-" + $scope.model.alias).val(start.format(uFormat) + "," + end.format(uFormat)).trigger('input');
                }
                );

                $('#diplo-drp-cal-' + $scope.model.alias).click(function () {
                    $('#diplo-drp-' + $scope.model.alias).focus();
                });

            }, 1000);
        });

    //load the seperate css for the editor to avoid it blocking our js loading
    assetsService.loadCss("/App_Plugins/DateRangePicker/lib/daterangepicker-bs2.css");
});