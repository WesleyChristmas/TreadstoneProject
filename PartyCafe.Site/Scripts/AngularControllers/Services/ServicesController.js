
var servicesapp = new angular.module("servicesapp", ["ngRoute"]);
servicesapp.directive('jqdatepicker', function () {
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