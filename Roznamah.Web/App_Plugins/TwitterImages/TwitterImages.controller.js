angular.module("umbraco").controller("TwitterImages", function ($scope, $http, editorState) {

    //get twitter images if page is in edit mode
    if (editorState.current.id) {
        $http.get("/umbraco/api/CustomProperties/GetTwitterImagesData?pageId=" + editorState.current.id).then(function (response) {
            debugger;
            var responseData = JSON.parse(response.data);
            if (responseData) {
                var parsedData = JSON.parse(responseData);
                $scope.modelData.tags = parsedData.tags;
                $scope.modelData.tagsImages = parsedData.tagsImages;
            }
        });
    }

    $scope.noImageForTags = false;

    $scope.modelData = { tags: "", tagsImages: [] };

    $scope.getModelValue = function () {

        $scope.model.value = JSON.stringify($scope.modelData);
        $scope.noImageForTags = false;
        $("#twitterInputBox").removeClass("tagInputError");
    }

    $scope.fetchImageFromTwitter = function () {

        if (!$scope.modelData.tags.trim()) {
            $("#twitterInputBox").addClass("tagInputError");
            return false;
        }

        $http.get("/umbraco/api/CustomProperties/GetTwitterImages?pageId=" + editorState.current.id + "&hashTags=" + $scope.modelData.tags).then(function (response) {

            if (response.data) {
                $scope.modelData.tagsImages = response.data;
                $scope.getModelValue();
                if ($scope.modelData.tagsImages.length) {
                    $scope.noImageForTags = false;
                }
                else {
                    $scope.noImageForTags = true;
                }

            }
            else {
                $scope.noImageForTags = true;
            }

        });
        return false;
    }

    $scope.removeImage = function (index) {
        var r = confirm("Are you sure !");
        if (r == true) {
            $scope.modelData.tagsImages.splice(index, 1);
            $scope.getModelValue();
        }

    }


});