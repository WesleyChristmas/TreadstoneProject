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
    $scope.More = function (id) { More($scope, id); }
    $scope.Oreder = function () {
        $scope.serviceOrder = false;
        $scope.serviceOrderForm = true;
    }

    GetAllServices($scope, $http);
});

function GetAllServices($scope, $http) {
    /*var tem = [
        {
            idRecord: 1, name: 'Кафе', photoPath: 'http://wp.wwwebinvader.com/Owl/wp-content/uploads/2015/07/car-6902751.jpg'
        },
        {
            idRecord: 2, name: 'Банкеты', photoPath: 'http://wp.wwwebinvader.com/Owl/wp-content/uploads/2015/07/car-854098_19201.jpg'
        },
        {
            idRecord: 3, name: 'Свадьбы', photoPath: 'http://wp.wwwebinvader.com/Owl/wp-content/uploads/2015/07/Dogs1.jpeg'
        },
        {
            idRecord: 4, name: 'День рождения', photoPath: 'http://wp.wwwebinvader.com/Owl/wp-content/uploads/2015/07/Girl-Sitting-on-a-Bridge-ID8738-1920x14401.jpg'
        },
        {
            idRecord: 5, name: 'Корпаоративы', photoPath: 'http://wp.wwwebinvader.com/Owl/wp-content/uploads/2015/07/car-6902751.jpg'
        },
        {
            idRecord: 6, name: 'Семинары', photoPath: 'http://wp.wwwebinvader.com/Owl/wp-content/uploads/2015/07/girl-6917121.jpg'
        },
        {
            idRecord: 7, name: 'Тренинги', photoPath: 'http://wp.wwwebinvader.com/Owl/wp-content/uploads/2015/07/Dogs1.jpeg'
        },
        {
            idRecord: 8, name: 'Выступления', photoPath: 'http://wp.wwwebinvader.com/Owl/wp-content/uploads/2015/07/Girl-Sitting-on-a-Bridge-ID8738-1920x14401.jpg'
        },
        {
            idRecord: 9, name: 'Вечеринки', photoPath: 'http://wp.wwwebinvader.com/Owl/wp-content/uploads/2015/07/car-854098_19201.jpg'
        },
        {
            idRecord: 10, name: 'Мальчишники/Девишники', photoPath: 'http://wp.wwwebinvader.com/Owl/wp-content/uploads/2015/07/Dogs1.jpeg'
        },
        {
            idRecord: 11, name: 'Барбекю', photoPath: 'http://wp.wwwebinvader.com/Owl/wp-content/uploads/2015/07/Girl-Sitting-on-a-Bridge-ID8738-1920x14401.jpg'
        },
        {
            idRecord: 12, name: 'Аренда VIP комнат', photoPath: 'http://wp.wwwebinvader.com/Owl/wp-content/uploads/2015/07/girl-6917121.jpg'
        }
    ];*/

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
    console.log($scope.ServicesMore);
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