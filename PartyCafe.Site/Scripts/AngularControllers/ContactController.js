var contactapp = new angular.module("ContactApp",[]);

contactapp.controller("ContactController",function($scope){
    $scope.Map = {};

    var mapCanvas = document.getElementById("map");
    var cafePosition = new google.maps.LatLng(55.7972157, 37.7320033);
    var mapOptions = {
        center: cafePosition,
        zoom: 18,
        scrollwheel: false,
    }

    var marker = new google.maps.Marker({
        position: cafePosition,
        title: 'Party cafe'
    });


    $scope.Map = new google.maps.Map(mapCanvas, mapOptions);
    marker.setMap($scope.Map);
    
    /*
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
            description: 'Москва, Большая Черкизовская 24 А, стр. 1<br>Тел +7 916 99999 80<br>Email hello@partycafe.ru',
            'marker-size': 'large',
            'marker-color': '#BE9A6B',
            'marker-symbol': 'cafe'
        }
    }).addTo($scope.Map);
    $scope.Map.scrollWheelZoom.disable();
    */
});