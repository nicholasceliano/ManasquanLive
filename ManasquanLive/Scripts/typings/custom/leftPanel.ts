module LeftPanel {

    export function toggle() {
        var panel = $('.left-panel'),
            centerPanel = $('.center-panel'),
            leftOffers = $('.left-offers'),
            leftToggleImage = $('#left-toggle-image'),
            panelWidth,
            animateTime = 0;

        if (leftOffers.is(':visible')) {
            var mapWidth = Math.round((100 * parseFloat(centerPanel.css('width')) / parseFloat(centerPanel.parent().css('width'))));
            if (mapWidth == 83) {
                centerPanel.animate({ width: '97%' }, animateTime);
            } else {
                centerPanel.animate({ width: '83%' }, animateTime);
            }
            panelWidth = '1%';
            leftToggleImage.css('background', 'url(/Content/Images/ToggleArrowRight.png) no-repeat');
            $('.left-toggle').width('100%');
        } else {
            var mapWidth = Math.round((100 * parseFloat(centerPanel.css('width')) / parseFloat(centerPanel.parent().css('width'))));
            if (mapWidth == 83) {
                centerPanel.animate({ width: '69%' }, animateTime);
            } else {
                centerPanel.animate({ width: '83%' }, animateTime);
            }

            panelWidth = '15%';
            leftToggleImage.css('background', 'url(/Content/Images/ToggleArrowLeft.png) no-repeat');
            $('.left-toggle').width('10%');
        }
        
        panel.animate({ width: panelWidth }, animateTime, function () {
            leftOffers.toggle();
        });
    }

    export function loadOffers() {

        //load list of offers onto page
        //Need to figure out to to structure this

    }
}