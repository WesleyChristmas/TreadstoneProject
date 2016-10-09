

usrApp.controller("UsersNewController", function($scope,$http,$location){



    $scope.Back = function(){
        $location.path('/');
    }
});