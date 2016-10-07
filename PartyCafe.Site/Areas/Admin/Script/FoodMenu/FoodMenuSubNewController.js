
foodmenuApp.controller("FoodMenuSubNewController",function($scope,$http,$location,$routeParams){

    $scope.MenuItem = {};
    $scope.Header = "Добавление подраздела меню";
    $scope.BtnName = "Добавить подраздел меню";

    $scope.Back = function(){
        $location.path('/SubMenu/' + $routeParams.menuId);
    }

    $scope.SaveChanges = function(){
        if($scope.MenuForm.$valid){
            var fd = new FormData();

            fd.append('name',$scope.MenuItem.name);
            fd.append('parentId',$routeParams.menuId);

            $http.post('FoodMenu/AddGroupItem', fd, {
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
});