﻿
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

calendarApp.service("sharedDataService", function() {
    this.Data = null;

    this.addData = function(data) {
        this.Data = data;
    }

    this.getData = function() {
        return this.Data;
    }
});

calendarApp.controller("CalendarController", function($location) {
    $location.path('/');
});

calendarApp.controller("CalendarEventsController", function($scope, $http, $location,sharedDataService) {
    $scope.Base = new BaseClass($scope, $http, $location, sharedDataService);

    DataModelClass.prototype = $scope.Base;
    $scope.DataModel = new DataModelClass();

    $scope.DataModel.FillCalendar();

   /* $scope.SelectCalendarEvent = function(obj) {
        $location.path('/EditCalendarEvent/' + obj.item.IdRecord);
    }*/

    $scope.AddNewCalendarEvent = function() {
        $location.path('/EditCalendarEvent/0');
    }

    $scope.DeleteCalendarEvent = function (obj) {
        $http.post('Calendar/DeleteBlogCalendar', { 'idCalendar': obj.item.IdRecord }).success(function (response) {
            $scope.DataModel.FillCalendar();
        });
    }
});

calendarApp.controller("EditCalendarEventController", function($scope, $http,$location, $routeParams, sharedDataService) {

    var calendars = sharedDataService.getData();
    var id = $routeParams.idCalendar;

    if (id == 0 || calendars == null) {
        $scope.CurCalendarEvent = new CalendarEntityClass(null);
        $scope.isCreate = true;
    } else {
        for (var i in calendars) {
            if (calendars[i].IdRecord == id) {
                $scope.CurCalendarEvent = calendars[i];
                $scope.CurCalendarEvent.EventDate = CorrectDate($scope.CurCalendarEvent.EventDate);
                $scope.isCreate = false;
            }
        }
    }

    //Methods
    $scope.SaveChanges = function() {
        var url;
        var xhr = new XMLHttpRequest();
        var fd = new FormData();

        if ($scope.isCreate) {
            url = 'Calendar/AddBlogCalendar';
        } else {
            url = 'Calendar/UpdateBlogCalendar';
            fd.append('IdRecord', $scope.CurCalendarEvent.IdRecord);
        }

        if ($scope.Image != null) {
            fd.append('file', $scope.Image);
            fd.append('filename', $scope.Image.name);
        }

        fd.append('Header', $scope.CurCalendarEvent.Header);
        fd.append('EventDate', $scope.CurCalendarEvent.EventDate);

        xhr.upload.onload = function() {
            $scope.$apply($location.path('/'));
        }

        xhr.open('POST', url, true);
        xhr.send(fd);
    }

    $scope.DeleteCalendarEvent = function () {
        $http.post('Calendar/DeleteBlogCalendar', { 'idCalendar': $scope.CurCalendarEvent.IdRecord }).success(function (response) {
            $location.path('/');
        });
    }

    $scope.AddFile = function (file) {
        $scope.Image = file[0];
    }
});

calendarApp.filter("CorrectDate", function() {
    return function(jsDate) {
        return CorrectDate(jsDate);
    }
});