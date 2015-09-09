app.controller('MainCtrl', function ($scope, $location, userDataService, stateService) {

    $scope.newBid = function (item) {
        if (userDataService.isLoggedIn()) {
            $location.path('/newbid/');
        } else {
            alert('יש להיכנס למערכת');
        }
    };
    
    $scope.user = userDataService.getUserData();

    $scope.stateService = stateService;

    stateService.setStateData("list");
});
