
var calendarApp = new angular.module("CalendarApp", ['ngRoute']);

calendarApp.config(function($routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: 'Calendar/CalendarEvents',
            controller: 'CalendarEventsController'
        })
        .when('/EditCalendarEvent/:idCalendar', {
            templateUrl: 'Calendar/EditCalendarEvent',
            controller: 'EditCalendarEventController'
        });
});

calendarApp.controller("CalendarController", function($location) {
    $location.path('/');
});

calendarApp.controller("CalendarEventsController", function($scope, $http, $location) {
    $scope.Base = new BaseClass($scope, $http, $location);

    DataModelClass.prototype = $scope.Base;
    $scope.DataModel = new DataModelClass();

    $scope.DataModel.FillCalendar();
});

calendarApp.controller("EditCalendarEventController", function($scope, $http, $location) {

});

calendarApp.filter("CorrectDate", function(jsDate) {
    return CorrectDate(jsDate);
});