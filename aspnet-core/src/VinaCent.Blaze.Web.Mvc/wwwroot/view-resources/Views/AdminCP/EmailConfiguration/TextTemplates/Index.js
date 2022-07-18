(function ($) {
    var _textTemplateService = abp.services.app.textTemplate,
        l = abp.localization.getSource('Blaze'),
        _$modal = $('#TextTemplateCreateModal'),
        _$form = _$modal.find('form'),
        _$modalTest = $('#TextTemplateTestModal'),
        _$formTest = _$modalTest.find('form'),
        _$table = $('#TextTemplatesTable');

    var _$textTemplatesTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        listAction: {
            ajaxFunction: _textTemplateService.getAll,
            inputFilter: function () {
                return $('#TextTemplatesSearchForm').serializeFormToObject(true);
            }
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                className: 'waves-effect waves-light',
                action: () => _$textTemplatesTable.draw(false)
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
                data: 'name',
                sortable: false,
                render: data => `<div class="fw-bold">${l(data)}</div><div class="text-muted small">${data}</div>`
            },
            {
                targets: 2,
                data: 'isStatic',
                sortable: false,
                render: data => `<input type="checkbox" disabled ${data ? 'checked' : ''}>`
            },
            {
                targets: 3,
                data: null,
                sortable: false,
                autoWidth: false,
                defaultContent: '',
                render: (data, type, row, meta) => {
                    return [
                        `   <button type="button" class="btn btn-sm btn-success test-textTemplate" data-bs-textTemplateId="${row.id}" data-bs-title="${encodeURIComponent(`<div class="fw-bold">${l(row.name)}</div><div class="text-muted small">${row.name}</div>`)}" data-bs-toggle="modal" data-bs-target="#TextTemplateTestModal">`,
                        `       <i class="mdi mdi-send"></i> ${l(LKConstants.Test)}`,
                        '   </button>',
                        `   <button type="button" class="btn btn-sm btn-warning edit-textTemplate" data-textTemplate-id="${row.id}" data-bs-toggle="modal" data-bs-target="#TextTemplateEditModal">`,
                        `       <i class="fas fa-pencil-alt"></i> ${l(LKConstants.Edit)}`,
                        '   </button>',
                        `   <button type="button" class="btn btn-sm btn-danger ${row.isStatic ? 'disabled' : 'delete-textTemplate'}" ${row.isStatic ? '' : `data-textTemplate-id="${row.id}" data-tenancy-name="${row.name}"`}>`,
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

        var textTemplate = _$form.serializeFormToObject();

        abp.ui.setBusy(_$modal);

        _textTemplateService
            .create(textTemplate)
            .done(function () {
                _$modal.modal('hide');
                _$form[0].reset();
                abp.notify.info(l(LKConstants.SavedSuccessfully));
                _$textTemplatesTable.ajax.reload();
            })
            .always(function () {
                abp.ui.clearBusy(_$modal);
            });
    });

    $(document).on('click', '.delete-textTemplate', function () {
        var textTemplateId = $(this).attr('data-textTemplate-id');
        var tenancyName = $(this).attr('data-tenancy-name');

        deleteTextTemplate(textTemplateId, tenancyName);
    });

    $(document).on('click', '.edit-textTemplate', function (e) {
        var textTemplateId = $(this).attr('data-textTemplate-id');

        abp.ajax({
            url: abp.appPath + 'admincp/email-configuration/text-templates/edit-modal?id=' + textTemplateId,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#TextTemplateEditModal div.modal-content').html(content);
            },
            error: function (e) {
            }
        });
    });

    abp.event.on('textTemplate.edited', (data) => {
        _$textTemplatesTable.ajax.reload();
    });

    function deleteTextTemplate(textTemplateId, tenancyName) {
        abp.message.confirm(
            abp.utils.formatString(
                l('AreYouSureWantToDelete'),
                tenancyName
            ),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    _textTemplateService
                        .delete({
                            id: textTemplateId
                        })
                        .done(() => {
                            abp.notify.info(l(LKConstants.SuccessfullyDeleted));
                            _$textTemplatesTable.ajax.reload();
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
        _$textTemplatesTable.ajax.reload();
    });

    $('.btn-clear').on('click', (e) => {
        $('input[name=Keyword]').val('');
        $('input[name=IsActive][value=""]').prop('checked', true);
        _$textTemplatesTable.ajax.reload();
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which == 13) {
            _$textTemplatesTable.ajax.reload();
            return false;
        }
    });

    // Start extend
    _$modalTest.on('shown.bs.modal', (event) => {
        _$modalTest.find('input:not([type=hidden]):first').focus();
        // Set value
        // Button that triggered the modal
        var button = event.relatedTarget
        // Extract info from data-bs-* attributes
        var textTemplateId = button.getAttribute('data-bs-textTemplateId');
        var title = button.getAttribute('data-bs-title');
        title = decodeURIComponent(title);

        $('#test-text-template-title').html(title);
        $('#test-text-template-id').val(textTemplateId);
    }).on('hidden.bs.modal', () => {
        _$formTest.clearForm();
    });

    _$formTest.find('.save-button').click(function (e) {
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

                    abp.ui.setBusy(_$modalTest);

                    _textTemplateService
                        .testTextTemplate(data)
                        .done(function () {
                            _$modalTest.modal('hide');
                            _$formTest[0].reset();
                            abp.notify.success(l(LKConstants.EmailHasBeenSentSuccessfully));
                        })
                        .always(function () {
                            abp.ui.clearBusy(_$modalTest);
                        });
                }
            }
        );
    });
    // End extend
})(jQuery);
