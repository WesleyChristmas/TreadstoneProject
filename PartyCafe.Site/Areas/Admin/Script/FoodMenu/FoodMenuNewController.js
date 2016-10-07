
foodmenuApp.controller("FoodMenuNewController",function($scope,$http,$location,$routeParams){

    $scope.MenuItem = {};
    $scope.Header = "Добавление основного раздела меню";
    $scope.BtnName = "Добавить основной раздел меню";

    $scope.Back = function(){
        $location.path('/');
    }

    $scope.SaveChanges = function(){
        if($scope.MenuForm.$valid){
            var fd = new FormData();

            fd.append('name',$scope.MenuItem.name);

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