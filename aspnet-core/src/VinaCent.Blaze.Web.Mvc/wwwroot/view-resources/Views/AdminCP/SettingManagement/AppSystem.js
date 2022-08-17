(function () {
    const l = abp.localization.getSource('Blaze');

    var _$form = $('#AppSystemForm');

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

// UserLockOut
const userLockoutEnabledEl = document.getElementById('UserManagement_UserLockOut_IsEnabled');
userLockoutEnabledEl.addEventListener('change', () => {
    updateUserLockoutElStates();
});

function updateUserLockoutElStates() {
    const isEnabled = userLockoutEnabledEl.checked;
    document.getElementById('UserManagement_UserLockOut_MaxFailedAccessAttemptsBeforeLockout').disabled = !isEnabled;
    document.getElementById('UserManagement_UserLockOut_DefaultAccountLockoutSeconds').disabled = !isEnabled;
}

// TwoFactorLogin
const twoFactorLoginEnabledEl = document.getElementById('UserManagement_TwoFactorLogin_IsEnabled');
twoFactorLoginEnabledEl.addEventListener('change', (event) => {
    updateTwoFactorLoginElStates();
});

function updateTwoFactorLoginElStates() {
    const isEnabled = twoFactorLoginEnabledEl.checked;
    document.getElementById('UserManagement_TwoFactorLogin_IsEmailProviderEnabled').disabled = !isEnabled;
    document.getElementById('UserManagement_TwoFactorLogin_IsSmsProviderEnabled').disabled = !isEnabled;
    document.getElementById('UserManagement_TwoFactorLogin_IsRememberBrowserEnabled').disabled = !isEnabled;
}

updateUserLockoutElStates();
updateTwoFactorLoginElStates();