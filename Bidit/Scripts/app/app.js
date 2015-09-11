var app = angular.module("TodoApp", ["ngResource", "ngRoute", "ngStorage"]);
    
app.config(function ($routeProvider) {
    $routeProvider.
         when('/', { controller: 'ListCtrl', templateUrl: 'main.html' }).
         when('/login', { controller: 'LoginCtrl', templateUrl: 'login.html' }).
         when('/newbid', { controller: 'NewbidCtrl', templateUrl: 'newbid.html' }).
         when('/pricebid/:id', { controller: 'PricebidCtrl', templateUrl: 'pricebid.html' }).
         when('/signup', { controller: 'SignupCtrl', templateUrl: 'signup.html' }).
         when('/settings', { controller: 'SettingsCtrl', templateUrl: 'settings.html' }).
         when('/privatearea', { controller: 'PrivateListCtrl', templateUrl: 'private_main.html' }).
         otherwise({ redirectTo: '/' });
});

app.factory('Item', function ($resource) {
    return $resource('/api/item/:id', { id: '@id' }, { update: { method: 'PUT' } });
});

app.factory('Login', function ($resource) {
    return $resource('/api/login', { id: '@id' }, { update: { method: 'PUT' } });
});

app.factory('Data', function () {
   return { isForgetPassword: false };
});

///////// Resources /////////

var categories = [
    { 'id': 0, 'name': 'בחר קטגוריה' },
    { 'id': 1, 'name': 'כלי עבודה' },
    { 'id': 2, 'name': 'מוצרי חשמל' },
    { 'id': 3, 'name': 'ריהוט' }
];

var getCategories = function () {
    return categories;
};


var subCategories = {};
subCategories[0] = [
    { 'id': 1000, 'name': 'כל תתי הקטגוריה' }
];

subCategories[1] = [
    { 'id': 1000, 'name': 'כל תתי הקטגוריה' },
    { 'id': 100, 'name': 'חומרי עבודה' },
    { 'id': 101, 'name': 'כלי עבודה' },
    { 'id': 102, 'name': 'כלים לנגרות' }
];

subCategories[2] = [
    { 'id': 1000, 'name': 'כל תתי הקטגוריה' },
    { 'id': 200, 'name': 'מאוורר' },
    { 'id': 201, 'name': 'מזגן' },
    { 'id': 202, 'name': 'טלויזיה' }
];

subCategories[3] = [
    { 'id': 1000, 'name': 'כל תתי הקטגוריה' },
    { 'id': 300, 'name': 'ארונות' },
    { 'id': 301, 'name': 'כסאות' },
    { 'id': 302, 'name': 'שולחנות' }
];

var getSubCategories = function (parentId) {
    return subCategories[parentId];
};


var products = {};
products[1000] = [
    { 'id': 0, 'name': 'הכל' }
];

products[100] = [
    { 'id': 0, 'name': 'הכל' },
    { 'id': 10001, 'name': 'קרמיקה' },
    { 'id': 10002, 'name': 'צבע' },
    { 'id': 10003, 'name': 'פלקט' }
];

products[101] = [
    { 'id': 0, 'name': 'הכל' },
    { 'id': 10101, 'name': 'מגרפה' },
    { 'id': 10102, 'name': 'מעדר' },
    { 'id': 10103, 'name': 'מטאטא' }
];

products[102] = [
    { 'id': 0, 'name': 'הכל' },
    { 'id': 10201, 'name': 'מסור' },
    { 'id': 10202, 'name': 'פטיש' },
    { 'id': 10203, 'name': 'מסמר' }
];


products[200] = [
    { 'id': 0, 'name': 'הכל' },
    { 'id': 20001, 'name': 'מאוורר תקרה' }
];

products[201] = [
    { 'id': 0, 'name': 'הכל' },
    { 'id': 20101, 'name': 'מזגן עילי' }
];

products[202] = [
    { 'id': 0, 'name': 'הכל' },
//    { 'id': 20200, 'name': 'הכל' },
    { 'id': 20201, 'name': 'LCD' },
    { 'id': 20202, 'name': 'LED' },
];


products[300] = [
//    { 'id': 30000, 'name': 'הכל' },
    { 'id': 0, 'name': 'הכל' },
    { 'id': 30001, 'name': 'ארון קיר' },
    { 'id': 30002, 'name': 'ארונית' },
    { 'id': 30003, 'name': 'ארון מטבח' }
];

products[301] = [
    { 'id': 0, 'name': 'הכל' },
//    { 'id': 30100, 'name': 'הכל' },
    { 'id': 30101, 'name': 'כסא פלסטיק' },
    { 'id': 30102, 'name': 'כסא בר' },
    { 'id': 30103, 'name': 'שרשפרף' }
];

products[302] = [
    { 'id': 0, 'name': 'הכל' },
//    { 'id': 30200, 'name': 'הכל' },
    { 'id': 30201, 'name': 'שולחן מטבח' },
    { 'id': 30202, 'name': 'שולחן מחשב' },
    { 'id': 30203, 'name': 'שולחן ישיבות' }
];

var getProducts = function (parentId) {
    return products[parentId];
};