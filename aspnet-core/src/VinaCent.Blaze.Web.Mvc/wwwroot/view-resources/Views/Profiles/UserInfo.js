(function ($) {
    const l = abp.localization.getSource('Blaze');

    const countrySelector = document.getElementById('countrySelector');

    const stateSelector = document.getElementById('stateSelector');

    const citySelector = document.getElementById('citySelector');

    // Get default value in first load
    const defaultCountry = countrySelector.dataset.default;
    const defaultState = stateSelector.dataset.default;
    const defaultCity = citySelector.dataset.default;

    if (defaultCountry) {
        loadStates(defaultCountry, () => {
            stateSelector.value = defaultState;

            if (defaultState) {
                loadCities(defaultState, () => {
                    citySelector.value = defaultCity;
                });
            }
        });
    }

    // =======================================  START DATA CHOICE STORES  ======================================= //
    let countryChoice = new Choices(countrySelector, {
        placeholder: l('PleaseSelectCountry'),
        shouldSort: false,
    });
    let stateChoice = new Choices(stateSelector, {
        placeholder: l('PleaseSelectState'),
        shouldSort: false,
    });
    let cityChoice = new Choices(citySelector, {
        placeholder: l('PleaseSelectCity'),
        shouldSort: false,
    });

    // Proceess country
    countrySelector.addEventListener('choice', (e) => {
        clearCities();
        loadStates(e.detail.choice.value);
    })

    // State process
    function loadStates(countryCode, onDone) {
        clearStates();

        if (countryCode?.length == 0) {
            return;
        }

        abp.services.app.commonData.getAllList({
            keyword: '',
            type: 'STATE',
            parentKey: countryCode
        }).done(function (result) {
            const runAble = result && result.items.length > 0;
            if (runAble) {
                const items = result.items.map((x) => {
                    return {
                        label: x.value,
                        value: x.key
                    }
                });
                stateChoice.enable();
                stateChoice.setChoices([{
                    label: l('PleaseSelectState'),
                    value: '',
                    selected: true,
                    disabled: true,
                }, ...items]);
            } else {
                stateChoice.disable();
            }
            if (typeof onDone === 'function') {
                onDone();
            }
        });
    }
    function clearStates() {
        // Clear all old state
        stateChoice.clearStore();
        stateChoice.clearInput();
        stateChoice.disable();
    }
    stateSelector.addEventListener('choice', function (e) {
        loadCities(e.detail.choice.value);
    });

    // City process
    function loadCities(stateCode, onDone) {
        clearCities();

        if (!stateCode || stateCode.length === 0) {
            return;
        }

        abp.services.app.commonData.getAllList({
            keyword: '',
            type: 'CITY',
            parentKey: stateCode
        }).done(function (result) {
            const runAble = result && result.items.length > 0;
            if (runAble) {
                const items = result.items.map((x) => {
                    return {
                        label: x.value,
                        value: x.key
                    }
                });
                cityChoice.enable();
                cityChoice.setChoices([{
                    label: l('PleaseSelectCity'),
                    value: '',
                    selected: true,
                    disabled: true,
                },...items]);
            } else {
                cityChoice.disable();
            }

            if (typeof onDone === 'function') {
                onDone();
            }
        });
    }
    function clearCities() {
        // Clear all old city
        cityChoice.clearStore();
        cityChoice.clearInput();
        cityChoice.disable();
    }
    // =======================================   END DATA CHOICE STORES   ======================================= //
})(jQuery);