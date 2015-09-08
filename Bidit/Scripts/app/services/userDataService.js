app.service('userDataService', function () {
    var userData = {
        username: "",
        CID: 0
    };

    var getUserData = function () {
        return userData;
    };

    var isLoggedIn = function () {
        if (userData != null && userData.CID != 0) {
            return true;
        } else {
            return false;
        }
    };

    return {
        isLoggedIn: isLoggedIn,
        getUserData: getUserData
    };

});