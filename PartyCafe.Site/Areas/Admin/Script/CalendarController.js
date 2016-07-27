var calendarapp = new angular.module("CalendarApp", ['ngRoute']);

calendarapp.directive('jqdatepicker', function () {
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

calendarapp.filter('filterDate', function () {
    return function (date) {
        var curDate = date.match(/\d+/g);
        if (curDate.length) {
            var _data = new Date(parseInt(curDate)),
                _day = _data.getDate(),
                _month = _data.getMonth() + 1,
                _yaer = _data.getFullYear(),
                result = "";

            result += _day < 10 ? "0" + _day + "." : _day + ".";
            result += _month < 10 ? "0" + _month + "." : _month + ".";
            result += _yaer;

            return result;
        }
    };
});
calendarapp.filter('filterTime', function () {
    return function (time) {
        if (time !== undefined) {
            var result = "";

            result += time.Hours < 10 ? "0" + time.Hours : time.Hours;
            result += ":";
            result += time.Minutes < 10 ? "0" + time.Minutes : time.Minutes;
            
            return result;
        }
    };
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
    $scope.addCalendar = function () { $location.path('/add'); };
    $scope.editCalendar = function () {
        sharedDataService.setItem($scope.selectForEdit);
        $location.path('/edit');
    };
    $scope.removeCalendar = function (item) {
        $http.post('Calendar/RemoveCalendarEvent', {
            id: item.idRecord
        }).success(function (response) {
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
    $scope.Header = "Добавление мероприятия в календарь событий";
    $scope.Back = function () { $location.path('/'); }
    $scope.addEvent = function () {
        if ($scope.eventsForm.$valid) {
            var fd = new FormData();
            fd.append('name', $scope.eventsAdd.Name);
            fd.append('description', $scope.eventsAdd.Desc);
            fd.append('date', $scope.eventsAdd.Date);
            fd.append('time', $scope.eventsAdd.Time);
            fd.append('file', document.getElementsByName('eventsPhoto')[0].files[0]);

            $http.post('Calendar/AddCalendarEvent', fd, {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            }).success(function (response) {
                if (response === 'ok') {
                    $location.path('/');
                } else {
                    $scope.error = response;
                }
            });
        }
    };
});

/* Calendar Edit Controller */
calendarapp.controller("CalendarEditController", function ($scope, $http, $location, $routeParams, sharedDataService) {
    $scope.Header = "Редактирование мероприятия";
    $scope.itemForEdit = sharedDataService.getItem();
    $scope.itemForEdit.DateEvent = parseDate($scope.itemForEdit.DateEvent);
    $scope.itemForEdit.TimeEvent = parseTime($scope.itemForEdit.TimeEvent);

    $scope.updateEvent = function () {
        if ($scope.eventsForm.$valid) {
            var fd = new FormData();
            fd.append('id', $scope.itemForEdit.idRecord);
            fd.append('name', $scope.itemForEdit.name);
            fd.append('desc', $scope.itemForEdit.Description);
            fd.append('date', $scope.itemForEdit.DateEvent);
            fd.append('time', $scope.itemForEdit.TimeEvent);
            fd.append('file', document.getElementsByName('eventsPhoto')[0].files[0]);

            $http.post('Calendar/UpdateCalendarEvent', fd, {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            }).success(function (response) {
                if (response === 'ok') {
                    $location.path('/');
                } else {
                    $scope.error = response;
                }
            });
        }
    };
    $scope.BackToCalendarList = function () {
        $location.path('/');
    };
});

function GetAllCalendar($scope, $http) {
    $('#loader').css({ "display": "block" });
    $('.spinner').show();

    $http.get('Calendar/GetAllEvents').success(function (result) {
        $scope.Calendar = result;
    });

    $('#loader').css({ "display": "none" });
    $('.spinner').hide();
}
function restrictTime(myfield, e) {
    if (!e) { var e = window.event; }
    if (e.keyCode) { code = e.keyCode; }
    else if (e.which) { code = e.which; }

    var character = String.fromCharCode(code);
    if (!e.ctrlKey && (code >= 48 && code <= 57) || (code >= 96 && code <= 105)) {
        var currentValue = myfield.value,
            currentValueSplited = currentValue.split('');

        if (currentValueSplited.length > 0) {
            switch (currentValueSplited.length) {
                case 1:
                    if (currentValue.search(/[0-2]{1}/) != -1) {
                        return currentValue;
                    } else {
                        myfield.value = '';
                        return myfield.value;
                    }
                    break;
                case 2:
                    if (currentValueSplited[0] === '0' || currentValueSplited[0] === '1') {
                        if (currentValue.search(/[0-2]{1}[0-9]{1}/) != -1) {
                            myfield.value = currentValueSplited[0] + currentValueSplited[1] + ':';
                            return myfield.value;
                        } else {
                            myfield.value = myfield.value.slice(0, -1);
                            return myfield.value;
                        }
                    } else {
                        if (currentValue.search(/[0-2]{1}[0-3]{1}/) != -1) {
                            myfield.value = currentValueSplited[0] + currentValueSplited[1] + ':';
                            return myfield.value;
                        } else {
                            myfield.value = myfield.value.slice(0, -1);
                            return myfield.value;
                        }
                    }
                    break;
                case 3:
                    if (currentValueSplited.indexOf(':') === -1) {
                        myfield.value = myfield.value.splice(2, 0, ':');
                        return myfield.value;
                    }
                    break;
                case 4:
                    if (currentValueSplited[3].search(/[0-5]/) === 0) {
                        return myfield.value;
                    } else {
                        myfield.value = myfield.value.slice(0, -1);
                        return myfield.value;
                    }
                    break;
            }
        }
    } else {
        if (code !== 8) { myfield.value = myfield.value.slice(0, -1); }
        return myfield.value;
    }
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
function parseTime(time) {
    if (time !== undefined) {
        var result = "";

        result += time.Hours < 10 ? "0" + time.Hours : time.Hours;
        result += ":";
        result += time.Minutes < 10 ? "0" + time.Minutes : time.Minutes;

        return result;
    }
}
function parseDate(date) {
    var curDate = date.match(/\d+/g);
    if (curDate.length) {
        var _data = new Date(parseInt(curDate)),
            _day = _data.getDate(),
            _month = _data.getMonth() + 1,
            _yaer = _data.getFullYear(),
            result = "";

        result += _day < 10 ? "0" + _day + "." : _day + ".";
        result += _month < 10 ? "0" + _month + "." : _month + ".";
        result += _yaer;

        return result;
    }
}