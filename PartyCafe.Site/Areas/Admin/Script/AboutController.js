var aboutapp = new angular.module("AboutApp", ['ngRoute']);

aboutapp.config(function ($routeProvider) {
    $routeProvider.when('/',
    {
        templateUrl: 'AboutUs/AboutHome',
        controller: 'AboutHomeController'
    }).when('/add', {
        templateUrl: 'AboutUs/AboutAdd',
        controller: 'AboutAddController'
    }).when('/edit', {
        templateUrl: 'AboutUs/AboutEdit',
        controller: 'AboutEditController'
    }).otherwise('/');
});

aboutapp.service("sharedDataService", function () {
    this.rolesItem = {};

    this.setItem = function (item) { this.rolesItem = item; }
    this.getItem = function () { return this.rolesItem; }
});

/* About Home Controller */
aboutapp.controller("AboutHomeController", function ($scope, $http, $location, sharedDataService) {
    $scope.isActive = function (item) { return $scope.selectForEdit === item; };
    $scope.HighlightItem = function (item) { $scope.selectForEdit = item; };
    $scope.AddAbout = function () { $location.path('/add'); };
    $scope.EditAbout = function () {
        sharedDataService.setItem($scope.selectForEdit);
        $location.path('/edit');
    };
    $scope.removeAboutItem = function (item) {
        $http.post('AboutUs/DeleteAboutItem', { id: item.idRecord }).success(function (response) {
            if (response === 'ok') {
                $location.path('/');
            } else {
                $scope.error = response;
            }
        });
    };

    $scope.About = [];
    $scope.Header = "Управление фотогалереей";
    $scope.selectForEdit = '';

    GetAllAbout($scope, $http);
});

/* About Edit Controller */
aboutapp.controller("AboutEditController", function ($scope, $http, $location, $routeParams, sharedDataService) {
    $scope.Header = "Редактирование блока";
    $scope.itemForEdit = sharedDataService.getItem();

    $scope.updateAbout = function () {
        var fd = new FormData();
        fd.append('id', $scope.itemForEdit.idRecord);
        fd.append('name', $scope.itemForEdit.name);
        fd.append('desc', $scope.itemForEdit.description);
        fd.append('file', document.getElementsByName('galleryPhoto')[0].files[0]);

        $http.post('About/UpdateAbout', fd, {
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
    $scope.BackToAboutList = function () {
        $location.path('/');
    };
});


function GetAllAbout($scope, $http) {
    $('#loader').css({ "display": "block" });
    $('.spinner').show();

    $http.get('AboutUs/GetAllAbout').success(function (result) {
        $scope.About = result;
    });

    $('#loader').css({ "display": "none" });
    $('.spinner').hide();
}