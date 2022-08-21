﻿$(() => {
    const l = abp.localization.getSource('Blaze');

    const renderTranslateSetUrl = 'admincp/language-texts/render-group-modal?id=';

    const services = abp.services.app.languageTextManagement;
    const transateSetModal = $('#translate-set-modal');

    $(document).on('click', `button[data-translate-set]`, function (e) {
        let id = $(e.currentTarget).data('translate-set');
        loadModalContent(id);
    });

    function loadModalContent(id) {
        if (!id || id <= 0) id = '';

        abp.ajax({
            url: abp.appPath + renderTranslateSetUrl + id,
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
    }

    function initForm() {
        const _$form = transateSetModal.find('form');

        const refId = _$form.find('input[name="RefLanguageTextId"]').val() + '';
        if (!refId || refId?.length === 0 || refId <= 0) {
            autoLoadExistTranslate(_$form);
        }

        _$form.find('input').on('keypress', function (e) {
            if (e.which === 13) {
                e.preventDefault();
                _$form.find('.save-button').click();
            }
        });

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

    // ============================ START AUTO FETCH ============================ //
    function fetchExistRecords(_$form) {
        var dataSet = _$form.serializeFormToObject();
        dataSet["Pairs"] = null;

        abp.ui.setBusy(transateSetModal);
        services
            .getGroupLanguageTextAlreadyExists(dataSet)
            .done(function (result) {
                _$form.find('input[name="RefLanguageTextId"]').val(result.refLanguageTextId);
                result.pairs.forEach((e) => {
                    _$form.find(`input[data-pair="${e.languageName}"]`).val(e.value);
                });

                if (result && result?.refLanguageTextId > 0) {

                    abp.notify.info(l(LKConstants.SystemFoundThisKeyWasHaveExistsTranslate));
                }
            })
            .always(function () {
                abp.ui.clearBusy(transateSetModal);
            });
    }

    function autoLoadExistTranslate(_$form) {
        const checkContainer = _$form[0].querySelector('[data-check-exist]');

        let previousTimeoutId = null;
        checkContainer.querySelector('input').oninput = (e) => {
            clearTimeout(previousTimeoutId);
            previousTimeoutId = setTimeout(() => {
                fetchExistRecords(_$form);
            }, 1000);
        }

        checkContainer.querySelector('button').addEventListener('click', () => {
            fetchExistRecords(_$form);
        });
    }
    // ============================  END AUTO FETCH  ============================ //

});

function RenderAction(row, masterName, defaultButtons) {
    $('#default-lang').html($("#DefaultLanguageName").find(":selected").html());
    $('#current-lang').html($("#CurrentLanguageName").find(":selected").html());
    const l = abp.localization.getSource('Blaze');
    return [
        `   <button type="button" class="btn btn-sm waves-effect waves-light btn-primary" data-translate-set="${row.id}">`,
        `       <i class="mdi mdi-view-dashboard-edit"></i> ${l(LKConstants.UpdateTranslateSet)}`,
        '   </button>',
        `   <button type="button" class="btn btn-sm waves-effect waves-light btn-warning edit-${masterName.toLowerCase()}" data-${masterName.toLowerCase()}-id="${row.id}">`,
        `       <i class="fas fa-pencil-alt"></i> ${l(LKConstants.Edit)} (${row.languageName})`,
        '   </button>',
        `   <button type="button" class="btn btn-sm waves-effect waves-light btn-danger delete-${masterName.toLowerCase()}" data-${masterName.toLowerCase()}-id="${row.id}" data-${masterName.toLowerCase()}-name="${row.value}">`,
        `       <i class="fas fa-trash"></i> ${l(LKConstants.Delete)} (${row.languageName})`,
        '   </button>'
    ].join('');
}