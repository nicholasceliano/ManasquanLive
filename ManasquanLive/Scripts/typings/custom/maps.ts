/// <reference path="../google.maps.d.ts" />

module Maps {
    var map;
    google.maps.event.addDomListener(window, 'load', initialize);

    export function resizeMap(borderPanelSize: number){ 
        var animateTime = 350;

        $('.center-panel').animate({ width: $(window).width() - borderPanelSize }, animateTime, function () {
            google.maps.event.trigger(map, 'resize');
        });
    }

    export function initialize() {
        var mapOptions = {
            center: { lat: 40.1130, lng: -74.0469 },
            zoom: 15
        };
        map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);
        Maps.resizeMap(500);
    }
    
    export function loadMapData() {
        //load map data onto  page
        //Need to figure out to to structure this
    }

}