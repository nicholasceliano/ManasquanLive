module RightPanel {

    export function toggle() {
        var panel = $('.right-panel'),
            centerMap = $('.center-map'),
            rightContent = $('.right-content'),
            panelWidth,
            animateTime = 0;

        if (rightContent.is(':visible')) {
            var mapWidth = Math.round((100 * parseFloat($('.center-map').css('width')) / parseFloat($('.center-map').parent().css('width'))));
            if (mapWidth == 83) {
                centerMap.animate({ width: '97%' }, animateTime);
            } else {
                centerMap.animate({ width: '83%' }, animateTime);
            }
            panelWidth = '1%';
            $('.right-toggle').width('100%');
        } else {
            var mapWidth = Math.round((100 * parseFloat($('.center-map').css('width')) / parseFloat($('.center-map').parent().css('width'))));
            if (mapWidth == 83) {
                centerMap.animate({ width: '69%' }, animateTime);
            } else {
                centerMap.animate({ width: '83%' }, animateTime);
            }
            panelWidth = '15%'
            $('.right-toggle').width('10%');
        }
        panel.animate({ width: panelWidth }, 0, function () {
            rightContent.toggle();
        });
    }
}