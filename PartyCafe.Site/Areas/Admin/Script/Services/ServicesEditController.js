servicesApp.controller("ServicesEditController",function($scope,$http,$location,$routeParams){
    $scope.Header = "Редактирование раздела";
    $scope.Subheader = "";
    $scope.BtnSaveName = "Сохранить измнения";

    $scope.Service = {};


    $http.get('Services/GetServicesFull?id=' + $routeParams.id).success(function(response){
        $scope.Service = new Service(response);
    });


    $scope.Back = function(){
        $location.path('/');
    }

    $scope.SaveChanges = function(){
        if ($scope.AboutPartForm.$valid){

            var fd = new FormData();
            fd.append('name', $scope.Service.name);
            fd.append('desc', $scope.Service.description);
            fd.append('file', document.getElementsByName('aboutusPhoto')[0].files[0]);
            fd.append('id',$scope.Service.idRecord);

            $http.post('Services/UpdateServices', fd, {
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

    $scope.AddPhoto = function(){
        var photo = document.getElementsByName('subphotoinp')[0].files[0];
        if(photo == null) {
            $scope.SubPhotoError = "Вы не выбрали фотографию";
            return;
        }

        var fd = new FormData();
        fd.append('id',$scope.Service.idRecord);
        fd.append('file', photo);

        $http.post('Services/AddPhotoToServices', fd, {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            }).success(function (response) {
                if (response == 'ok') {
                    $http.get('AboutUs/GetServicesFull?id=' + $routeParams.id).success(function(response){
                        $scope.Service = new Service(response);
                    });
                } else {
                    $scope.SubPhotoError = "Произошла ошибка";
                }
        });
    }

    $scope.Delete = function(){
        $http.get('Services/RemoveServices?id=' + $scope.Service.idRecord).success(function(response){
            $location.path('/');
        });
    }

    $scope.DeletePhoto = function(index){
        $http.get('Services/RemovePhotoFromServices?id=' + $scope.Service.photos[index].idRecord).success(function(response){
            if(response == 'ok'){
                $http.get('AboutUs/GetServicesFull?id=' + $routeParams.id).success(function(response){
                        $scope.Service = new Service(response);
                });
            }
        });
    }
});