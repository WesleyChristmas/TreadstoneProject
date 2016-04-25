var eventcalendarapp = new angular.module("eventcalendarapp", ['ngRoute']);

eventcalendarapp.config(function ($routeProvider) {
    $routeProvider.when('/',
    {
        templateUrl: 'EventCalendar/Calendar',
        controller: 'CalendarController'
    }).when('/add', {
        templateUrl: 'EventCalendar/Invite',
        controller: 'InviteController'
    });
});

/*eventcalendarapp.controller("EventCalendar", function ($scope, $http) {
    $scope.Events = [];
    getData($scope, $http);
});*/

eventcalendarapp.service("sharedDataService", function () {
    this.eventsList = [];
    this.eventsItem = {};

    this.setevents = function (obj) { this.eventsList = obj; }
    this.getevents = function () { return this.eventsList; }

    this.setItem = function (item) { this.eventsItem = item; }
    this.getItem = function () { return this.eventsItem; }
});

/* Calendar Controller */
eventcalendarapp.controller("CalendarController", function ($scope, $http, $location, sharedDataService) {
    /*Helpers*/
    $scope.More = function () {

    };

    $scope.Events = [];
    $scope.selectForEdit = '';

    $http.get("/EventCalendar/GetCalendar").success(function (data, status) {
        Calendar('calendar-wrap', 30, data.Calendar, data.CurDate.replace(/\D+/g, ""), $scope);
    });
});

eventcalendarapp.directive('note', function ($timeout) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            $timeout(function () {
                var iso = new Isotope(document.querySelector('#calendar-wrap'), {
                    itemSelector: '.event-day',
                    layoutMode: 'fitRows'
                });
            }, 100);
        }
    };
});


/* Helpers */
Date.prototype.monthDays = function () {
    return new Date(this.getFullYear(), this.getMonth() + 1, 0).getDate();
}
function setCalendaData(_d, _w, _m) {
    return { day: _d, week: _w, month: _m, data: [] };
}

function getData($scope, $http) {
    $http.get("/EventCalendar/GetCalendar").success(function (data, status) {
        Calendar('calendar-wrap', 30, data.Calendar, data.CurDate.replace(/\D+/g, ""), $scope);
    });
}

function Calendar(obj, dcount, respons, curdate, $scope) {
    var date = new Date(parseInt(curdate)),
        m = date.getMonth() + 1, daysInMonth = date.monthDays(),
        cal = [],
        _month = ["Января", "Февраля", "Марта", "Апреля", "Мая", "Июня", "Июля", "Августа", "Сентября", "Октября", "Ноября", "Декабря"],
        weekShort = ["ВС","ПН", "ВТ", "СР", "ЧТ", "ПТ", "СБ"],
        weekFull = ["Воскресенье","Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота"],
        l = dcount;

    /* формирование календаря */
    for (var i = 0; i < l; ++i) {
        var d = date.getDate();

        if (d >= daysInMonth) {
            var obj = setCalendaData(d, weekFull[date.getDay()], _month[m - 1]);
            cal[i] = obj;

            if (m != 13) {
                date.setDate(1);
                date.setMonth(date.getMonth() + 1);

                var obj = setCalendaData(d, weekFull[date.getDay()], _month[m - 1]);
                cal[i] = obj;
                m = date.getMonth() + 1;
            }
        }
        else {
            var obj = setCalendaData(d, weekFull[date.getDay()], _month[m - 1]);

            cal[i] = obj;
            date.setDate(d + 1);
        }
    }

    /* Заполнение календаря событиями */
    l = respons.length;
    for (var i = 0; i < l; i++) {
        var _d = respons[i].DateEvent.replace(/\D+/g, ""),
            date = new Date(parseInt(_d)),
            d = date.getDate(),
            m = date.getMonth(),
            _calL = cal.length;

        for (var j = 0; j < _calL; j++) {
            //if (cal[j].day === d && cal[j].month === m+1) {
            if (cal[j].day === d) {
                var _obj = {
                    header: respons[i].name,
                    photo: respons[i].PhotoPath,
                    time: respons[i].TimeEvent
                }
                cal[j].data = _obj;
                break;
            }
        }
    }
    console.log(cal);
    $scope.Events = cal;
}
function fillCalendar(obj) {
    var calendar = document.getElementById('calendar-wrap'),
        l = obj.length,
        counter = 0,
        _week = document.createElement('div'),
        _weekDay = document.createElement('div');

    for (var i = 0; i < l; i++) {
        _weekDay = document.createElement('div');

        if (counter == 6 || i == l - 1) {
            var _div = document.createElement('div');
            _div.textContent = obj[i].data.header;

            _weekDay.className = 'event-day';
            _weekDay.textContent = obj[i].day;
            _weekDay.appendChild(_div);

            _week.className = 'event-week';
            _week.appendChild(_weekDay);

            calendar.appendChild(_week);

            _week = document.createElement('div');
            counter = 0;
        } else {
            var _div = document.createElement('div');
            _div.textContent = obj[i].data.header;

            _weekDay.className = 'event-day';
            _weekDay.textContent = obj[i].day;
            _weekDay.appendChild(_div);

            _week.appendChild(_weekDay);
            counter++;
        }
    }
}