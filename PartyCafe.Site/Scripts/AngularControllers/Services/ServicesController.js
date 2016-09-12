
var servicesapp = new angular.module("servicesapp", ["ngRoute"]);
servicesapp.directive('jqdatepicker', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            $(function () {
                element.datepicker({
                    dateFormat: 'dd.mm.yy',
                    onSelect: function (date) {
                        scope.$apply(function () {
                            ngModelCtrl.$setViewValue(date);
                        });
                    }
                });
            });
        }
    };
});

servicesapp.config(function($routeProvider){
    $routeProvider
    .when('/',{
        templateUrl: 'Services/ServiceList',
        controller: 'ServiceListController'
    })
    .when('/service/:id',{
        templateUrl:'Services/ServiceDetailed',
        controller: 'ServiceDetailedController'
    });
});

servicesapp.controller("ServiceController",function($scope){

});