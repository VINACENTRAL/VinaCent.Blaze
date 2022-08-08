(function ($) {
    var src = document.getElementById('crr-user-avt').attr('vnc-src');
    if (!src) {
        hideAction()
    } else {
        // Init cropper
        initCropper(src);
    }

    function showAction() {
        $(".action-no-image").hide();
        $(".action-container").show();
    }

    function hideAction() {
        $(".action-no-image").show();
        $(".action-container").hide();
    }
})(jQuery);