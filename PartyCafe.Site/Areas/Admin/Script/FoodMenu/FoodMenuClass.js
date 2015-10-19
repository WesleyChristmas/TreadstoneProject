
function BaseClass($scope, $http,service) {
    this.scope = $scope;
    this.http = $http;
    this.service = service;
}

function MenuTypesClass() {
    this.MenuSections = [];

    this.GetMenuSections = function() {
        GetAllMenuTypes(this.scope, this.http,this.service);
    }
}

function EditMenuTypesClass(type) {
    if (type != null) {
        this.IdRecord = type.IdRecord;
        this.PhotoLink = type.PhotoLink;
        this.Name = type.Name;
        this.Description = type.Description;
    } else {
        this.IdRecord = null;
        this.PhotoLink = null;
        this.Name = null;
        this.Description = null;
    }
}