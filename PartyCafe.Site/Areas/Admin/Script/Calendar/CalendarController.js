var calendarApp = new angular.module("CalendarApp",['ngRoute']);

calendarApp.directive('jqdatepicker', function () {
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

calendarApp.config(function($routeProvider){
    $routeProvider
    .when('/',{
        templateUrl:'Calendar/CalendarList',
        controller: 'CalendarListController'
    })
    .when('/edit/:id',{
        templateUrl:'Calendar/CalendarEdit',
        controller: 'CalendarEditController'
    })
    .when('/new',{
        templateUrl:'Calendar/CalendarNew',
        controller:'CalendarNewController'
    });
})

calendarApp.controller("CalendarController",function($scope){

});

function Calendar(entity){
    this.IdRecord = entity.idRecord;
    this.Name = entity.name;
    this.Date = CorrectDate(entity.DateEvent);
    this.Time = CorrectTime(entity.TimeEvent);
    this.PhotoPath = entity.PhotoPath;
}

function CorrectDate(date) {
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

function CorrectTime(time) {
        if (time !== undefined) {
            var result = "";

            result += time.Hours < 10 ? "0" + time.Hours : time.Hours;
            result += ":";
            result += time.Minutes < 10 ? "0" + time.Minutes : time.Minutes;
            
            return result;
        }
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
                case 3:
                    if (currentValueSplited.indexOf(':') === -1) {
                        myfield.value = myfield.value.splice(2, 0, ':');
                        return myfield.value;
                    }
                case 4:
                    if (currentValueSplited[3].search(/[0-5]/) === 0) {
                        return myfield.value;
                    } else {
                        myfield.value = myfield.value.slice(0, -1);
                        return myfield.value;
                    }
            }
        }
    } else {
        if (code !== 8) { myfield.value = myfield.value.slice(0, -1); }
        return myfield.value;
    }
}

