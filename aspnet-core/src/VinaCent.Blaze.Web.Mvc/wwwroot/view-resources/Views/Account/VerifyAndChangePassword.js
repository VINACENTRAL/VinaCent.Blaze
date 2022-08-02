(function ($) {
    $('#ChangePasswordWasSuccessfulContainer').hide();

    var _accountService = abp.services.app.account,
        l = abp.localization.getSource('Blaze'),
        _$form = $('#VerifyAndChangePasswordForm');

    _$form.find('.save-button').on('click', (e) => {
        e.preventDefault();

        if (!_$form.valid()) {
            return;
        }

        var data = _$form.serializeFormToObject();

        abp.ui.setBusy(_$form);
        _accountService
            .resetPassword(data)
            .done(function () {
                abp.notify.success(l(LKConstants.PasswordResetSuccessful));
                $('#ChangePasswordContainer').hide(300);
                $('#ChangePasswordWasSuccessfulContainer').show(300);
            })
            .always(function () {
                abp.ui.clearBusy(_$form);
            });
    });

})(jQuery);
