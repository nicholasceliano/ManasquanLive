/// <reference path="../google.maps.d.ts" />



module Maps {
    export var map;
    var flipInitiation = false;
    google.maps.event.addDomListener(window, 'load', initializeMap);

    export function resizeMap(borderPanelSize: number){ 
        var animateTime = 0;

        $('.center-panel').animate({ width: $(window).width() - borderPanelSize - 5 }, animateTime, function () {
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

    export function loadLocations(jsonLocations: string) {
        var jsonString = jsonLocations.replace(new RegExp('&quot;', 'g'), '"').replace(new RegExp('�', 'g'), "'");
        var locationsArray: Locations[] = JSON.parse(jsonString);

        for (var i = 0; i < locationsArray.length; i++) {
            $('.map-locations-list').append('<p>' + locationsArray[i].BusinessName + '</p>');


        }

    }
    
    export function loadCategories(jsonCategories: string) {
        var jsonString = jsonCategories.replace(new RegExp('&quot;', 'g'), '"');
        var categoriesArray: CategoriesList = JSON.parse(jsonString);
 
        buildMapSelection(categoriesArray);
    }

    function buildMapSelection(categoriesArray: CategoriesList) {
        var table = $('.map-selection table'),
            width = 100 / categoriesArray.Categories.length;

        table.append('<tr><th colspan="' + categoriesArray.Categories.length + '">Map Filters <p onclick= "Maps.flipCenterPanel()">List of locations</p></th></tr>');
        table.append('<tr>');

        for (var i = 0; i < categoriesArray.Categories.length; i++) {
            var cat: Category = categoriesArray.Categories[i];
            table.find('tr').not(':first').append('<td style="width:' + width + '%"><input catID="' + cat.ID + '" type="checkbox" />' + cat.Cat + '</td>');
        }
        table.append('</tr>');
    }

    export function loadMapData() {
        //load map data onto  page
        //Need to figure out to to structure this
    }


    class Locations {
        public BusinessName: string;
        public Address: string;
        public Telephone: string;
        public Email: string;
        public Website: string;
        public Description: string;
        public Categories: string[];
    }

    class CategoriesList {
        public Categories: Category[];
    }

    class Category {
        public ID: string;
        public Cat: string;
    }
}