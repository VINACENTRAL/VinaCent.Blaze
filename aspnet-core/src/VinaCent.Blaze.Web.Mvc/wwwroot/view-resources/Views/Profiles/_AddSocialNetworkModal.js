(function ($) {
    const l = abp.localization.getSource('Blaze'),
        _profileService = abp.services.app.profile,
        _$modal = $('#AddSocialLinkModal'),
        _$form = _$modal.find('form'),
        _$saveBtn = _$form.closest('div.modal-content').find(".save-button");

    var lsnrj = $("#ListSocialNetworkRawJson").val();

    $('[data-box-value]').click((e) => {
        e.preventDefault();

        const _icon = $(e.currentTarget).data('box-value');

        let icon = $('[data-social-icon]').find('i');
        icon.removeClass();
        icon.addClass(_icon);
        $('#SocialIcon').val(_icon);
        $('[data-social-icon]').attr('data-social-icon', _icon).change();
    });

    _$saveBtn.click((e) => {
        e.preventDefault();
        let obj = lsnrj.length > 0 ? JSON.parse(lsnrj) : [];
        const icon = $('#SocialIcon').val();
        const url = $('#SocialUrl').val();
        const name = $('#SocialName').val();

        const existedUrl = obj.findIndex(item => {
            return item.url.replace(/\/$/, "") === url.replace(/\/$/, "");
        });

        if (existedUrl !== -1) {
            abp.notify.error(l(LKConstants.UrlIsExisted));
            return;
        }

        obj.push({ icon, url, name });
        lsnrj = JSON.stringify(obj);
        
        const input = {"listSocialNetworkRawJson": lsnrj };
        abp.ui.setBusy(_$modal);
        _profileService
            .updateSocialNetwork(input)
            .done(function () {
                _$modal.modal('hide');
                _$form[0].reset();
                abp.notify.info(l(LKConstants.SavedSuccessfully));
                $("#ListSocialNetworkRawJson").val(lsnrj);
                abp.event.trigger('reloadSocial');
            })
            .always(function () {
                abp.ui.clearBusy(_$modal);
            });
    });
})(jQuery);