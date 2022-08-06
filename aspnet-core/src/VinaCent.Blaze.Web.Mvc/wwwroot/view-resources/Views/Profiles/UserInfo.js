(function ($) {
    const countrySelector = $("#countrySelector");

    const stateSelector = $("#stateSelector");
    const stateSelectorParent = stateSelector.parent();

    const citySelector = $("#citySelector");
    const citySelectorParent = citySelector.parent();

    let defaultCountry = countrySelector.data('default');
    let defaultState = stateSelector.data('default');
    let defaultCity = citySelector.data('default');

    if (defaultCountry) {
        loadStates(defaultCountry, () => {
            stateSelector.val(defaultState).change();
        });
    }

    if (defaultState) {
        loadCities(defaultState, () => {
            citySelector.val(defaultCity).change();
        });
    }

    countrySelector.on('change', function () {
        var country = this.value;
        clearStates();
        clearCities();
        loadStates(country);
    });

    stateSelector.on('change', function () {
        var state = this.value;
        clearCities();
        loadCities(state);
    });

    function clearStates() {
        // Clear all old state
        stateSelector.find('option').remove().end();
        stateSelector.append($(`<option>`, {
            value: null,
            text: l('PleaseSelectState')
        }));
        stateSelector.setAttribute('disabled', '');
    }

    function loadStates(countryCode, onDone) {
        clearStates();
        stateSelectorParent.addClass("loading");
        abp.services.app.commonData.getAllList({
            keyword: '',
            type: 'STATE',
            parentKey: countryCode
        }).then((result) => {
            result.items.forEach((item) => {
                stateSelector.append($(`<option>`, {
                    value: `${item.key}`,
                    text: item.value
                }));
            });
            stateSelectorParent.removeClass("loading");
            if (typeof onDone === 'function') {
                onDone();
            }
        }).catch(() => {
            setTimeout(() => {
                loadStates(countryCode);
            }, 1500);
        });
    }
})(jQuery);