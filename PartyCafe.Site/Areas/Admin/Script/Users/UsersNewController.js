

usrApp.controller("UsersNewController", function($scope,$http,$location){

    $scope.User = new User();

    $scope.Back = function(){
        $location.path('/');
    }

    $scope.SaveChanges = function(){
        if($scope.UserForm.$valid){

            if($scope.User.Pass != $scope.Users.RepeatPass){
                $scope.Errors = 'Пароли не совпадают!';
            }

            if($scope.User.Pass.length < 6){
                $scope.Errors = 'Пароль не может быть меньше 6 символов!';
            }   

            var fd = new FormData();

            fd.append('userName',$scope.User.Name);
            fd.append('pass',$scope.User.Pass);
            fd.append('repeatPass',$scope.User.RepeatPass);

            $http.post('Users/AddUser', fd, {
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
        else{
            $scope.Error = 'Не все обязательные поля заполнены';
        }
    }
});