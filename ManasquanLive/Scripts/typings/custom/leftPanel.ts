module LeftPanel {

    export function toggle() {
        var panel = $('.left-panel'),
            centerPanel = $('.center-panel'),
            leftOffers = $('.left-offers'),
            leftToggleImage = $('#left-toggle-image'),
            rightContent = $('.right-content'),
            panelWidth,
            animateTime = 350,
            mapResizeAmnt = 0;

        if (leftOffers.is(':visible')) {
            if (rightContent.is(':visible')) {
                mapResizeAmnt = 270;
            } else {
                mapResizeAmnt = 40;
            }
            panelWidth = '20px';
            leftToggleImage.css('background', 'url(/Content/Images/ToggleArrowRight.png) no-repeat');
        } else {
            if (rightContent.is(':visible')) {
                Maps.resizeMap(500);
            } else {
                Maps.resizeMap(270);
            }
            panelWidth = '250px';
            leftToggleImage.css('background', 'url(/Content/Images/ToggleArrowLeft.png) no-repeat');
        }
        
        panel.animate({ width: panelWidth }, animateTime, function () {
            leftOffers.toggle();
            if (mapResizeAmnt > 0)
                Maps.resizeMap(mapResizeAmnt);
        });
    }

    export function loadOffers() {

        //load list of offers onto page
        //Need to figure out to to structure this

    }
}