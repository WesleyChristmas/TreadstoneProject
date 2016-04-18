var photogalleryapp = new angular.module("photogalleryapp", []);

photogalleryapp.controller("PhotoGallery", function ($scope, $http) {
    $scope.PhotoGallery = [];

    GetAllPhoto($scope, $http);
});

function GetAllPhoto($scope, $http) {
    $http.get("PhotoGallery/GetAllPhotos").success(function (data, status) {
        $scope.PhotoGallery = data;
    });
}