(function () {
    var _$form = $('#AppMetaForm');

    _$form.submit(function (e) {
        e.preventDefault();

        if (!_$form.valid()) {
            return;
        }

        abp.ui.setBusy(_$form);
        abp.ajax({
            contentType: 'application/x-www-form-urlencoded',
            url: _$form.attr('action'),
            data: _$form.serialize(),
        }).done(function (data) {
            abp.notify.success(LKConstants.SavedSuccessfully);
        }).always(function () {
            abp.ui.clearBusy(_$form);
        });
    });
})();
