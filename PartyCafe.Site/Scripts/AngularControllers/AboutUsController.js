var aboutusapp = new angular.module("AboutUsApp", []);


aboutusapp.controller("AboutUsController", function ($scope, $http) {
    $http.get("AboutUs/GetAllUs").success(function(response){
        $scope.About = new About(response);
    })
});

function About(entity){
    this.Photo = entity.PhotoPath,
    this.Name = entity.Name,
    this.Description = entity.Description;
}
