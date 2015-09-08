app.controller('NavbarCtrl', function ($scope, $location, userDataService) {

    $scope.login = function () {
        if (!userDataService.isLoggedIn()) {
            $location.path('/login/');
        } else {
            alert('אתה כבר מחובר למערכת');
        }
    };

    $scope.user = userDataService.getUserData();
});
