app.controller('RateCtrl', function ($scope, $location, $routeParams, $timeout, Login, bidService, userDataService) {
    $scope.user = userDataService.getUserData();

    $scope.askCID = $routeParams.CID;
    
    $scope.addNewBid = function () {

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
    function loadRemoteData()
    {
        $scope.isAlertSuccess = true;

        $timeout(function () { $scope.alertTimeout(); }, 1500);
    };

    $scope.alertTimeout = function () {
     //   $scope.isAlertSuccess = false;
        $scope.closeNewBidForm();
    };

    $scope.isAlertSuccess = false;

    $scope.closeNewBidForm = function () {
        $location.url('/');
    };
    
    // Rates

    $scope.rate = function (idPassedFromNgClick) {
        $scope.rates = idPassedFromNgClick;
        // $scope.rates = this.id;
        // $scope.rates = idPassedFromNgClick.target.attributes.data.value;
    };

    $scope.rates = -1;

});