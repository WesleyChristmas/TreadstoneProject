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
        }
    ];
    $scope.AboutUs = tem;
    $scope.aboutusBloks = true;
    /*$http.get("/Services/GetAllServices").success(function (data, status) {
        $scope.Services = data;
        $scope.serviceBloks = true;
        console.log(data);
    });*/
}

function More($scope, id) {
    $scope.aboutusMore = true;
    $scope.aboutusOrder = true;
    $scope.aboutusBloks = false;
    $scope.aboutusMore = $scope.AboutUs[id];
}