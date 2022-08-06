(function ($) {
    var _currencyExchangeRateService = abp.services.app.currencyUnit,
        l = abp.localization.getSource('Blaze'),
        _$modal = $('#CurrencyExchangeRateEditModal'),
        _$form = _$modal.find('form');

    function save() {
        if (!_$form.valid()) {
            return;
        }

        var currencyExchangeRate = _$form.serializeFormToObject();

        abp.ui.setBusy(_$form);
        _currencyExchangeRateService.update(currencyExchangeRate).done(function () {
            _$modal.modal('hide');
            abp.notify.info(l(LKConstants.SavedSuccessfully));
            abp.event.trigger('currencyExchangeRate.edited', currencyExchangeRate);
        }).always(function () {
            abp.ui.clearBusy(_$form);
        });
    }

    _$form.closest('div.modal-content').find(".save-button").click(function (e) {
        e.preventDefault();
        save();
    });

    _$form.find('input').on('keypress', function (e) {
        if (e.which === 13) {
            e.preventDefault();
            save();
        }
    });

    _$modal.on('shown.bs.modal', function () {
        _$form.find('input[type=text]:first').focus();
    });
})(jQuery);
