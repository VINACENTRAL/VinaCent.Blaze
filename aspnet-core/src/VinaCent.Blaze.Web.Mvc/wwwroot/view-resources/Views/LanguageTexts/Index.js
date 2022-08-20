$(() => {
    const l = abp.localization.getSource('Blaze');

    const renderTranslateSetUrl = 'admincp/language-texts/render-group-modal?id=';

    const services = abp.services.app.languageTextManagement;
    const transateSetModal = $('#translate-set-modal');

    $('#translate-set-btn').click(() => {
        abp.ajax({
            url: abp.appPath + renderTranslateSetUrl,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                transateSetModal.find('div.modal-content').html(content);
                transateSetModal.modal('show');

                initForm();
            },
            error: function (e) {
            }
        })
    });

    function initForm() {
        const _$form = transateSetModal.find('form');

        _$form.find('.save-button').on('click', (e) => {
            e.preventDefault();

            if (!_$form.valid()) {
                return;
            }

            var dataSet = _$form.serializeFormToObject();

            abp.ui.setBusy(transateSetModal);
            services
                .saveGroupLanguageText(dataSet)
                .done(function () {
                    transateSetModal.modal('hide');
                    transateSetModal.find('div.modal-content').html('');
                    abp.notify.success(l(LKConstants.SavedSuccessfully));

                    $('#language-text-table').dataTable().fnDraw();
                })
                .always(function () {
                    abp.ui.clearBusy(transateSetModal);
                });
        });
    }
});