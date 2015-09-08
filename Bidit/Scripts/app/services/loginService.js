app.service("LoginService", function ($http, $q) {

    // Return public API.
    return ({
        login: login,
        logout: logout
    });


    // ---
    // PUBLIC METHODS.
    // ---

    function login(cred) {

        var request = $http({
            method: "post",
            url: "/api/login/",
            data:
            {
                Username: cred.username,
                Password: cred.password
            }
        });

        return (request.then(handleSuccess, handleError));
    }

    function logout(cred) {

        var request = $http({
            method: "post",
            url: "/api/login/",
            data:
            {
                Username: cred.username,
                IsLogout: true
            }
        });

        return (request.then(handleSuccess, handleError));
    }

    // ---
    // PRIVATE METHODS.
    // ---


    // I transform the error response, unwrapping the application dta from
    // the API response payload.
    function handleError(response) {

        // The API response from the server should be returned in a
        // nomralized format. However, if the request was not handled by the
        // server (or what not handles properly - ex. server error), then we
        // may have to normalize it on our end, as best we can.
        if (
            !angular.isObject(response.data) ||
            !response.data.message
            ) {

            return ($q.reject("An unknown error occurred."));

        }

        alert("handleError");

        // Otherwise, use expected error message.
        return ($q.reject(response.data.message));


    }


    // I transform the successful response, unwrapping the application data
    // from the API response payload.
    function handleSuccess(response) {

        // alert("handleSuccess");

        return (response.data);

    }

}
);

