var abp = abp || {};
(function ($) {
    if (!sweetAlert || !$) {
        return;
    }

    const l = abp.localization.getSource('Blaze');

    /* DEFAULTS *************************************************/

    abp.libs = abp.libs || {};
    abp.libs.sweetAlert = {
        config: {
            'default': {}, info: {
                icon: 'info',
                timer: 10000,
            }, success: {
                icon: 'https://cdn.lordicon.com/tqywkdcz.json',
                timer: 10000,
            }, warn: {
                icon: '/Common/images/vinacent-error-outline.gif',
            }, error: {
                icon: 'error',
                dangerMode: true,
            }, confirm: {
                icon: 'https://cdn.lordicon.com/inrunzby.json',
                title: l('AreYouSure'), buttons: [l('Cancel'), l('Confirm')],
                closeOnClickOutside: false,
                closeOnEsc: false,
                dangerMode: true,
            }
        }
    };

    /* MESSAGE **************************************************/

    var showMessage = function (type, message, title, callback, options) {
        options = options || {};
        var messageContent = {};
        if (title) {
            messageContent.title = title;
        }

        if (options.isHtml) {
            var el = document.createElement('div');
            //https://github.com/t4t5/sweetalert/issues/842
            el.style = 'position: relative;';
            el.innerHTML = message;

            messageContent.content = el;
        } else {
            messageContent.text = message;
        }

        var opts = $.extend(
            {},
            abp.libs.sweetAlert.config['default'],
            abp.libs.sweetAlert.config[type],
            messageContent,
            options
        );

        return $.Deferred(function ($dfd) {
            sweetAlert(opts).then(function (isConfirmed) {
                callback && callback(isConfirmed);
                $dfd.resolve(isConfirmed);
            });
        });
    };

    abp.message.info = function (message, title, options) {
        return showMessage('info', message, title, null, options);
    };

    abp.message.success = function (message, title, options) {
        return showMessage('success', message, title, null, options);
    };

    abp.message.warn = function (message, title, options) {
        return showMessage('warn', message, title, null, options);
    };

    abp.message.error = function (message, title, options) {
        return showMessage('error', message, title, null, options);
    };

    abp.message.confirm = function (message, title, callback, options) {
        return showMessage('confirm', message, title, callback, options);
    };

    abp.event.on('abp.dynamicScriptsInitialized', function () {
        abp.libs.sweetAlert.config.confirm.title = l('AreYouSure');
        abp.libs.sweetAlert.config.confirm.buttons = [l('Cancel'), l('Confirm')];
    });

})(jQuery);