app.service('stateService', function () {
    var stateData = {
        state: ""
    };

    var getStateData = function () {
        return stateData;
    };

    var setStateData = function (state) {
        stateData.state = state;
    };

    var isCurrentState = function (state) {
        if (state === stateData.state) {
            return true;
        } else {
            return false;
        }
    };

    return {
        getStateData: getStateData,
        setStateData: setStateData,
        isCurrentState: isCurrentState
    };

});