app.service("ReviewService", function ($http, $q) {

    // Return public API.
    return ({
        rate: rate,
        getReviews: getReviews
    });


    // ---
    // PUBLIC METHODS.
    // ---

    function rate(data) {

        var request = $http({
            method: "post",
            url: "/api/review/",
            data:
            {
                FromCID: data.CID,
                AboutCID: data.AskCID,
                Rate: data.Rate,
                Feedback: data.Feedback
            }
        });

        return (request.then(handleSuccess, handleError));
    }

    function getReviews(data) {

        var request = $http({
            method: "get",
            url: "/api/review/",
            params: {
                CID: data.CID
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

