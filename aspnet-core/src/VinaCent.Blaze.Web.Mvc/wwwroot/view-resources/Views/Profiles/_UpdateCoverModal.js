(function ($) {
    const l = abp.localization.getSource('Blaze'),
        _$modal = $('#UpdateCoverModal'),
        _$form = _$modal.find('form'),
        _$saveBtn = _$form.closest('div.modal-content').find(".save-button");

    const userCoverEl = _$form.find('.profile-wid-img');
    const imagePicker = $("#cover-picker");

    $('#pick-cover').on('click', (e) => {
        e.preventDefault();
        imagePicker.click();
    });

    imagePicker.on('change', () => {
        const file = imagePicker.prop('files')[0];
        if (file) {
            let reader = new FileReader();
            reader.onload = function (event) {
                userCoverEl.attr("src", event.target.result);
            };
            reader.readAsDataURL(file);
            _$saveBtn.removeAttr('disabled');
            _$saveBtn.on('click', function (e) {
                e.preventDefault();
                e.stopPropagation();

                abp.ui.setBusy(_$form);
                const form = document.getElementById('uploadCoverForm');
                var fd = new FormData(form);
                fd.append('File', file, `cover~${new Date().getTime()}.png`);

                abp.ajax({
                    url: abp.appPath + 'api/services/app/Profile/UpdateCover',
                    type: 'PUT',
                    data: fd,
                    processData: false,
                    contentType: false,
                }).done(function (res) {
                    _$modal.modal('hide');
                    _$form[0].reset();
                    $('#user-cover-image').attr('src', res.cover);
                    abp.notify.info(l(LKConstants.SavedSuccessfully));
                }).always(function () {
                    abp.ui.clearBusy(_$form);
                });
            });

            // Show modal
            _$modal.modal('show');
        }
    });

    _$modal.on('shown.bs.modal', () => {
    }).on('hidden.bs.modal', () => {
        _$form.clearForm();
        _$saveBtn.attr('disabled', 'disabled');
    });

})(jQuery);