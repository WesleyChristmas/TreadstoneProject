var photogalleryapp = new angular.module("photogalleryapp", []);

photogalleryapp.controller("PhotoGallery", function ($scope, $http) {
    $scope.PhotoGallery = [];

    GetAllPhoto($scope, $http);
});

function GetAllPhoto($scope, $http) {
    var tem = [
        {
            idRecord: 1, name: 'Главный зал', photoPath: 'http://wp.wwwebinvader.com/Owl/wp-content/uploads/2015/07/car-6902751.jpg'
        },
        {
            idRecord: 2, name: 'VIP 1', photoPath: 'http://wp.wwwebinvader.com/Owl/wp-content/uploads/2015/07/car-854098_19201.jpg'
        },
        {
            idRecord: 3, name: 'VIP 2', photoPath: 'http://wp.wwwebinvader.com/Owl/wp-content/uploads/2015/07/Dogs1.jpeg'
        },
        {
            idRecord: 4, name: 'VIP 3', photoPath: 'http://wp.wwwebinvader.com/Owl/wp-content/uploads/2015/07/Girl-Sitting-on-a-Bridge-ID8738-1920x14401.jpg'
        },
        {
            idRecord: 5, name: 'Летняя веранда', photoPath: 'http://wp.wwwebinvader.com/Owl/wp-content/uploads/2015/07/car-6902751.jpg'
        },
        {
            idRecord: 6, name: 'Летняя веранда 2', photoPath: 'http://wp.wwwebinvader.com/Owl/wp-content/uploads/2015/07/girl-6917121.jpg'
        },
        {
            idRecord: 7, name: 'Главный зал', photoPath: 'http://wp.wwwebinvader.com/Owl/wp-content/uploads/2015/07/car-6902751.jpg'
        },
        {
            idRecord: 8, name: 'VIP 1', photoPath: 'http://wp.wwwebinvader.com/Owl/wp-content/uploads/2015/07/car-854098_19201.jpg'
        },
        {
            idRecord: 9, name: 'VIP 2', photoPath: 'http://wp.wwwebinvader.com/Owl/wp-content/uploads/2015/07/Dogs1.jpeg'
        },
        {
            idRecord: 10, name: 'VIP 3', photoPath: 'http://wp.wwwebinvader.com/Owl/wp-content/uploads/2015/07/Girl-Sitting-on-a-Bridge-ID8738-1920x14401.jpg'
        },
        {
            idRecord: 11, name: 'Летняя веранда', photoPath: 'http://wp.wwwebinvader.com/Owl/wp-content/uploads/2015/07/car-6902751.jpg'
        },
        {
            idRecord: 12, name: 'Летняя веранда 2', photoPath: 'http://wp.wwwebinvader.com/Owl/wp-content/uploads/2015/07/girl-6917121.jpg'
        },
        {
            idRecord: 13, name: 'Главный зал', photoPath: 'http://wp.wwwebinvader.com/Owl/wp-content/uploads/2015/07/car-6902751.jpg'
        },
        {
            idRecord: 14, name: 'VIP 1', photoPath: 'http://wp.wwwebinvader.com/Owl/wp-content/uploads/2015/07/car-854098_19201.jpg'
        },
        {
            idRecord: 15, name: 'VIP 2', photoPath: 'http://wp.wwwebinvader.com/Owl/wp-content/uploads/2015/07/Dogs1.jpeg'
        },
        {
            idRecord: 16, name: 'VIP 3', photoPath: 'http://wp.wwwebinvader.com/Owl/wp-content/uploads/2015/07/Girl-Sitting-on-a-Bridge-ID8738-1920x14401.jpg'
        },
        {
            idRecord: 17, name: 'Летняя веранда', photoPath: 'http://wp.wwwebinvader.com/Owl/wp-content/uploads/2015/07/car-6902751.jpg'
        },
        {
            idRecord: 18, name: 'Летняя веранда 2', photoPath: 'http://wp.wwwebinvader.com/Owl/wp-content/uploads/2015/07/girl-6917121.jpg'
        }
    ];
    $scope.PhotoGallery = tem;
    /*$http.get("/Services/GetAllServices").success(function (data, status) {
        $scope.Services = data;
        $scope.serviceBloks = true;
        console.log(data);
    });*/
}