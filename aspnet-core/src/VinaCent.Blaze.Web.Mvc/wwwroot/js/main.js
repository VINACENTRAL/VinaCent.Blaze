(function ($) {
    const l = abp.localization.getSource('Blaze');
    //Notification handler
    abp.event.on('abp.notifications.received', function (userNotification) {
        abp.notifications.showUiNotifyForUserNotification(userNotification);

        //Desktop notification
        Push.create("Blaze", {
            body: userNotification.notification.data.message,
            icon: abp.appPath + 'img/logo.png',
            timeout: 6000,
            onClick: function () {
                window.focus();
                this.close();
            }
        });
    });

    //serializeFormToObject plugin for jQuery
    $.fn.serializeFormToObject = function (camelCased = false) {
        //serialize to array
        var data = $(this).serializeArray();

        //add also disabled items
        $(':disabled[name]', this).each(function () {
            data.push({ name: this.name, value: $(this).val() });
        });

        //map to object
        var obj = {};
        data.map(function (x) { // Prevent duplicated in checkbox
            if (obj[x.name] === undefined) {
                obj[x.name] = x.value;
            }
        });

        if (camelCased && camelCased === true) {
            return convertToCamelCasedObject(obj);
        }

        return obj;
    };

    //Configure blockUI
    if ($.blockUI) {
        $.blockUI.defaults.baseZ = 2000;
    }

    //Configure validator
    $.validator.setDefaults({
        highlight: (el) => {
            $(el).addClass('is-invalid');
        },
        unhighlight: (el) => {
            $(el).removeClass('is-invalid');
        },
        errorElement: 'p',
        errorClass: 'text-danger',
        errorPlacement: (error, element) => {
            if (element.parent('.input-group').length) {
                error.insertAfter(element.parent());
            } else {
                error.insertAfter(element);
            }
        }
    });

    function convertToCamelCasedObject(obj) {
        var newObj, origKey, newKey, value;
        if (obj instanceof Array) {
            return obj.map(value => {
                if (typeof value === 'object') {
                    value = convertToCamelCasedObject(value);
                }
                return value;
            });
        } else {
            newObj = {};
            for (origKey in obj) {
                if (obj.hasOwnProperty(origKey)) {
                    newKey = (
                        origKey.charAt(0).toLowerCase() + origKey.slice(1) || origKey
                    ).toString();
                    value = obj[origKey];
                    if (
                        value instanceof Array ||
                        (value !== null && value.constructor === Object)
                    ) {
                        value = convertToCamelCasedObject(value);
                    }
                    newObj[newKey] = value;
                }
            }
        }
        return newObj;
    }

    function initAdvSearch() {
        $('.abp-advanced-search').each((i, obj) => {
            var $advSearch = $(obj);
            setAdvSearchDropdownMenuWidth($advSearch);
            setAdvSearchStopingPropagations($advSearch);
        });
    }

    initAdvSearch();

    $(window).resize(() => {
        clearTimeout(window.resizingFinished);
        window.resizingFinished = setTimeout(() => {
            initAdvSearch();
        }, 500);
    });

    function setAdvSearchDropdownMenuWidth($advSearch) {
        var advSearchWidth = 0;
        $advSearch.each((i, obj) => {
            advSearchWidth += parseInt($(obj).width(), 10);
        });
        $advSearch.find('.dropdown-menu').width(advSearchWidth)
    }

    function setAdvSearchStopingPropagations($advSearch) {
        $advSearch.find('.dd-menu, .btn-search, .txt-search')
            .on('click', (e) => {
                e.stopPropagation();
            });
    }

    $.fn.clearForm = function () {
        var $this = $(this);
        $this.validate().resetForm();
        $('[name]', $this).each((i, obj) => {
            $(obj).removeClass('is-invalid');
        });
        $this[0].reset();
    };

    $.fn.db = function (ajaxFunction, searFormId, dataCols, action) {
        let _columnDefs = [...dataCols];
        if (action && typeof action === 'function') {
            _columnDefs.push({
                data: null,
                sortable: false,
                autoWidth: false,
                defaultContent: '',
                render: (data, type, row, meta) => action(row)
            });
        }
        _columnDefs = _columnDefs.map((el, index) => {
            el.targets = index;
            el.sortable = false;
            return el
        });

        //serialize to array
        const _$dataTable = $(this).DataTable({
            paging: true,
            serverSide: true,
            listAction: {
                ajaxFunction: ajaxFunction,
                inputFilter: function () {
                    return $(searFormId).serializeFormToObject(true);
                }
            },
            buttons: [
                {
                    name: 'refresh',
                    text: '<i class="fas fa-redo-alt"></i>',
                    className: 'waves-effect waves-light',
                    action: () => _$dataTable.draw(false)
                }
            ],
            responsive: {
                details: {
                    type: 'column'
                }
            },
            columnDefs: _columnDefs
        });

        return _$dataTable;
    };

    $.fn.modalCreate = function (ajaxFunction, onSuccess, onShow, onHide) {
        const _$modal = $(this);
        const _$formCreate = _$modal.find('form');

        _$formCreate.find('.save-button').on('click', (e) => {
            e.preventDefault();

            if (!_$formCreate.valid()) {
                return;
            }

            var formObjected = _$formCreate.serializeFormToObject();

            abp.ui.setBusy(_$modal);
            ajaxFunction(formObjected)
                .done(function () {
                    _$modal.modal('hide');
                    _$formCreate[0].reset();
                    abp.notify.success(l(LKConstants.SavedSuccessfully));
                    if (onSuccess && typeof onSuccess === 'function') {
                        onSuccess();
                    }
                })
                .always(function () {
                    abp.ui.clearBusy(_$modal);
                });
        });

        _$modal.on('shown.bs.modal', () => {
            _$modal.find('input:not([type=hidden]):first').focus();
            _$formCreate.find('input').unbind().on('keypress', function (e) {
                if (e.which === 13) {
                    e.preventDefault();
                    _$formCreate.find('.save-button').click();
                }
            });
            if (onShow && typeof onShow === 'function') {
                onShow();
            }
        }).on('hidden.bs.modal', () => {
            _$formCreate.clearForm();
            _$formCreate.find('.field-validation-valid').html('');
            if (onHide && typeof onHide === 'function') {
                onHide();
            }
        });
    }

    $.fn.modalUpdate = function (ajaxFunction, onSuccess, onShow, onHide) {
        const _$modal = $(this);
        const _$formUpdate = _$modal.find('form');

        function saveFormUpdate() {
            if (!_$formUpdate.valid()) {
                return;
            }

            var formObjected = _$formUpdate.serializeFormToObject();

            abp.ui.setBusy(_$formUpdate);
            ajaxFunction(formObjected).done(function () {
                _$modal.modal('hide');
                abp.notify.success(l(LKConstants.SavedSuccessfully));
                if (onSuccess && typeof onSuccess === 'function') {
                    onSuccess();
                }
            }).always(function () {
                abp.ui.clearBusy(_$formUpdate);
            });
        }

        _$formUpdate.closest('div.modal-content').find(".save-button").click(function (e) {
            e.preventDefault();
            saveFormUpdate();
        });

        _$formUpdate.find('input').on('keypress', function (e) {
            if (e.which === 13) {
                e.preventDefault();
                saveFormUpdate();
            }
        });

        _$modal.on('shown.bs.modal', () => {
            _$modal.find('input:not([type=hidden]):first').focus();
            if (onShow && typeof onShow === 'function') {
                onShow();
            }
        }).on('hidden.bs.modal', () => {
            _$formUpdate.clearForm();
            _$formUpdate.find('.field-validation-valid').html('');
            if (onHide && typeof onHide === 'function') {
                onHide();
            }
        });
    }

    $.fn.filePond = function (options = {}) {
        if (!options || !options.labelIdle) {
            options['labelIdle'] = l(LKConstants.FilePickSuggestionMessage);
        }
        const result = [...$(this)].map((el) => {
            return FilePond.create(el, options);
        });
        if (result.length === 1) return result[0];
        return result;
    }
})(jQuery);