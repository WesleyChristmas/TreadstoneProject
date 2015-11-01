var foodMenuApp = new angular.module("FoodMenuAdminApp", ['ngRoute']);

foodMenuApp.config(function($routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: 'FoodMenu/MenuSections',
            controller: 'FoodMenuSectionController'
        })
        .when('/EditMenuSection/:idtype', {
            templateUrl: 'FoodMenu/EditMenuSection',
            controller: 'FoodMenuSectionEditController'
        });
});

foodMenuApp.service("sharedDataService", function() {
    this.MenuTypes = [];

    this.setMenuTypes = function(types) {
        this.MenuTypes = types;
    }

    this.getMenuTypes = function() {
        return this.MenuTypes;
    }
});

foodMenuApp.controller("FoodMenuAdminController", function ($scope, $http) {
    
});

foodMenuApp.controller("FoodMenuSectionController", function($scope, $http,$location,sharedDataService) {
    $scope.Base = new BaseClass($scope, $http, sharedDataService);
    MenuTypesClass.prototype = $scope.Base;
    $scope.MenuTypes = new MenuTypesClass();

    $scope.MenuTypes.GetMenuSections();

    //Methods
    $scope.DeleteMenuType = function (obj) {
        $http.post('FoodMenu/DeleteMenuType', { 'idType': obj.item.IdRecord }).success(function (response) {
            $scope.MenuTypes.GetMenuSections();
        });
    }

    $scope.AddMenuType = function() {
        sharedDataService.setMenuTypes(null);
        $location.path('EditMenuSection/0');
    }
});

foodMenuApp.controller("FoodMenuSectionEditController", function($scope, $http,$routeParams,$location,sharedDataService) {
    var types = sharedDataService.getMenuTypes();
    $scope.isCreate = false;

    if ((types == null) && ($routeParams.idtype == 0)) {
        $scope.MenuType = new EditMenuTypesClass(null);
        $scope.isCreate = true;
    }

    for (var i in types) {
        if ($routeParams.idtype == types[i].IdRecord) {
            $scope.MenuType = new EditMenuTypesClass(types[i]);
            break;
        }
    }

    //Methods
    $scope.SaveChanges = function () {
        var url;
        var xhr = new XMLHttpRequest();
        var fd = new FormData();

        if ($scope.isCreate) {
            url = 'FoodMenu/AddMenuType';
        } else {
            url = 'FoodMenu/UpdateMenuType';
            fd.append('IdRecord', $scope.MenuType.IdRecord);
        }

        if ($scope.MenuType.Image!=null) {
            fd.append('file', $scope.MenuType.Image);
            fd.append('filename', $scope.MenuType.Image.name);
        }

        fd.append('name', $scope.MenuType.Name);
        fd.append('description', $scope.MenuType.Description);

        xhr.upload.onload = function() {
            $scope.$apply($location.path('/'));
        }

        xhr.open('POST', url, true);
        xhr.send(fd);
    }

    $scope.DeleteMenuType = function () {
        $http.post('FoodMenu/DeleteMenuType', { 'idType': $scope.MenuType.IdRecord }).success(function (response) {
            $location.path('/');
        });
    }

    $scope.AddFile = function(file) {
        $scope.MenuType.Image = file[0];
    }
});