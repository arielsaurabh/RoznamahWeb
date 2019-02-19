angular.module("umbraco").controller("Test", function ($scope, $http, editorState) {
    if (editorState.current.id) {
        //get opening time for event if event is in edit mode
        $http.get("/umbraco/api/CustomProperties/Test?pageId=" + editorState.current.id).then(function (response) {
            debugger;
            $scope.modelData = JSON.parse(JSON.parse(response.data));
        });
    }
});
