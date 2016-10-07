
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
    .when('/SubMenu/:menuId',{
        templateUrl: 'FoodMenu/MenuSubList',
        controller: 'FoodMenuSubListController'
    })
    .when('/SubMenuEdit/:menuId/:subMenuId',{
        templateUrl: 'FoodMenu/MenuEdit',
        controller: 'FoodMenuSubEditController'
    })
    .when('/SubMenuNew/:menuId',{
        templateUrl: 'FoodMenu/MenuNew',
        controller : 'FoodMenuSubNewController'
    })
    .when('/MenuItems/:menuId/:subMenuId',{
        templateUrl: 'FoodMenu/MenuItems',
        controller: 'FoodMenuItemsController'
    })
    .when('/MenuItemEdit/:menuId/:subMenuId/:itemId',{
        templateUrl: 'FoodMenu/MenuItemEdit',
        controller: 'FoodMenuItemEditController'
    })
    .when('/MenuItemNew/:menuId/:subMenuId',{
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