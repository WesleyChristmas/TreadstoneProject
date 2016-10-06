var servicesApp = new angular.module("ServicesApp",['ngRoute']);

servicesApp.config(function($routeProvider){
    $routeProvider
    .when('/',{
        templateUrl:'Services/ServicesList',
        controller: 'ServicesListController'
    })
    .when('/edit/:id',{
        templateUrl:'Services/ServicesEdit',
        controller: 'ServicesEditController'
    })
    .when('/new',{
        templateUrl:'Services/ServicesNew',
        controller:'ServicesNewController'
    });
})
servicesApp.controller("ServicesController",function($scope){

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