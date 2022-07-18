(function ($) {
    var _userLoginAttemptService = abp.services.app.userLoginAttempt,
        l = abp.localization.getSource('Blaze'),
        _$table = $('#SecurityLogsTable');

    var _$securityLogsTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        listAction: {
            ajaxFunction: _userLoginAttemptService.getAll,
            inputFilter: function () {
                return $('#SecurityLogsSearchForm').serializeFormToObject(true);
            }
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                className: 'waves-effect waves-light',
                action: () => _$securityLogsTable.draw(false)
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
                data: 'userNameOrEmailAddress',
                sortable: false
            },
            {
                targets: 1,
                data: 'clientIpAddress',
                sortable: false
            },
            {
                targets: 2,
                data: 'browserInfo',
                sortable: false,
                render: (data, type, row, meta) => {
                    return [
                        `   <div class="truncate" data-type="" title="${data}">`,
                        `       <p>${data}</p>`,
                        `   </div>`
                    ].join('');
                }
            },
            {
                targets: 3,
                data: 'creationTimeStr', 
                sortable: false,
            },
            {
                targets: 4,
                data: 'result',
                sortable: false,
                render: function (data) {
                    return l('Enum:AbpLoginResultType:' + data);
                }
            }
        ]
    });

    $('.btn-search').on('click', (e) => {
        _$securityLogsTable.ajax.reload();
    });

    $('.btn-clear').on('click', (e) => {
        $('input').val('');
        _$securityLogsTable.ajax.reload();
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which == 13) {
            _$securityLogsTable.ajax.reload();
            return false;
        }
    });
})(jQuery);