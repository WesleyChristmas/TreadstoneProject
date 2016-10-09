
calendarApp.controller("CalendarEditController",function($scope,$http,$location,$routeParams){
    $scope.Calendar = {};

    $http.get("Calendar/GetEvent?id=" + $routeParams.id).success(function(response){
        $scope.Calendar = new Calendar(response);
    });


    $scope.Back = function(){
        $location.path('/');
    }

    $scope.SaveChanges = function(){
        if ($scope.CalendarForm.$valid){

            var fd = new FormData();
            fd.append('id', $scope.Calendar.IdRecord);
            fd.append('name', $scope.Calendar.Name);
            fd.append('date',$scope.Calendar.Date);
            fd.append('time', $scope.Calendar.Time);
            fd.append('desc', "");
            fd.append('isOpen',$scope.Calendar.IsOpen);
            fd.append('file', document.getElementsByName('calendarPhoto')[0].files[0]);

            $http.post('Calendar/UpdateCalendarEvent', fd, {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            }).success(function (response) {
                if (response == 'ok') {
                    $location.path('/');
                } else {
                    $scope.Error = response;
                }
            });
        } else {
            $scope.Error = "Одно из обязательных полей не заполнено!"
        }  
    }

    $scope.Delete = function(){
        $http.get('Calendar/RemoveCalendarEvent?id=' + $scope.Calendar.IdRecord).success(function(response){
            $location.path('/');
        });
    }
});