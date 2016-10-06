
foodmenuApp.controller("FoodMenuSubListController",function($scope,$http,$location,$routeParams){

    $scope.SubMenu = {};
    $scope.Header = "";
    
    $http.get('FoodMenu/GetMenuSubList?listId=' + $routeParams.menuId).success(function(response){
        $scope.Header = response.name;
        $scope.SubMenu = response.subGroups;
    })

    $scope.Back = function(){
        $location.path('/');
    }

    $scope.Select = function(index){
       $location.path('/MenuItems/' + $scope.SubMenu[index].idRecord);
    }
});