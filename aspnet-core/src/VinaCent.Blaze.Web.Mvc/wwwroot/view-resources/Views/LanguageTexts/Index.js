(function ($) {
    var _languageTextService = abp.services.app.languageTextManagement,
        l = abp.localization.getSource('Blaze'),
        _$modal = $('#LanguageTextCreateModal'),
        _$form = _$modal.find('form'),
        _$table = $('#LanguageTextsTable');

    var _$languageTextsTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        listAction: {
            ajaxFunction: _languageTextService.getAll,
            inputFilter: function () {
                return $('#LanguageTextsSearchForm').serializeFormToObject(true);
            }
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                className: 'waves-effect waves-light',
                action: () => _$languageTextsTable.draw(false)
            }
        ],
        responsive: {
            details: {
                type: 'column'
            }
        },
        columnDefs: [
            {
                targets: 0,
                className: 'control',
                defaultContent: '',
            },
            {
                targets: 1,
                data: 'key',
                sortable: false
            },
            {
                targets: 2,
                data: 'defaultValue',
                sortable: false
            },
            {
                targets: 3,
                data: 'value',
                sortable: false,
            },
            {
                targets: 4,
                data: 'source',
                sortable: false,
            },
            {
                targets: 5,
                data: null,
                sortable: false,
                autoWidth: false,
                defaultContent: '',
                render: (data, type, row, meta) => {
                    return [
                        `   <button type="button" class="btn btn-sm btn-warning edit-languageText" data-languageText-id="${row.id}" data-bs-toggle="modal" data-bs-target="#LanguageTextEditModal">`,
                        `       <i class="fas fa-pencil-alt"></i> ${l(LKConstants.Edit)}`,
                        '   </button>',
                        `   <button type="button" class="btn btn-sm btn-danger delete-languageText" data-languageText-id="${row.id}" data-tenancy-name="${row.name}">`,
                        `       <i class="fas fa-trash"></i> ${l(LKConstants.Delete)}`,
                        '   </button>'
                    ].join('');
                }
            }
        ]
    });

    _$form.find('.save-button').click(function (e) {
        e.preventDefault();

        if (!_$form.valid()) {
            return;
        }

        var languageText = _$form.serializeFormToObject();

        abp.ui.setBusy(_$modal);

        _languageTextService
            .create(languageText)
            .done(function () {
                _$modal.modal('hide');
                _$form[0].reset();
                abp.notify.info(l(LKConstants.SavedSuccessfully));
                _$languageTextsTable.ajax.reload();
            })
            .always(function () {
                abp.ui.clearBusy(_$modal);
            });
    });

    $(document).on('click', '.delete-languageText', function () {
        var languageTextId = $(this).attr('data-languageText-id');
        var tenancyName = $(this).attr('data-tenancy-name');

        deleteLanguageText(languageTextId, tenancyName);
    });

    $(document).on('click', '.edit-languageText', function (e) {
        var languageTextId = $(this).attr('data-languageText-id');

        abp.ajax({
            url: abp.appPath + 'admincp/language-texts/edit-modal?languageTextId=' + languageTextId,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#LanguageTextEditModal div.modal-content').html(content);
            },
            error: function (e) {
            }
        });
    });

    abp.event.on('languageText.edited', (data) => {
        _$languageTextsTable.ajax.reload();
    });

    function deleteLanguageText(languageTextId, tenancyName) {
        abp.message.confirm(
            abp.utils.formatString(
                l(LKConstants.AreYouSureWantToDelete),
                tenancyName
            ),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    _languageTextService
                        .delete({
                            id: languageTextId
                        })
                        .done(() => {
                            abp.notify.info(l(LKConstants.SuccessfullyDeleted));
                            _$languageTextsTable.ajax.reload();
                        });
                }
            }
        );
    }

    _$modal.on('shown.bs.modal', () => {
        _$modal.find('input:not([type=hidden]):first').focus();
    }).on('hidden.bs.modal', () => {
        _$form.clearForm();
    });

    $('.btn-search').on('click', (e) => {
        _$languageTextsTable.ajax.reload();
    });

    $('.btn-clear').on('click', (e) => {
        $('input[name=Keyword]').val('');
        $('input[name=IsActive][value=""]').prop('checked', true);
        _$languageTextsTable.ajax.reload();
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which == 13) {
            _$languageTextsTable.ajax.reload();
            return false;
        }
    });
})(jQuery);
