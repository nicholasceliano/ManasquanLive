module Page {

    export function setHeight() {
        var height = $(window).height() - $('header').height();
        $('.page').height(height);
    }

} 