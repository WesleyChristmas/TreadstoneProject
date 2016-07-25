﻿var foodmenuapp = new angular.module("FoodMenuApp", ['ngRoute']);

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
    $scope.getModel = function (item) {
        if (item.Platform === null) {
            return item.Weight;
        } else {
            return item.Platform;
        }
        //return item.Platform === null ? item.Weight : item.Platform;
    };

    $scope.FoodMenu = [];
    $scope.SubMenu = [];
    $scope.SubMenuItems = [];

    $scope.selectedmain = 0;
    $scope.selectedsub = 0;

    $scope.Header = "Управление меню кафе";
    $scope.selectForEdit = '';
    $scope.editmainitem = false;

    /* Main */
    $scope.addMainItem = function (itemName) {
        $http.post('FoodMenu/AddGroupItem', {
            name: itemName
        }).success(function (response) {
            if (response === 'ok') {
                $scope.mainsection.name = "";
                GetAllFoodMenu($scope, $http);
            } else {
                $scope.error = response;
            }
        });
    };
    $scope.editMainItem = function (itemName, item) {
        $http.post('FoodMenu/EditGroupItem', {
            name: itemName,
            id: item.idRecord
        }).success(function (response) {
            if (response === 'ok') {
                $scope.selectForEdit = '';
                $scope.selectedmain = 0;
                GetAllFoodMenu($scope, $http);
            } else {
                $scope.error = response;
            }
        });
    };
    $scope.removeMainItem = function (item) {
        $http.post('FoodMenu/RemoveGroupItem', {
            id: item.idRecord
        }).success(function (response) {
            if (response === 'ok') {
                GetAllFoodMenu($scope, $http);
            } else {
                $scope.error = response;
            }
        });
    };

    $scope.getSubmenu = function (item) {
        if (item === undefined) {
            $scope.SubMenuEdit = false;
            $scope.SubMenu = [];
            $scope.selectedmain = 0;
        } else {
            $scope.SubMenuEdit = item.subGroups.length >= 0 ? true : false;
            $scope.SubMenu = item.subGroups;
            $scope.selectedmain = item.idRecord;
        }
    };
    $scope.getSubmenuItems = function (item) {
        if (item == undefined) {
            $scope.SubMenuItems = [];
            $scope.selectedsub = 0;
        } else {
            $scope.SubMenuItemsEdit = item.subGroups.length > 0 ? true : false;
            $scope.SubMenuItems = item.items;
            $scope.selectedsub = item.idRecord;
        }
    };

    /* Submenu item */
    $scope.addSubItem = function (itemName) {
        if ($scope.selectedmain !== 0) {
            $http.post('FoodMenu/AddGroupItem', {
                name: itemName,
                parentId: $scope.selectedmain
            }).success(function (response) {
                if (response === 'ok') {
                    $scope.subsection.name = '';
                    GetAllFoodMenu($scope, $http);
                } else {
                    $scope.error = response;
                }
            });
        }
    };
    $scope.removeSubItem = function (item) {
        $http.post('FoodMenu/RemoveGroupItem', {
            id: item.idRecord
        }).success(function (response) {
            if (response === 'ok') {
                GetAllFoodMenu($scope, $http);
            } else {
                $scope.error = response;
            }
        });
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