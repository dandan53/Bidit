app.controller('NavbarCtrl', function ($scope, $location, userDataService) {

    $scope.login = function () {
        if (!userDataService.isLoggedIn()) {
            $location.path('/login/');
        } else {
            alert('אתה כבר מחובר למערכת');
        }
    };

    $scope.logout = function () {
        $scope.user.username = "";
        $scope.user.CID = 0;
    };

    $scope.user = userDataService.getUserData();
    
    $scope.isLoggedIn = function () {
        return userDataService.isLoggedIn();
    };

});
