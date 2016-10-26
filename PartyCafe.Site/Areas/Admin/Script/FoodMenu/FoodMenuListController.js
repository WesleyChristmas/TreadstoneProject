
foodmenuApp.controller("FoodMenuListController",function($scope,$http,$location){

    $scope.Menu = {};

    $http.get('FoodMenu/GetMenuList').success(function(response){
        $scope.Menu = response;
    });

    $scope.Select = function(index){
        $location.path('/MenuItems/' + $scope.Menu[index].idRecord);
    }

    $scope.Edit = function(index){
        $location.path('/MenuEdit/' + $scope.Menu[index].idRecord);
    }

    $scope.AddNew = function(){
        $location.path('/MenuNew');
    }
});