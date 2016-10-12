var galleryApp = new angular.module("GalleryApp",[]);

galleryApp.controller("GalleryController", function ($scope, $http,$timeout) {
    $scope.PhotoGallery = [];
    $scope.Hashtags = [];
    $scope.Paging = new Paging();

    $scope.GetAllPhoto = function(pos,step){
        $http.get("Gallery/GetAllGallery?startPos=" + pos + "&count=" + step).success(function (data, status) {
            $scope.PhotoGallery = $scope.PhotoGallery.concat(data);
            if(data.length < $scope.Paging.PageStep) $scope.Paging.ShowMore = false;
            else $scope.Paging.ShowMore = true;
        });
    };

    $scope.GetPhotoByTag = function(pos,step){
        $http.get("Gallery/GetAllGalleryByTags?tag=" + $scope.Paging.Tag + "&startPos=" + pos + "&count=" + step).success(function(response){
            $scope.PhotoGallery = $scope.PhotoGallery.concat(response);
            if(response.length < $scope.Paging.PageStep) $scope.Paging.ShowMore = false;
            else $scope.Paging.ShowMore = true;
        })
    }

    $scope.StartTagProcess = function(){
        $scope.Paging.CurPos = 0;
        $scope.PhotoGallery = [];
        if($scope.Paging.Timeout != null) $timeout.cancel($scope.Paging.Timeout);
        if($scope.Paging.Tag){
            $scope.Paging.Timeout = $timeout(function(){
                $scope.GetPhotoByTag($scope.Paging.CurPos,$scope.Paging.PageStep);
            },500);
        } else{
            $scope.Paging.Timeout = $timeout(function(){
                $scope.GetAllPhoto($scope.Paging.CurPos,$scope.Paging.PageStep);
            },500);
        }
    }

    $scope.SelectTag = function(element)
    {
        $scope.Paging.Tag = element.tag.Tag;
        $scope.StartTagProcess();
    }

    $scope.MorePhoto = function(){
        $scope.Paging.CurPos += $scope.Paging.PageStep;

        if($scope.Paging.Tag){
            $scope.GetPhotoByTag($scope.Paging.CurPos,$scope.Paging.PageStep);
        } else{
            $scope.GetAllPhoto($scope.Paging.CurPos,$scope.Paging.PageStep);
        }
    }

    //Constructor

    $scope.GetAllPhoto($scope.Paging.CurPos,$scope.Paging.PageStep);

    $http.get("Gallery/GellAllHashtags").success(function(response){
        $scope.Hashtags = response;
    });
});

function Paging(){
    this.CurPos = 0;
    this.PageStep = 18;
    this.Tag = "";
    this.Timeout = null;
    this.ShowMore = true;
}