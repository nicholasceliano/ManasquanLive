module LeftPanel {

    export function toggle() {
        var panel = $('.left-panel'),
            centerMap = $('.center-map'),
            leftOffers = $('.left-offers'),
            panelWidth,
            animateTime = 0;

        if (leftOffers.is(':visible')) {
            var mapWidth = Math.round((100 * parseFloat($('.center-map').css('width')) / parseFloat($('.center-map').parent().css('width'))));
            if (mapWidth == 83) {
                centerMap.animate({ width: '97%' }, animateTime);
            } else {
                centerMap.animate({ width: '83%' }, animateTime);
            }
            panelWidth = '1%';
            $('.left-toggle').width('100%');
        } else {
            var mapWidth = Math.round((100 * parseFloat($('.center-map').css('width')) / parseFloat($('.center-map').parent().css('width'))));
            if (mapWidth == 83) {
                centerMap.animate({ width: '69%' }, animateTime);
            } else {
                centerMap.animate({ width: '83%' }, animateTime);
            }

            panelWidth = '15%';
            $('.left-toggle').width('10%');
        }
        
        panel.animate({ width: panelWidth }, animateTime, function () {
            leftOffers.toggle();
        });
    }
}