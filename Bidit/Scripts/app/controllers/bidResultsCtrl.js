app.controller('BidResultsCtrl', function ($scope, $location, $routeParams, $timeout, bidService, userDataService, ReviewService) {
    $scope.user = userDataService.getUserData();

    $scope.bidId = $routeParams.id;
    $scope.isBidUser = $routeParams.isBidUser;

    $scope.firstAsk = null;

    $scope.isAskArea = false;

    $scope.getBidResults = function () {

        bidService.getBidResults($scope.bidId)
                        .then(
                            loadData,
                            function (errorMessage) {
                                console.warn(errorMessage);
                            }
                        );
    };

    function loadData(data) {
        if (data != null) {
            $scope.item = data.Bid;
            $scope.firstAsk = data.FirstAsk;
            if ($scope.firstAsk != null) {
                $scope.isAskArea = true;
                
                $scope.getReviews();
            }
        }
    };

    $scope.getBidResults();
    
    $scope.removeBid = function () {

        if (userDataService.isLoggedIn()) {
            var result;
            if (confirm("האם אתה בטוח שאתה רוצה להסיר את המכרז") == true) {
                result = true;
            } else {
                result = false;
            }

            if (result == true) {
                var updatedBid = {
                    Id: $scope.bidId,
                    NewAskCID: -1,
                    BidCID: $scope.user.CID
                };

                bidService.updateBid(updatedBid)
                    .then(
                        loadRemoteData,
                        function (errorMessage) {

                            console.warn(errorMessage);

                        }
                    );
            }
        }
        else {
            alert('יש להיכנס למערכת');
        }
    };

    function loadRemoteData() {
        $scope.isAlertSuccess = true;
        $timeout(function () { $scope.alertTimeout(); }, 1500);
    };

    $scope.alertTimeout = function () {
        $scope.closeBidResultsForm();
    };

    $scope.isAlertSuccess = false;
    
    $scope.closeBidResultsForm = function () {
        $location.path('/privatearea/' + $scope.isBidUser);
    };

    //Rate
    $scope.rate = function () {
        if ($scope.firstAsk != null) {
            $location.path('/review/' + $scope.firstAsk.CID);
        }
    };
    
    $scope.reviews = function () {
        if ($scope.firstAsk != null) {
            $location.path('/reviews/' + $scope.firstAsk.CID);
        }
    };
    

    //// Reivews ////
    
   // $scope.reviews = {};
    $scope.averageRate = 0;
    $scope.numOfReviews = 0;

    $scope.getReviews = function () {
        if (userDataService.isLoggedIn()) {
            var data = {
                CID: $scope.firstAsk.CID,
            };

            ReviewService.getReviews(data)
                                .then(
                                    reviewsHandler,
                                    function (errorMessage) {

                                        console.warn(errorMessage);

                                    }
                                );
        }
        else {
            alert('יש להיכנס למערכת');
        }
    };

    // I load the remote data from the server.
    function reviewsHandler(data) {
        if (data != null) {
            var reviews = data;

            if (reviews.length > 0) {
                var len = reviews.length;
                var sum = 0;
                for (var i = 0; i < len; i++) {
                    sum += reviews[i].Rate;
                }

                $scope.averageRate = sum / len;

                $scope.numOfReviews = len;

                $scope.starsWidth = $scope.getWidth();
            }
        }
    };

    $scope.getWidth = function () {
        var width = ($scope.averageRate / 5) * 93;
        var style = "width: " + width + "px";
        return style;
    };
    
});

