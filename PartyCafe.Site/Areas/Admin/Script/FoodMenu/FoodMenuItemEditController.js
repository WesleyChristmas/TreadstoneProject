
foodmenuApp.controller("FoodMenuItemEditController",function($scope,$http,$location,$routeParams){

    $scope.MenuItem = {};

    $http.get('FoodMenu/GetMenuItem?itemId=' + $routeParams.itemId).success(function(response){
        $scope.MenuItem = response;
        $scope.MenuItem.price = correctPrice($scope.MenuItem.price);
    });

    $scope.Back = function(){
        $location.path('/MenuItems/' + $routeParams.subMenuId);
    }

    $scope.SaveChanges = function(){
        if ($scope.MenuForm.$valid){

            var fd = new FormData();
            fd.append('idrecord',$scope.MenuItem.idRecord);
            fd.append('name', $scope.MenuItem.name);
            fd.append('des', $scope.MenuItem.description);
            fd.append('weipla', $scope.MenuItem.Weight);
            fd.append('price',$scope.MenuItem.price);
            fd.append('groupid', $routeParams.subMenuId)
            fd.append('file', document.getElementsByName('menuitemPhoto')[0].files[0]);

            $http.post('FoodMenu/EditItem', fd, {
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
        $http.get('FoodMenu/RemoveItem?id=' + $scope.MenuItem.idRecord).success(function(response){
            $scope.Back();
        });
    }
});