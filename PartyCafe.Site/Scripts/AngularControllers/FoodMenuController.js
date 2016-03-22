var foodmenuapp = new angular.module("foodmenuapp", []);

foodmenuapp.controller("FoodMenuCt", function ($scope, $http) {
    $scope.Food = [];
    $scope.title = 'Наше меню в кафе';
    $scope.selectedIndex = '';
    $scope.ShowSubMenu = function (obj) { $scope.selectedIndex = obj; }

    GetAllMenu($scope, $http);
});

function GetAllMenu($scope, $http) {
    $http.get("/FoodMenu/GetAllMenu").success(function (data, status) {
        $scope.Food = data;
    });
}