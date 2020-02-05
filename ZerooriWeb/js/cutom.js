


$(function() {
    
  $('.toggle-nav').click(function() {
    $('body').toggleClass('show-nav');
    return false;
  });
    
});


        $(document).ready(function () {
            $('.toggle-nav').click(function () {
                // alert('done');
                $this = $(this);
                $nav = $('.nice-nav');
                //$nav.fadeToggle("fast", function() {
                //  $nav.slideLeft('250');
                //  });

                $nav.toggleClass('open');

            });
            $('.menu-close').click(function () {
                $nav.addClass('open');
            });
            $('.body-part').click(function () {
                $nav.removeClass('open');
            });
            //  $nav.addClass('open');

            //drop down menu
            $submenu = $('.child-menu-ul');
            $('.child-menu .toggle-right').on('click', function (e) {
                e.preventDefault();
                $this = $(this);
                $parent = $this.parent().next();
                // $parent.addClass('active');
                $tar = $('.child-menu-ul');
                if (!$parent.hasClass('active')) {
                    $tar.removeClass('active').slideUp('fast');
                    $parent.addClass('active').slideDown('fast');

                } else {
                    $parent.removeClass('active').slideUp('fast');
                }

            });

            $('a[href="#search"]').on('click', function (event) {
                $('#search').addClass('open');
                $('#search > form > input[type="search"]').focus();
            });
            $('#search, #search button.close').on('click keyup', function (event) {
                if (event.target == this || event.target.className == 'close' || event.keyCode == 27) {
                    $(this).removeClass('open');
                }
            });
        });













    
  