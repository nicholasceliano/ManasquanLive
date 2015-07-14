/// <reference path="../google.maps.d.ts" />

module Maps {

    export function initialize() {
        var mapOptions = {
            center: { lat: 40.1130, lng: -74.0469 },
            zoom: 15
        };
        var map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);
    }
    google.maps.event.addDomListener(window, 'load', initialize);
}