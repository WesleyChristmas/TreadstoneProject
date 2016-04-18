var servicesapp = new angular.module("servicesapp", []);
servicesapp.directive('jqdatepicker', function () {
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

servicesapp.controller("ServicesCt", function ($scope, $http) {
    $scope.Services = [];
    $scope.ServicesMore = [];
    $scope.services = {};
    $scope.More = function (id) { More($scope, id); }
    $scope.Oreder = function () {
        $scope.serviceOrder = false;
        $scope.serviceOrderForm = true;
        $(".services-wrapper").animate({ scrollTop: $('.services-wrapper').prop("scrollHeight") }, 500);
    }
    $scope.formOrder = function () { ServiceNewOrder($scope, $http); }

    GetAllServices($scope, $http);
});

function GetAllServices($scope, $http) {
    $http.get("/Services/GetAllServices").success(function (data, status) {
        $scope.Services = data;
        $scope.serviceBloks = true;
        console.log(data);
    });
}

function More($scope, id) {
    $scope.serviceMore = true;
    $scope.serviceOrder = true;
    $scope.serviceBloks = false;
    $scope.ServicesMore = $scope.Services[id];
    $scope.services.Service = console.log($scope.Services[id].name);

        lightbox.option({
            'alwaysShowNavOnTouchDevices': true,
            'resizeDuration': 0,
            'wrapAround': true,
            'disableScrolling': true,
        });
}

function ServiceNewOrder($scope, $http) {
    var newSO = {
        user: $scope.services.User,
        phone: $scope.services.PhoneNumber,
        date: $scope.services.Date,
        time: $scope.services.Time,
        service: $scope.services.Service
    };

    $http.post("Services/NewOrder",
        {
            user: newSO.user,
            phone: newSO.phone,
            date: newSO.date,
            time: newSO.time,
            service: newSO.service
        }).success(function (data) {
            if (data == 'good') {
                $scope.formResult = true;
                $scope.result = "Спасибо за заказ! В ближайшее время наш менеджер с вами свяжется.";
                $scope.serviceOrderForm = false;
                setTimeout(function () {
                    $scope.$apply(function () {
                        $scope.serviceOrder = true;
                        $scope.formResult = false;
                    });
                }, 3500);
            } else {
                $scope.result = "Что-то пошло не так. Попробуйте еще раз или позвоните на наш контактный номер.";
            }
    });
}

/* Helpers */
function SetTime(obj) {
    var v = obj.value;
    if (v.length > 2 && v.length <= 5) {
        if (v.indexOf(":") === -1) {
            var fval = v.substr(0, 2),
                sval = v.substr(2, 2);
            obj.value = fval + ":" + sval;
            return obj.value;
        } else {
            if (obj.value.length > 5) {
                return obj.substr(-1);
            } else {
                return obj.value;
            }
        }
    } else {
        v = v.replace(/[^0-9:]+/g, '');
        return v;
    }
}