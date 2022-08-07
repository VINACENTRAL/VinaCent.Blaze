(function ($) {
    var _currencyExchangeRateService = abp.services.app.currencyExchangeRate,
        l = abp.localization.getSource('Blaze'),
        // Create
        _$modal = $('#CurrencyExchangeRateUpdateModal'),
        _$form = _$modal.find('form'),
        // Index
        _$table = $('#CurrencyExchangeRatesTable'),
        // History
        _$historyModal = $('#CurrencyExchangeRateHistoryModal'),
        _$historyTable = _$historyModal.find('table');

    var _$currencyExchangeRatesTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        listAction: {
            ajaxFunction: _currencyExchangeRateService.getAllList,
            inputFilter: function () {
                return $('#CurrencyExchangeRatesSearchForm').serializeFormToObject(true);
            }
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                className: 'waves-effect waves-light',
                action: () => _$currencyExchangeRatesTable.draw(false)
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
                    return `<div class="d-flex"><i class="me-1 ${row.isActive == true ? 'ri-checkbox-circle-fill text-success' : ' ri-close-circle-fill text-danger'}"></i> ${data}</div>`;
                }
            },
            {
                targets: 2,
                data: 'exchangeRateBaseOnOneDefaultDesciption',
                sortable: false
            },
            {
                targets: 3,
                data: 'exchangeRateBaseOnOneCurrentDesciption',
                sortable: false
            },
            {
                targets: 4,
                data: 'creationTimeStr',
                sortable: false
            },
            {
                targets: 5,
                data: null,
                sortable: false,
                autoWidth: false,
                defaultContent: '',
                render: (data, type, row, meta) => {
                    return [
                        `   <button type="button" class="btn btn-sm btn-warning" data-cruid="${row.id}" data-symbol="${encodeURIComponent(row.currencySymbol)}" data-bs-toggle="modal" data-bs-target="#CurrencyExchangeRateUpdateModal">`,
                        `       <i class="fas fa-pencil-alt"></i> ${l(LKConstants.UpdateExchangeRate)}`,
                        '   </button>',
                        `   <button type="button" class="btn btn-sm btn-primary" data-cruid="${row.id}" data-bs-toggle="modal" data-bs-target="#CurrencyExchangeRateHistoryModal">`,
                        `       <i class="mdi mdi-history"></i> ${l(LKConstants.ExchangeRateHistory)}`,
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

        var currencyExchangeRate = _$form.serializeFormToObject();

        abp.ui.setBusy(_$modal);
        _currencyExchangeRateService
            .updateExchangeRate(currencyExchangeRate)
            .done(function () {
                _$modal.modal('hide');
                _$form[0].reset();
                abp.notify.info(l(LKConstants.SavedSuccessfully));
                _$currencyExchangeRatesTable.ajax.reload();
            })
            .always(function () {
                abp.ui.clearBusy(_$modal);
            });
    });

    _$modal.on('shown.bs.modal', (evt) => {
        const currencyUnitId = $(evt.relatedTarget).attr("data-cruid");
        const currencyUnitSymbol = $(evt.relatedTarget).attr("data-symbol");

        _$form.find('#CurrencyUnitId').val(currencyUnitId);
        _$form.find('#CurrencyUnitSymbol').html(`(${decodeURIComponent(currencyUnitSymbol)})`);

        _$modal.find('input:not([type=hidden]):first').focus();
    }).on('hidden.bs.modal', () => {
        _$form.clearForm();
    });

    $('.btn-search').on('click', (e) => {
        _$currencyExchangeRatesTable.ajax.reload();
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which == 13) {
            _$currencyExchangeRatesTable.ajax.reload();
            return false;
        }
    });

    // =============================== START HISTORY =============================== //
    let _$currencyExchangeRatesHistoryTable;
    _$historyModal.on('shown.bs.modal', (evt) => {
        const currencyUnitId = $(evt.relatedTarget).attr("data-cruid");

        _$historyModal.find('#CurrentCurrencyUnitId').val(currencyUnitId);

        _$currencyExchangeRatesHistoryTable = _$historyTable.DataTable({
            paging: true,
            serverSide: true,
            listAction: {
                ajaxFunction: _currencyExchangeRateService.getAllHistory,
                inputFilter: function () {
                    return _$historyModal.find('#SearchForm').serializeFormToObject(true);
                }
            },
            buttons: [
                {
                    name: 'refresh',
                    text: '<i class="fas fa-redo-alt"></i>',
                    className: 'waves-effect waves-light',
                    action: () => _$currencyExchangeRatesHistoryTable.draw(false)
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
                    data: 'creator',
                    sortable: false,
                    render: function (data, type, row, meta) {
                        return [
                            `<div class="mini-stats-wid d-flex align-items-center">` +
                            `    <div class="flex-shrink-0 avatar-sm">` +
                            `        <span class="mini-stat-icon avatar-title rounded-circle text-success bg-soft-success fs-4">` +
                            `           <img class="w-100 h-100" src="${data.picture}" alt="${data.fullName}">` +
                            `        </span>` +
                            `    </div>` +
                            `    <div class="flex-grow-1 ms-3">` +
                            `        <h6 class="mb-1">${data.fullName}</h6>` +
                            `        <p class="text-muted mb-0">${l(LKConstants.ExchangeRateWasSettedThat, `<span class="fw-bold text-danger">${row.currencyFromStr}</span> ➤ <span class="fw-bold text-success">${row.currencyToStr}</span>`)}</p>` +
                            `    </div >` +
                            `    <div class="flex-shrink-0">` +
                            `        <p class="text-muted mb-0">${moment(new Date(row.creationTime)).fromNow()}</p>` +
                            `    </div>` +
                            `</div>` +
                            ``,
                        ].join('');
                    }
                }
            ]
        });
    }).on('hidden.bs.modal', () => {
        _$currencyExchangeRatesHistoryTable.destroy();
    });
    // ===============================  END HISTORY  =============================== //
})(jQuery);
