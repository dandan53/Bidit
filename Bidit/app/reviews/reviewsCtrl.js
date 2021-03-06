﻿app.controller('ReviewsCtrl', function ($scope, $location, $routeParams, $timeout, ReviewService, userDataService) {
    $scope.user = userDataService.getUserData();

    $scope.CID = $routeParams.CID;

    $scope.reviews = {};
    $scope.averageRate = 0;

    $scope.getReviews = function () 
    {
        if (userDataService.isLoggedIn()) 
        {
            var data = {
                    CID: $scope.CID,
                   };

            ReviewService.getReviews(data)
                                .then(
                                    loadRemoteData,
                                    function (errorMessage) {

                                        console.warn(errorMessage);

                                    }
                                );
        }
        else
        {
            alert('יש להיכנס למערכת');
        }
    };

    // I load the remote data from the server.
    function loadRemoteData(data)
    {
        if (data != null) {
            $scope.reviews = data;

            if ($scope.reviews != null && $scope.reviews.length > 0) {
                var len = $scope.reviews.length;
                var sum = 0;
                for (var i = 0; i < len; i++) {
                    sum += $scope.reviews[i].Rate;
                }

                $scope.averageRate = sum / len;
            }
        }
    };

    $scope.getWidth = function (rate) {
        var width = (rate / 5) * 93;
        var style = "width: " + width + "px";
        return style;
    };

   $scope.closeForm = function () {
        //$location.path('/privatearea/' + $scope.isBidUser);
    };

    $scope.getReviews();
});