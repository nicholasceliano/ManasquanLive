module RightPanel {

    export function toggle() {
        var panel = $('.right-panel'),
            leftPanel = $('.left-panel'),
            rightContent = $('.right-content'),
            rightToggleImage = $('#right-toggle-image'),
            panelWidth,
            fadeDuration = 100,
            animateTime = 350,
            mapResizeAmnt = 0,
            resizeLater = false;

        if (panel.width() !== rightToggleImage.width()) {
            panelWidth = rightToggleImage.width()
            rightContent.children().find('ul').fadeToggle({ duration: 100 });
        } else {
            panelWidth = 250
        }

        if (panelWidth == 20 && leftPanel.width() == 20) {
            resizeLater = true;
            mapResizeAmnt = 40;
        } else if (panelWidth == 20 && leftPanel.width() == 250) {
            resizeLater = true;
            mapResizeAmnt = 270
        } else if (panelWidth == 250 && leftPanel.width() == 20) {
            mapResizeAmnt = 270
        } else if (panelWidth == 250 && leftPanel.width() == 250) {
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
                    rightContent.width(currWidth - rightToggleImage.width());

                    if (currWidth === panelWidth) {
                        if (panelWidth == 250) {
                            rightContent.children().fadeToggle({ duration: fadeDuration });
                            rightContent.children().find('ul').fadeToggle({ duration: fadeDuration });
                            rightToggleImage.css('transform', '');
                        }
                        else {
                            rightToggleImage.css('transform', 'rotate(180deg)');
                            rightContent.children().toggle();
                        }

                        if (resizeLater)
                            Maps.resizeMap(mapResizeAmnt);
                    }
                }
            });
    }

    export function loadNews() {
        //Load google news

        // load coast star news....order it

        var title = "jQuery";

        $.getJSON("https://news.google.com/news?q=manasquan&output=rss&format=json&callback=?", function (data) {
            console.log(data);
        });

        $.ajax({
            url: 'https://news.google.com/news?q=manasquan&output=rss' +'$callback=?',
            type: 'GET',
            success: function (data) {
                $('#test').val(data);
                console.log(data);

            }
        });

        //load news onto  page
        //Need to figure out to to structure this
    }

    export function loadEvents() {
        //load events onto  page
        //Need to figure out to to structure this
    }
}