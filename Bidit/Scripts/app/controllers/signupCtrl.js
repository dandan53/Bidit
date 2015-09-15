app.controller('SignupCtrl', function ($scope, $location, $routeParams, SignupService, userDataService) {

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

    function loadRemoteData(data) {
        if (data != null && data.User != null) {
            $scope.username = data.User.Username;

            $scope.user.username = data.User.Username;
            $scope.user.CID = data.User.CID;

            userDataService.save();

            alert('ההרשמה עברה בהצלחה. ברוכים הבאים');
            
            $location.url('/');
        } else {
            alert('ההרשמה נכשלה. נא נסו שנית');
        }
    };

    $scope.newUser = {};
    $scope.newUser.username = "";
    $scope.newUser.password = "";
    $scope.newUser.email = "";
    
    $scope.user = userDataService.getUserData();
    
});
