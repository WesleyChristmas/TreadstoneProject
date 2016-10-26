

foodmenuApp.controller("FoodMenuItemNewController",function($scope,$http,$location,$routeParams){
    
    $scope.MenuItem = {};

    $scope.Back = function(){
        $location.path('/MenuItems/' + $routeParams.menuId);
    }

    $scope.SaveChanges = function(){
        if ($scope.MenuForm.$valid){

            var fd = new FormData();
            fd.append('name', $scope.MenuItem.name);
            fd.append('des', $scope.MenuItem.description == null ? "" : $scope.MenuItem.description);
            fd.append('weipla', $scope.MenuItem.Weight);
            fd.append('price',$scope.MenuItem.price);
            fd.append('menuId', $routeParams.menuId)
            fd.append('file', document.getElementsByName('menuitemPhoto')[0].files[0]);

            $http.post('FoodMenu/AddItem', fd, {
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