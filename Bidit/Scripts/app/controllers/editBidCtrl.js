app.controller('EditbidCtrl', function ($scope, $location, $routeParams, Login, bidService, userDataService) {
    $scope.user = userDataService.getUserData();

    $scope.bidId = $routeParams.id;
    $scope.isBidUser = $routeParams.isBidUser;

    $scope.getBid = function () {

        bidService.getBid($scope.bidId)
                        .then(
                            loadData,
                            function (errorMessage) {
                                console.warn(errorMessage);
                            }
                        );
    };

    function loadData(data) {
        $scope.item = data;

        $scope.dueDateObj = new Date($scope.item.DueDate);
        $scope.amount = $scope.item.Amount;
    };

    $scope.getBid();
    
    $scope.editBid = function () {

        if (userDataService.isLoggedIn()) {
            if ($scope.item.Amount != $scope.amount || new Date($scope.item.DueDate).getTime() != $scope.dueDateObj.getTime()) {
                var updatedBid = {
                    Id: $scope.bidId,
                    DueDate: $scope.dueDateObj,
                    Amount: $scope.amount,
                    BidCID: $scope.user.CID
                };

                bidService.updateBid(updatedBid)
                    .then(
                        loadRemoteData,
                        function(errorMessage) {

                            console.warn(errorMessage);

                        }
                    );

            } else {
                alert('לא נעשה שינוי במכרז');
            }
        } else {
            alert('יש להיכנס למערכת');
        }
    };

    $scope.removeBid = function () {

        if (userDataService.isLoggedIn())
        {
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
                        function(errorMessage) {

                            console.warn(errorMessage);

                        }
                    );
            }
        }
        else
        {
            alert('יש להיכנס למערכת');
        }
    };

    // I load the remote data from the server.

    function loadRemoteData() {
        $location.path('/privatearea/' + $scope.isBidUser);
    };


    $scope.closeEditBidForm = function () {
        $location.path('/privatearea/' + $scope.isBidUser);
    };

    $scope.dueDateObj = new Date();
    $scope.amount = 0;

});

