app.controller('LoginCtrl', function ($scope, $location, $routeParams, LoginService, Data, userDataService) {
    $scope.Data = Data;

    $scope.toggle_forget_password = function () {
        $scope.Data.isForgetPassword = !$scope.Data.isForgetPassword;
    };

    $scope.login = function () {

        if ($scope.cred.username != "" && $scope.cred.password != "") {
            var cred = {
                username: $scope.cred.username,
                password: $scope.cred.password
            };

            LoginService.login(cred)
                .then(
                    loadRemoteData,
                    function (errorMessage) {
                        console.warn(errorMessage);
                    }
                );
        }
        else {
            alert('נא הכנס משתמש וסיסמא');
        }
    };

    // I load the remote data from the server.

    function loadRemoteData(data) {
        if (data != null && data.User != null) {
            $scope.user.username = data.User.Username;
            $scope.user.CID = data.User.CID;

            userDataService.save();

            $location.path('/#/');
            
            //$scope.is_logged_in(true);
        } else {
            alert('שם משתמש או סיסמא אינם נכונים');
        }
    };

    $scope.cred = {};
    $scope.cred.username = "";
    $scope.cred.password = "";

    $scope.settings = function () {
        if (userDataService.isLoggedIn()) {
            $location.path('/settings/');
        } else {
            alert('יש להיכנס למערכת');
        }
    };

    $scope.user = userDataService.getUserData();

    $scope.isLoggedIn = function() {
        return userDataService.isLoggedIn();
    };

});
