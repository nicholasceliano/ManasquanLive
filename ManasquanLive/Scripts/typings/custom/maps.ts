/// <reference path="../google.maps.d.ts" />

module Maps {
    google.maps.event.addDomListener(window, 'load', initialize);

    export function initialize() {
        var mapOptions = {
            center: { lat: 40.1130, lng: -74.0469 },
            zoom: 15
        };
        var map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);
    }
    
    export function loadMapData() {
        //load map data onto  page
        //Need to figure out to to structure this
    }

}