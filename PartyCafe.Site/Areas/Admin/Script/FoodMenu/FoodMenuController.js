
var foodmenuApp = new angular.module("FoodMenuApp",['ngRoute']);

foodmenuApp.config(function($routeProvider){
    $routeProvider
    .when('/',{
    templateUrl: 'FoodMenu/MenuList',
    controller: 'FoodMenuListController'
    })
    .when('/MenuEdit/:menuId',{
        templateUrl: 'FoodMenu/MenuEdit',
        controller: 'FoodMenuEditController'
    })
    .when('/MenuNew',{
        templateUrl: 'FoodMenu/MenuNew',
        controller : 'FoodMenuNewController'
    })
    .when('/MenuItems/:menuId/',{
        templateUrl: 'FoodMenu/MenuItems',
        controller: 'FoodMenuItemsController'
    })
    .when('/MenuItemEdit/:menuId/:itemId',{
        templateUrl: 'FoodMenu/MenuItemEdit',
        controller: 'FoodMenuItemEditController'
    })
    .when('/MenuItemNew/:menuId',{
        templateUrl: 'FoodMenu/MenuItemNew',
        controller: 'FoodMenuItemNewController'
    })
});

foodmenuApp.filter("CorrectPrice",function(){
    return  function(price){
        return correctPrice(price);
    }
});

foodmenuApp.filter("Column",function(){
    return function(str){
        return str.split('\n').join('<br/>');
    }
})

foodmenuApp.controller("FoodMenuController", function($scope){

});

function correctPrice(price){
        var result = parseInt(price);
        if(result == NaN) return;
        return result.toFixed(2);
};