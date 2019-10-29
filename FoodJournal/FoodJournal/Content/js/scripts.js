(function($){
  "use strict";

  var $window = $(window);

  $window.on('load', function() {

    // Preloader
    $('.loader').fadeOut();
    $('.loader-mask').delay(350).fadeOut('slow');

    $window.trigger("resize");
    initInstaWidth();

  });


  // Init
  initOwlCarousel();



  /* Detect Browser Size
  -------------------------------------------------------*/
  var minWidth;
  if (Modernizr.mq('(min-width: 0px)')) {
    // Browsers that support media queries
    minWidth = function (width) {
      return Modernizr.mq('(min-width: ' + width + 'px)');
    };
  }
  else {
    // Fallback for browsers that does not support media queries
    minWidth = function (width) {
      return $window.width() >= width;
    };
  }

  /* Mobile Detect
  -------------------------------------------------------*/
  if (/Android|iPhone|iPad|iPod|BlackBerry|Windows Phone/i.test(navigator.userAgent || navigator.vendor || window.opera)) {
     $("html").addClass("mobile");
     $('.dropdown-toggle').attr('data-toggle', 'dropdown');
  }
  else {
    $("html").removeClass("mobile");
  }

  /* IE Detect
  -------------------------------------------------------*/
  if(Function('/*@cc_on return document.documentMode===10@*/')()){ $("html").addClass("ie"); }


  /* Sticky Navigation
  -------------------------------------------------------*/
  var $stickyNav = $('.nav--sticky');
  var $nav = $('.nav');

  $window.scroll(function(){
    scrollToTop();    

    if ($window.scrollTop() > 2 & minWidth(992)) {
      $stickyNav.addClass('sticky');
      $nav.addClass('sticky');
    } else {
      $stickyNav.removeClass('sticky');
      $nav.removeClass('sticky');
    }

  });


  function stickyNavRemove() {
    if (!minWidth(992)) {
      $stickyNav.removeClass('sticky');
    }
  } 


  /* Mobile Navigation
  -------------------------------------------------------*/
  var $dropdownTrigger = $('.nav__dropdown-trigger');
  var $navDropdownMenu = $('.nav__dropdown-menu');
  var $navDropdown = $('.nav__dropdown');

  $dropdownTrigger.on('click', function() {
    if ($(this).hasClass("active")) {
      $(this).removeClass("active");
    }
    else {
      $(this).addClass("active");
    }
  });
  

  if ( $('html').hasClass('mobile') ) {

    $('body').on('click',function() {
      $navDropdownMenu.addClass('hide-dropdown');
    });

    $navDropdown.on('click', '> a', function(e) {
      e.preventDefault();
    });

    $navDropdown.on('click',function(e) {
      e.stopPropagation();
      $navDropdownMenu.removeClass('hide-dropdown');
    });
  }



  /* Instagram Width
  -------------------------------------------------------*/

  function initInstaWidth() {
    var instaItems = $('#instagram-feed__list li');
    var instaLength = instaItems.length;

    instaItems.width( 100 / instaLength + '%' );
  }
  

  /* Accordion
  -------------------------------------------------------*/
  var $accordion = $('#accordion');

  function toggleChevron(e) {
    $(e.target)
      .prev('.accordion-panel__heading')
      .find("a")
      .toggleClass('plus minus');
  }
  $accordion.on('hide.bs.collapse', toggleChevron);
  $accordion.on('show.bs.collapse', toggleChevron);



  /* Tabs
  -------------------------------------------------------*/
  $('.tabs__link').on('click', function(e) {
    var currentAttrValue = $(this).attr('href');
    $('.tabs__content ' + currentAttrValue).stop().fadeIn(1000).siblings().hide();
    $(this).parent('li').addClass('active').siblings().removeClass('active');
    e.preventDefault();
  });


  /* Owl Carousel
  -------------------------------------------------------*/
  function initOwlCarousel(){
    (function($){
      "use strict";

      // Featured Posts
      $("#owl-hero").owlCarousel({
        center: true,
        items:1,
        loop:true,
        nav:true,
        navSpeed: 500,
        navText: ['<i class="ui-arrow-left">','<i class="ui-arrow-right">']
      });


      // Gallery post
      $("#owl-single").owlCarousel({
        items:1,
        loop:true,
        nav:true,
        animateOut: 'fadeOut',
        navText: ['<i class="ui-arrow-left">','<i class="ui-arrow-right">']
      });

    })(jQuery);
  };



  /* ---------------------------------------------------------------------- */
  /*  Contact Form
  /* ---------------------------------------------------------------------- */

  var submitContact = $('#submit-message'),
    message = $('#msg');

  submitContact.on('click', function(e){
    e.preventDefault();

    var $this = $(this);
    
    $.ajax({
      type: "POST",
      url: 'contact.php',
      dataType: 'json',
      cache: false,
      data: $('#contact-form').serialize(),
      success: function(data) {

        if(data.info !== 'error'){
          $this.parents('form').find('input[type=text],input[type=email],textarea,select').filter(':visible').val('');
          message.hide().removeClass('success').removeClass('error').addClass('success').html(data.msg).fadeIn('slow').delay(5000).fadeOut('slow');
        } else {
          message.hide().removeClass('success').removeClass('error').addClass('error').html(data.msg).fadeIn('slow').delay(5000).fadeOut('slow');
        }
      }
    });
  });


  /* Scroll to Top
  -------------------------------------------------------*/
  function scrollToTop() {
    var scroll = $window.scrollTop();
    var $backToTop = $("#back-to-top");
    if (scroll >= 50) {
      $backToTop.addClass("show");
    } else {
      $backToTop.removeClass("show");
    }
  }

  $('a[href="#top"]').on('click',function(){
    $('html, body').animate({scrollTop: 0}, 1000, "easeInOutQuint");
    return false;
  });

})(jQuery);