(function () {
    const l = abp.localization.getSource('Blaze');

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
        }).done(function () {
            abp.notify.success(l(LKConstants.SavedSuccessfully));
        }).always(function () {
            abp.ui.clearBusy(_$form);
        });
    });
})();
