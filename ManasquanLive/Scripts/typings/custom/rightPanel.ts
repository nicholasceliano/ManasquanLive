module RightPanel {

    export function toggle() {
        var panel = $('.right-panel'),
            centerPanel = $('.center-panel'),
            rightContent = $('.right-content'),
            leftOffers = $('.left-offers'),
            rightToggleImage = $('#right-toggle-image'),
            panelWidth,
            animateTime = 350,
            mapResizeAmnt = 0;

        if (rightContent.is(':visible')) {
            if (leftOffers.is(':visible')) {
                mapResizeAmnt = 270;
            } else {
                mapResizeAmnt = 40;
            }
            panelWidth = '20px';
            rightToggleImage.css('background', 'url(/Content/Images/ToggleArrowLeft.png) no-repeat');
        } else {
            if (leftOffers.is(':visible')) {
                Maps.resizeMap(500);
            } else {
                Maps.resizeMap(270);
            }
            panelWidth = '250px'
            rightToggleImage.css('background', 'url(/Content/Images/ToggleArrowRight.png) no-repeat');
        }

        panel.animate({ width: panelWidth }, animateTime, function () {
            rightContent.toggle();
            if (mapResizeAmnt > 0)
                Maps.resizeMap(mapResizeAmnt);
        });
    }

    export function loadNews() {
        //load news onto  page
        //Need to figure out to to structure this
    }

    export function loadEvents() {
        //load events onto  page
        //Need to figure out to to structure this
    }
}