var servicesapp = new angular.module("servicesapp", []);

servicesapp.controller("ServicesCt", function ($scope, $http) {
    $scope.Services = [];
    $scope.FoodSub = [];
    $scope.selectedItem = '';
    $scope.selectedLast = 0;
    $scope.ShowSubMenu = function (id, obj) {
        $scope.selected = true;
        $scope.FoodSub = obj.subGroups;
        if ($('.barMenuLeft').hasClass('show')) {
            $('.barMenuLeft').removeClass('show');
        }
    }
    $scope.ShowSubMenuItem = function (id, obj) {
        if ($('.accordion').css('display') == 'block') {
            $scope.FoodSubItem = obj.items;

            if ($scope.selectedLast == id) {
                if ($scope.selectedItems) {
                    $scope.selectedItems = false;
                    $('.accordion-section-content').slideUp(0).removeClass('open');
                } else {
                    $scope.selectedItems = true;
                    $('.accordion-section-content').slideUp(0).removeClass('open');
                    $('#accordion-' + id).slideDown(0).addClass('open');
                }
            } else {
                $('.accordion-section-content').slideUp(0).removeClass('open');
                $('#accordion-' + id).slideDown(0).addClass('open');

                $scope.selectedItem = id;
                $scope.selectedItems = true;
                $scope.FoodSubItem = obj.items;
            }
        } else {
            $scope.selectedItem = id;
            $scope.selectedItem = true;
            $scope.FoodSubItem = obj.items;
        }
        $scope.selectedLast = id;
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
        console.log(data);
    });
}
