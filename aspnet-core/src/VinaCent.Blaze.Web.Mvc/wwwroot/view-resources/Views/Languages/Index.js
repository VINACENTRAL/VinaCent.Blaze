function RenderIsDefaultColumn(row, masterName) {
    return `<i class="${row.isDefault == true ? 'ri-checkbox-circle-fill text-success' : ' ri-close-circle-fill text-danger'}"></i>`;
}

function RenderIsDisabledColumn(row, masterName) {
    return `<i class="${row.isDisabled != true ? 'ri-checkbox-circle-fill text-success' : ' ri-close-circle-fill text-danger'}"></i>`;
}

function RenderIconColumn(row, masterName) {
    return `<img src="/vinacent/flags/4x3/${row.icon}" height="18">`;
}
