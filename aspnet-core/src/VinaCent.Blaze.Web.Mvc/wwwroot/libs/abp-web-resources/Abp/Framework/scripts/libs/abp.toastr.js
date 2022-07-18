(function (define) {
    define(['toastr', 'abp-web-resources'], function (toastr, abp) {
        return (function () {

            if (!toastr) {
                return;
            }

            if (!abp) {
                return;
            }

            /* DEFAULTS *************************************************/

            toastr.options.positionClass = 'toast-bottom-center';
            toastr.options.progressBar = true;
            toastr.options.closeDuration = 5000;
            
            toastr.options.showEasing = 'swing';
            toastr.options.closeEasing = 'swing';
            toastr.options.hideEasing = 'linear';

            toastr.options.preventDuplicates = true;


            /* NOTIFICATION *********************************************/

            var showNotification = function (type, message, title, options) {
                toastr[type](message, title, options);
            };

            abp.notify.success = function (message, title, options) {
                showNotification('success', message, title, options);
            };

            abp.notify.info = function (message, title, options) {
                showNotification('info', message, title, options);
            };

            abp.notify.warn = function (message, title, options) {
                showNotification('warning', message, title, options);
            };

            abp.notify.error = function (message, title, options) {
                showNotification('error', message, title, options);
            };

            /* ============================================================= */

            /* STACK NOTIFICATION *********************************************/
            
            const savedNotificationListKey = 'vinacent.com_toastr_saved_notifications';
            
            abp.notifyStack.success = function (message, title, options) {
                stackNotify('success', message, title, options);
            };

            abp.notifyStack.info = function (message, title, options) {
                stackNotify('info', message, title, options);
            };

            abp.notifyStack.warn = function (message, title, options) {
                stackNotify('warning', message, title, options);
            };

            abp.notifyStack.error = function (message, title, options) {
                stackNotify('error', message, title, options);
            };

            // Method for add noty manually
            function stackNotify(type, message, title, options) {
                // let crr = abp.utils.setCookieValue
                let crr = abp.utils.getCookieValue(savedNotificationListKey);
                if (crr) {
                    crr = [...JSON.parse(crr)];
                } else {
                    crr = [];
                }
                crr.push({type, message, title, options});
                abp.utils.setCookieValue(savedNotificationListKey, JSON.stringify(crr));
            }

            // AUTO RENDER PREVIOUS NOTIFICATIONS
            (() => {
                const crr = abp.utils.getCookieValue(savedNotificationListKey);
                if (crr) {
                    [...JSON.parse(crr)].forEach((item) => {
                        toastr[item.type](item.message, item.title, item.options ?? undefined);
                    });
                }
                abp.utils.setCookieValue(savedNotificationListKey, null);
            })();
            /* ============================================================= */

            return abp;
        })();
    });
}(typeof define === 'function' && define.amd ? define : function (deps, factory) {
    if (typeof module !== 'undefined' && module.exports) {
        module.exports = factory(require('toastr'), require('abp-web-resources'));
    } else {
        window.abp = factory(window.toastr, window.abp);
    }
}));
