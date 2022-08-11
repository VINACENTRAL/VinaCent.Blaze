$(function () {
    const l = abp.localization.getSource('Blaze');

    const _profileService = abp.services.app.profile;
    const personalInfoForm = $('#PersonalInfoForm');
    const changePasswordForm = $('#ChangePasswordForm');

    const confirmChangeEmailModal = $('#ConfirmChangeEmailModal');
    const confirmChangeEmailForm = confirmChangeEmailModal.find('form');
    const sendCodeEmailBtn = confirmChangeEmailForm.find('.send-code-btn');
    const sendCodeEmailBtn_TimeRemaining = sendCodeEmailBtn.children('span');

    const changeEmailModal = $('#ChangeEmailModal');
    const changeEmailForm = changeEmailModal.find('form');
    const sendCodeBtn = changeEmailForm.find('.send-code-btn');
    const sendCodeBtn_TimeRemaining = sendCodeBtn.children('span');

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

    confirmChangeEmailModal.on('shown.bs.modal', () => {

    }).on('hidden.bs.modal', () => {
        confirmChangeEmailForm.clearForm();
    });

    sendCodeEmailBtn.click((e) => {
        sendCodeEmailBtn.attr('disabled', 'true');
        let waitTimeRemaining = CommonTimer.BlockElementTimeInSeconds + 1;

        const email = document.getElementById('user-email').value;
        abp.ajax({
            url: abp.appPath + 'profile/send-code?emailAddress=' + email,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                const tokenInput = document.getElementById('Token');
                var obj = JSON.parse(content);
                tokenInput.value = obj.result;

                setInterval(() => {
                    waitTimeRemaining--;
                    if (waitTimeRemaining <= 0) {
                        sendCodeEmailBtn_TimeRemaining.html('');
                        sendCodeEmailBtn.removeAttr('disabled');
                    } else {
                        sendCodeEmailBtn_TimeRemaining.html(`(${waitTimeRemaining}s)`);
                        sendCodeEmailBtn.attr('disabled', 'true');
                    }
                }, 1000);
            },
            error: function (e) {
            }
        });
    });

    confirmChangeEmailForm.submit((e) => {
        e.preventDefault();
        if (!confirmChangeEmailForm.valid()) {
            return;
        }
        var verify = confirmChangeEmailForm.serializeFormToObject();
        abp.ui.setBusy(confirmChangeEmailModal);
        _profileService
            .confirmCode(verify)
            .done(function (result) {
                confirmChangeEmailModal.modal('hide');
                confirmChangeEmailForm[0].reset();
                abp.event.trigger('changeEmailNextStep');
            })
            .always(function () {
                abp.ui.clearBusy(confirmChangeEmailModal);
            });
    });

    abp.event.on('changeEmailNextStep', () => {
        changeEmailModal.modal('show')
    });

    sendCodeBtn.click((e) => {
        sendCodeBtn.attr('disabled', 'true');
        let waitTimeRemaining = CommonTimer.BlockElementTimeInSeconds + 1;

        const email = document.getElementById('NewEmail').value;
        abp.ajax({
            url: abp.appPath + 'profile/send-code?emailAddress=' + email,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                const tokenInput = document.getElementById('VerifyToken');
                var obj = JSON.parse(content);
                tokenInput.value = obj.result;

                setInterval(() => {
                    waitTimeRemaining--;
                    if (waitTimeRemaining <= 0) {
                        sendCodeBtn_TimeRemaining.html('');
                        sendCodeBtn.removeAttr('disabled');
                    } else {
                        sendCodeBtn_TimeRemaining.html(`(${waitTimeRemaining}s)`);
                        sendCodeBtn.attr('disabled', 'true');
                    }
                }, 1000);
            },
            error: function (e) {
            }
        });
    });

    changeEmailForm.submit((e) => {
        e.preventDefault();
        if (!changeEmailForm.valid()) {
            return;
        }
        var verify = changeEmailForm.serializeFormToObject();
        abp.ui.setBusy(changeEmailModal);
        _profileService
            .changeEmail(verify)
            .done(function (result) {
                changeEmailModal.modal('hide');
                changeEmailForm[0].reset();
                window.location.reload();
            })
            .always(function () {
                abp.ui.clearBusy(changeEmailModal);
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
