jQuery(function ($) {

    'use strict';
	
	/*==============================================================*/
    // Table of index
    /*==============================================================*/

    /*==============================================================
    # sticky-nav
    # Date Time
    # language Select
	# Search Slide
	# Breaking News
	# Owl Carousel
	# magnificPopup
	# newsletter
	# weather	
	
    ==============================================================*/
	
	
	
	
	/*==============================================================*/
    // # sticky-nav
    /*==============================================================*/
	(function () {
		var windowWidth = $(window).width();
		if(windowWidth > 1000 ){
        $(window).scroll (function () {
            var sT = $(this).scrollTop();
				if (sT >= 120) {
					$('.homepage .navbar, .homepage-two.fixed-nav .navbar').addClass('sticky-nav')
				}else {
					$('.homepage .navbar, .homepage-two.fixed-nav .navbar').removeClass('sticky-nav')
				};
			});
		}else{				
			$('.homepage .navbar, .homepage-two.fixed-nav .navbar').removeClass('sticky-nav')			
		};	
		if(windowWidth > 1000 ){
        $(window).scroll (function () {
            var sT = $(this).scrollTop();
				if (sT >= 120) {
					$('.homepage #menubar, .homepage-two.fixed-nav #navigation').removeClass('container')
					$('.homepage #menubar, .homepage-two.fixed-nav #navigation').addClass('container-fluid')
				}else {
					$('.homepage #menubar, .homepage-two.fixed-nav #navigation').removeClass('container-fluid')
					$('.homepage #menubar').addClass('container')
				}
			});
		}else{				
			$('.homepage #menubar, .homepage-two.fixed-nav #navigation').removeClass('container-fluid')			
		};	 

    }());
	
	
    /*==============================================================*/
    // TheiaStickySidebar
    /*==============================================================*/
           
   (function() {

        $('.tr-sticky')
            .theiaStickySidebar({
                additionalMarginTop: 0
            });
    }());
	
		
	/*==============================================================*/
    // # Date Time
    /*==============================================================*/

    (function() {

		var datetime = null,
        date = null;
		var update = function() {
			date = moment(new Date())
			datetime.html(date.format('dddd, MMMM D,  YYYY'));
		};
		datetime = $('#date-time')
		update();
		setInterval(update, 1000);

    }());
	
	/*==============================================================*/
    // # language Select
    /*==============================================================*/
   (function() {
		$('.language-dropdown').on('click', '.language-change a', function(ev) {
			if ("#" === $(this).attr('href')) {
				ev.preventDefault();
				var parent = $(this).parents('.language-dropdown');
				parent.find('.change-text').html($(this).html());
			}
		});
	}());
	
	/*==============================================================*/
	// Search Slide
	/*==============================================================*/
	
	$('.search-icon').on('click', function() {
		$('.searchNlogin').toggleClass("expanded");
	});
		
	
	/*==============================================================*/
    // Breaking News
    /*==============================================================*/
	 (function() {
		$('.breaking-news-scroll').easyTicker({
			direction: 'up',
			easing: 'swing',
			speed: 'slow',
			interval: 3000,
			height: 'auto',
			visible: 1,
			mousePause: 1,
			controls: {
				up: '',
				down: '',
				toggle: '',
				playText: 'Play',
				stopText: 'Stop'
			}
		});
	
	}());
	
	
	/*==============================================================*/
    // sticky
    /*==============================================================*/
	(function () {
		$("#sticky").stick_in_parent();
	}());
	
	/*==============================================================*/
    // Owl Carousel
    /*==============================================================*/
	$("#home-slider").owlCarousel({ 
		pagination	: true,	
		autoPlay	: true,
		singleItem	: true,
		stopOnHover	: true,
	});
	
	$("#latest-news").owlCarousel({ 
		items : 4,
		pagination	: true,	
		autoPlay	: true,
		stopOnHover	: true,
	});
	
	$(".twitter-feeds").owlCarousel({ 
		items : 1,
		singleItem : true,
		pagination	: false,	
		autoPlay	: true,
		stopOnHover	: true,
	});
	
	$("#main-slider").owlCarousel({ 
		items : 3,
		pagination	: false,
		navigation	: false,
		autoPlay	: true,
		stopOnHover	: true
		
	});
	
	
	

	/*==============================================================*/
    // Magnific Popup
    /*==============================================================*/
	
	(function () {
		$('.image-link').magnificPopup({
			gallery: {
			enabled: true
			},		
			type: 'image' 
		});
		$('.feature-image .image-link').magnificPopup({
			gallery: {
				enabled: false
			},		
			type: 'image' 
		});
		$('.image-popup').magnificPopup({	
			type: 'image' 
		});
		$('.video-link').magnificPopup({type:'iframe'});
	}());
	
	
	/*==============================================================*/
    // Newsletter Popup
    /*==============================================================*/
	(function () {
		$(".subscribe-me").subscribeBetter({
			trigger: "onidle",  
			animation: "fade",          
			delay:70,  
			showOnce: true,
			autoClose: false, 
			scrollableModal: false 
		});
	}());
	

	
});

/*==============================================================*/
 // Weather
/*==============================================================*/

$.getJSON("https://api.openweathermap.org/data/2.5/weather?q=Seattle&amp;units=imperial&amp;appid=ab85ba57bbbb423fb62bfb8201126ede", function(data) {

	console.log(data);

	var temp = Math.floor(data.main.temp);

	var weather = data.weather[0].main;

	$(".temp").append(temp  + '°C');

});

	  
	