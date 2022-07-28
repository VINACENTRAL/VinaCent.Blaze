(function ($) {
    var _languageService = abp.services.app.languageManagement,
        l = abp.localization.getSource('Blaze'),
        _$modal = $('#LanguageEditModal'),
        _$form = _$modal.find('form');

    function save() {
        if (!_$form.valid()) {
            return;
        }

        var language = _$form.serializeFormToObject();

        abp.ui.setBusy(_$form);
        _languageService.update(language).done(function () {
            _$modal.modal('hide');
            abp.notify.info(l(LKConstants.SavedSuccessfully));
            abp.event.trigger('language.edited', language);
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
