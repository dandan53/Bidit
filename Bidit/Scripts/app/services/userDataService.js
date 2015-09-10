app.service('userDataService', ['$localStorage', function (localStorage) {
    var userData = {
        username: "",
        CID: 0,
        isEmailUpdates: true
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

    var save = function() {
        localStorage.userData = userData;
    };

    var load = function() {
        userData = localStorage.userData;
        
        if (userData === undefined) {
            userData = {
                username: "",
                CID: 0,
                isEmailUpdates: true
            };
        }
    };

    load();

    return {
        isLoggedIn: isLoggedIn,
        getUserData: getUserData,
        save: save,
        load: load
    };

}]);