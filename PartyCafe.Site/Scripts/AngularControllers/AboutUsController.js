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
        controller: 'ServiceDetailedController'
    });
});

aboutusapp.controller("AboutUsController",function($scope){

});

aboutusapp.controller("AboutUsListController", function ($scope, $http) {
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
    $http.get("AboutUs/GetAllUs").success(function(response){
        $scope.About = new About(response);
    });    
});


function About(entity){
    this.Photo = entity.PhotoPath,
    this.Name = entity.Name,
    this.Description = entity.Description;
}
