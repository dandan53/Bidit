app.controller('SettingsCtrl', function ($scope, $location, $routeParams, Login, SettingsService, userDataService) {

    $scope.userSettings = {};
    $scope.userSettings.isEmailUpdates = true;
    $scope.userSettings.itemToUpdatesDic = {};
    $scope.itemToUpdatesDicLength = 0;

    $scope.addItemToUpdatesDic = function () {

        if (userDataService.isLoggedIn()) {
            var product_name = $scope.selectedProduct.name;
            if (product_name === 'הכל') {
                alert('נא בחר מוצר');
            }
            else {
                var productId = $scope.selectedProduct.id;
                $scope.userSettings.itemToUpdatesDic[$scope.itemToUpdatesDicLength] = productId;
                $scope.itemToUpdatesDicLength++;
            }
        } else {
            alert('יש להיכנס למערכת');
        }

    };
   
    
    $scope.closeSettingsForm = function () {
        $location.url('/');
    };

    $scope.saveSettings = function () 
    {
        if (userDataService.isLoggedIn()) {
            var settings = {
                isEmailUpdates: $scope.userSettings.isEmailUpdates,
                subscribedProductIdDic: $scope.userSettings.itemToUpdatesDic,
                    CID: $scope.user.CID
                };

            SettingsService.saveSettings(settings)
                                .then(
                                    loadRemoteData,
                                    function (errorMessage) {
                                        console.warn(errorMessage);
                                    }
                                );

        } else {
            alert('יש להיכנס למערכת');
        }
    };

    // I load the remote data from the server.

    function loadRemoteData() {
        $location.url('/');
    };

    $scope.user = userDataService.getUserData();

    $scope.options = getCategories();
    $scope.selectedOption = $scope.options[0];

    $scope.update_selected_option = function () {
        $scope.subOptions = getSubCategories($scope.selectedOption.id);
        $scope.selectedSubOption = $scope.subOptions[0];
    };

    $scope.update_selected_option();

    $scope.update_selected_subOption = function () {
        $scope.products = getProducts($scope.selectedSubOption.id);
        $scope.selectedProduct = $scope.products[0];
    };

    $scope.update_selected_subOption();  

});