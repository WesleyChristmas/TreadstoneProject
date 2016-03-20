var food = new angular.module('food', ['ngRoute']);

food.controller('Food', function ($scope, $http) {
    $scope.FoodMenu = [];
    //GetFoodData($scope, $http);
});

function GetFoodData($scope, $http) {
    $http.get("/FoodMenu/GetAllMenu").success(function (respond, status) {
        console.log(respond);
    });
}