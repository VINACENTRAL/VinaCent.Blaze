$(function () {
    const l = abp.localization.getSource('Blaze');

    const _profileService = abp.services.app.profile;
    const personalInfoForm = $('#PersonalInfoForm');
    const changePasswordForm = $('#ChangePasswordForm');

    personalInfoForm.submit(function (e) {
        e.preventDefault();

        if (!personalInfoForm.valid()) {
            return false;
        }

        const input = personalInfoForm.serializeFormToObject();

        _profileService.update(input).done(function () {
            abp.notify.info(l(LKConstants.SavedSuccessfully));
            updateConcurrencyStamp();
        });
    });

    changePasswordForm.submit(function (e) {
        e.preventDefault();

        if (!changePasswordForm.valid()) {
            return false;
        }

        const input = changePasswordForm.serializeFormToObject();

        if (input.newPassword !== input.newPasswordConfirm || input.newPassword.length === 0) {
            abp.message.error(l(LKConstants.NewPasswordConfirmFailed));
            return;
        }

        if (!input || input.currentPassword.length === 0) {
            return;
        }

        _profileService.changePassword(input).then(function () {
            abp.message.success(l(LKConstants.PasswordChanged));
            abp.event.trigger('passwordChanged');
            changePasswordForm.trigger("reset");
        });
    });

    abp.event.on('passwordChanged', updateConcurrencyStamp);

    const modal = $('UpdateAvatarModal');
    const src = document.getElementById('crr-user-avt').getAttribute('vnc-src');

    modal.on('shown.bs.modal', () => {

    }).on('hidden.bs.modal', () => {
        modal.clearForm();
    });

    function updateConcurrencyStamp() {
        _profileService.get().then(function (profile) {
            $("#ConcurrencyStamp").val(profile.concurrencyStamp);
        });
    }
});
