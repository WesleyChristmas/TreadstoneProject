
servicesApp.controller("ServicesListController",function($scope,$http,$location){

    $scope.Services = [];

    $http.get('Services/GetAllServices').success(function(response){
        $scope.Services = response;
    });


    $scope.AddNew = function(){
        $location.path("/new");
    }

    $scope.Edit = function(index){
        $location.path('/edit/' + $scope.Services[index].idRecord);
    }
});