
//Get all menu types

function GetAllMenuTypes(s, h,service) {
    h.get('FoodMenu/GetAllMenuTypes').success(function(response) {
        s.MenuTypes.MenuSections = response;
        service.setMenuTypes(s.MenuTypes.MenuSections);
    });
}