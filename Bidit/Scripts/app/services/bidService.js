app.service("bidService", function ($http, $q) {

    // Return public API.
    return ({
        addBid: addBid,
        getBid: getBid,
        updateBid: updateBid

        //removeFriend: removeFriend
    });


    // ---
    // PUBLIC METHODS.
    // ---


    // I add a friend with the given name to the remote collection.
    function addBid(item) {

        var request = $http({
            method: "post",
            url: "/api/item/",
            //params: {
            //    action: "add"
            //},
            data:
            {
                CategoryId: item.CategoryId,
                Category: item.Category,
                SubCategoryId: item.SubCategoryId,
                SubCategory: item.SubCategory,
                ProductId: item.ProductId,
                Product: item.Product,
                DueDate: item.DueDate,
                Amount: item.Amount,
                BidCID: item.BidCID
            }
        });

        return (request.then(handleSuccess, handleError));

    }

    function updateBid(item) {

        var request = $http({
            method: "post",
            url: "/api/item/",
            //params: {
            //    action: "add"
            //},
            data:
            {
                Id: item.Id,
                NewPrice: item.NewPrice,
                NewAskCID: item.NewAskCID,
                DueDate: item.DueDate,
                Amount: item.Amount,
                BidCID: item.BidCID
            }
        });

        return (request.then(handleSuccess, handleError));

    }               // I get all of the friends in the remote collection.
    function getBid(id) {

        var request = $http({
            method: "get",
            url: "/api/item/",
            params: {
                id: id
            }
        });

        return (request.then(handleSuccess, handleError));

    }


    //// I get all of the friends in the remote collection.
    //function getFriends() {

    //    var request = $http({
    //        method: "get",
    //        url: "api/index.cfm",
    //        params: {
    //            action: "get"
    //        }
    //    });

    //    return (request.then(handleSuccess, handleError));

    //}


    //// I remove the friend with the given ID from the remote collection.
    //function removeFriend(id) {

    //    var request = $http({
    //        method: "delete",
    //        url: "api/index.cfm",
    //        params: {
    //            action: "delete"
    //        },
    //        data: {
    //            id: id
    //        }
    //    });

    //    return (request.then(handleSuccess, handleError));

    //}


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
