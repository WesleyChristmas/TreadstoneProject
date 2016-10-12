var galleryApp = new angular.module("GalleryApp",[]);

galleryApp.controller("GalleryController", function ($scope, $http,$timeout) {
    $scope.PhotoGallery = [];
    $scope.Hashtags = [];
    $scope.Paging = new Paging();
    $scope.NewPhoto = {};
    $scope.NewPhoto.tag = '';


    $scope.GetHashTags = function(){
        $http.get("Gallery/GellAllHashtags").success(function(response){
            $scope.Hashtags = response;
        });
    }

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

    $scope.AddPhoto = function(){
        if($scope.photoaddForm.$valid){
            var fd = new FormData();

            fd.append('name',$scope.NewPhoto.name);
            fd.append('desc','');
            fd.append('hashtag', $scope.NewPhoto.tag);
            fd.append('file', document.getElementsByName('newphoto')[0].files[0]);

            $http.post('Gallery/AddGallery', fd, {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            }).success(function (response) {
                if (response == 'ok') {
                    $scope.Paging.CurPos = 0;
                    $scope.PhotoGallery = [];
                    $scope.GetAllPhoto($scope.Paging.CurPos,$scope.Paging.PageStep);
                    $scope.GetHashTags();

                } else {
                    $scope.SubPhotoError = response;
                }
            });
        }
        else{
            $scope.SubPhotoError = 'Не все поля заполнены';
        }
    }

    $scope.UpdatePhoto = function(index){
            var fd = new FormData();

            var tag = '';
            if($scope.PhotoGallery[index].hashtags != null){
                tag = $scope.PhotoGallery[index].hashtags[0];
            }

            fd.append('id',$scope.PhotoGallery[index].idRecord);
            fd.append('name',$scope.PhotoGallery[index].name);
            fd.append('desc','');
            fd.append('hashtag', tag);

            $http.post('Gallery/UpdateGallery', fd, {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            }).success(function (response) {
                if (response == 'ok') {
                    $scope.GetHashTags();
                } 
            });
    }

    $scope.DeletePhoto = function(index){
        $http.get('Gallery/DeleteGalleryItem?id=' + $scope.PhotoGallery[index].idRecord).success(function(response){
            if(response == 'ok')
            {
                $scope.PhotoGallery.splice(index,1);
            }
        });
    }

    //Constructor

    $scope.GetAllPhoto($scope.Paging.CurPos,$scope.Paging.PageStep);
    $scope.GetHashTags();

});

function Paging(){
    this.CurPos = 0;
    this.PageStep = 18;
    this.Tag = "";
    this.Timeout = null;
    this.ShowMore = true;
}