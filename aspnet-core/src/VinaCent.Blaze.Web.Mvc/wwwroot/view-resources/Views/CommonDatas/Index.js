$(() => {
    const l = abp.localization.getSource('Blaze');
});

function RenderAction(row, masterName, defaultButtons) {
    const l = abp.localization.getSource('Blaze');
    return [
        `   <button type="button" class="btn btn-sm waves-effect waves-light btn-primary" data-common-id="${row.id}">`,
        `       <i class="mdi mdi-view-dashboard-edit"></i> ${l(LKConstants.Edit)}`,
        '   </button>',
        `   <button type="button" class="btn btn-sm waves-effect waves-light btn-danger delete-${masterName.toLowerCase()}" data-${masterName.toLowerCase()}-id="${row.id}" data-${masterName.toLowerCase()}-name="${row.value}">`,
        `       <i class="fas fa-trash"></i> ${l(LKConstants.Delete)}`,
        '   </button>'
    ].join('');
}