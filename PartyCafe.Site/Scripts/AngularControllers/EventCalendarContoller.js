var eventcalendarapp = new angular.module("eventcalendarapp", []);

eventcalendarapp.controller("EventCalendar", function ($scope, $http) {
    $scope.Events = [];
    getData($scope);
});

Date.prototype.monthDays = function () {
    return new Date(this.getFullYear(), this.getMonth() + 1, 0).getDate();
}
function setCalendaData(_d, _m) {
    return { day: _d, month: _m, data: [] };
}

function getData($scope) {
    var xhr = new XMLHttpRequest();

    xhr.open('GET', 'GetCalendar', true);
    xhr.send();
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4) {
            var result = JSON.parse(xhr.response);
            Calendar('calendar-wrap', 30, result.Calendar, result.CurDate.replace(/\D+/g, ""), $scope);
        }
    }
}

function Calendar(obj, dcount, respons, curdate, $scope) {
    var date = new Date(parseInt(curdate)),
        m = date.getMonth() + 1, daysInMonth = date.monthDays(),
        cal = [],
        _month = ["Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь"],
        l = dcount;

    /* формирование календаря */
    for (var i = 0; i < l; ++i) {
        var d = date.getDate();

        if (d >= daysInMonth) {
            var obj = setCalendaData(d, _month[m-1]);
            cal[i] = obj;

            if (m != 13) {
                date.setDate(1);
                date.setMonth(date.getMonth() + 1);

                var obj = setCalendaData(d, _month[m - 1]);
                cal[i] = obj;
                m = date.getMonth() + 1;
            }
        }
        else {
            var obj = setCalendaData(d, _month[m - 1]);

            cal[i] = obj;
            date.setDate(d + 1);
        }
    }

    /* Заполнение календаря событиями */
    l = respons.length;
    for (var i = 0; i < l; i++) {
        var _d = respons[i].EventDate.replace(/\D+/g, ""),
            date = new Date(parseInt(_d)),
            d = date.getDate(),
            _calL = cal.length;

        for (var j = 0; j < _calL; j++) {
            if (cal[j].day === d) {
                var _obj = { header: respons[i].Header, photo: respons[i].PhotoLink }
                cal[j].data = _obj;
                break;
            }
        }
    }
    //console.log(cal);
    $scope.Events = cal;
    $scope.$apply();
    //fillCalendar(cal);
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

    var elem = document.querySelector('#calendar-wrap');
    var iso = new Isotope(elem, {
        itemSelector: '.event-day',
        layoutMode: 'masonry'
    });

}