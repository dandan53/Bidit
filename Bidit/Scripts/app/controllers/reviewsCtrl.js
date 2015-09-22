app.controller('ReviewsCtrl', function ($scope, $location, $routeParams, $timeout, ReviewService, userDataService) {
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
            //$scope.averageRate = data.AverageRate;
        }
    };

   $scope.closeForm = function () {
        //$location.path('/privatearea/' + $scope.isBidUser);
    };


    $scope.getReviews();
});