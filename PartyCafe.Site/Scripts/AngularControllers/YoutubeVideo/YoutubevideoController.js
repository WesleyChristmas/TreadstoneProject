
var yvApp = new angular.module("YoutubeApp", []);


yvApp.config(function($sceDelegateProvider) {
  $sceDelegateProvider.resourceUrlWhitelist([
    'self',
    'https://www.youtube.com/**'
  ]);
});

yvApp.controller("YoutubeController", function($scope,$http,$timeout){

   $scope.Video = [];
   $scope.SelectedVideo = 'https://www.youtube.com/embed/FPwXRrTGZnI';
   $scope.Api = new YoutubeApi();
   $scope.ShowPlayer = false;

   $http.get('https://www.googleapis.com/youtube/v3/search?key='+ $scope.Api.Key +
    '&channelId=' + $scope.Api.Channel +
     '&part=snippet,id&order=date&maxResults=' + $scope.Api.MaxResults).success(function(response){
         $scope.ParseYoutubeAnswer(response);
     });


   $scope.ParseYoutubeAnswer = function(answer){
       $scope.Api.NextPage = answer.nextPageToken;

       for(var i=0; i< answer.items.length;i++)
       {
           var video = new Video(answer.items[i]);
           if(video.Id != null) $scope.Video.push(video);
       }
   }

   $scope.SelectVideo = function(index){
       $scope.SelectedVideo = 'https://www.youtube.com/embed/' + $scope.Video[index].Id;
       $timeout(function(){  $scope.ShowPlayer = true; },200);
   }

   $scope.CloseVideo = function(){
       $scope.SelectedVideo = null;
       $scope.ShowPlayer = false;

       var iframe = document.getElementById("youtubevideo");
        iframe.src = "about:blank";
   }

   $scope.MoreVideo = function(){
       $http.get('https://www.googleapis.com/youtube/v3/search?key='+ $scope.Api.Key +
    '&channelId=' + $scope.Api.Channel +
     '&part=snippet,id&order=date&maxResults=' + $scope.Api.MaxResults +
     '&pageToken=' + $scope.Api.NextPage).success(function(response){
         $scope.ParseYoutubeAnswer(response);
     });
   }

});

function Video(entity){
    if(entity.id.videoId == null) return null;
    this.Id = entity.id.videoId;
    this.Image = 'https://img.youtube.com/vi/'+ this.Id +'/maxresdefault.jpg';
    this.Title = entity.snippet.title;
}

function YoutubeApi(){
    this.Key = 'AIzaSyBQVAzvPpA4xNzc_FE485h55SFTms6C48Q';
    this.Channel = 'UCePzOHyGJBJpirlN6TGDxDw';
    this.NextPage = null;
    this.MaxResults = 18;
}