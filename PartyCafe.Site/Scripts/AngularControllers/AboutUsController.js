var aboutusapp = new angular.module("aboutusapp", ['ngRoute']);

aboutusapp.config(function ($routeProvider) {
    $routeProvider.when('/',
    {
        templateUrl: 'AboutUs/Home',
        controller: 'HomeController'
    }).when('/current', {
        templateUrl: 'AboutUs/Current',
        controller: 'CurrentController'
    }).otherwise('/');
});

aboutusapp.service("sharedDataService", function () {
    this.eventsItem = {};

    this.setItem = function (item) { this.eventsItem = item; }
    this.getItem = function () { return this.eventsItem; }
});


/* Calendar Controller */
aboutusapp.controller("HomeController", function ($scope, $http, $location, sharedDataService) {
    /*Helpers*/
    $scope.AboutUs = [];
    $scope.More = function (id) {
        sharedDataService.setItem($scope.AboutUs[id]);
        $location.path('/current');
    };

    $http.get("/AboutUs/GetAllUs").success(function (data) {
        $scope.AboutUs = data;
    });
});

/* Current Controller */
aboutusapp.controller("CurrentController", function ($scope, $http, $location, sharedDataService) {
    $scope.aboutusMore = sharedDataService.getItem();

    $scope.description = $scope.aboutusMore.description;

    lightbox.option({
        'alwaysShowNavOnTouchDevices': true,
        'resizeDuration': 0,
        'wrapAround': true,
        'disableScrolling': true,
    });
});
