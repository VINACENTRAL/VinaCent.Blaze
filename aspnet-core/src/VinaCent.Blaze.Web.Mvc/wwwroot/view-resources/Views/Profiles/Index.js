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

    personalInfoForm.find('.cancel-btn').on('click', () => {
        abp.message.confirm(
            abp.utils.formatString(
                l(LKConstants.AreYouSureWantToCancel)
            ),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    window.location.reload();
                }
            }
        );
    })

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

    const alert = document.getElementById('confirm-alert');

    confirmChangeEmailModal.on('shown.bs.modal', () => {
        const maskedEmail = document.getElementById('MaskedEmailAddress').value;
        alert.innerHTML += `<strong>${maskedEmail}</strong>`
    }).on('hidden.bs.modal', () => {
        confirmChangeEmailForm.clearForm();
        const emailStr = alert.getElementsByTagName('strong')[0];
        emailStr.remove();
    });

    sendCodeEmailBtn.click((e) => {
        sendCodeEmailBtn.attr('disabled', 'true');
        let waitTimeRemaining = CommonTimer.BlockElementTimeInSeconds + 1;

        const sendCode = (email) => {
            if (!email || email?.length === 0 || !email.match(AvailableRegexs.EmailChecker)) {
                abp.notify.error(l(LKConstants.InvalidEmail));
                return;
            }
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
        }

        _profileService.get().done((r) => sendCode(r?.emailAddress));
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
            .done(function () {
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

                const intervalId = setInterval(() => {
                    waitTimeRemaining--;
                    if (waitTimeRemaining <= 0) {
                        sendCodeBtn_TimeRemaining.html('');
                        sendCodeBtn.removeAttr('disabled');
                        clearInterval(intervalId);
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
            .done(function () {
                changeEmailModal.modal('hide');
                changeEmailForm[0].reset();
                abp.notifyStack.success(l(LKConstants.ChangeEmailSuccessMessage));
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

        if (input['NewPassword'] !== input['NewPasswordConfirm'] || input['NewPassword'].length === 0) {
            abp.message.error(l(LKConstants.NewPasswordConfirmFailed));
            return;
        }

        if (!input || input['CurrentPassword'].length === 0) {
            return;
        }

        _profileService.changePassword(input).then(function () {
            abp.message.success(l(LKConstants.YouWillSignOutAllOfYourDevicesForSecurity),
                l(LKConstants.PasswordChanged), {
                closeOnEsc: false,
                closeOnClickOutside: false,
                timer: null
            })
                .then((result) => {
                    if (result) {
                        window.location.reload();
                    }
                });
            abp.event.trigger('passwordChanged');
            changePasswordForm.trigger("reset");
        });
    });

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

    //-------------------
    const snContainerEl = $('#social-network-container');
    const lsnrj = $("#ListSocialNetworkRawJson").val();
    if (lsnrj && lsnrj.length > 0) {
        var socials = JSON.parse(lsnrj);
        if (socials) {
            socials.forEach((el) => {
                generateSocial(el.icon, el.url, el.name);
            })
        }
    } else {
        generateSocialTest();
    }

    function generateSocial(icon, url, name) {
        snContainerEl.append(`
            <div class="mb-3 d-flex align-items-center">
                <div class="avatar-xs d-block flex-shrink-0 me-3">
                    <span class="avatar-title rounded-circle fs-16 bg-dark text-light shadow">
                        <i class="${icon}"></i>
                    </span>
                </div>
                <div class="input-group">
                    <input type="email" class="form-control" id="gitUsername" placeholder="Username" value="${name}" readonly>
                    <div type="button" class="btn btn-danger" id="social-network-delete">
                        <i class="fas fa-trash"></i>
                    </div>
                </div>
            </div>
        `);
        //initSocialRemoveIcon();
    }

    function generateSocialTest() {
        snContainerEl.append(`
            <div class="mb-3 d-flex align-items-center">
                <div class="avatar-xs d-block flex-shrink-0 me-3 social-box-icon">
                    <span class="avatar-title rounded-circle fs-16 bg-dark text-light shadow normal">
                        <i class="ri-github-fill"></i>
                    </span>
                    <span class="avatar-title rounded-circle fs-16 bg-danger text-light shadow removal">
                        <i class="mdi mdi-close"></i>
                    </span>
                </div>
                <input type="email" class="form-control" id="gitUsername" placeholder="Username" value="@daveadame" readonly>
            </div>
        `);
        //initSocialRemoveIcon();
    }
});
