var photogalleryapp = new angular.module("photogalleryapp", []);

photogalleryapp.controller("PhotoGallery", function ($scope, $http,$timeout) {
    $scope.PhotoGallery = [];
    $scope.Paging = new Paging();

    $scope.GetAllPhoto = function(){
        $http.get("PhotoGallery/GetAllPhotos").success(function (data, status) {
            $scope.PhotoGallery = data;
        });
    };

    $scope.GetPhotoByTag = function(){
        $http.get("PhotoGallery/GetAllByTags?tag=" + $scope.Paging.Tag).success(function(response){
            $scope.PhotoGallery = response;
        })
    }

    $scope.StartTagProcess = function(){
        if($scope.Paging.Timeout != null) $timeout.cancel($scope.Paging.Timeout);
        if($scope.Paging.Tag){
            $scope.Paging.Timeout = $timeout(function(){
                $scope.GetPhotoByTag();
            },1500);
        } else{
            $scope.Paging.Timeout = $timeout(function(){
                $scope.GetAllPhoto();
            },1500);
        }
    }

    //Constructor

    $scope.GetAllPhoto($scope, $http);
});

function Paging(){
    this.CurPos = 1;
    this.PageStep = 20;
    this.Tag = "";
    this.Timeout = null;
}