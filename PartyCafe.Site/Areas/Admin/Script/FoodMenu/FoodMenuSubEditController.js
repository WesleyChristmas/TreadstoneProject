
foodmenuApp.controller("FoodMenuSubEditController",function($scope,$http,$location,$routeParams){

    $scope.MenuItem = {};
    $scope.Header = "Редактирование подраздела меню";

    $http.get('FoodMenu/GetMenuSub?submenuId=' + $routeParams.subMenuId).success(function(response){
        $scope.MenuItem = response;
    })

    $scope.Back = function(){
        $location.path('/SubMenu/' + $routeParams.menuId);
    }

    $scope.SaveChanges = function(){
        if($scope.MenuForm.$valid){
            var fd = new FormData();

            fd.append('id', $scope.MenuItem.idRecord);
            fd.append('idparent',$routeParams.menuId);
            fd.append('name',$scope.MenuItem.name);

            $http.post('FoodMenu/EditGroupItem', fd, {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            }).success(function (response) {
                if (response == 'ok') {
                    $scope.Back();
                } else {
                    $scope.Error = response;
                }
            });
        } else {
            $scope.Error = "Одно из обязательных полей не заполнено!"
        }  
    }

    $scope.Delete = function(){
        $http.get('FoodMenu/RemoveGroupItem?id=' + $scope.MenuItem.idRecord).success(function(response){
            $scope.Back();
        });
    }
});