(function ($) {
    var _userService = abp.services.app.user, l = abp.localization.getSource('Blaze'), _$modal = $('#UserCreateModal'),
        _$form = _$modal.find('form'), _$table = $('#UsersTable');

    $('#UsersSearchForm').submit((e) => {
        e.preventDefault();
    });

    var _$usersTable = _$table.DataTable({
        paging: true, serverSide: true, listAction: {
            ajaxFunction: _userService.getAll, inputFilter: function () {
                return $('#UsersSearchForm').serializeFormToObject(true);
            }
        }, buttons: [{
            name: 'refresh',
            text: '<i class="fas fa-redo-alt"></i>',
            className: 'waves-effect waves-light',
            action: () => _$usersTable.draw(false)
        }], responsive: {
            details: {
                type: 'column'
            }
        }, columnDefs: [{
            targets: 0,
            data: 'userName',
            sortable: false,
            render: (data, type, row) => buildUserBox(row),
            className: 'p-0 border-bottom-0 d-block h-100'
        }], createdRow: function (row, data, dataIndex) {
            $(row).attr('class', 'col');
        }
    });

    _$form.validate({
        rules: {
            Password: "required", ConfirmPassword: {
                equalTo: "#Password"
            }
        }
    });

    _$form.find('.save-button').on('click', (e) => {
        e.preventDefault();

        if (!_$form.valid()) {
            return;
        }

        var user = _$form.serializeFormToObject();
        user.roleNames = [];
        var _$roleCheckboxes = _$form[0].querySelectorAll("input[name='role']:checked");
        if (_$roleCheckboxes) {
            for (var roleIndex = 0; roleIndex < _$roleCheckboxes.length; roleIndex++) {
                var _$roleCheckbox = $(_$roleCheckboxes[roleIndex]);
                user.roleNames.push(_$roleCheckbox.val());
            }
        }

        abp.ui.setBusy(_$modal);
        _userService.create(user).done(function () {
            _$modal.modal('hide');
            _$form[0].reset();
            abp.notify.info(l(LKConstants.SavedSuccessfully));
            _$usersTable.ajax.reload();
        }).always(function () {
            abp.ui.clearBusy(_$modal);
        });
    });

    $(document).on('click', '.delete-user', function () {
        var userId = $(this).attr("data-user-id");
        var userName = $(this).attr('data-user-name');

        deleteUser(userId, userName);
    });

    function deleteUser(userId, userName) {
        abp.message.confirm(abp.utils.formatString(l(LKConstants.AreYouSureWantToDelete), userName), null, (isConfirmed) => {
            if (isConfirmed) {
                _userService.delete({
                    id: userId
                }).done(() => {
                    abp.notify.info(l(LKConstants.SuccessfullyDeleted));
                    _$usersTable.ajax.reload();
                });
            }
        });
    }

    $(document).on('click', '.edit-user', function (e) {
        var userId = $(this).attr("data-user-id");

        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'admincp/users/edit-modal?userId=' + userId,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#UserEditModal div.modal-content').html(content);
            },
            error: function (e) {
            }
        });
    });

    $(document).on('click', 'a[data-target="#UserCreateModal"]', (e) => {
        $('.nav-tabs a[href="#user-details"]').tab('show')
    });

    abp.event.on('user.edited', (data) => {
        _$usersTable.ajax.reload();
    });

    _$modal.on('shown.bs.modal', () => {
        _$modal.find('input:not([type=hidden]):first').focus();
    }).on('hidden.bs.modal', () => {
        _$form.clearForm();
    });

    $('.btn-search').on('click', (e) => {
        _$usersTable.ajax.reload();
    });

    $('.txt-search').on('input', () => {
        _$usersTable.ajax.reload();
    });

    function buildUserBox(row) {
        return `
        <div class="card team-box ${row.isTwoFactorEnabled ? 'ribbon-box border overflow-hidden' : ''}">
            <div class="team-cover">
                <img src="${row.background ?? row.avatar}" alt="${row.fullName}" class="img-fluid" onerror="this.src='${abp.setting.get('App.SiteHolderImage')}'"/>
            </div>
            <div class="card-body p-4">
                ${row.isTwoFactorEnabled ? `
                <div class="ribbon ribbon-info ribbon-shape trending-ribbon">
                    <span class="trending-ribbon-text">2FA OK</span> <i class="ri-shield-user-line text-white align-bottom float-end ms-1"></i>
                </div>
                ` : ''}
                <div class="row align-items-center team-row">
                    <div class="col team-settings">
                        <div class=" text-end dropdown">
                            <a href="javascript:void(0);" id="dropdownMenuLink2" data-bs-toggle="dropdown" aria-expanded="false">
                                <i class="ri-more-fill fs-17"></i>
                            </a>
                            <ul class="dropdown-menu dropdown-menu-end" aria-labelledby="dropdownMenuLink2">
                                <li>
                                    <button type="button" class="dropdown-item edit-user" data-user-id="${row.id}" data-bs-toggle="modal" data-bs-target="#UserEditModal"><i class="fas fa-pencil-alt me-2 align-middle"></i> ${l(LKConstants.Edit)}</button>
                                </li>
                                <li>
                                    <button class="dropdown-item delete-user" data-user-id="${row.id}" data-user-name="${row.name}"> <i class="ri-delete-bin-5-line me-2 align-middle"> </i>${l(LKConstants.Delete)} </button>
                                </li>
                            </ul>
                        </div>
                    </div>
                    <div class="col-lg-4 col">
                        <div class="team-profile-img">
                            <div class="avatar-lg img-thumbnail rounded-circle shadow flex-shrink-0">
                                <img src="${row.avatar}" alt="${row.fullName}" class="img-fluid d-block rounded-circle" onerror="this.src='${abp.setting.get('App.SiteUserAvatarHolder')}'"/>
                            </div>
                            <div class="team-content">
                                <a data-bs-toggle="offcanvas" href="#offcanvasExample" aria-controls="offcanvasExample">
                                    <h5 class="fs-16 mb-1 ${row.isActive ? '' : 'text-decoration-line-through'}">${row.fullName}${(row.isEmailConfirmed || row.isPhoneNumberConfirmed) ? '<i class="ri-checkbox-circle-fill align-middle text-info ms-1"></i>' : ''}</h5>
                                </a>
                                <p class="text-muted mb-0">${row.roleNames?.length > 0 ? row.roleNames.join(', ') : l(LKConstants.Member)}</p>
                            </div>
                        </div>
                    </div>
<!--                    <div class="col-lg-4 col">-->
<!--                        <div class="row text-muted text-center">-->
<!--                            <div class="col-6 border-end border-end-dashed">-->
<!--                                <h5 class="mb-1">225</h5>-->
<!--                                <p class="text-muted mb-0">Projects</p>-->
<!--                            </div>-->
<!--                            <div class="col-6">-->
<!--                                <h5 class="mb-1">197</h5>-->
<!--                                <p class="text-muted mb-0">Tasks</p>-->
<!--                            </div>-->
<!--                        </div>-->
<!--                    </div>-->
                    <div class="col-lg-2 col">
                        <div class="text-end">
                            <a href="pages-profile.html" class="btn btn-light view-btn"> View Profile </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        `;
    }

})(jQuery);
