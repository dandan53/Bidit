app.controller('ReviewCtrl', function ($scope, $location, $routeParams, $timeout, Login, ReviewService, userDataService) {
    $scope.user = userDataService.getUserData();

    $scope.askCID = $routeParams.CID;
    
    $scope.setRate = function (rate) {
        $scope.rate = rate;
    };

    $scope.rate = 0;

    $scope.feedback = "";

    $scope.rateUser = function () {

        if (userDataService.isLoggedIn()) {
            if ($scope.rate === 0) {
                alert('נא דרג את המוכר');
            }
            else {
                var data = {
                    CID: $scope.user.CID,
                    AskCID: $scope.askCID,
                    Rate: $scope.rate,
                    Feedback: $scope.feedback
                };

                ReviewService.rate(data)
                                .then(
                                    loadRemoteData,
                                    function (errorMessage) {

                                        console.warn(errorMessage);

                                    }
                                );
            }
        } else {
            alert('יש להיכנס למערכת');
        }
        
    };

    // I load the remote data from the server.
    function loadRemoteData(data)
    {
        if (data === true) {
            $scope.isAlertSuccess = true;
            $timeout(function () { $scope.alertTimeout(); }, 1500);
        }
    };

    $scope.alertTimeout = function () {
     //   $scope.isAlertSuccess = false;
        $scope.closeForm();
    };

    $scope.isAlertSuccess = false;

    $scope.closeForm = function () {
        $location.url('/');
    };
    
});