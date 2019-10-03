

$(window).on("load", function () {
    setTimeout(function () { $('.loading').fadeOut(''); }, 2000);

});

//$(document).ready(function() {

//	$(".resultTable").mCustomScrollbar({
//		      axis:"x",
//		      setTop: "30px"
//		    });


//});

$(document).ready(function () {
    $(".main-table").clone(true).appendTo('#table-scroll').addClass('clone');

    $(".table-wrap").mCustomScrollbar({
        axis: "x",
        setTop: "0"
    });

    // //	$(".resultTable").mCustomScrollbar({
    // //	    axis:"x",
    // //	    setTop: "30px"
    // //	});

    // $('.resultTable tbody').scroll(function (e) { //detect a scroll event on the tbody
    ///*
    // Setting the thead left value to the negative valule of tbody.scrollLeft will make it track the movement
    // of the tbody element. Setting an elements left value to that of the tbody.scrollLeft left makes it maintain 			it's relative position at the left of the table.    
    // */
    //     $('.resultTable thead').css("left", -$("tbody").scrollLeft()); //fix the thead relative to the body scrolling
    //     $('.resultTable thead th:nth-child(1)').css("left", $("tbody").scrollLeft()); //fix the first cell of the header
    //     $('.resultTable tbody td:nth-child(1)').css("left", $("tbody").scrollLeft()); //fix the first column of tdbody
    // });


});


// ===== Scroll to Top ==== 
$(window).scroll(function () {
    if ($(this).scrollTop() >= 500) {        // If page is scrolled more than 50px
        $('.sticky_header').fadeIn(500);    // Fade in the arrow
    } else {
        $('.sticky_header').fadeOut(500);   // Else fade out the arrow
    }
});


window.pressed = function () {
    var uploadwrap = document.getElementById('upload');
    if (uploadwrap.value == "") {

    }
    else {

    }
};

//*********************************************************
$(function () {
    $("#slider-range").slider({
        range: true,
        min: 0,
        max: 500,
        values: [75, 300],
        slide: function (event, ui) {
            $("#amount").val("$" + ui.values[0] + " - $" + ui.values[1]);
        }
    });
    $("#amount").val("$" + $("#slider-range").slider("values", 0) +
        " - $" + $("#slider-range").slider("values", 1));
});
//*********************************************************
$(function () {
    $("#slider-range2").slider({
        range: true,
        min: 0,
        max: 500,
        values: [75, 300],
        slide: function (event, ui) {
            $("#amount2").val("$" + ui.values[0] + " - $" + ui.values[1]);
        }
    });
    $("#amount2").val("$" + $("#slider-range2").slider("values", 0) +
        " - $" + $("#slider-range2").slider("values", 1));
});

$(function () {
    $("#slider-range3").slider({
        range: true,
        min: 0,
        max: 500,
        values: [75, 300],
        slide: function (event, ui) {
            $("#amount3").val("$" + ui.values[0] + " - $" + ui.values[1]);
        }
    });
    $("#amount3").val("$" + $("#slider-range3").slider("values", 0) +
        " - $" + $("#slider-range3").slider("values", 1));
});

$(function () {
    $("#slider-range4").slider({
        range: true,
        min: 0,
        max: 500,
        values: [75, 300],
        slide: function (event, ui) {
            $("#amount4").val("$" + ui.values[0] + " - $" + ui.values[1]);
        }
    });
    $("#amount4").val("$" + $("#slider-range4").slider("values", 0) +
        " - $" + $("#slider-range4").slider("values", 1));
});

$(function () {
    $("#year-range").slider({
        range: true,
        min: 1980,
        max: 2018,
        values: [75, 2018],
        slide: function (event, ui) {
            $("#year").val("Classic " + ui.values[0] + " - " + ui.values[1]);
        }
    });
    $("#year").val("Classic " + $("#year-range").slider("values", 0) +
        " - " + $("#year-range").slider("values", 1));
});

$(function () {
    $("#km-range").slider({
        range: true,
        min: 0,
        max: 10000,
        values: [0, 5000],
        slide: function (event, ui) {
            $("#km").val(ui.values[0] + " km " + " - " + ui.values[1] + " km");
        }
    });
    $("#km").val($("#km-range").slider("values", 0) + " km " +
        " - " + $("#km-range").slider("values", 1) + " km");
});


$(document).ready(function () {

    $('.label.ui.dropdown')
        .dropdown();

    $('.no.label.ui.dropdown')
        .dropdown({
            useLabels: false
        });

    $('.ui.button').on('click', function () {
        $('.ui.dropdown')
            .dropdown('restore defaults')
    })



    $('.evaluate_sec .carDetailForm').on('click', function () {
        $('.evaluate_sec .car_detail_form').addClass('act');
    });
    $('.evaluate_new .close').on('click', function () {
        $('.evaluate_sec .car_detail_form').removeClass('act');
    });

    $('.tradeinsection .carDetailForm').on('click', function () {
        $('.tradeinsection .car_detail_form').addClass('act');
    });
    $('.tradeinsection .close').on('click', function () {
        $('.tradeinsection .car_detail_form').removeClass('act');
    });

    $('.shownumber').on('click', function () {
        $(this).find('.v_number').addClass('act');
    });

    /* Grid Buttons */
    $('.gridBtns a').on('click', function () {
        $(this).addClass('act');
        $(this).siblings().removeClass('act');
    });

    $('.gridBtns .listBtn').on('click', function () {

        $('.listing').addClass('list_parent');

        if ($('.listing').hasClass('list_parent')) {

            $('.list_parent').children('.bx').removeClass('col-md-6');
            $('.list_parent').find('.bx').addClass('col-lg-12');

        } else {
            $('.list_parent').find('.bx').removeClass('col-lg-6');
        }

    });

    $('.gridBtns .gridBtn').on('click', function () {

        $('.listing').removeClass('list_parent');
        $('.listing').addClass('grid_parent');

        if ($('.listing').hasClass('grid_parent')) {

            $('.grid_parent').find('.bx').addClass('col-md-6');
            $('.grid_parent').find('.bx').removeClass('col-lg-12');

        } else {

        }

    });




    $('.compare_search input').on('click', function (e) {
        e.stopPropagation();
        $('.comp_search_auto').fadeIn();
    });


    $('.showallfeature').on('click', function () {
        $('.accor_box .accor_head').toggleClass('act');
        $('.expendBox').stop(0, 0).slideToggle('fast');

        if ($(this).text() == "Expand all") {
            $(this).text("Collapse all")
        }
        else {
            $(this).text("Expand all")
        }
        //$(this).parent().find('.expendBox').stop(0,0).slideToggle('fast');
        //$(this).parent().siblings().find('.expendBox').slideUp();
        //$(this).parent().siblings().find('.accor_head').removeClass('act');
    });

    $('.accor_box .accor_head').on('click', function () {
        $(this).toggleClass('act');
        $(this).parent().find('.expendBox').stop(0, 0).slideToggle('fast');
        //$(this).parent().siblings().find('.expendBox').slideUp();
        //$(this).parent().siblings().find('.accor_head').removeClass('act');
    });

    //$('.searchBox span').on('click', function(){
    //  $('.searchBar').toggleClass('show');
    //});

    //$('.searchBar button').on('click', function(){
    //  $(this).parents('.searchBar').removeClass('show');
    //});

    /* Heart Toggle */
    $('.heartBtn').on('click', function () {
        $(this).toggleClass('act');
    });

    /* Call Wid Slider */
    //var mySwiper = new Swiper('.call_widget .swiper-container', {
    //    spaceBetween: 10,
    //    slidesPerView: 1,
    //    loop: true,
    //    autoplay: true,
    //    speed: 1000,
    //    pagination: {
    //      el: '.swiper-pagination',
    //      type: 'bullets',
    //      clickable: true
    //    }
    //});

    ///* Join Club Slider */
    //var mySwiper = new Swiper('.join_slider.swiper-container', {
    //    spaceBetween: 20,
    //    slidesPerView: 4,
    //    navigation: {
    //      nextEl: '.joinNext',
    //      prevEl: '.joinPrev',
    //    },
    //     scrollbar: {
    //      el: '.swiper-scrollbar',
    //      hide: false,
    //    },
    //    pagination: {
    //      el: '.swiper-pagination',
    //      type: 'bullets',
    //      clickable: true
    //    },
    //    breakpoints: {

    //      320: {
    //        slidesPerView: 1,
    //        freeMode: false,
    //        spaceBetween: 10
    //      },

    //      480: {
    //        slidesPerView: 1,
    //        spaceBetween: 10
    //      },

    //      640: {
    //        slidesPerView:2,
    //        spaceBetween:20
    //      }
    //    }
    //});


    /* Instagram Slider */
    //var mySwiper = new Swiper('.slider_wrap.swiper-container', {
    //    spaceBetween: 10,
    //    freeMode: true,

    //    loop: true,
    //    autoplay: true,
    //    speed: 1000,
    //    slidesPerView: 3,
    //    navigation: {
    //      nextEl: '.nextCar',
    //      prevEl: '.prevCar',
    //    },


    //    breakpoints: {

    //      1367: {
    //        slidesPerView: 2,
    //        freeMode: false,
    //        spaceBetween: 10
    //      },
    //   320: {
    //        slidesPerView: 1,
    //        freeMode: false,
    //        spaceBetween: 10
    //      },

    //      480: {
    //        slidesPerView: 1,
    //        spaceBetween: 10
    //      },

    //      640: {
    //        slidesPerView:2,
    //        spaceBetween:20
    //      }
    //    }
    //});

    /* Compare Slider */
    var mySwiper = new Swiper('.prof_compare .swiper-container', {
        spaceBetween: 20,
        loop: true,
        slidesPerView: 3,
        navigation: {
            nextEl: '.nextCmp',
            prevEl: '.prevCmp',
        },

        breakpoints: {

            320: {
                slidesPerView: 1,
                freeMode: false,
                spaceBetween: 10
            },

            480: {
                slidesPerView: 1,
                spaceBetween: 10
            },

            640: {
                slidesPerView: 2,
                spaceBetween: 20
            }
        }
    });

    /* Course Tabs */
    $(".course_links a").click(function (event) {
        event.preventDefault();
        $(this).parent().addClass("current");
        $(this).parent().siblings().removeClass("current");
        var tab = $(this).attr("href");
        $(".course_tab").not(tab).css("display", "none");
        $(tab).fadeIn();
    });

    /* Join Tabs */
    $(".fav_tabs_links a").click(function (event) {
        event.preventDefault();
        $(this).parent().addClass("current");
        $(this).parent().siblings().removeClass("current");
        var tab = $(this).attr("href");
        $(".fav_tab_content").not(tab).css("display", "none");
        $(tab).fadeIn();
    });

    /* Dash Tabs */
    $(".dash_tabs a").click(function (event) {
        event.preventDefault();
        $(this).parent().addClass("current");
        $(this).parent().siblings().removeClass("current");
        var tab = $(this).attr("href");
        $(".dash_tab").not(tab).css("display", "none");
        $(tab).fadeIn();
    });

    /* Dash Tabs */
    $(".tabs_links a").click(function (event) {
        event.preventDefault();
        $(this).parent().addClass("current");
        $(this).parent().siblings().removeClass("current");
        var tab = $(this).attr("href");
        $(".form_container").not(tab).css("display", "none");
        $(tab).fadeIn();
    });

    // /* Dash Tabs */
    $(".carDetails a").click(function (event) {
        var tab = $(this).attr("href");
        if (tab != "#tab2") {
            event.preventDefault();
            $(this).addClass("current");
            $(this).siblings().removeClass("current");
            $(".carDetail_tab").not(tab).css("display", "none");
            $(tab).fadeIn();
        }
    });

    /* Dash Tabs */
    $(".tbs a").click(function (event) {
        event.preventDefault();
        $(this).addClass("current");
        $(this).siblings().removeClass("current");
        var tab = $(this).attr("href");
        $(".cours_tbs").not(tab).css("display", "none");
        $(tab).fadeIn();
        //$('.call_widget .swiper-container')[0].swiper.update();
    });

    /* Dash Tabs */

    $(".virtualTabs a").click(function (event) {
        event.preventDefault();
        $(this).parent().addClass("current");
        $(this).parent().siblings().removeClass("current");
        var tab = $(this).attr("href");
        $(".virtualTab_content").not(tab).css("display", "none");
        $(tab).fadeIn();

    });


    $(".form_tab_links a").click(function (event) {
        event.preventDefault();
        $(this).parent().addClass("current");
        $(this).parent().siblings().removeClass("current");
        var tab = $(this).attr("href");
        $(".form_tab").not(tab).css("display", "none");
        $(tab).fadeIn();
        $('.enquire_form').removeClass('act');
        $('.form_tab_links li a.downl').removeClass('act');

    });

    /* FancyBox 3 */
    $('[data-fancybox]').fancybox({
        arrows: true,
        keyboard: true,
        infobar: true,
        loop: false,
        touch: false,
        // afterShow: function( instance, current ) {
        //   this.content.find('video').trigger('stop');
        // },

        buttons: [
            // 'slideShow',
            // 'fullScreen',
            // 'thumbs',
            'share',
            'download',
            // 'zoom',
            'close'
        ]
    });

});


var time = 2;
var $bar,
    $slick,
    isPause,
    tick,
    percentTime;

$slick = $('.slider');
$slick.slick({
    draggable: true,
    adaptiveHeight: false,
    dots: true,
    autoplay: false,
    mobileFirst: true,
    pauseOnDotsHover: true,
    pauseOnHover: true,
});

$bar = $('.slider-progress .progress');

$('.slider-wrapper').on({
    mouseenter: function () {
        isPause = true;
    },
    mouseleave: function () {
        isPause = false;
    }
})

function startProgressbar() {
    resetProgressbar();
    percentTime = 0;
    isPause = false;
    tick = setInterval(interval, 30);
}

function interval() {
    if (isPause === false) {
        percentTime += 1 / (time + 0.1);
        $bar.css({
            width: percentTime + "%"
        });
        if (percentTime >= 100) {
            $slick.slick('slickNext');
            startProgressbar();
        }
    }
}


function resetProgressbar() {
    $bar.css({
        width: 0 + '%'
    });
    clearTimeout(tick);
}

startProgressbar();


$(document).on('click', function () {

    $('.comp_search_auto').fadeOut();
    $('.login_dropdown').slideUp();

    $(".fa-bars").on("click", function () {
        $(".mainNav").toggleClass("show");
        $('html').toggleClass('fix');
    });
    $(".closebtn").on("click", function () {
        $(".mainNav").removeClass("show");
        $('html').removeClass('fix');
    });


});


//$(".closeregn, button").on("click", function() {
//     $(".select_regn_popup, .overlay3").fadeOut('');
//   }); 




$(window).on("load", function () {



});

$("footer h3 i.fa").click(function () {
    $(this).parent().next().slideToggle();
    $(this).toggleClass("fa-minus-square-o");
});
$("footer h3 i.fa, footer .disclaimertext h5 i.fa").click(function () {
    $(this).parent().parent().parent().children('.mobMenuFot, disc').slideToggle();
    //$(this).toggleClass("fa-minus-square-o");
});


/* Mobile Filter*/
$('.mobileFilter').on('click', function () {
    $('.sideBar').addClass('act');
    $('body').toggleClass('fix');

});
$('.closeFilter').on('click', function () {
    $('.sideBar').removeClass('act');
    $('body').removeClass('fix');
});
/* Mobile Filter*/







$('.SuccessAlertClose').on('click', function () {
    $('body').removeClass('overlaynew');
    $('.custom_alert.success').fadeOut('');
});
$('.WarningAlertClose').on('click', function () {
    $('body').removeClass('overlaynew');
    $('.custom_alert.erorr').fadeOut('');
});

$('.car_detail_slider').slick({
    slidesToShow: 1,
    slidesToScroll: 1,
    arrows: false,
    dots: false,
    fade: true,
    asNavFor: '.car_detail_slider_nav'
});

$('.car_detail_slider_nav').slick({
    slidesToShow: 5,
    slidesToScroll: 1,
    asNavFor: '.car_detail_slider',
    dots: false,
    centerMode: true,
    arrows: true,

    //vertical: true,
    focusOnSelect: true,
    responsive: [
        {
            breakpoint: 1367,
            settings: {
                slidesToShow: 4,
            }
        },
        {
            breakpoint: 650,
            settings: {
                slidesToShow: 2,
            }
        }
    ]
});

//$('.car_detail_slider4').slick({
//    slidesToShow: 1,
//    slidesToScroll: 1,
//    arrows: false,
//    dots: false,
//    centerMode: true,
//    centerPadding: '0',
//    asNavFor: '.car_detail_slider_nav4',
//    responsive: [
//        {
//            breakpoint: 1367,
//            settings: {
//                centerPadding: '0',
//            }
//        },
//        {
//            breakpoint: 650,
//            settings: {
//                slidesToShow: 2,
//            }
//        }
//    ]
//});

//$('.car_detail_slider_nav4').slick({
//    slidesToShow: 3,
//    slidesToScroll: 1,
//    asNavFor: '.car_detail_slider4',
//    dots: false,
//    centerMode: true,
//    arrows: true,
//    vertical: true,
//    centerPadding: '0',

//    focusOnSelect: true,
//    responsive: [
//        {
//            breakpoint: 1367,
//            settings: {
//                slidesToShow: 2,
//                centerPadding: '0',
//            }
//        },
//        {
//            breakpoint: 650,
//            settings: {
//                slidesToShow: 2,
//            }
//        }
//    ]
//});







$('.car_detail_slider3').slick({
    slidesToShow: 1,
    slidesToScroll: 1,
    arrows: true,
    dots: false,
    fade: true,
    asNavFor: '.car_detail_slider_nav3'
});


$('.car_detail_slider3').slick({
    slidesToShow: 1,
    slidesToScroll: 1,
    arrows: true,
    dots: false,
    fade: true,
    asNavFor: '.car_detail_slider_nav3'
});

$('.car_detail_slider_nav3').slick({
    slidesToShow: 5,
    slidesToScroll: 1,
    focusOnSelect: true,
    asNavFor: '.car_detail_slider3',
    responsive: [
        {
            breakpoint: 1367,
            settings: {
                slidesToShow: 4,
            }
        },
        {
            breakpoint: 650,
            settings: {
                slidesToShow: 2,
            }
        }
    ]
});
//*****************************
// Responsive Slider
//*****************************
var respsliders = {
    1: { slider: '.multiple-items' }

};
$.each(respsliders, function () {
    $(this.slider).slick({
        arrows: false,
        dots: true,
        autoplay: true,
        settings: "unslick",
        responsive: [
            {
                breakpoint: 2000,
                settings: "unslick"
            },
            {
                breakpoint: 767,
                settings: {
                    unslick: true
                }
            }
        ]
    });
});
