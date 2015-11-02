
function BaseClass($scope, $http, $location) {
    this.scope = $scope;
    this.http = $http;
    this.location = $location;
    this.DateTimeNow = null;
}

function DataModelClass() {
    this.Calendar = [];

    this.FillCalendar = function() {
        this.http.get('/Calendar/GetCalendar').success(function(response) {
            this.scope.Base.DateTimeNow = Json2JsDateTime(response.CurDate);
            for (var i in response.Calendar) {
                this.scope.DataModel.Calendar.push(new CalendarEntityClass(response.Calendar[i]));
            }
        });
    }
}

function CalendarEntityClass(entity) {
    this.IdRecord = entity.IdRecord;
    this.EventDateTime = Json2JsDateTime(entity.EventDateTime);
    this.Header = entity.Header;
    this.PhotoLink = entity.PhotoLink;
}