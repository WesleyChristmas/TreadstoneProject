
var foodmenuApp = new angular.module("FoodMenuApp",['ngRoute']);

foodmenuApp.config(function($routeProvider){
    $routeProvider
    .when('/',{
    templateUrl: 'FoodMenu/MenuList',
    controller: 'FoodMenuListController'
    })
    .when('/SubMenu/:menuId',{
        templateUrl: 'FoodMenu/MenuSubList',
        controller: 'FoodMenuSubListController'
    })
    .when('/MenuItems/:subMenuId',{
        templateUrl: 'FoodMenu/MenuItems',
        controller: 'FoodMenuItemsController'
    })
    .when('/MenuItemEdit/:subMenuId/:itemId',{
        templateUrl: 'FoodMenu/MenuItemEdit',
        controller: 'FoodMenuItemEditController'
    })
    .when('/MenuItemNew/:subMenuId',{
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