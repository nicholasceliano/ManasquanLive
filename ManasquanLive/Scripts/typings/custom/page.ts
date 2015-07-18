module Page {

    export function setHeight() {
        var height = $(window).height() - $('header').height() - 30;
        $('.page').height(height);
    }

} 