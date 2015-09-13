app.controller('PricebidCtrl', function ($scope, $location, $routeParams, Login, bidService, userDataService) {
    $scope.user = userDataService.getUserData();

    $scope.bid_id = $routeParams.id;

    $scope.get_bid = function () {

        bidService.getBid($scope.bid_id)
                        .then(
                            loadData,
                            function (errorMessage) {
                                console.warn(errorMessage);
                            }
                        );
    };

    function loadData(data) {
        $scope.item = data;
    };


    $scope.get_bid();


    $scope.update_bid = function () {

        if (userDataService.isLoggedIn()) {
            var price = $scope.price;
            if (price > 0) {
                var updatedBid = {
                    Id: $scope.bid_id,
                    NewPrice: price,
                    NewAskCID: $scope.user.CID
                };

                bidService.updateBid(updatedBid)
                    .then(
                        loadRemoteData,
                        function(errorMessage) {

                            console.warn(errorMessage);

                        }
                    );

            } else {
                alert('נא הגש הצעה');
            }
        } else {
            alert('יש להיכנס למערכת');
        }
    };


    // I load the remote data from the server.

    function loadRemoteData() {
        $location.url('/');
    };


    $scope.closePriceBidForm = function () {
        $location.url('/');
    };

});

