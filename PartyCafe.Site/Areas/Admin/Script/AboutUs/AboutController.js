var aboutApp = new angular.module("AboutApp",['ngRoute']);

aboutApp.config(function($routeProvider){
    $routeProvider
    .when('/',{
        templateUrl:'AboutUs/AboutList',
        controller: 'AboutListController'
    })
    .when('/edit/:id',{
        templateUrl:'AboutUs/AboutEdit',
        controller: 'AboutEditController'
    })
    .when('/new',{
        templateUrl:'AboutUs/AboutNew',
        controller:'AboutNewController'
    });
})
aboutApp.controller("AboutController",function($scope){

});

function Service(entity){
    if(entity != null)
    {
        this.name = entity.name;
        this.description = entity.description;
        this.photoPath = entity.photoPath;
        this.idRecord = entity.idRecord;
        this.photos = entity.photos;
    } else{
        this.name = "";
        this.description = "";
    }
}