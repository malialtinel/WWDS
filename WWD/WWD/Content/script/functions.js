jQuery(document).ready(function() {
  'use strict';
//***************************
// FlexSlider Function
//***************************
jQuery('.flexslider').flexslider({
    animation: "slide",
    prevText: "<em class='fa fa-angle-left'></em>",
    nextText: "<em class='fa fa-angle-right'></em>",
    start: function(slider) {
        jQuery('body').removeClass('loading');
    }
});

//***************************
// ProGress Function
//***************************
    jQuery('#as-goal').goalProgress({
        goalAmount: 600,
        currentAmount: 400,
        textBefore: 'Raised: $',
    });

//***************************
// Owl Carousel
//***************************
    var owl = $(".owl-carousel");

    owl.owlCarousel({
        items: 6,
        itemsDesktop: [1000, 5],
        itemsDesktopSmall: [900, 3],
        itemsTablet: [600, 4],
        itemsMobile: false,
        autoplay: true,
        autoplayTimeout: 1000,
        autoplayHoverPause: true
    });

//***************************
// PrettyPhoto Function
//***************************
    jQuery("area[rel^='prettyPhoto']").prettyPhoto();

    jQuery(".gallery:first a[rel^='prettyPhoto']").prettyPhoto({
        animation_speed: 'normal',
        theme: 'light_square',
        slideshow: 3000,
        autoplay_slideshow: true
    });
    jQuery(".gallery:gt(0) a[rel^='prettyPhoto']").prettyPhoto({
        animation_speed: 'fast',
        slideshow: 10000,
        hideflash: true
    });

    jQuery("#custom_content a[rel^='prettyPhoto']:first").prettyPhoto({
        custom_markup: '<div id="map_canvas" style="width:260px; height:265px"></div>',
        changepicturecallback: function() {
            initialize();
        }
    });

    jQuery("#custom_content a[rel^='prettyPhoto']:last").prettyPhoto({
        custom_markup: '<div id="bsap_1259344" class="bsarocks bsap_d49a0984d0f377271ccbf01a33f2b6d6"></div><div id="bsap_1237859" class="bsarocks bsap_d49a0984d0f377271ccbf01a33f2b6d6" style="height:260px"></div><div id="bsap_1251710" class="bsarocks bsap_d49a0984d0f377271ccbf01a33f2b6d6"></div>',
        changepicturecallback: function() {
            _bsap.exec();
        }
    });

//***************************
// Event Toggle Function
//***************************
    jQuery('#btn-one').on("click", function() {
        jQuery("#map-one").slideToggle("slow");
    });

    jQuery('#btn-two').on("click", function() {
        jQuery("#map-two").slideToggle("slow");
    });

    jQuery('#btn-three').on("click", function() {
        jQuery("#map-three").slideToggle("slow");
    });

//***************************
// Click to Top Button
//***************************
  jQuery('.backtop-btn').on("click", function() {
      jQuery('html, body').animate({
          scrollTop: 0
      }, 800);
      return false;
  });

//***************************
// Donation Active Function
//***************************
  jQuery('.as-donation-section ul li').on('click', function() {
      jQuery('li.as-current').removeClass('as-current');
      jQuery(this).addClass('as-current');
  });

//***************************
// Countdown Function
//***************************
  jQuery(function() {
      var austDay = new Date();
      austDay = new Date(austDay.getFullYear() + 1, 1 - 1, 26);
      jQuery('#eventCountdown').countdown({
          until: austDay
      });
      jQuery('#year').text(austDay.getFullYear());
  });

//***************************
// WordCounter Function
//***************************
  jQuery(".word-count").counterUp({
      delay: 10,
      time: 1000
  });

//***************************
// Cart Hover Function
//***************************
  jQuery('.as-cart-wrap').hover(function() {
      jQuery('.as-cart-box').stop(true, false).slideDown();
  },

  function() {
      jQuery('.as-cart-box').stop(true, false).slideUp();
  });

//***************************
// Responsive Menu Function
//***************************
  jQuery(function() {
      jQuery('#as-menu').asmenu();
  });

  //>>>> Trigger ColorSwitcher
   jQuery(".ec-handle").click(function(){
      jQuery(".ec-colorswitcher").trigger('click')
      jQuery(this).toggleClass('btnclose');
      jQuery(".ec-colorswitcher") .toggleClass('sidebarmain');
      return false;
    });
   jQuery('.ec-boxed,.pattren-wrap a,.background-wrap a').on('click', function(){
      jQuery(".as-mainwrapper").addClass('wrapper-boxed');
      jQuery(".as-mainwrapper").removeClass('wrapper-wide');
  });
  jQuery('.ec-wide').on('click', function(){
      jQuery(".as-mainwrapper").addClass('wrapper-wide');
      jQuery(".as-mainwrapper").removeClass('wrapper-boxed');
  });

});

//***************************
// ContactForm Function
//***************************
$('.myform').on('submit',function(){
    // Add text 'loading...' right after clicking on the submit button. 
    $('.output_message').text('Loading...'); 
     
    var form = $(this);
    $.ajax({
        url: form.attr('action'),
        method: form.attr('method'),
        data: form.serialize(),
        success: function(result){
            if (result == 'success'){
                $('.output_message').html('<span class="success-msg"><i class="fa fa-check-circle"></i> Message Sent successfully!</span>');
            } else if (result == 'validate'){
                $('.output_message').html('<span class="spam-error-msg"><i class="fa fa-warning"></i> You have already sent message. Try again after one hour.</span>');
            } else {
                $('.output_message').html('<span class="error-msg"><i class="fa fa-times-circle"></i> Error Sending email!</span>');
            }
        }
    });
     
    // Prevents default submission of the form after clicking on the submit button. 
    return false;   
});