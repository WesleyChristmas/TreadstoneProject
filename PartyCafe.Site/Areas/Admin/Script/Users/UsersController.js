
var usrApp = new angular.module("UsersApp", ['ngRoute']);

usrApp.config(function($routeProvider){
    $routeProvider
    .when('/',{
        templateUrl:'Users/UsersList',
        controller:'UsersListController'
    })
    .when('/new',{
        templateUrl:'Users/UsersNew',
        controller:'UsersNewController'
    })
    .when('/edit/:usrName',{
        templateUrl: 'Users/UsersEdit',
        controller:'UsersEditController'
    });
});

usrApp.controller("UsersController",function($scope){

});

function User(entity){
    if(entity == null){
        this.Name = '';
        return;
    }
    this.Name = entity.user[0].UserName;
}