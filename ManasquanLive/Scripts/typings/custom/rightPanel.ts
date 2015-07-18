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

    export function loadNews(jsonNews: string) {
        var jsonString = jsonNews.replace(new RegExp('&quot;', 'g'), '"').replace(new RegExp('�', 'g'), "'");
        var newsArray: News[] = JSON.parse(jsonString);

        for (var i = 0; i < newsArray.length; i++) {
            //load this into panel
            var newsIconHref;

            if (newsArray[i].Provider == 'Star News Group') {
                newsIconHref = '/Content/Images/NewsIcons/starNewsGroup.ico';
            } else if (newsArray[i].Provider == 'Asbury Park Press') {
                newsIconHref = '/Content/Images/NewsIcons/app.ico';
            } else if (newsArray[i].Provider == 'Patch.com') {
                newsIconHref = '/Content/Images/NewsIcons/patch.ico';
            } else {
                newsIconHref = '/Content/Images/NewsIcons/news.ico';
            }

            var listItem = '<li><a href="' + newsArray[i].URL + '" target="_blank" title="Published: ' + new Date(newsArray[i].Date).toDateString() + '">- '+ newsArray[i].Headline + '</a>\
            <img src="' + newsIconHref + '" title="' + newsArray[i].Provider + '" /></li>';

            $('#news-list').append(listItem);

            setNewsHeight();
        }
    }

    export function setNewsHeight() {
        var newsContent = $('.right-content-news');
        $('#news-list').height(newsContent.height() - newsContent.find('h1').height())
    }

    export function loadEvents() {
        //load events onto  page
        //Need to figure out to to structure this
    }

    class News {
        public Headline: string;
        public Date: string;
        public URL: string;
        public Provider: string;
    }
}