(function ($) {
    $(function () {
        var _profileService = abp.services.app.profile,
            l = abp.localization.getSource('Blaze');

        $('#PersonalInfoForm').submit(function (e) {
            e.preventDefault();

            if (!$('#PersonalInfoForm').valid()) {
                return false;
            }

            var input = $('#PersonalInfoForm').serializeFormToObject();

            _profileService.update(input).then(function (result) {
                abp.notify.info(l(LKConstants.SavedSuccessfully));
                updateConcurrencyStamp();
            });
        });

        $('#ChangePasswordForm').submit(function (e) {
            e.preventDefault();

            if (!$('#ChangePasswordForm').valid()) {
                return false;
            }

            var input = $('#ChangePasswordForm').serializeFormToObject();

            if (
                input.newPassword != input.newPasswordConfirm ||
                input.newPassword == ''
            ) {
                abp.message.error(l(LKConstants.NewPasswordConfirmFailed));
                return;
            }

            if (input.currentPassword && input.currentPassword == '') {
                return;
            }

            _profileService.changePassword(input).then(function (result) {
                abp.message.success(l(LKConstants.PasswordChanged));
                abp.event.trigger('passwordChanged');
                $('#ChangePasswordForm').trigger("reset");
            });
        });

        abp.event.on('passwordChanged', updateConcurrencyStamp);
    });

    function updateConcurrencyStamp() {
        _profileService.get().then(function (profile) {
            $("#ConcurrencyStamp").val(profile.concurrencyStamp);
        });
    }
    
})(jQuery);