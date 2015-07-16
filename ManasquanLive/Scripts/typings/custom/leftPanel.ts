module LeftPanel {

    export function toggle() {
        var panel = $('.left-panel'),
            rightPanel = $('.right-panel'),
            leftOffers = $('.left-offers'),
            leftToggleImage = $('#left-toggle-image'),
            panelWidth,
            fadeDuration = 100,
            animateTime = 350,
            mapResizeAmnt = 0,
            resizeLater = false;

        if (panel.width() !== leftToggleImage.width()) {
            panelWidth = leftToggleImage.width()
            leftOffers.find('ul').fadeToggle({ duration: 100 });
        } else {
            panelWidth = 250;
        }
        
        if (panelWidth == 20 && rightPanel.width() == 20) {
            resizeLater = true;
            mapResizeAmnt = 40;
        } else if (panelWidth == 20 && rightPanel.width() == 250) {
            resizeLater = true;
            mapResizeAmnt = 270
        } else if (panelWidth == 250 && rightPanel.width() == 20) {
            mapResizeAmnt = 270
        } else if (panelWidth == 250 && rightPanel.width() == 250) {
            mapResizeAmnt = 500
        }

        if (!resizeLater)
            Maps.resizeMap(mapResizeAmnt);

        panel.animate({
            width: panelWidth
        }, {
                duration: animateTime,
                step: function (currWidth) {
                    panel.width(currWidth);
                    leftOffers.width(currWidth - leftToggleImage.width());

                    if (currWidth === panelWidth) {
                        if (panelWidth == 250) {
                            leftOffers.children().fadeToggle({ duration: fadeDuration });
                            leftToggleImage.css('transform', '');
                        }
                        else {
                            leftToggleImage.css('transform', 'rotate(180deg)');
                            leftOffers.children('h1').toggle();
                        }

                        if (resizeLater)
                            Maps.resizeMap(mapResizeAmnt);
                    }
                }
            });
    }

    export function loadOffers() {

        //load list of offers onto page
        //Need to figure out to to structure this

    }
}