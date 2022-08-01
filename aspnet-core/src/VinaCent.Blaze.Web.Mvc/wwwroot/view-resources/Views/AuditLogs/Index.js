(function ($) {
    var _auditLogService = abp.services.app.auditLog,
        l = abp.localization.getSource('Blaze'),
        _$table = $('#AuditLogsTable');

    var _$auditLogsTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        listAction: {
            ajaxFunction: _auditLogService.getAll,
            inputFilter: function () {
                return $('#AuditLogsSearchForm').serializeFormToObject(true);
            }
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                className: 'waves-effect waves-light',
                action: () => _$auditLogsTable.draw(false)
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
                data: 'userName',
                sortable: false
            },
            {
                targets: 1,
                data: 'clientIpAddress',
                sortable: false
            },
            {
                targets: 2,
                data: 'executionTimeStr', 
            },
            {
                targets: 3,
                data: 'executionDuration',
                sortable: false,
                render: (data, type, row, meta) => {
                    return `${data} ms`
                }
            },
            {
                targets: 4,
                data: 'serviceName',
                sortable: false,
                render: (data, type, row, meta) => {
                    const length = data.length;
                    const methodName = data.substring(length - 20, length);
                    return [
                        `   <div class="mid-truncate" data-type="${methodName}" title="${data}">`,
                        `       <p>${data}</p>`,
                        `   </div>`
                    ].join('');
                }
            },
            {
                targets: 5,
                data: 'methodName',
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
                        `   <button type="button" class="btn btn-sm btn-warning detail-audit-log" data-audit-log-id="${row.id}" data-bs-toggle="modal" data-bs-target="#DetailModal">`,
                        `       <i class="fas fa-pencil-alt"></i> ${l(LKConstants.Detail)}`,
                        '   </button>'
                    ].join('');
                }
            }
        ]
    });

    $(document).on('click', '.detail-audit-log', function (e) {
        var auditLogId = $(this).attr('data-audit-log-id');

        abp.ajax({
            url: abp.appPath + 'admincp/audit-logs/detail-modal?id=' + auditLogId,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#DetailModal div.modal-content').html(content);
            },
            error: function (e) {
            }
        });
    });

    $('.btn-search').on('click', (e) => {
        _$auditLogsTable.ajax.reload();
    });

    $('.btn-clear').on('click', (e) => {
        $('input').val('');
        _$auditLogsTable.ajax.reload();
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which == 13) {
            _$auditLogsTable.ajax.reload();
            return false;
        }
    });
})(jQuery);