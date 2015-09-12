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

        userDataService.save();
    };

    $scope.addNewBid = function () {
        if (userDataService.isLoggedIn()) {
            $location.path('/newbid/');
        } else {
            alert('יש להיכנס למערכת');
        }
    };

    $scope.settings = function () {
        if (userDataService.isLoggedIn()) {
            $location.path('/settings/');
        } else {
            alert('יש להיכנס למערכת');
        }
    };
    
    $scope.privateArea = function (isBidUser) {
        if (userDataService.isLoggedIn()) {
            $location.path('/privatearea/' + isBidUser);
            //$location.path('/');
        } else {
            alert('יש להיכנס למערכת');
        }
    };
    
    $scope.signup = function () {
         $location.path('/signup/');
    };

    $scope.user = userDataService.getUserData();
    
    $scope.isLoggedIn = function () {
        return userDataService.isLoggedIn();
    };

});
