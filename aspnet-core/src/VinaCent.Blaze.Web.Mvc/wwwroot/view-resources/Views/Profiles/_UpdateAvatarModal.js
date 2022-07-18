(function ($) {
    const l = abp.localization.getSource('Blaze'),
        _$modal = $('#UpdateAvatarModal'),
        _$form = _$modal.find('form'),
        _$saveBtn = _$form.closest('div.modal-content').find(".save-button");

    const currentUserAvatarEl = $('#crr-user-avt');
    const imagePicker = $("#image-picker");

    let src = currentUserAvatarEl.children('img[avatar]').attr('src');
    
    // remove inner
    if (src === abp.setting.get('App.SiteUserAvatarHolder')) src = '';
    currentUserAvatarEl.html('');

    _$form.find('.select-image').on('click', (e) => {
        e.preventDefault();
        imagePicker.click();
    });

    imagePicker.on('change', () => {
        const file = imagePicker.prop('files')[0];
        if (file) {
            const blob = URL.createObjectURL(file);
            initCropper(blob);
        }
    });

    function initCropper(source, oneDone) {
        currentUserAvatarEl.croppie('destroy');
        const mc = currentUserAvatarEl.croppie({
            viewport: {
                width: 300,
                height: 300,
                type: 'circle'
            },
            boundary: {
                width: 300,
                height: 300
            },
        });

        var bindPromise = mc.croppie('bind', {
            url: source,
        });

        if (typeof oneDone === 'function') {
            bindPromise.then(oneDone);
        }

        _$saveBtn.unbind();
        _$saveBtn.on('click', function (ev) {
            ev.preventDefault();
            ev.stopPropagation();

            mc.croppie('result', {
                type: 'blob',
                circle: false,
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
        fd.append('File', blob, `avatar~${new Date().getTime()}.png`);

        abp.ajax({
            url: abp.appPath + 'api/services/app/Profile/UpdateAvatar',
            type: 'PUT',
            data: fd,
            processData: false,
            contentType: false,
        }).done(function (res) {
            _$modal.modal('hide');
            _$form[0].reset();
            src = res.avatar;
            $('img[self-avatar]').attr('src', res.avatar);
            abp.notify.info(l(LKConstants.SavedSuccessfully));
        }).always(function () {
            abp.ui.clearBusy(_$form);
        });
    }

    _$modal.on('shown.bs.modal', () => {
        if (src) {
            abp.ui.setBusy(_$modal);
            initCropper(src, () => {
                abp.ui.clearBusy(_$modal);
            });
        }
    }).on('hidden.bs.modal', () => {
        currentUserAvatarEl.croppie('destroy');
        _$form.clearForm();
    });
})(jQuery);