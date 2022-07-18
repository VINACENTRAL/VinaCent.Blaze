(function ($) {
    var _currencyExchangeRateService = abp.services.app.currencyExchangeRate,
        l = abp.localization.getSource('Blaze'),
        _$modal = $('#CurrencyExchangeRateUpdateModal'),
        _$form = _$modal.find('form');

    function save() {
        if (!_$form.valid()) {
            return;
        }

        var currencyExchangeRate = _$form.serializeFormToObject();

        abp.ui.setBusy(_$modal);
        _currencyExchangeRateService
            .updateExchangeRate(currencyExchangeRate)
            .done(function () {
                _$modal.modal('hide');
                _$form[0].reset();
                abp.notify.info(l(LKConstants.SavedSuccessfully));
                abp.event.trigger('currencyExchangeRate.edited', currencyExchangeRate);
            })
            .always(function () {
                abp.ui.clearBusy(_$modal);
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
