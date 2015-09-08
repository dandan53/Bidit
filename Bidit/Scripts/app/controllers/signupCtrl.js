app.controller('SignupCtrl', function ($scope, $location, $routeParams, SignupService) {

    $scope.register = function () {

        if ($scope.user.username != "" && $scope.user.password != "" && $scope.user.email != "") {
            var user = {
                username: $scope.user.username,
                password: $scope.user.password,
                Email: $scope.user.email
            };

            SignupService.register(user)
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
            alert('ההרשמה עברה בהצלחה. ברוכים הבאים');
            $location.url('/');
        } else {
            alert('ההרשמה נכשלה. נא נסו שנית');
        }
    };

    $scope.user = {};
    $scope.user.username = "";
    $scope.user.password = "";
    $scope.user.email = "";
});
