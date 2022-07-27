(function ($) {
    var _fileUnitService = abp.services.app.fileUnit,
        l = abp.localization.getSource('Blaze'),
        _$directoryCreateModal = $('#DirectoryCreateModal'),
        _$directoryCreateForm = _$directoryCreateModal.find('form'),
        _$uploadFileModal = $('#UploadFileModal'),
        _$uploadFileForm = _$uploadFileModal.find('form'),
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
                        `   <button type="button" class="btn btn-sm btn-warning edit-file-unit" data-file-unit-id="${row.id}" data-bs-toggle="modal" data-bs-target="#RenameFileUnitModal">`,
                        `       <i class="fas fa-pencil-alt"></i> ${l(LKConstants.Rename)}`,
                        '   </button>',
                        `   <button type="button" class="btn btn-sm btn-danger delete-file-unit" data-file-unit-id="${row.id}" data-file-unit-name="${row.name}">`,
                        `       <i class="fas fa-trash"></i> ${l(LKConstants.Delete)}`,
                        '   </button>',
                    ].join('');
                }
            }
        ]
    });

    _$directoryCreateForm.find('.save-button').on('click', (e) => {
        e.preventDefault();

        if (!_$directoryCreateForm.valid()) {
            return;
        }

        if (_$directoryCreateForm.find('#current-directory').val == '')
        {
            _$directoryCreateForm.find('#current-directory').val = null
        }

        var fileUnit = _$directoryCreateForm.serializeFormToObject();
        abp.ui.setBusy(_$directoryCreateModal);
        _fileUnitService
            .createDirectory(fileUnit)
            .done(function () {
                _$directoryCreateModal.modal('hide');
                _$directoryCreateForm[0].reset();
                abp.notify.info(l(LKConstants.SavedSuccessfully));
                _$fileUnitsTable.ajax.reload();
            })
            .always(function () {
                abp.ui.clearBusy(_$directoryCreateModal);
            });
    });

    _$uploadFileForm.find('.save-button').on('click', (e) => {
        e.preventDefault();

        if (!_$uploadFileForm.valid()) {
            return;
        }

        if (_$uploadFileForm.find('#current-directory').val == '') {
            _$uploadFileForm.find('#current-directory').val = null
        }

        var fileUnit = _$uploadFileForm.serializeFormToObject();
        console.log(fileUnit);
        //abp.ui.setBusy(_$uploadFileForm);
        //_fileUnitService
        //    .uploadFile(fileUnit)
        //    .done(function () {
        //        _$uploadFileForm.modal('hide');
        //        _$uploadFileForm[0].reset();
        //        abp.notify.info(l(LKConstants.SavedSuccessfully));
        //        _$fileUnitsTable.ajax.reload();
        //    })
        //    .always(function () {
        //        abp.ui.clearBusy(_$uploadFileModal);
        //    });
    });

    $(document).on('click', '.delete-file-unit', function () {
        var fileUnitId = $(this).attr('data-file-unit-id');
        var fileUnitName = $(this).attr('data-file-unit-name');
        
        deleteFileUnit(fileUnitId, fileUnitName);
    });

    $(document).on('click', '.edit-file-unit', function (e) {
        var fileUnitId = $(this).attr('data-file-unit-id');

        abp.ajax({
            url: abp.appPath + 'admincp/file-management/rename-modal?fileUnitId=' + fileUnitId,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#RenameFileUnitModal div.modal-content').html(content);
            },
            error: function (e) {
            }
        });
    });

    abp.event.on('fileUnit.edited', (data) => {
        _$fileUnitsTable.ajax.reload();
    });

    function deleteFileUnit(fileUnitId, fileUnitName) {
        abp.message.confirm(
            abp.utils.formatString(
                l(LKConstants.AreYouSureWantToDelete),
                fileUnitName
            ),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    _fileUnitService
                        .delete(fileUnitId)
                        .done(() => {
                            abp.notify.info(l(LKConstants.SuccessfullyDeleted));
                            _$fileUnitsTable.ajax.reload();
                        });
                }
            }
        );
    }

    //abp.services.app.fileUnit.getAllParent({}).done(function (result) {
    //    console.log(result);
    //});

    _$table.on('click', '.is-folder', function (e) {
        e.preventDefault();
        const fullPath = $(this).attr("data-path");
        const dirId = $(this).attr("data-id");
        $('#Directory').val(fullPath);
        $('#current-directory-1').val(dirId);
        $('#current-directory-2').val(dirId);
        _$fileUnitsTable.ajax.reload();
    });

    _$directoryCreateModal.on('shown.bs.modal', () => {
        _$directoryCreateModal.find('input:not([type=hidden]):first').focus();

    }).on('hidden.bs.modal', () => {
        _$directoryCreateForm.clearForm();
    });

    _$uploadFileModal.on('shown.bs.modal', () => {
        _$uploadFileModal.find('input:not([type=hidden]):first').focus();

    }).on('hidden.bs.modal', () => {
        _$uploadFileForm.clearForm();
    });
})(jQuery);