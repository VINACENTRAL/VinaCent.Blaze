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
        const regex = /^(\w+)\[(\d+)]\.(\w+)$/;
        //serialize to array
        const data = $(this).serializeArray();

        //add also disabled items
        $(':disabled[name]', this).each(function () {
            data.push({name: this.name, value: $(this).val()});
        });

        //map to object
        const obj = {};
        data.map(function (x) { // Prevent duplicated in checkbox
            let m = regex.exec(x.name);
            if (m) {
                if (obj[m[1]] === undefined) {
                    obj[m[1]] = [];
                }
                if (obj[m[1]][m[2]] === undefined) {
                    obj[m[1]][m[2]] = {};
                }
                obj[m[1]][m[2]][m[3]] = x.value;
            } else {
                if (obj[x.name] === undefined) {
                    obj[x.name] = x.value;
                }
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
        let newObj, origKey, newKey, value;
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
            const $advSearch = $(obj);
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
        let advSearchWidth = 0;
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
        const $this = $(this);
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
                render: (data, type, row) => action(row)
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

            const formObjected = _$formCreate.serializeFormToObject();

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
            if (onShow && typeof onShow === 'string' && typeof window[onShow] === 'function') {
                window[onShow]();
            }
        }).on('hidden.bs.modal', () => {
            _$formCreate.clearForm();
            _$formCreate.find('.field-validation-valid').html('');
            if (onHide && typeof onHide === 'string' && typeof window[onHide] === 'function') {
                window[onHide]();
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

            const formObjected = _$formUpdate.serializeFormToObject();

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
            if (onShow && typeof onShow === 'string' && typeof window[onShow] === 'function') {
                window[onShow]();
            }
        }).on('hidden.bs.modal', () => {
            _$formUpdate.clearForm();
            _$formUpdate.find('.field-validation-valid').html('');
            if (onHide && typeof onHide === 'string' && typeof window[onHide] === 'function') {
                window[onHide]();
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

    $.fn.initQuickKey = function () {
        const validateKeys = ['F1', 'F2', 'F8', 'F9', 'F10'];
        const currentElement = $(this);
        const targetKey = currentElement.data('quick-key');
        if (!validateKeys.includes(targetKey)) {
            return; // Not valid
        }
        let badgeClass = 'bg-success';
        
        currentElement.removeClass('waves-effect');
        currentElement.removeClass('waves-light');
        currentElement.addClass('me-3');
        if (currentElement.hasClass('btn-success')) {
            badgeClass = 'bg-warning';
        }
        currentElement.append(`<span class="position-absolute top-0 start-100 translate-middle badge rounded-pill ${badgeClass}">${targetKey}</span>`);
        $(document).on('keydown', (e) => {
            if (e.originalEvent.code === targetKey) {
                e.preventDefault();
                currentElement.click();
            }
        })
    }

    $('[data-quick-key]').initQuickKey();
})(jQuery);