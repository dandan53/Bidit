app.controller('BidResultsCtrl', function ($scope, $location, $routeParams, $timeout, bidService, userDataService) {
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
            $location.path('/rate/' + $scope.firstAsk.CID);
        }
    };
});

