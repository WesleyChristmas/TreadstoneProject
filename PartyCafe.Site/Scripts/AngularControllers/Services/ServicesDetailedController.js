
servicesapp.controller("ServiceDetailedController",function($scope,$http,$routeParams){

    $scope.Order = new Order();

    $http.get("/Services/GetServicePhotos?serviceId=" + $routeParams.id).success(function(response){
        $scope.ServicePhotos = response;
    });

    $scope.SendOrder = function(){

        $scope.Order.ClearValidation();

        if(!$scope.Order.Validation()) {
        $scope.Order.ValError = true;
        return;
        }

        $http.post("/Services/NewOrder",{
            user : $scope.Order.Name,
            phone : $scope.Order.Phone,
            date : $scope.Order.Date,
            time : $scope.Order.Time,
            service : $scope.Order.ServiceName
        }).success(function(response){
            if(response == "good"){
                $scope.Order.Good = true;
            } else{
                $scope.Order.ServerError = true;
            }
            $scope.Order.ShowBtn = false;
        });
    }
});

function Order(){
    this.Name = null;
    this.Phone = null;
    this.Date = null;
    this.Time = null;
    this.ServiceName = "Наименование услуги";

    this.ValError = false;
    this.ServerError = false;
    this.Good = false;
    this.ShowBtn = true;

    this.Validation = function(){
        return ((this.Name)&&(this.Phone)&&(this.Date)&&(this.Time)&&(this.ServiceName));
    }

    this.ClearValidation = function(){
        this.ValError = false;
        this.ServerError = false;
        this.Good = false;
        this.ShowBtn = true;
    }
}