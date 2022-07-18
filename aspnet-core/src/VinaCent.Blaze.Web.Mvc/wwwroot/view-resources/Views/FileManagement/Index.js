(function ($) {
    var _fileUnitService = abp.services.app.fileUnit,
        l = abp.localization.getSource('Blaze'),
        _$directoryCreateModal = $('#DirectoryCreateModal'),
        _$directoryCreateForm = _$directoryCreateModal.find('form'),
        _$uploadFileModal = $('#UploadFileModal'),
        _$uploadFileForm = _$uploadFileModal.find('form'),
        _$filePond = $('#file-uploader').filePond(),
        _$table = $('#FileUnitsTable');

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
                },
                visible: false,
                searchable: false,
            },
            {
                targets: 1,
                data: 'name',
                sortable: false,
                render: (data, type, row, meta) => {
                    if (row.isFolder == true) {
                        return [
                            `<a href="" class="is-folder" data-path="${row.fullName}" data-id="${row.id}"><span class="fw-bold d-flex"><i class="ri-folder-fill me-1"></i> ${data}</span></a>`
                        ]
                    }
                    else {
                        return [
                            `<span class="fw-bold d-flex detail-file-unit" data-id="${row.id}" data-bs-toggle="modal" data-bs-target="#DetailFileModal"><i class="ri-file-3-fill me-1"></i><p class="text-decoration-underline cursor-pointer text-truncate" style="width: 150px">${data}</p></span>`
                        ]
                    }
                }
            },
            {
                targets: 2,
                data: 'length',
                sortable: false,
                render: (data, type, row, meta) => {
                    return row.isFolder ? l(LKConstants.Directory) : `${data} (bytes)`;
                }
            },
            {
                targets: 3,
                data: 'description',
                sortable: false
            },
            {
                targets: 4,
                data: 'creationTimeStr',
                sortable: false
            },
            {
                targets: 5,
                data: 'lastModificationTimeStr',
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
                        `   <button type="button" class="btn btn-sm ${row.isStatic ? 'disabled btn-light' : ' btn-warning edit-file-unit'}" data-file-unit-id="${row.id}" data-bs-toggle="modal" data-bs-target="#RenameFileUnitModal">`,
                        `       <i class="fas fa-pencil-alt"></i> ${l(LKConstants.Rename)}`,
                        '   </button>',
                        `   <button type="button" class="btn btn-sm ${row.isStatic ? 'disabled btn-light' : 'btn-danger delete-file-unit'}" data-file-unit-id="${row.id}" data-file-unit-name="${row.name}">`,
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

        if (_$directoryCreateForm.find('#current-directory').length === 0) {
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

        if (_$uploadFileForm.find('#current-directory').length === 0) {
            _$uploadFileForm.find('#current-directory').val = null
        }

        abp.ui.setBusy(_$uploadFileForm);
        const crrForm = document.getElementById('uploadFileForm');

        var files = _$filePond.getFiles().map((fileObj) => fileObj.file);

        var formData = new FormData(crrForm);

        for (var i = 0; i !== files.length; i++) {
            formData.append("File", files[i]);
        }

        abp.ajax({
            url: abp.appPath + 'api/services/app/FileUnit/UploadFile',
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
        }).done(function () {
            _$uploadFileModal.modal('hide');
            _$uploadFileForm[0].reset();
            abp.notify.info(l(LKConstants.SavedSuccessfully));
            _$fileUnitsTable.ajax.reload();
        }).always(function () {
            abp.ui.clearBusy(_$uploadFileForm);
        });
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

    $(document).on('click', '.detail-file-unit', function (e) {
        var fileUnitId = $(this).attr('data-id');

        abp.ajax({
            url: abp.appPath + 'admincp/file-management/detail-modal?fileUnitId=' + fileUnitId,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#DetailFileModal div.modal-content').html(content);
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

    let breadcrumbStorage = [];

    _$table.on('click', '.is-folder', function (e) {
        e.preventDefault();
        const fullPath = $(this).attr("data-path");
        const dirId = $(this).attr("data-id");

        updateStickData(dirId, fullPath);
        breadcrumbStorage.push({
            id: dirId ?? '',
            path: fullPath,
            name: [...fullPath.split('/')].pop()
        });

        renderBreadcrumb();
    });

    function updateStickData(id, fullPath) {
        $('#Directory').val(fullPath);
        $('#current-directory-1').val(id);
        $('#current-directory-2').val(id);
    }

    function renderBreadcrumb() {
        const breadcumbs = [];
        breadcumbs.push(`<div class="progress-bar"><span class="px-3 pointer" data-change-dir=""><i class="mdi mdi-folder-home"></i>${l(LKConstants.RootDirectory)}</span></div>`);
        breadcrumbStorage.forEach((item) => {
            breadcumbs.push(`<div class="progress-bar"><span class="px-3 pointer" data-change-dir="${item.id}">${item.name}</span></div>`);
        });
        document.getElementById('appfilebreadcrumbs').innerHTML = breadcumbs.join('');
        _$fileUnitsTable.ajax.reload();
        processBreadcrumb();
    }

    function processBreadcrumb() {
        $('[data-change-dir]').click((event) => {
            event.preventDefault();
            event.stopPropagation();

            const item = $(event.currentTarget);
            const id = item.data('change-dir') ?? '';
            const index = breadcrumbStorage.findIndex((item) => item.id == id);

            if (index >= 0) {
                const crrDir = breadcrumbStorage[index];
                if (crrDir.path === $('#Directory').val()) return;
                updateStickData(crrDir.id, crrDir.path);
            } else {
                updateStickData(null, null);
            }

            breadcrumbStorage = breadcrumbStorage.slice(0, index + 1);
            renderBreadcrumb();
        });
    }

    _$directoryCreateModal.on('shown.bs.modal', () => {
        _$directoryCreateModal.find('input:not([type=hidden]):first').focus();

    }).on('hidden.bs.modal', () => {
        _$directoryCreateForm.clearForm();
        _$directoryCreateForm.find('.field-validation-valid').html('');
    });

    _$uploadFileModal.on('shown.bs.modal', () => {
        _$uploadFileModal.find('input:not([type=hidden]):first').focus();

    }).on('hidden.bs.modal', () => {
        _$uploadFileForm.clearForm();
    });

    $('.btn-search').on('click', (e) => {
        _$fileUnitsTable.ajax.reload();
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which === 13) {
            _$fileUnitsTable.ajax.reload();
            return false;
        }
    });

})(jQuery);