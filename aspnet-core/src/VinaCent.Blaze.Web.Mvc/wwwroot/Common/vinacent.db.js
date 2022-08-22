$('section[app-main-section]').each((index, section) => {
    const masterName = `sectionRef${new Date().getTime()}`;

    const l = abp.localization.getSource('Blaze');
    const currentSection = $(section);

    const commonRef = JSON.parse(currentSection.attr('app-main-section'));
    currentSection.removeAttr('app-main-section')

    const _services = abp.services.app[commonRef.serviceName];

    const mainTable = currentSection.find('.main-table');
    const searchForm = currentSection.find('form.search-form')

    const tableCols = mainTable.find('th[data-ref]').map((index, el) => {
        const renderFuncName = $(el).attr('data-render');
        return {
            data: $(el).attr('data-ref'),
            render: (data, type, row) => {
                if (renderFuncName && renderFuncName.length > 0 && typeof renderFuncName === 'string' && typeof window[renderFuncName] === 'function')
                    return eval(renderFuncName)(row, masterName);
                return data;
            }
        }
    });

    let actionRender = null;
    const thActionElement = mainTable.find('th[data-action]');
    if (thActionElement) {
        const actionRenderFuncName = thActionElement.attr('data-action');
        let primaryKey = thActionElement.attr('data-primary');
        let nameKey = thActionElement.attr('data-name');

        if (!primaryKey || primaryKey.length === 0) {
            primaryKey = 'id';
        }

        if (!nameKey || nameKey.length === 0) {
            nameKey = 'name';
        }

        actionRender = (row) => {
            const defaultButtons = [
                `   <button type="button" class="btn btn-sm waves-effect waves-light btn-warning edit-${masterName.toLowerCase()}" data-${masterName.toLowerCase()}-id="${row[primaryKey]}">`,
                `       <i class="fas fa-pencil-alt"></i> ${l(LKConstants.Edit)}`,
                '   </button>',
                `   <button type="button" class="btn btn-sm waves-effect waves-light btn-danger delete-${masterName.toLowerCase()}" data-${masterName.toLowerCase()}-id="${row[primaryKey]}" data-${masterName.toLowerCase()}-name="${row[nameKey]}">`,
                `       <i class="fas fa-trash"></i> ${l(LKConstants.Delete)}`,
                '   </button>',
            ].join('');

            if (actionRenderFuncName && actionRenderFuncName.length > 0 && typeof actionRenderFuncName === 'string' && typeof window[actionRenderFuncName] === 'function')
                return eval(actionRenderFuncName)(row, masterName, defaultButtons);

            return defaultButtons;
        };

        // Default process edit
        $(document).on('click', `.edit-${masterName.toLowerCase()}`, function (e) {
            const id = $(this).attr(`data-${masterName.toLowerCase()}-id`);
            e.preventDefault();

            modalUpdate?.modal('show');

            abp.ajax({
                url: abp.appPath + commonRef.updateHtmlInnerAction + '?id=' + id,
                type: 'POST',
                dataType: 'html',
                success: function (content) {
                    modalUpdateInit(content);
                },
                error: function (e) {
                }
            })
        });

        // Default process delete
        $(document).on('click', `.delete-${masterName.toLowerCase()}`, function () {
            var id = $(this).attr(`data-${masterName.toLowerCase()}-id`);
            var name = $(this).attr(`data-${masterName.toLowerCase()}-name`);

            abp.message.confirm(
                abp.utils.formatString(
                    l(LKConstants.AreYouSureWantToDelete),
                    name),
                null,
                (isConfirmed) => {
                    if (isConfirmed) {
                        _services[commonRef.deleteAction]({
                            id: id
                        }).done(() => {
                            abp.notify.info(l(LKConstants.SuccessfullyDeleted));
                            _$dataTable.ajax.reload();
                        });
                    }
                }
            );
        });
    }

    const _$dataTable = mainTable.db(_services[commonRef.tableAction], searchForm, tableCols, actionRender);

    // ========================================== START MODAL OF CREATE ========================================== //
    const modalCreate = currentSection.find('.create-modal');
    if (modalCreate) {
        currentSection.find('.create-btn')?.click(() => {
            modalCreate.modal('show');
        });
        modalCreate.modalCreate(_services[commonRef.createAction], _$dataTable.ajax.reload, commonRef.onModalCreateShow, commonRef.onModalCreateHide);
    }
    // ==========================================  END MODAL OF CREATE  ========================================== //

    // ========================================== START MODAL OF UPDATE ========================================== //
    const modalUpdate = currentSection.find('.update-modal');

    const modalUpdateInit = (content) => {
        if (modalUpdate) {
            modalUpdate.find('div.modal-content').html(content);
            modalUpdate.modalUpdate(_services[commonRef.updateAction], () => {
                _$dataTable.ajax.reload();
            }, commonRef.onModalUpdateShow, commonRef.onModalUpdateHide);
        }
    }
    // ==========================================  END MODAL OF UPDATE  ========================================== //    

    // ========================================== START PROCESS OF SEARCH ========================================== //
    if (searchForm) {
        searchForm.find('.btn-search')?.on('click', () => {
            _$dataTable.ajax.reload();
        });

        searchForm.find('.txt-search')?.on('keypress', (e) => {
            if (e.which === 13) {
                _$dataTable.ajax.reload();
                return false;
            }
        });
    }
    // ==========================================  END PROCESS OF SEARCH  ========================================== //
});