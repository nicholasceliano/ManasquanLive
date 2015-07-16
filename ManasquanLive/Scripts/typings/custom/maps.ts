/// <reference path="../google.maps.d.ts" />



module Maps {
    export var map;
    var flipInitiation = false;
    google.maps.event.addDomListener(window, 'load', initializeMap);

    export function resizeMap(borderPanelSize: number){ 
        var animateTime = 0;

        $('.center-panel').animate({ width: $(window).width() - borderPanelSize }, animateTime, function () {
            google.maps.event.trigger(map, 'resize');
            if (!flipInitiation)
                initializeFlip();
        });
    }

    export function initializeMap() {
        var mapOptions = {
            center: { lat: 40.1130, lng: -74.0469 },
            zoom: 15
        };
        map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);
        resizeMap(500);
    }

    export function initializeFlip() {
        $('.center-panel-flip').flip({
            trigger: 'manual',
            front: '.center-map',
            back: '.map-locations-list'
        });
        $('.map-locations-list').show();
        flipInitiation = true;
    }

    export function flipCenterPanel() {
        $('.center-panel-flip').flip('toggle');
    }
    
    export function loadMapData() {
        //load map data onto  page
        //Need to figure out to to structure this
    }

}