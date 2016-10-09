

usrApp.controller("UsersEditController", function($scope,$http,$location,$routeParams){

    $scope.User = {};

    $http.get('Users/GetUserDetail?username=' + $routeParams.usrName).success(function(response){

        $scope.User = new User(response);

    });

    $scope.Back = function(){
        $location.path('/');
    }

    $scope.Delete = function(){
        $http.get('Users/DeleteUser?username=' + $scope.User.Name).success(function(response){
            if(response == 'ok'){
                $location.path('/');
            } else{
                $scope.Error = response;
            }
        })
    }

    $scope.ChangePass = function(){
        if($scope.User.Pass.length && $scope.User.RepeatPass.length){
            $scope.Errors = 'Не все поля заполнены!';
        }

        if($scope.User.Pass != $scope.User.RepeatPass){
            $scope.Errors = 'Пароли не совпадают!';
        }

        if($scope.User.Pass.length < 6){
            $scope.Errors = 'Пароль не может быть меньше 6 символов!';
        }

        var fd = new FormData();

            fd.append('userName',$scope.User.Name);
            fd.append('pass',$scope.User.Pass);
            fd.append('repeatPass',$scope.User.RepeatPass);

            $http.post('Users/ChangePassword', fd, {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            }).success(function (response) {
                if (response == 'ok') {
                    $location.path('/');
                } else {
                    $scope.Error = response;
                }
            });
    }

});