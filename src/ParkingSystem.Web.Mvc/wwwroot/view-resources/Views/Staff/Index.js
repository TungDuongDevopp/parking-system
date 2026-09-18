(function ($) {
    // 1. SERVICES & LOCALIZATION
    var _staffService = abp.services.app.staff,
        l = abp.localization.getSource("ParkingSystem");

    // 2. DOM ELEMENTS
    var _$createModal = $("#StaffCreateModal"),
        _$createForm = _$createModal.find("form"),
        _$table = $("#StaffsTable"),
        _$searchForm = $("#StaffsSearchForm"),
        _$changeStatusModal = $("#StaffChangeStatusModal"),
        _$changeStatusForm = $("#StaffChangeStatusForm");

    // 3. PERMISSIONS (UX only)
    var isManager = abp.auth.isGranted("Pages.Staffs.Manager"),
        canEdit = abp.auth.isGranted("Pages.Staffs"),
        canDelete = isManager;

    // 4. DATATABLE INITIALIZATION
    var _$staffsTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        processing: true,
        listAction: {
            ajaxFunction: _staffService.getAll,
            inputFilter: function () {
                return _$searchForm.serializeFormToObject(true);
            }
        },
        buttons: [
            {
                name: "refresh",
                text: '<i class="fas fa-redo-alt"></i>',
                action: function () {
                    _$staffsTable.draw(false);
                }
            }
        ],
        responsive: {
            details: {
                type: "column"
            }
        },
        columnDefs: [
            { targets: 0, className: "control", defaultContent: "", orderable: false },
            { targets: 1, data: "name" },
            { targets: 2, data: "phoneNumber" },
            { targets: 3, data: "email" },
            {
                targets: 4, data: "genderName",
                name: "gender" },
            {
                targets: 5,
                data: "dateOfBirth",
                render: function (data) {
                    return data ? moment(data).format("YYYY-MM-DD") : "";
                }
            },
            {
                targets: 6,
                data: "hiredDate",
                render: function (data) {
                    return data ? moment(data).format("YYYY-MM-DD") : "";
                }
            },
            {
                targets: 7,
                data: "status",
                render: function (data, type, row) {
                    if (data === 0) {
                        return '<span class="badge badge-success bg-success">' + l("Active") + '</span>';
                    } else if (data === 1) {
                        return '<span class="badge badge-secondary bg-secondary">' + l("Inactive") + '</span>';
                    } else if (data === 2) {
                        return '<span class="badge badge-danger bg-danger">' + l("Suspended") + '</span>';
                    }
                    return row.statusName || "";
                }
            },
            {
                targets: 8,
                data: null,
                orderable: false,
                autoWidth: false,
                defaultContent: "",
                render: function (data, type, row) {
                    var actions = [];

                    if (canEdit) {
                        actions.push(
                            '<button type="button" class="btn btn-sm bg-secondary edit-staff me-1 mr-1" data-staff-id="' + row.id + '" data-bs-toggle="modal" data-bs-target="#StaffEditModal">',
                            '    <i class="fas fa-pencil-alt"></i> ' + l("Edit"),
                            '</button>'
                        );
                    }

                    if (isManager) {
                        actions.push(
                            '<button type="button" class="btn btn-sm bg-info change-status-staff me-1 mr-1" data-staff-id="' + row.id + '" data-staff-name="' + row.name + '" data-staff-status="' + row.status + '" data-bs-toggle="modal" data-bs-target="#StaffChangeStatusModal">',
                            '    <i class="fas fa-toggle-on"></i> ' + l("ChangeStatus"),
                            '</button>'
                        );
                    }

                    if (canDelete) {
                        actions.push(
                            '<button type="button" class="btn btn-sm bg-danger delete-staff" data-staff-id="' + row.id + '" data-staff-name="' + row.name + '">',
                            '    <i class="fas fa-trash"></i> ' + l("Delete"),
                            '</button>'
                        );
                    }

                    return actions.join("");
                }
            }
        ]
    });

    // 5. FORM VALIDATION (CREATE)
    _$createForm.validate({
        rules: {
            UserId: "required",
            Name: "required",
            PhoneNumber: "required",
            HiredDate: "required"
        }
    });

    // 6. CREATE (SAVE)
    _$createForm.find(".save-button").on("click", function (e) {
        e.preventDefault();
        if (!_$createForm.valid()) {
            return;
        }

        var staff = _$createForm.serializeFormToObject();
        if (staff.Gender === "true") staff.Gender = true;
        else if (staff.Gender === "false") staff.Gender = false;
        else staff.Gender = null;

        abp.ui.setBusy(_$createModal);
        _staffService.create(staff).done(function () {
            _$createModal.modal("hide");
            _$createForm[0].reset();
            abp.notify.info(l("SavedSuccessfully"));
            _$staffsTable.ajax.reload();
        }).always(function () {
            abp.ui.clearBusy(_$createModal);
        });
    });

    // 7. EDIT MODAL OPEN
    $(document).on("click", ".edit-staff", function (e) {
        var staffId = $(this).attr("data-staff-id");
        e.preventDefault();
        abp.ajax({
            url: abp.appPath + "Staff/EditModal?staffId=" + staffId,
            type: "POST",
            dataType: "html",
            success: function (content) {
                $("#StaffEditModal div.modal-content").html(content);
            }
        });
    });

    // 8. DELETE
    $(document).on("click", ".delete-staff", function () {
        var staffId = $(this).attr("data-staff-id");
        var staffName = $(this).attr("data-staff-name");

        deleteStaff(staffId, staffName);
    });

    function deleteStaff(id, name) {
        abp.message.confirm(
            abp.utils.formatString(l("AreYouSureWantToDelete"), name),
            null,
            function (isConfirmed) {
                if (isConfirmed) {
                    _staffService.delete({ id: id }).done(function () {
                        abp.notify.info(l("SuccessfullyDeleted"));
                        _$staffsTable.ajax.reload();
                    });
                }
            }
        );
    }

    // 9. SEARCH & FILTERS
    $(".btn-search").on("click", function () {
        _$staffsTable.ajax.reload();
    });

    $(".txt-search").on("keypress", function (e) {
        if (e.which === 13) {
            _$staffsTable.ajax.reload();
            return false;
        }
    });

    $(".staff-status-filter").on("change", function () {
        _$staffsTable.ajax.reload();
    });

    $(".btn-clear-search").on("click", function () {
        _$searchForm[0].reset();
        _$staffsTable.ajax.reload();
    });

    // 10. MODAL EVENTS & ABP EVENT LISTENERS
    _$createModal.on("shown.bs.modal", function () {
        _$createModal.find("input:not([type=hidden]):first").focus();
    }).on("hidden.bs.modal", function () {
        _$createForm[0].reset();
    });

    abp.event.on("staff.edited", function () {
        _$staffsTable.ajax.reload();
    });

    // --- DOMAIN-SPECIFIC BUSINESS FEATURES (STAFF) ---
    // Auto fill Name, Email, Phone when User selected in Create Modal
    $("#StaffCreate_UserId").on("change", function () {
        var $selected = $(this).find("option:selected");
        var name = $selected.data("name");
        var email = $selected.data("email");
        var phone = $selected.data("phone");

        if (name && !$("#StaffCreate_Name").val()) {
            $("#StaffCreate_Name").val(name);
        }
        if (email && !$("#StaffCreate_Email").val()) {
            $("#StaffCreate_Email").val(email);
        }
        if (phone && !$("#StaffCreate_PhoneNumber").val()) {
            $("#StaffCreate_PhoneNumber").val(phone);
        }
    });

    // Change Status Modal Open
    $(document).on("click", ".change-status-staff", function () {
        var staffId = $(this).attr("data-staff-id");
        var staffName = $(this).attr("data-staff-name");
        var status = $(this).attr("data-staff-status");

        $("#ChangeStatus_StaffId").val(staffId);
        $("#ChangeStatus_StaffName").text(staffName);
        $("#ChangeStatus_Select").val(status);
    });

    // Change Status Form Submit
    _$changeStatusForm.on("submit", function (e) {
        e.preventDefault();
        var staffId = $("#ChangeStatus_StaffId").val();
        var newStatus = parseInt($("#ChangeStatus_Select").val(), 10);

        abp.ui.setBusy(_$changeStatusModal);
        _staffService.changeStatus({
            id: staffId,
            status: newStatus
        }).done(function () {
            _$changeStatusModal.modal("hide");
            abp.notify.info(l("SavedSuccessfully"));
            _$staffsTable.ajax.reload();
        }).always(function () {
            abp.ui.clearBusy(_$changeStatusModal);
        });
    });

})(jQuery);
