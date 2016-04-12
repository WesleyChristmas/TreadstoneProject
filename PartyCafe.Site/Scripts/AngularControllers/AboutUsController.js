var aboutusapp = new angular.module("aboutusapp", []);

aboutusapp.controller("AboutUs", function ($scope, $http) {
    $scope.AboutUs = [];
    $scope.AboutusMore = [];
    $scope.More = function (id) { More($scope, id); }
    $scope.Oreder = function () {
        $scope.aboutusOrder = false;
        $scope.aboutusOrderForm = true;
    }

    GetAllAboutUs($scope, $http);
});

function GetAllAboutUs($scope, $http) {
    $scope.aboutusBloks = true;
    $http.get("/AboutUs/GetAllUs").success(function (data, status) {
        $scope.AboutUs = data;
        $scope.serviceBloks = true;
        console.log(data);
    });
}

function More($scope, id) {
    $scope.aboutusMore = true;
    $scope.aboutusOrder = true;
    $scope.aboutusBloks = false;
    $scope.aboutusMore = $scope.AboutUs[id];
}