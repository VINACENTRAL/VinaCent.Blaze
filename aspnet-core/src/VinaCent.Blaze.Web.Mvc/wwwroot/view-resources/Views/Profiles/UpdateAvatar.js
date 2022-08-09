(function ($) {
    var src = document.getElementById('crr-user-avt').getAttribute('vnc-src');
    var modal = $('UploadFileModal');

    modal.on('hidden.bs.modal', () => {
        modal.clearForm();
    });

    if (!src) {
        hideAction()
    } else {
        // Init cropper
        initCropper(src);
    }

    $("#image-picker").on('change', (event) => {
        event.preventDefault();
        var el = $(event.currentTarget);
        var file = el.prop('files')[0];
        if (file) {
            var blob = URL.createObjectURL(file);
            initCropper(blob);
        }
    });

    function showAction() {
        $(".action-no-image").hide();
        $(".action-container").show();
    }

    function hideAction() {
        $(".action-no-image").show();
        $(".action-container").hide();
    }


})(jQuery);