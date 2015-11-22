var instagalleryapp = new angular.module("instagalleryapp", []);

instagalleryapp.controller("InstaGallery", function ($scope, $http) {
    $scope.Gallery = [];
    getPhoto($scope, $http);
});

function getPhoto($scope, $http) {
    var settings = {
        userId: '2284394077',
        token: '2284394077.8c76a05.841864c045494c31a8b3a6e0137e672a'
    },
    url = 'https://api.instagram.com/v1/users/' + settings.userId + '/media/recent/?access_token=' + settings.token;

    $http.jsonp(url + '&callback=JSON_CALLBACK').success(function (data, status) {
        //SetGallery('calendar-wrap', 30, data.Calendar, data.CurDate.replace(/\D+/g, ""), $scope);
       console.log(data);
        $scope.Gallery = data.data;
    });
}