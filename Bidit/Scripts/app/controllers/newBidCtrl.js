app.controller('NewbidCtrl', function ($scope, $location, $routeParams, Login, bidService, userDataService) {
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

    $scope.dueDate = new Date();

    $scope.amount = 1;

    $scope.add_bid = function () {

        if (userDataService.isLoggedIn()) {
            var product_name = $scope.selectedProduct.name;
            if (product_name === 'הכל') {
                alert('נא בחר מוצר');
            }
            else {
                var newBid = {
                    CategoryId: $scope.selectedOption.id,
                    Category: $scope.selectedOption.name,
                    SubCategoryId: $scope.selectedSubOption.id,
                    SubCategory: $scope.selectedSubOption.name,
                    Product: $scope.selectedProduct.name,
                    ProductId: $scope.selectedProduct.id,
                    DueDate: $scope.dueDate,
                    Amount: $scope.amount,
                    BidCID: $scope.user.CID
                };

                bidService.addBid(newBid)
                                .then(
                                    loadRemoteData,
                                    function (errorMessage) {

                                        console.warn(errorMessage);

                                    }
                                );

            }
        } else {
            alert('יש להיכנס למערכת');
        }
        
    };

    // I load the remote data from the server.

    function loadRemoteData() {
        $location.url('/');
    };

});