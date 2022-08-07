(function ($) {
    var _currencyUnitManagementService = abp.services.app.currencyUnit,
        l = abp.localization.getSource('Blaze'),
        _$modal = $('#CurrencyUnitManagementCreateModal'),
        _$form = _$modal.find('form'),
        _$table = $('#CurrencyUnitManagementTable');

    var _$currencyUnitManagementsTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        listAction: {
            ajaxFunction: _currencyUnitManagementService.getAll,
            inputFilter: function () {
                return $('#CurrencyUnitManagementSearchForm').serializeFormToObject(true);
            }
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                className: 'waves-effect waves-light',
                action: () => _$currencyUnitManagementsTable.draw(false)
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
                data: 'currencyNativeName',
                sortable: false,
                render: function (data, type, row, meta) {
                    return `<div class="d-flex"><i class="me-1 ${row.isActive == true ? 'ri-checkbox-circle-fill text-success' : ' ri-close-circle-fill text-danger'}"></i> ${data} ${row.isDefault == true ? '<i class="ms-1 mdi mdi-pin text-danger"></i>' : ''}</div>`;
                }
            },
            {
                targets: 2,
                data: 'currencyDecimalSeparator',
                sortable: false
            },
            {
                targets: 3,
                data: 'currencyGroupSeparator',
                sortable: false
            },
            {
                targets: 4,
                data: 'currencySymbol',
                sortable: false
            },
            {
                targets: 5,
                data: 'isoCurrencySymbol',
                sortable: false
            },
            {
                targets: 6,
                data: null,
                sortable: false,
                autoWidth: false,
                defaultContent: '',
                render: (data, type, row, meta) => {
                    return [
                        `   <button type="button" class="btn btn-sm btn-warning edit-currencyUnitManagement" data-currencyUnitManagement-id="${row.id}" data-bs-toggle="modal" data-bs-target="#CurrencyUnitManagementEditModal">`,
                        `       <i class="fas fa-pencil-alt"></i> ${l(LKConstants.Edit)}`,
                        '   </button>',
                        `   <button type="button" class="btn btn-sm btn-danger delete-currencyUnitManagement" data-currencyUnitManagement-id="${row.id}" data-currencyUnitManagement-name="${row.name}">`,
                        `       <i class="fas fa-trash"></i> ${l(LKConstants.Delete)}`,
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

        var currencyUnitManagement = _$form.serializeFormToObject();

        abp.ui.setBusy(_$modal);
        _currencyUnitManagementService
            .create(currencyUnitManagement)
            .done(function () {
                _$modal.modal('hide');
                _$form[0].reset();
                abp.notify.info(l(LKConstants.SavedSuccessfully));
                _$currencyUnitManagementsTable.ajax.reload();
            })
            .always(function () {
                abp.ui.clearBusy(_$modal);
            });
    });

    $(document).on('click', '.delete-currencyUnitManagement', function () {
        var currencyUnitManagementId = $(this).attr("data-currencyUnitManagement-id");
        var currencyUnitManagementName = $(this).attr('data-currencyUnitManagement-name');

        deleteCurrencyUnitManagement(currencyUnitManagementId, currencyUnitManagementName);
    });

    $(document).on('click', '.edit-currencyUnitManagement', function (e) {
        var currencyUnitManagementId = $(this).attr("data-currencyUnitManagement-id");

        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'businesscp/currency-units/edit-modal?id=' + currencyUnitManagementId,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#CurrencyUnitManagementEditModal div.modal-content').html(content);
            },
            error: function (e) {
            }
        })
    });

    abp.event.on('currencyUnitManagement.edited', (data) => {
        _$currencyUnitManagementsTable.ajax.reload();
    });

    function deleteCurrencyUnitManagement(currencyUnitManagementId, currencyUnitManagementName) {
        abp.message.confirm(
            abp.utils.formatString(
                l(LKConstants.AreYouSureWantToDelete),
                currencyUnitManagementName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    _currencyUnitManagementService.delete({
                        id: currencyUnitManagementId
                    }).done(() => {
                        abp.notify.info(l(LKConstants.SuccessfullyDeleted));
                        _$currencyUnitManagementsTable.ajax.reload();
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
        _$currencyUnitManagementsTable.ajax.reload();
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which == 13) {
            _$currencyUnitManagementsTable.ajax.reload();
            return false;
        }
    });
})(jQuery);
