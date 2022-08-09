(function ($) {
    var mc;
    const l = abp.localization.getSource('Blaze'),
        _$modal = $('#UpdateAvatarModal'),
        _$form = _$modal.find('form'),
        _$saveBtn = _$form.closest('div.modal-content').find(".save-button");

    _$form.find('#select-image').on('click', (e) => {
        e.preventDefault();
        $('#image-picker').click();
    });

    $("#image-picker").on('change', (event) => {
        const el = $(event.currentTarget);
        const file = el.prop('files')[0];
        if (file) {
            const blob = URL.createObjectURL(file);
            initCropper(blob);
        }
    });
    const src = document.getElementById('crr-user-avt').getAttribute('vnc-src');

    //if (!src) {
    //    hideAction()
    //} else {
    //    // Init cropper
    //    initCropper(src);
    //}

    function showAction() {
        $(".action-no-image").hide();
        $(".action-container").show();
    }

    function hideAction() {
        $(".action-no-image").show();
        $(".action-container").hide();
    }

    function initCropper(source) {
        showAction();
        $('#crr-user-avt').croppie('destroy');
        mc = $('#crr-user-avt');
        mc.croppie({
            viewport: {
                width: 300,
                height: 300,
                type: 'circle'
            },
            boundary: {
                width: 300,
                height: 300
            },
            url: source,
            // enforceBoundary: false
            // mouseWheelZoom: false
        });
        mc.on('update.croppie', function (ev, data) {
            // console.log('jquery update', ev, data);
        });
        _$saveBtn.unbind();
        _$saveBtn.on('click', function (ev) {
            ev.preventDefault();
            ev.stopPropagation();

            mc.croppie('result', {
                type: 'blob',
                circle: false,
                // size: { width: 300, height: 300 },
                format: 'png'
            }).then(function (blob) {
                uploadAvatar(blob);
            });
        });
    }

    function uploadAvatar(blob) {
        abp.ui.setBusy(_$form);
        const form = document.getElementById('uploadAvatarForm');
        var fd = new FormData(form);
        fd.append('File', blob);

        abp.ajax({
            url: abp.appPath + 'api/services/app/Profile/UpdateAvatar',
            type: 'PUT',
            data: fd,
            processData: false,
            contentType: false,
        }).done(function () {
            _$modal.modal('hide');
            _$form[0].reset();
            abp.notify.info(l(LKConstants.SavedSuccessfully));
            window.location.reload();
        }).always(function () {
            abp.ui.clearBusy(_$form);
        });
    }
})(jQuery);