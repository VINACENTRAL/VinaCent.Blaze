function RenderTitleColumn(row, masterName) {
    return `${row.title} <i class="align-middle ${row.isActive ? 'ri-checkbox-circle-fill text-success' : ' ri-close-circle-fill text-danger'}"></i>`;
}
