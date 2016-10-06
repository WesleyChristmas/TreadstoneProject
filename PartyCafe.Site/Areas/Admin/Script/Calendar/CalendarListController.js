
calendarApp.controller("CalendarListController",function($scope,$http,$location){

    $scope.Events = [];

    $http.get("Calendar/GetAllEvents").success(function(response){
       for(var i=0; i < response.length; i++){
           $scope.Events.push(new Calendar(response[i]));
       }
    });

    $scope.AddNew = function(){
        $location.path('/new');
    }

    $scope.Edit = function(index){
        $location.path('/edit/' + $scope.Events[index].IdRecord);
    }
});