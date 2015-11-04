
function BaseClass($scope, $http, $location,service) {
    this.scope = $scope;
    this.http = $http;
    this.location = $location;
    this.service = service;
    this.DateTimeNow = null;
}

function DataModelClass() {
    this.Calendar = [];

    var s = this.scope;
    var service = this.service;
    this.FillCalendar = function () {
        this.Calendar = [];
        this.http.get('/Calendar/GetCalendar').success(function(response) {
            s.Base.DateTimeNow = Json2JsDateTime(response.CurDate);
            for (var i in response.Calendar) {
                s.DataModel.Calendar.push(new CalendarEntityClass(response.Calendar[i]));
            }
            service.addData(s.DataModel.Calendar);
        });
    }
}

function CalendarEntityClass(entity) {
    if (entity != null) {
        this.IdRecord = entity.IdRecord;
        this.EventDate = Json2JsDateTime(entity.EventDate);
        this.Header = entity.Header;
        this.PhotoLink = entity.PhotoLink;
    } else {
        this.IdRecord = null;
        this.EventDate = null;
        this.Header = null;
        this.PhotoLink = null;
    }
    
}