app.controller('SignupCtrl', function ($scope, $location, $routeParams, $timeout,SignupService, userDataService) {

    $scope.register = function () {

        if ($scope.newUser.username != "" && $scope.newUser.password != "" && $scope.newUser.email != "") {
            var newUser = {
                username: $scope.newUser.username,
                password: $scope.newUser.password,
                email: $scope.newUser.email,
                phone: $scope.newUser.phone
            };

            SignupService.register(newUser)
                .then(
                    loadRemoteData,
                    function (errorMessage) {
                        console.warn(errorMessage);
                    }
                );
        }
        else {
            alert('נא מלא את כל הפרטים');
        }
    };

    // I load the remote data from the server.

    function loadRemoteData(data)
    {
        if (data != null && data.User != null)
        {
            $scope.username = data.User.Username;

            $scope.user.username = data.User.Username;
            $scope.user.CID = data.User.CID;

            userDataService.save();

            $scope.isAlertSuccess = true;
            $timeout(function () { $scope.alertTimeout(); }, 1500);
        }
        else
        {
            $scope.isAlertDanger = true;
            $timeout(function () { $scope.alertDangrTimeout(); }, 2500);
        }
    };

    $scope.alertTimeout = function () {
        $location.url('/');
    };
    
    $scope.alertDangrTimeout = function () {
        $scope.isAlertDanger = false;
    };

    $scope.isAlertSuccess = false;
    $scope.isAlertDanger = false;

    $scope.newUser = {};
    $scope.newUser.username = "";
    $scope.newUser.password = "";
    $scope.newUser.email = "";
    
    $scope.user = userDataService.getUserData();
    
});
