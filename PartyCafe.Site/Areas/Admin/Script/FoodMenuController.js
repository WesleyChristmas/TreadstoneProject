var foodmenuapp = new angular.module("FoodMenuApp", ['ngRoute']);

foodmenuapp.config(function ($routeProvider) {
    $routeProvider.when('/',
    {
        templateUrl: 'FoodMenu/FoodMenuHome',
        controller: 'FoodMenuHomeController'
    }).otherwise('/');
});

foodmenuapp.service("sharedDataService", function () {
    this.rolesItem = {};

    this.setItem = function (item) { this.rolesItem = item; }
    this.getItem = function () { return this.rolesItem; }
});

/* FoodMenu Home Controller */
foodmenuapp.controller("FoodMenuHomeController", function ($scope, $http, $location, sharedDataService) {
    /*Helpers*/
    $scope.isActive = function (item) { return $scope.selectForEdit === item; };
    $scope.HighlightItem = function (item) { $scope.selectForEdit = item; };
    $scope.addFoodMenu = function () { $location.path('/add'); };
    $scope.editFoodMenu = function () {
        sharedDataService.setItem($scope.selectForEdit);
        $location.path('/edit');
    };
    $scope.removeFoodMenu = function (item) {
        $http.post('FoodMenu/RemoveFoodMenuEvent', {
            id: item.idRecord
        }).success(function (response) {
            if (response === 'ok') {
                $location.path('/');
            } else {
                $scope.error = response;
            }
        });
    };

    $scope.FoodMenu = [];
    $scope.Header = "Управление меню кафе";
    $scope.selectForEdit = '';

    /* Main */
    $scope.addMain = function () { };
    $scope.getSubmenu = function (item) {
        $scope.SubMenu = item;
        if (item === undefined) {
            $scope.SubMenuEdit = false;
        } else {
            $scope.SubMenuEdit = item.length > 0 ? true : false;
        }
    };

    GetAllFoodMenu($scope, $http);
});

/* FoodMenu Add Controller */
foodmenuapp.controller("FoodMenuAddController", function ($scope, $http, $location) {
    $scope.Header = "Добавление мероприятия в календарь событий";
    $scope.Back = function () { $location.path('/'); }
    $scope.addEvent = function () {
        var fd = new FormData();
        fd.append('name', $scope.eventsAdd.Name);
        fd.append('description', $scope.eventsAdd.Desc);
        fd.append('date', $scope.eventsAdd.Date);
        fd.append('time', $scope.eventsAdd.Time);
        fd.append('file', document.getElementsByName('eventsPhoto')[0].files[0]);

        $http.post('FoodMenu/AddFoodMenuEvent', fd, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).success(function (response) {
            if (response === 'ok') {
                $location.path('/');
            } else {
                $scope.error = response;
            }
        });
    };
});

/* FoodMenu Edit Controller */
foodmenuapp.controller("FoodMenuEditController", function ($scope, $http, $location, $routeParams, sharedDataService) {
    $scope.Header = "Редактирование мероприятия";
    $scope.itemForEdit = sharedDataService.getItem();

    $scope.itemForEdit.DateEvent = parseDate($scope.itemForEdit.DateEvent);
    $scope.itemForEdit.TimeEvent = parseTime($scope.itemForEdit.TimeEvent);

    $scope.updateEvent = function () {
        var fd = new FormData();
        fd.append('id', $scope.itemForEdit.idRecord);
        fd.append('name', $scope.itemForEdit.name);
        fd.append('desc', $scope.itemForEdit.Description);
        fd.append('date', $scope.itemForEdit.DateEvent);
        fd.append('time', $scope.itemForEdit.TimeEvent);
        fd.append('file', document.getElementsByName('eventsPhoto')[0].files[0]);

        $http.post('FoodMenu/UpdateFoodMenuEvent', fd, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).success(function (response) {
            if (response === 'ok') {
                $location.path('/');
            } else {
                $scope.error = response;
            }
        });
    };
    $scope.BackToFoodMenuList = function () {
        $location.path('/');
    };
});

function GetAllFoodMenu($scope, $http) {
    $('#loader').css({ "display": "block" });
    $('.spinner').show();

    $http.get('FoodMenu/GetAllMenu').success(function (result) {
        $scope.FoodMenu = result;
    });

    $('#loader').css({ "display": "none" });
    $('.spinner').hide();
}