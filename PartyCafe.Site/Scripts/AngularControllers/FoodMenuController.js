var foodmenuapp = new angular.module("foodmenuapp", []);

foodmenuapp.controller("FoodMenuCt", function ($scope, $http) {
    $scope.Menu = [];
    $scope.SubMenu = [];

    $http.get("/FoodMenu/GetAllMenu").success(function(response){
        if(response.length < 1) return;

        $scope.Menu = response;
        $scope.SubMenu = $scope.Menu[0].subGroups;
        $scope.CurrentItems = $scope.Menu[0].subGroups[0].items;
    });

    $scope.GetFoodItems = function(element){
        if(element.sub == null) return;
        $scope.CurrentItems = element.sub.items;
    }
});