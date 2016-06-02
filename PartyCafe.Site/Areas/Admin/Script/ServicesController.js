var servicesapp = new angular.module("ServicesApp", ['ngRoute']);

servicesapp.config(function ($routeProvider) {
    $routeProvider.when('/',
    {
        templateUrl: 'Services/ServicesHome',
        controller: 'ServicesHomeController'
    }).when('/add', {
        templateUrl: 'Services/ServicesAdd',
        controller: 'ServicesAddController'
    }).when('/edit', {
        templateUrl: 'Services/ServicesEdit',
        controller: 'ServicesEditController'
    }).when('/editphoto', {
        templateUrl: 'Services/ServicesEditPhoto',
        controller: 'ServicesEditPhotoController'
    }).otherwise('/');
});

servicesapp.service("sharedDataService", function () {
    this.rolesItem = {};
    this.setItem = function (item) { this.rolesItem = item; }
    this.getItem = function () { return this.rolesItem; }
});

/* Services Home Controller */
servicesapp.controller("ServicesHomeController", function ($scope, $http, $location, sharedDataService) {
    $scope.isActive = function (item) { return $scope.selectForEdit === item; };
    $scope.HighlightItem = function (item) { $scope.selectForEdit = item; };

    $scope.addServices = function () { $location.path('/add'); };
    $scope.editServices = function () {
        sharedDataService.setItem($scope.selectForEdit);
        $location.path('/edit');
    };
    $scope.editServicesPhoto = function () {
        sharedDataService.setItem($scope.selectForEdit);
        $location.path('/editphoto');
    };
    $scope.removeServices = function (item) {
        $http.post('Services/RemoveServices', {
            id: item.idRecord
        }).success(function (response) {
            if (response === 'ok') {
                $location.path('/');
            } else {
                $scope.error = response;
            }
        });
    };

    $scope.Services = [];
    $scope.Header = 'Управление разделом "Услуги"';
    $scope.selectForEdit = '';

    GetAllServices($scope, $http);
});
/* Services Add Controller */
servicesapp.controller("ServicesAddController", function ($scope, $http, $location, $routeParams, sharedDataService) {
    $scope.Header = "Добавление услуги";
    $scope.addServices = function () {
        var fd = new FormData();
        fd.append('name', $scope.servicesAdd.Name);
        fd.append('desc', $scope.servicesAdd.Desc);
        fd.append('file', document.getElementsByName('servicesPhoto')[0].files[0]);

        $http.post('Services/AddServices', fd, {
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
    $scope.Back = function () { $location.path('/'); }
});
/* Services Edit Controller */
servicesapp.controller("ServicesEditController", function ($scope, $http, $location, $routeParams, sharedDataService) {
    $scope.itemForEdit = sharedDataService.getItem();
    $scope.Header = "Редактирование услуги - " + $scope.itemForEdit.name;
    $scope.CurrentPhotoShow = true;

    $scope.updateServices = function () {
        var fd = new FormData();
        fd.append('id', $scope.itemForEdit.idRecord);
        fd.append('name', $scope.itemForEdit.name);
        fd.append('desc', $scope.itemForEdit.description);
        fd.append('oldphoto', $scope.itemForEdit.photoPath);
        fd.append('file', document.getElementsByName('servicesPhoto')[0].files[0]);

        $http.post('Services/UpdateServices', fd, {
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
    $scope.changePhoto = function () {
        $scope.CurrentPhotoShow = false;
        $scope.ChangePhotoShow = true;
    };
    $scope.Back = function () { $location.path('/'); };
});
/* Services Edit Photo Controller */
servicesapp.controller("ServicesEditPhotoController", function ($scope, $http, $location, $routeParams, sharedDataService) {
    $scope.BlockPhotos = sharedDataService.getItem();
    $scope.Header = "Редактирование фотографий услуги - " + $scope.BlockPhotos.name;
    $scope.CurrentPhotoShow = true;

    $scope.updatePhotoName = function (id) {
        var name = document.getElementsByTagName('textarea')[id];
        $http.post('AboutUs/UpdatePhotoServices', {
            id: $scope.BlockPhotos.photos[id].idRecord,
            name: name.value
        }).success(function (response) {
            if (response === 'ok') {
                $http.post('AboutUs/GetServicesPhotos', {
                    id: $scope.BlockPhotos.idRecord
                }).success(function (response) {
                    $scope.BlockPhotos = response;
                });
            } else {
                $scope.error = response;
            }
        });
    };

    $scope.removePhoto = function (id) {
        $http.post('Services/RemovePhotoFromServices', {
            id: $scope.BlockPhotos.photos[id].idRecord
        }).success(function (response) {
            if (response === 'ok') {
                $http.post('Services/GetServicesPhotos', {
                    id: $scope.BlockPhotos.idRecord
                }).success(function (response) {
                    $scope.BlockPhotos = response;
                });
            } else {
                $scope.error = response;
            }
        });
    };
    $scope.addPhoto = function () {
        var fd = new FormData();
        fd.append('id', $scope.BlockPhotos.idRecord);
        fd.append('name', $scope.servicesPhotoAdd.Name);
        fd.append('desc', $scope.servicesPhotoAdd.Desc);
        fd.append('file', document.getElementsByName('servicesPhoto')[0].files[0]);

        $http.post('Services/AddPhotoToServices', fd, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).success(function (response) {
            if (response === 'ok') {
                $scope.servicesPhotoAdd.Name = '';
                $scope.servicesPhotoAdd.Desc = '';
                document.getElementsByName('servicesPhoto').value = '';

                $http.post('Services/GetServicesPhotos', {
                    id: $scope.BlockPhotos.idRecord
                }).success(function (response) {
                    $scope.BlockPhotos = response;
                });
            } else {
                $scope.error = response;
            }
        });
    };

    $scope.changePhoto = function () {
        $scope.CurrentPhotoShow = false;
        $scope.ChangePhotoShow = true;
    };
    $scope.Back = function () { $location.path('/'); };
});

function GetAllServices($scope, $http) {
    $('#loader').css({ "display": "block" });
    $('.spinner').show();

    $http.get('Services/GetAllServices').success(function (result) {
        $scope.Services = result;
    });

    $('#loader').css({ "display": "none" });
    $('.spinner').hide();
}