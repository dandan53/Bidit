app.controller('ListCtrl', function ($scope, $location, Item, userDataService) {

    $scope.user = userDataService.getUserData();

    $scope.openItemDetails = function (item) {
    };

    $scope.currentItem = {};
    $scope.setCurrentItem = function (item) {
        $scope.currentItem = item;
    };

    $scope.get_items = function (categoryId, subCategoryId, productId) {
        Item.query({
            categoryId: categoryId,
            subCategoryId: subCategoryId,
            productId: productId
        },
            function (data) {
                $scope.items = data;
                //$scope.more = data.length > 5;
                //$scope.items = $scope.items.concat(data);
            });
    };

    $scope.search = function () {
        var categoryId = $scope.selectedOption.id;
        var subCategoryId = $scope.selectedSubOption.id;
        var productId = $scope.selectedProduct.id;

        $scope.get_items(categoryId, subCategoryId, productId);
    };

    $scope.sort = function (col) {
        if ($scope.sort_order === col) {
            $scope.is_desc = !$scope.is_desc;
        } else {
            $scope.sort_order = col;
            $scope.is_desc = false;
        }

        $scope.reset();
    };

    $scope.show_more = function () {
        $scope.offset += $scope.limit;
        $scope.search(0, 1000);
    };

    $scope.has_more = function () {
        return $scope.more;
    };

    $scope.reset = function () {
        $scope.limit = 5;
        $scope.offset = 0;
        $scope.items = [];
        $scope.more = true;
        $scope.search(0, 1000);
    };

    $scope.delete = function () {
        var id = this.todo.Id;
        Todo.delete({ id: id }, function () {
            $('#todo_' + id).fadeOut();
        });
    };

    $scope.sort_order = "Priority";
    $scope.is_desc = false;

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


    $scope.reset();

    $scope.price_bid = function () {
        if (userDataService.isLoggedIn()) {
            $location.path('/pricebid/' + $scope.currentItem.Id);
        } else {
            alert('יש להיכנס למערכת');
        }
    };

    $scope.he = {};
    $scope.he.cat = "קטגוריה";
    $scope.he.pri = "תאור פריט";
    $scope.he.bes = "המחיר הטוב ביותר";
    $scope.he.siu = "מועד סיום המכרז";
    $scope.he.det = "לפרטים";

    $scope.en = {};
    $scope.en.cat = "category";
    $scope.en.pri = "description";
    $scope.en.bes = "best";
    $scope.en.siu = "final";
    $scope.en.det = "details";

});