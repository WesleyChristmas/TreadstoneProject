var foodmenuapp = new angular.module("foodmenuapp", []);

foodmenuapp.filter("CorrectPrice",function(){
    return  function(price){
        var result = parseInt(price);
        if(result == NaN) return;
        return result.toFixed(2);
    };
})

foodmenuapp.controller("FoodMenuCt", function ($scope, $http) {
    $scope.Menu = [];
    $scope.SubMenu = [];

    $http.get("/FoodMenu/GetAllMenu").success(function(response){
        if(response.length < 1) return;

        $scope.Menu = response;
        $scope.CurrentItems = $scope.Menu[0].items;
    });

    $scope.GetFoodItems = function(element){
        $scope.CurrentItems = element.item.items;
    }
});