app.controller('ListCtrl', function ($scope, $location, $routeParams, Item, userDataService) {

    $scope.user = userDataService.getUserData();
    
    $scope.isBidUser = $routeParams.isBidUser;

    $scope.isPublicMode = $scope.isBidUser === undefined;

    $scope.currentItem = {};
    $scope.setCurrentItem = function (item) {
        $scope.currentItem = item;
    };

    $scope.get_items = function (categoryId, subCategoryId, productId, CID, isBidUser) {
        Item.query({
            categoryId: categoryId,
            subCategoryId: subCategoryId,
            productId: productId,
            CID: CID,
            isBidUser: isBidUser
        },
            function (data) {
                $scope.items = data;

                $scope.setPaging();

                $scope.showItemsInPage($scope.currentPage);
                
                //$scope.more = data.length > 5;
                //$scope.items = $scope.items.concat(data);
            });
    };

    $scope.search = function () {
        var categoryId = $scope.selectedOption.id;
        var subCategoryId = $scope.selectedSubOption.id;
        var productId = $scope.selectedProduct.id;

        var CID = 0;
        if (!$scope.isPublicMode) {
            CID = $scope.user.CID;
        }

        $scope.get_items(categoryId, subCategoryId, productId, CID, $scope.isBidUser);
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

    $scope.priceBid = function (item) {
            $location.path('/pricebid/' + item.Id);            
    };
    
    $scope.editBid = function (item) {
        $location.path('/editbid/' + item.Id + "/" + $scope.isBidUser);
    };
    
    // Paging

    $scope.pageSize = 10;
    $scope.pages = [1];
    $scope.numOfItems = 0;
    $scope.numOfPages = 1;

    $scope.setPaging = function () {
        if ($scope.items != null && $scope.items.length) {
            $scope.numOfItems = $scope.items.length;
            var divide = Math.floor($scope.numOfItems / $scope.pageSize);
            var modulo = $scope.numOfItems % $scope.pageSize;
            $scope.numOfPages = (modulo == 0 ? divide : divide + 1);
            $scope.pages = [];
            for (var i = 0; i < $scope.numOfPages; i++) {
                $scope.pages.push(i+1);
            }
        }
    };

    $scope.currentDisplayedItems = {};
    $scope.currentPage = 1;

    $scope.firstDisplayedItemIndex = 0;
    $scope.lastDisplayedItemIndex = 0;

    $scope.showItemsInPage = function (pageIndex) {

        var prevElementName = '#page_' + $scope.currentPage;
        var prevElement = angular.element(prevElementName);
        prevElement[0].className = "";

        var currentElementName = '#page_' + pageIndex;
        var currentElement = angular.element(currentElementName);
        currentElement[0].className = "active";

        $scope.currentPage = pageIndex;
        var begin = (($scope.currentPage - 1) * $scope.pageSize)
        , end = begin + $scope.pageSize;
        $scope.currentDisplayedItems = $scope.items.slice(begin, end);
        
        $scope.setPagingButtons();

        $scope.firstDisplayedItemIndex = begin + 1;
        $scope.lastDisplayedItemIndex = (end > $scope.numOfItems ? $scope.numOfItems : end);

        $scope.dataTableInfoText = ($scope.numOfItems > 0 ? "מציג מכרזים " + $scope.firstDisplayedItemIndex + "-" + $scope.lastDisplayedItemIndex + " מתוך " + $scope.numOfItems + " מכרזים " :
            "אין מכרזים להציג");
    };

    $scope.showItemsInPreviousPage = function () {
        if ($scope.currentPage > 1) {
            $scope.showItemsInPage($scope.currentPage - 1);
        }
    };

    $scope.showItemsInNextPage = function () {
        if ($scope.currentPage < $scope.numOfPages) {
            $scope.showItemsInPage($scope.currentPage + 1);
        }
    };

    $scope.setPagingButtons = function () {
        $scope.previousPageClass = ($scope.currentPage != 1 ? "paginate_button previous" : "paginate_button previous disabled");
        $scope.nextPageClass = ($scope.currentPage != $scope.numOfPages && $scope.numOfPages != 1 ? "paginate_button next" : "paginate_button next disabled");
    };

    $scope.setPagingButtons();

});