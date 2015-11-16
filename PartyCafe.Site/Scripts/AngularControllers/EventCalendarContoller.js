var eventcalendarapp = new angular.module("eventcalendarapp", []);
eventcalendarapp.directive('note', function ($timeout) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            $timeout(function () {
                var iso = new Isotope(document.querySelector('#calendar-wrap'), {
                    itemSelector: '.event-day',
                    layoutMode: 'masonry'
                });
            }, 100);
        }
    };
});


eventcalendarapp.controller("EventCalendar", function ($scope, $http) {
    $scope.Events = [];
    getData($scope, $http);
});

Date.prototype.monthDays = function () {
    return new Date(this.getFullYear(), this.getMonth() + 1, 0).getDate();
}
function setCalendaData(_d, _m) {
    return { day: _d, month: _m, data: [] };
}

function getData($scope, $http) {
    $http.get("/EventCalendar/GetCalendar").success(function (data, status) {
        Calendar('calendar-wrap', 30, data.Calendar, data.CurDate.replace(/\D+/g, ""), $scope);
    })
}

function Calendar(obj, dcount, respons, curdate, $scope) {
    var date = new Date(parseInt(curdate)),
        m = date.getMonth() + 1, daysInMonth = date.monthDays(),
        cal = [],
        _month = ["Января", "Февраля", "Марта", "Апреля", "Мая", "Июня", "Июля", "Августа", "Сентября", "Октября", "Ноября", "Декабря"],
        l = dcount;

    /* формирование календаря */
    for (var i = 0; i < l; ++i) {
        var d = date.getDate();

        if (d >= daysInMonth) {
            var obj = setCalendaData(d, _month[m-1]);
            cal[i] = obj;

            if (m != 13) {
                date.setDate(1);
                date.setMonth(date.getMonth() + 1);

                var obj = setCalendaData(d, _month[m - 1]);
                cal[i] = obj;
                m = date.getMonth() + 1;
            }
        }
        else {
            var obj = setCalendaData(d, _month[m - 1]);

            cal[i] = obj;
            date.setDate(d + 1);
        }
    }

    /* Заполнение календаря событиями */
    l = respons.length;
    for (var i = 0; i < l; i++) {
        var _d = respons[i].EventDate.replace(/\D+/g, ""),
            date = new Date(parseInt(_d)),
            d = date.getDate(),
            m = date.getMonth(),
            _calL = cal.length;

        for (var j = 0; j < _calL; j++) {
            //if (cal[j].day === d && cal[j].month === m+1) {
            if (cal[j].day === d) {
                var _obj = { header: respons[i].Header, photo: respons[i].PhotoLink }
                cal[j].data = _obj;
                break;
            }
        }
    }
    console.log(cal);
    $scope.Events = cal;
    //$scope.$apply();

    /*var elem = document.querySelector('#calendar-wrap');
    var iso = new Isotope(elem, {
        itemSelector: '.event-day',
        layoutMode: 'masonry'
    });

    setTimeout(function () {
        iso.layout(cal);
    }, 100);*/

}

function Masonry() {
    var $container = $('#calendar-wrap'),
        isotopeOptions = {},
        defaultOptions = {
            sortBy: 'original-order',
            sortAscending: true,
            layoutMode: 'masonry'
        };

    $container.isotope({
        itemSelector: '.event-day',
    });

    var $optionSets = $('#options').find('.option-set'),
        isOptionLinkClicked = false;

    // switches selected class on buttons
    function changeSelectedLink($elem) {
        // remove selected class on previous item
        $elem.parents('.option-set').find('.selected').removeClass('selected');
        // set selected class on new item
        $elem.addClass('selected');
    }

    $optionSets.find('a[href^="#filter"]').click(function () {
        var $this = $(this);
        // don't proceed if already selected
        if ($this.hasClass('selected')) { return; }

        changeSelectedLink($this);
        // get href attr, remove leading #
        var href = $this.attr('href').replace(/^#/, ''),
            // convert href into object
            // i.e. 'filter=.inner-transition' -> { filter: '.inner-transition' }
            option = $.deparam(href, true);

        // apply new option to previous
        $.extend(isotopeOptions, option);
        // set hash, triggers hashchange on window
        $.bbq.pushState(isotopeOptions);
        isOptionLinkClicked = true;
        return false;
    });

    var hashChanged = false;

    $(window).bind('hashchange', function (event) {
        // get options object from hash
        var hashOptions = window.location.hash ? $.deparam.fragment(window.location.hash, true) : {},
            // do not animate first call
            aniEngine = hashChanged ? 'best-available' : 'none',
            // apply defaults where no option was specified
            options = $.extend({}, defaultOptions, hashOptions, { animationEngine: aniEngine });

        // apply options from hash
        $container.isotope(options);
        // save options
        isotopeOptions = hashOptions;

        // if option link was not clicked
        // then we'll need to update selected links
        if (!isOptionLinkClicked) {
            // iterate over options
            var hrefObj, hrefValue, $selectedLink;
            for (var key in options) {
                hrefObj = {};
                hrefObj[key] = options[key];
                // convert object into parameter string
                // i.e. { filter: '.inner-transition' } -> 'filter=.inner-transition'
                hrefValue = $.param(hrefObj);
                // get matching link
                $selectedLink = $optionSets.find('a[href="#' + hrefValue + '"]');
                changeSelectedLink($selectedLink);
            }
        }

        isOptionLinkClicked = false;
        hashChanged = true;
    }).trigger('hashchange');

    return false;
}

function fillCalendar(obj) {
    var calendar = document.getElementById('calendar-wrap'),
        l = obj.length,
        counter = 0,
        _week = document.createElement('div'),
        _weekDay = document.createElement('div');

    for (var i = 0; i < l; i++) {
        _weekDay = document.createElement('div');

        if (counter == 6 || i == l - 1) {
            var _div = document.createElement('div');
            _div.textContent = obj[i].data.header;

            _weekDay.className = 'event-day';
            _weekDay.textContent = obj[i].day;
            _weekDay.appendChild(_div);

            _week.className = 'event-week';
            _week.appendChild(_weekDay);

            calendar.appendChild(_week);

            _week = document.createElement('div');
            counter = 0;
        } else {
            var _div = document.createElement('div');
            _div.textContent = obj[i].data.header;

            _weekDay.className = 'event-day';
            _weekDay.textContent = obj[i].day;
            _weekDay.appendChild(_div);

            _week.appendChild(_weekDay);
            counter++;
        }
    }
}