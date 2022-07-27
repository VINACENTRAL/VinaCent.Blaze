(function ($) {
    var _languageService = abp.services.app.languageManagement,
        l = abp.localization.getSource('Blaze'),
        _$modal = $('#LanguageCreateModal'),
        _$form = _$modal.find('form'),
        _$table = $('#LanguagesTable');

    var _$languagesTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        listAction: {
            ajaxFunction: _languageService.getAll,
            inputFilter: function () {
                return $('#LanguagesSearchForm').serializeFormToObject(true);
            }
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                className: 'waves-effect waves-light',
                action: () => _$languagesTable.draw(false)
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
                data: 'displayName',
                sortable: false
            },
            {
                targets: 2,
                data: 'name',
                sortable: false
            },
            {
                targets: 3,
                data: 'isDefault',
                sortable: false,
                render: function (data, type, row, meta) {
                    return `<i class="${data == true ? 'ri-checkbox-circle-fill text-success' : ' ri-close-circle-fill text-danger'}"></i>`;
                }
            },
            {
                targets: 4,
                data: 'isDisabled',
                sortable: false,
                render: function (data, type, row, meta) {
                    return `<i class="${data != true ? 'ri-checkbox-circle-fill text-success' : ' ri-close-circle-fill text-danger'}"></i>`;
                }
            },
            {
                targets: 5,
                data: 'icon',
                sortable: false,
                render: function (data, type, row, meta) {
                    return `<img src="/vinacent/flags/4x3/${data}" height="18">`;
                }
            },
            {
                targets: 6,
                data: null,
                sortable: false,
                autoWidth: false,
                defaultContent: '',
                render: (data, type, row, meta) => {
                    return [
                        `   <button type="button" class="btn btn-sm btn-warning edit-language" data-language-id="${row.id}" data-bs-toggle="modal" data-bs-target="#LanguageEditModal">`,
                        `       <i class="fas fa-pencil-alt"></i> ${l('Edit')}`,
                        '   </button>',
                        `   <button type="button" class="btn btn-sm btn-danger delete-language" data-language-id="${row.id}" data-language-name="${row.name}">`,
                        `       <i class="fas fa-trash"></i> ${l('Delete')}`,
                        '   </button>',
                    ].join('');
                }
            }
        ]
    });

    _$form.find('.save-button').on('click', (e) => {
        e.preventDefault();

        if (!_$form.valid()) {
            return;
        }

        var language = _$form.serializeFormToObject();

        abp.ui.setBusy(_$modal);
        _languageService
            .create(language)
            .done(function () {
                _$modal.modal('hide');
                _$form[0].reset();
                abp.notify.info(l('SavedSuccessfully'));
                _$languagesTable.ajax.reload();
            })
            .always(function () {
                abp.ui.clearBusy(_$modal);
            });
    });

    $(document).on('click', '.delete-language', function () {
        var languageId = $(this).attr("data-language-id");
        var languageName = $(this).attr('data-language-name');

        deleteLanguage(languageId, languageName);
    });

    $(document).on('click', '.edit-language', function (e) {
        var languageId = $(this).attr("data-language-id");

        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'admincp/languages/edit-modal?languageId=' + languageId,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#LanguageEditModal div.modal-content').html(content);
            },
            error: function (e) {
            }
        })
    });

    abp.event.on('language.edited', (data) => {
        _$languagesTable.ajax.reload();
    });

    function deleteLanguage(languageId, languageName) {
        abp.message.confirm(
            abp.utils.formatString(
                l('AreYouSureWantToDelete'),
                languageName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    _languageService.delete({
                        id: languageId
                    }).done(() => {
                        abp.notify.info(l('SuccessfullyDeleted'));
                        _$languagesTable.ajax.reload();
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
        _$languagesTable.ajax.reload();
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which == 13) {
            _$languagesTable.ajax.reload();
            return false;
        }
    });
})(jQuery);
