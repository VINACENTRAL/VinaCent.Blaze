(function ($) {
    const _languageTextService = abp.services.app.languageTextManagement;
    const l = abp.localization.getSource('Blaze');

    const _$table = $('#LanguageTextsTable');
    var _dataTable = _$table.db(_languageTextService.getAll,
        `#LanguageTextsSearchForm`,
        ['key', 'defaultValue', 'value', 'source'].map((item) => {
            return {data: item}
        }),
        (row) => {
            return [
                `   <button type="button" class="btn btn-sm btn-warning edit-languageText" data-languageText-id="${row.id}" data-bs-toggle="modal" data-bs-target="#LanguageTextEditModal">`,
                `       <i class="fas fa-pencil-alt"></i> ${l(LKConstants.Edit)}`,
                '   </button>',
                `   <button type="button" class="btn btn-sm btn-danger delete-languageText" data-languageText-id="${row.id}" data-tenancy-name="${row.name}">`,
                `       <i class="fas fa-trash"></i> ${l(LKConstants.Delete)}`,
                '   </button>'
            ].join('');
        });

    $(document).on('click', '.delete-languageText', function () {
        var languageTextId = $(this).attr('data-languageText-id');
        var tenancyName = $(this).attr('data-tenancy-name');

        deleteLanguageText(languageTextId, tenancyName);
    });

    $(document).on('click', '.edit-languageText', function () {
        var languageTextId = $(this).attr('data-languageText-id');

        abp.ajax({
            url: abp.appPath + 'admincp/language-texts/edit-modal?languageTextId=' + languageTextId,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#LanguageTextEditModal div.modal-content').html(content);
                $('#LanguageTextEditModal').modalUpdate(_languageTextService.update, _dataTable.ajax.reload);
            },
            error: function (e) {
            }
        });
    });

    function deleteLanguageText(languageTextId, tenancyName) {
        abp.message.confirm(
            abp.utils.formatString(
                l(LKConstants.AreYouSureWantToDelete),
                tenancyName
            ),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    _languageTextService
                        .delete({
                            id: languageTextId
                        })
                        .done(() => {
                            abp.notify.info(l(LKConstants.SuccessfullyDeleted));
                            _dataTable.ajax.reload();
                        });
                }
            }
        );
    }

    $('.btn-search').on('click', () => {
        _dataTable.ajax.reload();
    });

    $('.btn-clear').on('click', () => {
        $('input[name=Keyword]').val('');
        $('input[name=IsActive][value=""]').prop('checked', true);
        _dataTable.ajax.reload();
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which === 13) {
            _dataTable.ajax.reload();
            return false;
        }
    });

    $('#LanguageTextCreateModal').modalCreate(_languageTextService.create, _dataTable.ajax.reload)
})(jQuery);
