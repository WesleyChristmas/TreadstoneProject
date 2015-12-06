var blogApp = new angular.module("BlogApp", ['ngRoute']);

blogApp.config(function($routeProvider) {
    $routeProvider.when('/', {
            templateUrl: 'Blog/BlogTable',
            controller: 'BlogTableController'
        })
        .when('/Edit/:idBlog', {
            templateUrl: 'Blog/BlogEdit',
            controller: 'BlogEditController'
        });
});

blogApp.controller("BlogController", function($location) {
    $location.path('/');
});

blogApp.controller("BlogTableController", function ($scope, $http, $location) {
    $scope.Blogs = [];
    $http.get('Blog/GetBlogs').success(function(response) {
        $scope.Blogs = response;
    });

    $scope.SelectBlog = function(obj) {
        $location.path('/Edit/' + obj.item.IdRecord);
    };
});

blogApp.controller("BlogEditController", function($scope, $http, $location, $routeParams) {
    var idBlog = $routeParams.idBlog;

    $http.get('Blog/GetBlogDetail?idBlog=' + idBlog).success(function(response) {
        $scope.Blog = response;
    });
});