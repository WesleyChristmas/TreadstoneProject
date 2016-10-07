var aboutusapp = new angular.module("AboutUsApp", ["ngRoute"]);

aboutusapp.directive('jqdatepicker', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            element.datepicker({
                format: 'dd.mm.yyyy',
                language: 'ru-RU',
                autoclose: true
            });
        }
    };
});

aboutusapp.config(function($routeProvider){
    $routeProvider
    .when('/',{
        templateUrl: 'AboutUs/ServiceList',
        controller: 'AboutUsListController'
    })
    .when('/service/:id',{
        templateUrl:'AboutUs/ServiceDetailed',
        controller: 'AboutUsDetailedController'
    });
});

aboutusapp.controller("AboutUsController",function($scope){

});

aboutusapp.controller("AboutUsListController", function ($scope, $http,$location) {
    $scope.Services = [];

    $scope.GetAllServices = function() {
        $http.get("/AboutUs/GetAllServices").success(function (data, status) {
            $scope.Services = data;
        });
    }

    $scope.GetDetailed = function(index){
        $location.path('/service/' + $scope.Services[index].idRecord);
    }

    $scope.GetAllServices();
    $http.get("AboutUs/GetAllUs").success(function(response){
        $scope.About = new About(response);
    });    
});

aboutusapp.controller("AboutUsDetailedController",function($scope,$http,$routeParams){

    $http.get("/Services/GetServiceFull?serviceId=" + $routeParams.id).success(function(response){
        $scope.Service = new Service(response);
    });
});

function Service(entity){
    this.Name = entity.name;
    this.Description = entity.description;
    this.Photo = entity.photoPath;
    this.Photos = entity.photos;
}

function About(entity){
    this.Photo = entity.PhotoPath,
    this.Name = entity.Name,
    this.Description = entity.Description;
}
