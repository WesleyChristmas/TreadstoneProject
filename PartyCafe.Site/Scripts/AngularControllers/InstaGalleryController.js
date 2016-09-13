var instagalleryapp = new angular.module("instagalleryapp", []);

instagalleryapp.controller("InstaGallery", function ($scope, $http) {
    $scope.Gallery = [];
    getPhoto($scope, $http);
});

function getPhoto($scope, $http) {
    /* id - 2097458386 2097458386.d7a0222.e92c2f9e0d7540389b6208d8410e61c2 */
    var settings = {
        //userId: '2284394077',
        //token: '2284394077.8c76a05.841864c045494c31a8b3a6e0137e672a'
        userId: '2097458386',
        token: '2097458386.d7a0222.e92c2f9e0d7540389b6208d8410e61c2'
    },
    url = 'https://api.instagram.com/v1/users/' + settings.userId + '/media/recent/?access_token=' + settings.token;

    $http.jsonp(url + '&callback=JSON_CALLBACK').success(function (result, status) {
        console.log(result);
        var photocount = result.data.length;
        if (photocount > 11) { 
            $scope.Gallery = result.data.slice(0,6);
        } else {
            $scope.Gallery = result.data;
        }
    });
}