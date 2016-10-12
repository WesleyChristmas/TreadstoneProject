var eventcalendarapp = new angular.module("eventcalendarapp", ['ngRoute']);

eventcalendarapp.config(function ($routeProvider) {
    $routeProvider.when('/',
    {
        templateUrl: 'EventCalendar/Calendar',
        controller: 'CalendarController'
    });
});

eventcalendarapp.filter("CorrectTime",function(){
    return function(time){
        if(!time) return;
        return CheckTime(time.Hours) + ':' + CheckTime(time.Minutes);
    }
})

eventcalendarapp.controller("CalendarController", function ($scope, $http, $location, $anchorScroll,$timeout) {

    $scope.Order = new Order();

    $http.get("/EventCalendar/GetCalendar").success(function (data, status) {
        Calendar('calendar-wrap', 30, data.Calendar, data.CurDate.replace(/\D+/g, ""), $scope);
    });

    $location.hash('order');

    $scope.SelectEvent = function(element){

        $scope.Order.ClearValidation();
        if(!element.item.data.header){
            return;
        }
        $scope.Order.Show = true;
        $scope.Order.ServiceImg = element.item.data.photo;
        $scope.Order.ServiceName = element.item.data.header;
        $timeout(function(){
            $location.hash('order');
            $anchorScroll();
        },100);
    }

    $scope.SendOrder = function(){

        $scope.Order.ClearValidationLite();

        if(!$scope.Order.Validation()){
            $scope.Order.ValError = true;
            return;
        }

        var sendData = {
            username : $scope.Order.Name,
            phone : $scope.Order.Phone,
            service: $scope.Order.ServiceName
        };

        if($scope.Order.Person) sendData.peopleNum = $scope.Order.Person;
        if($scope.Order.Promo) sendData.promoCode = $scope.Order.Promo;


        $http.post("/EventCalendar/Invite",sendData).success(function(response){
            if(response == "ok"){
                $scope.Order.Good = true;
            } else{
                $scope.Order.ServerError = true;
            }
        });
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
        return ((this.Name)&&(this.Phone));
    }

    this.ClearValidation = function(){
        this.Show = false;
        this.ValError = false;
        this.ServerError = false;
        this.Good = false;
        this.ShowBtn = true;
    }

    this.ClearValidationLite = function(){
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

    var classes=['clnd-pink','clnd-green','clnd-blue', 'clnd-orange'];
    /* формирование календаря */
    for (var i = 0, k=0; i < l; ++i,k++) {
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
        if(k>3) k=0;
        cal[i].class = classes[k];
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


function CheckTime(num) {
    if (num < 10) {
        return "0" + num;
    }
    return num;
}