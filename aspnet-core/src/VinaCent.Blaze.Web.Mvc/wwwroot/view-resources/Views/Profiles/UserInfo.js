(function ($) {
    const l = abp.localization.getSource('Blaze');

    const countrySelector = document.getElementById('countrySelector');

    const stateSelector = document.getElementById('stateSelector');

    const citySelector = document.getElementById('citySelector');
    let defaultCountry = countrySelector.dataset.default;

    let defaultState = stateSelector.dataset.default;
    let defaultCity = citySelector.dataset.default;

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

    countrySelector.addEventListener('change', function () {
        var country = this.value; 
        clearStates();
        clearCities();
        loadStates(country);  
    });

    stateSelector.addEventListener('change', function () {
        var state = this.value;
        clearCities();
        loadCities(state);
    });

    function clearStates() {
        // Clear all old state
        stateSelector.innerHTML = `<option value="">${l('PleaseSelectState')}</option>`;
        stateSelector.disabled = true;
    }

    function loadStates(countryCode, onDone) {
        clearStates();

        if (!countryCode || countryCode.length === 0) {
            return;
        }

        abp.services.app.commonData.getAllList({
            keyword: '',
            type: 'STATE',
            parentKey: countryCode
        }).done(function (result) {
            const runAble = result && result.items.length > 0;
            stateSelector.disabled = !runAble;
            if (runAble) {
                result.items.forEach((item) => {
                    stateSelector.innerHTML += `<option value=${item.key}>${item.value}</option>`;
                })       
            }
            if (typeof onDone === 'function') {
                onDone();
            }
        });
    }

    function clearCities() {
        // Clear all old state
        citySelector.innerHTML = `<option value="">${l('PleaseSelectCity')}</option>`;
        citySelector.disabled = true;
    }

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
            citySelector.disabled = !runAble;
            if (runAble) {
                result.items.forEach((item) => {
                    citySelector.innerHTML += `<option value=${item.key}>${item.value}</option>`;
                })
            }
            
            if (typeof onDone === 'function') {
                onDone();
            }
        });
    }
})(jQuery);