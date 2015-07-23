module Page {
    export function setHeight() {
        var height = $(window).height() - $('header').height() - 30;
        $('.page').height(height);

        var centerPanelFlipHeight = (height * .95) - $('.map-selection').height();
        $('.center-panel-flip').height(centerPanelFlipHeight);
    }
} 