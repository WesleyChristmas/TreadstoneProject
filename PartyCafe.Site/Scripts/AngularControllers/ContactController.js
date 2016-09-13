var contactapp = new angular.module("ContactApp",[]);

contactapp.controller("ContactController",function($scope){
    $scope.Map = {};
    L.mapbox.accessToken = 'pk.eyJ1Ijoid2VzbGV5YnVybndvb2QiLCJhIjoiY2lsNTNvaWNxMDBmY3c5bTBuNnhyb2R4NSJ9.MY590yZ-rkPWcJUfbUjAUw';
    $scope.Map = L.mapbox.map('map', 'mapbox.streets').setView([55.7972157, 37.7320033], 16);
    L.mapbox.featureLayer({
        type: 'Feature',
        geometry: {
            type: 'Point',
            coordinates: [
                37.7320033,
                55.7972157
            ]
        },
        properties: {
            title: 'Zакрытая Территория',
            description: 'Москва, Большая Черкизовская 24 А, стр. 1<br>Тел +7 916 99999 80<br>Почта hello@partycafe.ru',
            'marker-size': 'large',
            'marker-color': '#BE9A6B',
            'marker-symbol': 'cafe'
        }
    }).addTo($scope.Map);
    $scope.Map.scrollWheelZoom.disable();

});