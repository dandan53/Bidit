app.controller('BidResultsCtrl', function ($scope, $location, $routeParams, bidService, userDataService) {
    $scope.user = userDataService.getUserData();

    $scope.bidId = $routeParams.id;
    $scope.isBidUser = $routeParams.isBidUser;

    $scope.firstAsk = null;

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

    // I load the remote data from the server.

    function loadRemoteData() {
        $scope.closeBidResultsForm();
    };
    
    $scope.closeBidResultsForm = function () {
        $location.path('/privatearea/' + $scope.isBidUser);
    };

});

