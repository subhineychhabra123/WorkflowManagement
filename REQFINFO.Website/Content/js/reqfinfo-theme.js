$(document).ready(function() {
    $('#showmenu').click(function() {
    var hidden = $('.sidebar-a-menu').data('hidden');
    if(hidden){
        $('.sidebar-a-menu').animate({
            left: '0px'
        },700)
    } else {
        $('.sidebar-a-menu').animate({
            left: '-200px'
        },700)
    }
    $('.sidebar-a-menu,.image').data("hidden", !hidden);

    });
}); 



WebFontConfig = {
	google: {
		families: ['Open+Sans:400,700,300,300italic,400italic,600,600italic,700italic,800italic:latin']
	}
};
(function() {
	var wf = document.createElement('script');
	wf.src = ('https:' == document.location.protocol ? 'https' : 'http') +
		'://ajax.googleapis.com/ajax/libs/webfont/1/webfont.js';
	wf.type = 'text/javascript';
	wf.async = 'true';
	var s = document.getElementsByTagName('script')[0];
	s.parentNode.insertBefore(wf, s);
})();

//side bar dropsown START

! function(e) {
    "use strict";
    var t;
    e.fn.tuxedoMenu = function(i) {
        var r = this;
        return t = e.extend({
            triggerSelector: ".tuxedo-menu-trigger",
            menuSelector: ".tuxedo-menu",
            isFixed: !0
        }, i), r.addClass("tuxedo-menu tuxedo-menu-pristine animated").toggleClass("tuxedo-menu-visible", !t.isFixed).toggleClass("tuxedo-menu-fixed slideOutLeft", t.isFixed), e(t.triggerSelector).addClass("tuxedo-menu-trigger"), e(t.triggerSelector).on("click", function() {
            return t.isFixed ? (e(t.menuSelector).toggleClass("slideInLeft slideOutLeft").addClass("tuxedo-menu-visible"), !1) : void 0
        }), e(document).click(function(i) {
            t.isFixed && !e(i.target).is(t.triggerSelector) && (e(i.target).is(t.triggerSelector) || e(i.target).closest(t.menuSelector).length || e(t.menuSelector).removeClass("slideInLeft").addClass("slideOutLeft"))
        }), r
    }
}(jQuery);

(function($) {
    var isFixed = true;
    $('#sidebar-1').tuxedoMenu({
        isFixed: isFixed
    });
    $('#toggle-is-fixed').on('click', function() {
        $('#sidebar-1').tuxedoMenu({
            isFixed: isFixed = !isFixed
        });
        $('#menu-container').toggleClass('col-sm-3');
        $('.tuxedo-menu-trigger').toggleClass('hidden');
        $(this).html(isFixed ? 'Drawer' : 'sidebar-1');
    });
})(jQuery);

//side bar dropsown CLOSE


//Tooltip START
$(document).ready(function(){
    $('[data-toggle="tooltip"]').tooltip();   
});

//Tooltip CLOSE


//Hide SIde Menu START
 $(document).ready(function () {           
	$('.menu-normal-icon').click(function () {            
		$('#sidebar').addClass('menu-min');
	})
	$('.menu-min-icon').click(function () {            
		$('#sidebar').removeClass('menu-min');
	})
	
  })
  //Hide SIde Menu CLOSE