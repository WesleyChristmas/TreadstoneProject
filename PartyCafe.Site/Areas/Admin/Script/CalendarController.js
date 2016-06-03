var calendarapp = new angular.module("CalendarApp", ['ngRoute']);

calendarapp.config(function ($routeProvider) {
    $routeProvider.when('/',
    {
        templateUrl: 'Calendar/CalendarHome',
        controller: 'CalendarHomeController'
    }).when('/add', {
        templateUrl: 'Calendar/CalendarAdd',
        controller: 'CalendarAddController'
    }).when('/edit', {
        templateUrl: 'Calendar/CalendarEdit',
        controller: 'CalendarEditController'
    }).otherwise('/');
});

calendarapp.service("sharedDataService", function () {
    this.rolesItem = {};

    this.setItem = function (item) { this.rolesItem = item; }
    this.getItem = function () { return this.rolesItem; }
});

/* Calendar Home Controller */
calendarapp.controller("CalendarHomeController", function ($scope, $http, $location, sharedDataService) {
    /*Helpers*/
    $scope.isActive = function (item) { return $scope.selectForEdit === item; };
    $scope.HighlightItem = function (item) { $scope.selectForEdit = item; };
    $scope.AddCalendar = function () { $location.path('/add'); };
    $scope.EditCalendar = function () {
        sharedDataService.setItem($scope.selectForEdit);
        $location.path('/edit');
    };
    $scope.removeCalendarItem = function (item) {
        $http.post('Calendar/DeleteCalendarItem', { id: item.idRecord }).success(function (response) {
            if (response === 'ok') {
                $location.path('/');
            } else {
                $scope.error = response;
            }
        });
    };

    $scope.Calendar = [];
    $scope.Header = "Управление мероприятиями";
    $scope.selectForEdit = '';

    GetAllCalendar($scope, $http);
});

/* Calendar Add Controller */
calendarapp.controller("CalendarAddController", function ($scope, $http, $location) {
    /*Helpers*/
    $scope.Header = "Добавление фото в галлерею";
    $scope.Back = function () { $location.path('/'); }
    $scope.addCalendar = function () {
        var fd = new FormData();
        fd.append('name', $scope.calendarAdd.Name);
        fd.append('description', $scope.calendarAdd.Desc);
        fd.append('file', document.getElementsByName('calendarPhoto')[0].files[0]);

        $http.post('Calendar/AddCalendar', fd, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).success(function (response) {
            if (response === 'ok') {
                $location.path('/');
            } else {
                $scope.error = response;
            }
        });
    };

});

/* Calendar Edit Controller */
calendarapp.controller("CalendarEditController", function ($scope, $http, $location, $routeParams, sharedDataService) {
    /*Helpers*/
    $scope.Header = "Редактирование фотографии";
    $scope.itemForEdit = sharedDataService.getItem();

    $scope.updateCalendar = function () {
        var fd = new FormData();
        fd.append('id', $scope.itemForEdit.idRecord);
        fd.append('name', $scope.itemForEdit.name);
        fd.append('desc', $scope.itemForEdit.description);
        fd.append('file', document.getElementsByName('calendarPhoto')[0].files[0]);

        $http.post('Calendar/UpdateCalendar', fd, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).success(function (response) {
            if (response === 'ok') {
                $location.path('/');
            } else {
                $scope.error = response;
            }
        });
    };
    $scope.BackToCalendarList = function () {
        $location.path('/');
    };
});

function GetAllCalendar($scope, $http) {
    $('#loader').css({ "display": "block" });
    $('.spinner').show();

    $http.get('Calendar/GetAllCalendar').success(function (result) {
        $scope.Calendar = result;
    });

    $('#loader').css({ "display": "none" });
    $('.spinner').hide();
}
function uploadFile($scope, $http, obj) {
    var xhr = new XMLHttpRequest(),
        fd = new FormData();

    fd.append(0, obj);

    xhr.onreadystatechange = function () {
        if (xhr.status != 200) {
            $('.summernote').summernote('insertImage', xhr.response);
        }
    }

    xhr.open('POST', 'Calendar/UploadFile', true);
    xhr.send(fd);
}