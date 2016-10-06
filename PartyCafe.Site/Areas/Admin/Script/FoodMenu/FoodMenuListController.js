
foodmenuApp.controller("FoodMenuListController",function($scope,$http,$location){

    $scope.Menu = {};

    $http.get('FoodMenu/GetMenuList').success(function(response){
        $scope.Menu = response;
    });

    $scope.Select = function(index){
        $location.path('/SubMenu/' + $scope.Menu[index].idRecord);
    }
});