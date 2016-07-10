var foodmenuapp = new angular.module("foodmenuapp", []);

foodmenuapp.controller("FoodMenuCt", function ($scope, $http) {
    $scope.Food = [];
    $scope.FoodSub = [];
    $scope.title = 'Наше меню в кафе';
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

    GetAllMenu($scope, $http);
});

foodmenuapp.filter('pricefilter', function () {
    return function (price) {
        return parseInt(price);
    };
});
foodmenuapp.filter('photofilter', function () {
    return function (photo) {
        var xhr = new XMLHttpRequest();

        xhr.open('HEAD', photo, false);
        xhr.send();

        return xhr.status != 404 ? photo : '';
    };
});

function GetAllMenu($scope, $http) {
    $http.get("/FoodMenu/GetAllMenu").success(function (data, status) {
        $scope.Food = data;
        console.log(data);
        $scope.ShowSubMenu(0, data[0]);
        $scope.ShowSubMenuItem(4, data[0].subGroups[4]);
    });
}