app.controller('SettingsCtrl', function ($scope, $location, $routeParams, $timeout, Login, SettingsService, userDataService) {

    $scope.user = userDataService.getUserData();

    $scope.userSettings = {};
    $scope.userSettings.isEmailUpdates = $scope.user.isEmailUpdates;
    $scope.userSettings.itemToUpdatesDic = {};
    $scope.addItemToUpdatesDic = function () {

        if (userDataService.isLoggedIn()) {
            var product_name = $scope.selectedProduct.name;
            if (product_name === 'הכל') {
                alert('נא בחר מוצר');
            }
            else {
                var productId = $scope.selectedProduct.id;
                $scope.userSettings.itemToUpdatesDic[productId] = productId;
                
                $scope.alertSuccessText = "המוצר התווסף לרשימת העדכונים";
                $scope.isAlertSuccess = true;
                $timeout(function () { $scope.alertTimeout(false); }, 1500);
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

    function loadRemoteData()
    {
        $scope.user.isEmailUpdates = $scope.userSettings.isEmailUpdates;

        $scope.alertSuccessText = "ההגדרות התעדכנו בהצלחה.";
        $scope.isAlertSuccess = true;
        $timeout(function () { $scope.alertTimeout(true); }, 1500);
    };

    $scope.alertTimeout = function (isCloseForm) {
        if (isCloseForm) {
            $location.url('/');
        } else {
            $scope.isAlertSuccess = false;
        }
    };

    $scope.isAlertSuccess = false;
    $scope.alertSuccessText = "";
    
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