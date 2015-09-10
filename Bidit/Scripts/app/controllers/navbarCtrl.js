﻿app.controller('NavbarCtrl', function ($scope, $location, userDataService) {

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

    $scope.user = userDataService.getUserData();
    
    $scope.isLoggedIn = function () {
        return userDataService.isLoggedIn();
    };

});
