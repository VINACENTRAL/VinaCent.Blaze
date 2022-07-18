// https://github.com/DataTables/DataTablesSrc/blob/master/js/ext/ext.classes.js#L7
var abp = abp || {};
(function () {
    if (!$.fn.dataTable) {
        return;
    }

    abp.libs = abp.libs || {};
    let l = abp.localization.getSource("Blaze");
    
    const language = {
        emptyTable: l(LKConstants.NoDataAvailableInTable),
        info: l(LKConstants.DataTable_Info_Start_End_of_Total_items, '_START_', '_END_', '_TOTAL_'),
        infoEmpty: l(LKConstants.NoRecords),
        infoFiltered: l(LKConstants.DataTable_FilteredFrom_MAX_TotalEntries, '_MAX_'),
        infoPostFix: "",
        infoThousands: ",",
        lengthMenu: l(LKConstants.DataTable_Show_MENU_Entries, '_MENU_'),
        loadingRecords: l(LKConstants.LoadingEtc___),
        processing: '<i class="fas fa-refresh fa-spin"></i>',
        search: "Search:",
        zeroRecords: l(LKConstants.NoMatchingRecordsFound),
        paginate: {
            first: '<i class="fas fa-angle-double-left"></i>',
            last: '<i class="fas fa-angle-double-right"></i>',
            next: '<i class="fas fa-chevron-right"></i>',
            previous: '<i class="fas fa-chevron-left"></i>'
        },
        aria: {
            sortAscending: ": activate to sort column ascending",
            sortDescending: ": activate to sort column descending"
        }
    };

    $.extend(true, $.fn.dataTable.defaults, {
        searching: false,
        ordering: false,
        language: language,
        processing: true,
        autoWidth: false,
        responsive: true,
        dom: [
            "<'row'<'col-md-12'f>>",
            "<'row'<'col-md-12't>>",
            "<'row mt-2 datatable-footer'",
            "<'col-lg-1 col-xs-12'<'float-start text-center data-tables-refresh'B>>",
            "<'col-lg-3 col-xs-12'<'d-flex h-100 justify-content-start align-items-center'i>>",
            "<'col-lg-3 col-xs-12'<'d-flex h-100 justify-content-center align-items-center'l>>",
            "<'col-lg-5 col-xs-12'<'d-flex h-100 justify-content-end align-items-center'p>>",
            ">"
        ].join('')
    });

    //$.fn.dataTable.ext.classes.sPageButton = '131313';
    //$.fn.dataTable.ext.classes.sPageButtonActive = '131313';
    //$.fn.dataTable.ext.classes.sPageButtonDisabled = 'disabled';
})();
