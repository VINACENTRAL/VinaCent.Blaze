(function ($) {
    const _emailerService = abp.services.app.emailer,
        l = abp.localization.getSource('Blaze'),
        _$form = $('form#SetupEmailerFrom');

    // Start process for UI of SMTP Configs
    const useDefaultCredentialsEl = document.getElementById('SmtpUseDefaultCredentials');
    const smptConfigurationArea = document.getElementById('smtp-config');

    function updateUseDefaultCredentialsState() {
        const useDefaultCredentials = useDefaultCredentialsEl.checked;
        setStateForSMTPConfigurationElements(useDefaultCredentials);
    }

    function setStateForSMTPConfigurationElements(isDisabled) {
        smptConfigurationArea.querySelectorAll('input').forEach((input) => {
            input.disabled = isDisabled;
        });
    }

    updateUseDefaultCredentialsState();

    useDefaultCredentialsEl.addEventListener('change', (event) => {
        updateUseDefaultCredentialsState();
    });
    // End process for UI of SMTP Configs

    // Start process for UI of save Setup Emailer
    _$form.find('.save-button').click(function (e) {
        e.preventDefault();

        if (!_$form.valid()) {
            return;
        }

        abp.message.confirm(
            l(LKConstants.DoYouReallyWantToSaveTheseChanges),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    var setupEmailer = _$form.serializeFormToObject();

                    abp.ui.setBusy(_$form);

                    _emailerService
                        .saveSetup(setupEmailer)
                        .done(function () {
                            abp.notify.info(l(LKConstants.SavedSuccessfully));
                        })
                        .always(function () {
                            abp.ui.clearBusy(_$form);
                        });
                }
            }
        );
    });
    // Start process for UI of save Setup Emailer

    // Start process for Test Setup Emailer
    var _$formTest = $('#TestEmailSenderFrom');

    _$formTest.submit(function (e) {
        e.preventDefault();

        if (!_$formTest.valid()) {
            return;
        }

        abp.message.confirm(
            l(LKConstants.SendTestEmailSenderMessage),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    var data = _$formTest.serializeFormToObject();

                    abp.ui.setBusy(_$formTest);

                    _emailerService
                        .testEmailSender(data)
                        .done(function () {
                            abp.notify.success(l(LKConstants.EmailHasBeenSentSuccessfully));
                        })
                        .always(function () {
                            abp.ui.clearBusy(_$formTest);
                        });
                }
            }
        );
    });
    // End process for Test Setup Emailer
})(jQuery);
