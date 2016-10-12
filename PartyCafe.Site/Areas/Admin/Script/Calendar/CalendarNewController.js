
calendarApp.controller("CalendarNewController",function($scope,$http,$location){
    $scope.Calendar = {};

    $scope.Back = function(){
        $location.path('/');
    }

    $scope.SaveChanges = function(){
        if ($scope.CalendarForm.$valid){

            var fd = new FormData();
            fd.append('name', $scope.Calendar.Name);
            fd.append('date',$scope.Calendar.Date);
            fd.append('time', $scope.Calendar.Time);
            fd.append('desc', "");
            fd.append('file', document.getElementsByName('calendarPhoto')[0].files[0]);

            $http.post('Calendar/AddCalendarEvent', fd, {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            }).success(function (response) {
                if (response == 'ok') {
                    $scope.Back();
                } else {
                    $scope.Error = response;
                }
            });
        } else {
            $scope.Error = "Одно из обязательных полей не заполнено!"
        }  
    }
});