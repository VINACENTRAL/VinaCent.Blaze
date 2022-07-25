(function ($) {
    var _fileUnitService = abp.services.app.fileUnit,
        l = abp.localization.getSource('Blaze'),
        _$modal = $('#DirectoryCreateModal'),
        _$form = _$modal.find('form'),
        _$table = $('#FileUnitsTable');
        _$parents = $('#Parents');

    var _$fileUnitsTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        listAction: {
            ajaxFunction: _fileUnitService.getAll,
            inputFilter: function () {
                return $('#FileUnitsSearchForm').serializeFormToObject(true);
            }
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                className: 'waves-effect waves-light',
                action: () => _$fileUnitsTable.draw(false)
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
                defaultContent: '',
                render: (data, type, row, meta) => {
                    return [
                        `<input class="form-check-input m-0 align-middle" type="checkbox">`
                    ]
                }
            },
            {
                targets: 1,
                data: 'name',
                sortable: false,
                render: (data, type, row, meta) => {
                    if (row.isFolder == true) {
                        return [
                            `<a href="" class="is-folder" data-path="${row.fullName}" data-id="${row.id}"><i class="mdi mdi-folder-outline"></i>${data}</a>`
                        ]
                    }
                    else {
                        return [
                            `<i class="mdi mdi-file-outline"></i>${data}`
                        ]
                    }
                }
            },
            {
                targets: 2,
                data: 'length',
                sortable: false
            },
            {
                targets: 3,
                data: 'description',
                sortable: false
            },
            {
                targets: 4,
                data: 'creationTime',
                sortable: false
            },
            {
                targets: 5,
                data: 'lastModificationTime',
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
                        `   <button type="button" class="btn btn-sm btn-warning edit-file-unit" data-file-unit-id="${row.id}" data-bs-toggle="modal" data-bs-target="#FileUnitEditModal">`,
                        `       <i class="fas fa-pencil-alt"></i> ${l('Edit')}`,
                        '   </button>',
                        `   <button type="button" class="btn btn-sm btn-danger delete-file-unit" data-file-unit-id="${row.id}" data-file-unit-name="${row.name}">`,
                        `       <i class="fas fa-trash"></i> ${l('Delete')}`,
                        '   </button>',
                    ].join('');
                }
            }
        ]
    });

    abp.services.app.fileUnit.getAllParent({}).done(function (result) {
        console.log(result);
    });

    _$table.on('click', '.is-folder', function (e) {
        e.preventDefault();
        const fullPath = $(this).attr("data-path");
        const dirId = $(this).attr("data-id");
        $('#Directory').val(fullPath);
        $('#current-directory').val(dirId);
        console.log(dirId);
        _$fileUnitsTable.ajax.reload();
    });

    _$modal.on('shown.bs.modal', () => {
        _$modal.find('input:not([type=hidden]):first').focus();

    }).on('hidden.bs.modal', () => {
        _$form.clearForm();
    });
})(jQuery);