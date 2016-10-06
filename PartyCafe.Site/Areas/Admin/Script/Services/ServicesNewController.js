
servicesApp.controller("ServicesNewController",function($scope,$http, $location){

    $scope.Service = new Service();

    $scope.Back = function(){
        $location.path('/');
    }

    $scope.SaveChanges = function(){
        if ($scope.AboutPartForm.$valid){

            var fd = new FormData();
            fd.append('name', $scope.Service.name);
            fd.append('desc', $scope.Service.description);
            fd.append('file', document.getElementsByName('aboutusPhoto')[0].files[0]);

            $http.post('Services/AddServices', fd, {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            }).success(function (response) {
                if (response == 'ok') {
                    $location.path('/');
                } else {
                    $scope.Error = response;
                }
            });
        } else {
            $scope.Error = "Одно из обязательных полей не заполнено!"
        }
    }
});