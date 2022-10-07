const $dropdown = $(".dropdown");
const $dropdownToggle = $(".dropdown-toggle");
const $dropdownMenu = $(".dropdown-menu");
const showClass = "show";
let $windowWidth = null;

$(window).on('beforeunload', function () {
    showLoader();
});

$(window).on("load resize", function () {
    $windowWidth = $(this).width();

    if ($windowWidth > 992) {
        $dropdown.hover(
            function () {
                const $this = $(this);
                $this.addClass(showClass);
                $this.find($dropdownToggle).attr("aria-expanded", "true");
                $this.find($dropdownMenu).addClass(showClass);
            },
            function () {
                const $this = $(this);
                $this.removeClass(showClass);
                $this.find($dropdownToggle).attr("aria-expanded", "false");
                $this.find($dropdownMenu).removeClass(showClass);
            }
        );
    } else {
        $dropdown.off("mouseenter mouseleave");
    }
});

$(window).on("scroll", function () {
    if (!$('.navbar').hasClass('always-shadow')) {
        if ($(this).scrollTop() > 86) {
            $('.fixed-top').addClass('shadow');
        } else {
            if (!$('.navbar-collapse').hasClass('show'))
                $('.fixed-top').removeClass('shadow');
        }
    }
});

$(window).on("load", function () {
    $windowWidth = $(this).width();

    configureDatepickers();

    setParallax();

    window.setTimeout(function () {
        if (!$('.navbar').hasClass('always-shadow')) {
            if ($(this).scrollTop() > 86) {
                $('.fixed-top').addClass('shadow');
            } else {
                if (!$('.navbar-collapse').hasClass('show'))
                    $('.fixed-top').removeClass('shadow');
            }
        }
    }, 500);

    let isBookingWindow = window.location.href.indexOf("book-a-bed") > -1;

    if (!isBookingWindow) {
        hideLoader();
    }

    $('.navbar-toggler').on('click', function () {
        if ($windowWidth < 992) {
            if ($(window).scrollTop() < 86 && !$('.fixed-top').hasClass('shadow'))
                $('.fixed-top').addClass('shadow');
            else if ($(window).scrollTop() < 86)
                $('.fixed-top').removeClass('shadow');
        }
    });

    $('#check-availability-button').on('click', function (e) {
        validateBookingSelection(e);
    })

    $('.nav-link[data-mdb-toggle="pill"]').on('click', function () {
        $('.tab-pane input[type=text], .tab-pane input[type=email], .tab-pane input[type=password], .tab-pane select').removeClass('active').val('').trigger('blur');
        $('.field-validation-error').removeClass('field-validation-error').addClass('field-validation-valid').find('span').remove();
    });

    $('form').submit(function (e) {
        let $form = $(this);

        if (e.originalEvent != undefined) {
            let $noValidate = $(e.originalEvent.submitter).attr('formnovalidate');

            if (typeof $noValidate !== 'undefined' && $noValidate !== false) {
                showLoader();
                return true;
            }
        }

        let $valid = $form.valid();
        $valid = validateSelectElements($form, $valid);

        if (!$valid) return false;
        showLoader();
    });

    $('#booking-register-form input').keypress((e) => {
        if (e.which === 13) {
            $('#submit-booking-registration-button').trigger('click');
        }
    });

    $('.select').on('change', function () {
        validateSelectElement($(this));
    });

    Cookie.showModalNewUser('cookiesModal');
});

Date.prototype.addDays = function (days) {
    var date = new Date(this.valueOf());
    date.setDate(date.getDate() + days);
    return date;
}

var showLoader = function () {
    $('.pre-loader').show();
    $('html').addClass('no-scrollbar');
}

var hideLoader = function () {
    $('html').removeClass('no-scrollbar');
    $('.pre-loader').hide()
}

var configureDatepickers = function () {
    $('.datepicker-disable-future').each(function () {
        let datepicker = new mdb.Datepicker(this, {
            disableFuture: true,
            toggleButton: false
        });
    });

    $('.datepicker-disable-past').each(function () {
        let datepicker = new mdb.Datepicker(this, {
            disablePast: true,
            toggleButton: false
        });

        this.addEventListener('dateChange.mdb.datepicker', (e) => {
            let id = e.srcElement.children[0].id;
            let date = e.date;

            if (id == "CheckInDate") {
                let minDate = date.addDays(1);
                let checkOutDatepicker = document.getElementById("CheckOutDatePicker");
                let instance = mdb.Datepicker.getInstance(checkOutDatepicker);

                if (instance) {
                    instance.dispose();
                    instance = new mdb.Datepicker(checkOutDatepicker, {
                        disablePast: true,
                        toggleButton: false,
                        min: minDate,
                        startDate: minDate
                    });
                }
            }
            else if (id == "CheckOutDate") {
                let maxDate = date;
                let checkInDatePicker = document.getElementById("CheckInDatePicker");
                let instance = mdb.Datepicker.getInstance(checkInDatePicker);

                if (instance) {
                    instance.dispose();
                    instance = new mdb.Datepicker(checkInDatePicker, {
                        disablePast: true,
                        toggleButton: false,
                        max: maxDate
                    });
                }
            }
        })
    });
}

var validateBookingSelection = function (e) {
    let controls = [$('#Id'), $('#CheckInDate'), $('#CheckOutDate')];
    let valid = true;

    $(controls).each(function () {
        if (!$(this).valid())
            valid = false;

        let thisId = $(this).attr('id');
        let thisValue = $('#' + thisId).val();
        $('#LoginInput_' + thisId).val(thisValue);
        $('#RegisterInput_' + thisId).val(thisValue);

        if ($(this).hasClass('select')) {
            let selectValid = validateSelectElement($(this));

            if (!selectValid && valid)
                valid = false;
        }
    });

    if (!valid) {
        e.preventDefault();
    }
    else {
        $('#book-now-sign-in-modal').modal('show');
    }

    return valid;
}

var validateSelectElements = function (form, valid) {
    let $formSelect = form.find('.select');

    if ($formSelect.length > 0)
        $formSelect.each(function (index, select) {
            let selectValid = validateSelectElement(select);

            if (!selectValid && valid)
                valid = false;
        });

    return valid;
}

var validateSelectElement = function (select) {
    let selectValue = $(select).val();
    let validationMessage = $(select).data('val-required');
    let name = $(select).attr('name');

    var validationSpan = $('span[data-valmsg-for="' + name + '"]');

    if (selectValue == null || selectValue.length == 0) {
        validationSpan.removeClass('field-validation-valid').addClass('field-validation-error').html('<span id="' + name + '-error" class="">' + validationMessage + '</span>');

        return false;
    }
    else validationSpan.removeClass('field-validation-error').addClass('field-validation-valid').html('');


    return true;
}

var setParallax = function () {
    $('.parallax[data-mdb-image-src]').each(function () {
        let imageSrc = $(this).data('mdb-image-src');
        $(this).css('background-image', 'url(' + imageSrc + ')');
    });
}

var scrollToElement = function (element, animateElement = false) {
    document.querySelector(element).scrollIntoView({
        behavior: 'smooth',
        block: 'end'
    });

    if (animateElement) {
        window.setTimeout(function () {
            const changeAnimationEl = document.getElementById(element.replace('#', ''));

            let animation = 'pulse';
            const changeAnimation = new mdb.Animate(changeAnimationEl, {
                animation: animation,
                animationStart: 'onLoad',
                animationDuration: 1000,
                animationRepeat: false,
            });

            changeAnimation.init();
            changeAnimation.stopAnimation();
            changeAnimation.changeAnimationType(animation);
            changeAnimation.startAnimation();
        }, 500);
    }
}