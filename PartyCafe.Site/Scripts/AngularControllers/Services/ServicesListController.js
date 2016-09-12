

servicesapp.controller("ServiceListController",function($scope,$http,$location){

    $scope.Services = [];

    //Methods

    $scope.GetAllServices = function() {
        $http.get("/Services/GetAllServices").success(function (data, status) {
            $scope.Services = data;
        });
    }

    $scope.GetDetailed = function(index){
        $location.path('/service/' + $scope.Services[index].idRecord);
    }

    //Constructor

    $scope.GetAllServices();
});