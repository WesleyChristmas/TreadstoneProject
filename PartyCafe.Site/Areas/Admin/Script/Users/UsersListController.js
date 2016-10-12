
usrApp.controller("UsersListController",function($scope,$http,$location){

    $scope.Users = [];

    $http.get('Users/GetAllUsers').success(function(response){
        $scope.Users = response;
    });

    $scope.AddNew = function(){
        $location.path('/new');
    }

    $scope.Edit = function(index){
        $location.path('/edit/' + $scope.Users[index].UserName)
    }
});