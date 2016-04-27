var galleryapp = new angular.module("GalleryApp", ['ngRoute']);

galleryapp.config(function ($routeProvider) {
    $routeProvider.when('/',
    {
        templateUrl: 'Gallery/GalleryHome',
        controller: 'GalleryHomeController'
    }).when('/add', {
        templateUrl: 'Gallery/GalleryAdd',
        controller: 'GalleryAddController'
    }).when('/edit', {
        templateUrl: 'Gallery/GalleryEdit',
        controller: 'GalleryEditController'
    }).otherwise('/');
});

galleryapp.service("sharedDataService", function () {
    this.rolesList = [];
    this.rolesItem = {};

    this.setGallery = function (obj) { this.rolesList = obj; }
    this.getGallery = function () { return this.rolesList; }

    this.setItem = function (item) { this.rolesItem = item; }
    this.getItem = function () { return this.rolesItem; }
});

/* Gallery Home Controller */
galleryapp.controller("GalleryHomeController", function ($scope, $http, $location, sharedDataService) {
    /*Helpers*/
    $scope.isActive = function (item) { return $scope.selectForEdit === item; };
    $scope.HighlightItem = function (item) { $scope.selectForEdit = item; };
    $scope.AddGallery = function () { $location.path('/add'); };
    $scope.EditGallery = function () {
        sharedDataService.setItem($scope.selectForEdit);
        $location.path('/edit');
    };

    $scope.Gallery = [];
    $scope.Header = "Управление фотогалереей";
    $scope.selectForEdit = '';

    GetAllGallery($scope, $http);
});

/* Gallery Add Controller */
galleryapp.controller("GalleryAddController", function ($scope, $http, $location) {
    /*Helpers*/
    $scope.Header = "Добавление фото в галлерею";
    $scope.Back = function () { $location.path('/'); }
    $scope.addGallery = function () {
        var fd = new FormData();
        fd.append('name', $scope.galleryAdd.Name);
        fd.append('description', $scope.galleryAdd.Desc);
        fd.append('file', document.getElementsByName('galleryPhoto')[0].files[0]);

        $http.post('Gallery/AddGallery', fd, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).success(function () {
            if (response === 'ok') {
                $location.path('/');
            } else {
                $scope.error = response;
            }
        });
    };

});

/* Gallery Edit Controller */
galleryapp.controller("GalleryEditController", function ($scope, $http, $location, $routeParams, sharedDataService) {
    /*Helpers*/
    $scope.Header = "Редактирование новости";
    $scope.itemForEdit = sharedDataService.getItem();
    $('#summernote').summernote('code', $scope.itemForEdit.Body);

    $scope.updateGallery = function () {
        if (confirm("Сохранить изменения?")) {
            $http.post('Gallery/UpdateGallery', {
                id: $scope.itemForEdit.RecordId,
                title: $scope.itemForEdit.Title,
                body: $('#summernote').summernote('code')
            }).success(function (response) {
                if (response == 'ok') {
                    $location.path('/');
                } else {
                    $scope.error = response;
                }
            });
        }
    };
    $scope.BackToGalleryList = function () {
        $location.path('/');
    };
});

function GetAllGallery($scope, $http) {
    $('#loader').css({ "display": "block" });
    $('.spinner').show();

    $http.get('Gallery/GetAllGallery').success(function (result) {
        $scope.Gallery = result;
    });

    $('#loader').css({ "display": "none" });
    $('.spinner').hide();
}
function uploadFile($scope, $http, obj) {
    var xhr = new XMLHttpRequest(),
        fd = new FormData();

    fd.append(0, obj);

    xhr.onreadystatechange = function () {
        if (xhr.status != 200) {
            $('.summernote').summernote('insertImage', xhr.response);
        }
    }

    xhr.open('POST', 'Gallery/UploadFile', true);
    xhr.send(fd);
    /*
        $http.post('Gallery/UploadFile', { file: fd }).success(function (respond) {
            $('.summernote').summernote('insertImage', respond);
        });*/
}