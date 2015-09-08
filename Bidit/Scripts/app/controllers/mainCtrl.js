app.controller('MainCtrl', function ($scope, $location, userDataService) {

    $scope.newBid = function (item) {
        if (userDataService.isLoggedIn()) {
            $location.path('/newbid/');
        } else {
            alert('יש להיכנס למערכת');
        }
    };
    
    $scope.user = userDataService.getUserData();
});
