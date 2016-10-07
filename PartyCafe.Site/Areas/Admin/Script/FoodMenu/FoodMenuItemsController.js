
foodmenuApp.controller("FoodMenuItemsController",function($scope,$http,$location,$routeParams){
    $scope.MenuItems = [];
    $scope.Header = "";

    $http.get('FoodMenu/GetMenuItems?sublistId=' + $routeParams.subMenuId).success(function(response){
        $scope.MenuItems = response.items;
        $scope.Header = response.name;
    });

    $scope.Edit = function(index){
        $location.path('/MenuItemEdit/' + $routeParams.menuId + '/' + $routeParams.subMenuId + '/' + $scope.MenuItems[index].idRecord);
    }

    $scope.AddNew = function(){
        $location.path('/MenuItemNew/' + $routeParams.menuId + '/' + $routeParams.subMenuId);
    }

    $scope.Back = function(){
        $location.path('/SubMenu/' + $routeParams.menuId);
    }
});