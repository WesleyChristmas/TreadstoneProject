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
        if ($scope.isCreate) {
            $http.post('FoodMenu/AddMenuType', { 'type': $scope.MenuType, 'File': $scope.MenuType.Image }).success(function (response) {
                $location.path('/');
            });
        } else {
            $http.post('FoodMenu/UpdateMenuType', { 'type': $scope.MenuType, 'File': $scope.MenuType.Image }).success(function (response) {
                $location.path('/');
            });
        }
    }

    $scope.DeleteMenuType = function () {
        $http.post('FoodMenu/DeleteMenuType', { 'idType': $scope.MenuType.IdRecord }).success(function (response) {
            $location.path('/');
        });
    }
});