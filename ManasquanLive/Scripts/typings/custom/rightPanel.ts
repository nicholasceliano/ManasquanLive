module RightPanel {

    export function toggle() {
        var panel = $('.right-panel'),
            centerPanel = $('.center-panel'),
            rightContent = $('.right-content'),
            rightToggleImage = $('#right-toggle-image'),
            panelWidth,
            animateTime = 0;

        if (rightContent.is(':visible')) {
            var mapWidth = Math.round((100 * parseFloat(centerPanel.css('width')) / parseFloat(centerPanel.parent().css('width'))));
            if (mapWidth == 83) {
                centerPanel.animate({ width: '97%' }, animateTime);
            } else {
                centerPanel.animate({ width: '83%' }, animateTime);
            }
            panelWidth = '1%';
            rightToggleImage.css('background', 'url(/Content/Images/ToggleArrowLeft.png) no-repeat');
            $('.right-toggle').width('100%');
        } else {
            var mapWidth = Math.round((100 * parseFloat(centerPanel.css('width')) / parseFloat(centerPanel.parent().css('width'))));
            if (mapWidth == 83) {
                centerPanel.animate({ width: '69%' }, animateTime);
            } else {
                centerPanel.animate({ width: '83%' }, animateTime);
            }
            panelWidth = '15%'
            $('.right-toggle').width('10%');
            rightToggleImage.css('background', 'url(/Content/Images/ToggleArrowRight.png) no-repeat');
        }
        panel.animate({ width: panelWidth }, 0, function () {
            rightContent.toggle();
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