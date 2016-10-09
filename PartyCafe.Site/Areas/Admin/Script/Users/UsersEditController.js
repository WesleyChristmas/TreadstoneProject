

usrApp.controller("UsersEditController", function($scope,$http,$location,$routeParams){

    $scope.User = {};

    $http.get('Users/GetUserDetail?username=' + $routeParams.usrName).success(function(response){

        $scope.User = new User(response);

    });

    $scope.Back = function(){
        $location.path('/');
    }

});