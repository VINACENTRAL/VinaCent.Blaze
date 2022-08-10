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

            _profileService.update(input).done(function (result) {
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

        const modal = $('UpdateAvatarModal');
        const src = document.getElementById('crr-user-avt').getAttribute('vnc-src');

        modal.on('shown.bs.modal', () => {
            console.log(src);
        }).on('hidden.bs.modal', () => {
            modal.clearForm();
        });

        function updateConcurrencyStamp() {
            _profileService.get().then(function (profile) {
                $("#ConcurrencyStamp").val(profile.concurrencyStamp);
            });
        }
    });
})(jQuery);