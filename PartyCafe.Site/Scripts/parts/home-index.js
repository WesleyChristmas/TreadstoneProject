$(document).ready(function () {
        var owl = $('.owl-carousel');
        owl.owlCarousel({
            items: 1,
            loop: true,
            margin: 10,
            autoplay: true,
            autoplayTimeout: 3000,
            autoplayHoverPause: false
        });

        $("button").click(function(){
            $("#anchor").ScrollTo();
        });
    });
