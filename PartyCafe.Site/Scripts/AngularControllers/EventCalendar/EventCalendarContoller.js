var eventcalendarapp = new angular.module("eventcalendarapp", ['ngRoute']);

eventcalendarapp.config(function ($routeProvider) {
    $routeProvider.when('/',
    {
        templateUrl: 'EventCalendar/Calendar',
        controller: 'CalendarController'
    });
});

eventcalendarapp.controller("CalendarController", function ($scope, $http, $location, $anchorScroll) {

    $scope.Order = new Order();

    $http.get("/EventCalendar/GetCalendar").success(function (data, status) {
        Calendar('calendar-wrap', 30, data.Calendar, data.CurDate.replace(/\D+/g, ""), $scope);
    });

    $scope.SelectEvent = function(element){
        if(!element.item) return;
        $scope.Order.Show = true;
        $scope.Order.ServiceImg = element.item.data.photo;
        $scope.Order.ServiceName = element.item.data.header;
        $location.hash('order');
        $anchorScroll();
    }
});


/*Order class */

function Order(){
    this.ServiceImg = null;
    this.ServiceName = null;
    this.Name = null;
    this.Phone = null;
    this.Person = null;
    this.Promo = null;

    this.Show = false;
    this.ValError = false;
    this.ServerError = false;
    this.Good = false;
    this.ShowBtn = true;
    
    this.Validation = function(){
        return ((this.Name)&&(this.Phone)&&(this.Person));
    }

    this.ClearValidation = function(){
        this.Show = false;
        this.ValError = false;
        this.ServerError = false;
        this.Good = false;
        this.ShowBtn = true;
    }
}

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
            if (cal[j].day === d) {
                var _obj = {
                    header: respons[i].name,
                    photo: respons[i].PhotoPath,
                    time: respons[i].TimeEvent,
                    desc: respons[i].Description,
                    childrenPhoto: respons[i].photos
                }
                cal[j].data = _obj;
                break;
            }
        }
    }
    console.log(cal);
    $scope.Events = cal;
}
