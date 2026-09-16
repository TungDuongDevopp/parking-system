(function ($) {
    var _staffService = abp.services.app.staff,
        l = abp.localization.getSource("ParkingSystem"),
        isManager = abp.auth.isGranted("Pages.Staffs.Manager"),
        _$createModal = $("#StaffCreateModal"),
        _$createForm = _$createModal.find("form"),
        _$changeStatusModal = $("#StaffChangeStatusModal"),
        _$changeStatusForm = $("#StaffChangeStatusForm"),
        _$table = $("#StaffsTable");

    var _$staffsTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        processing: true,
        listAction: {
            ajaxFunction: _staffService.getAll,
            inputFilter: function () {
                var filter = $("#StaffsSearchForm").serializeFormToObject(true);
                return filter;
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
        columnDefs: [
            { targets: 0, className: "control", defaultContent: "", orderable: false },
            { targets: 1, data: "name" },
            { targets: 2, data: "phoneNumber" },
            { targets: 3, data: "email" },
            { targets: 4, data: "genderName" },
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
                render: function (data, type, row) {
                    var actions = [];
                    actions.push(
                        '<button type="button" class="btn btn-sm bg-secondary edit-staff me-1 mr-1" data-staff-id="' + row.id + '" data-bs-toggle="modal" data-bs-target="#StaffEditModal">',
                        '   <i class="fas fa-pencil-alt"></i> ' + l("Edit"),
                        '</button>'
                    );

                    if (isManager) {
                        actions.push(
                            '<button type="button" class="btn btn-sm bg-info change-status-staff me-1 mr-1" data-staff-id="' + row.id + '" data-staff-name="' + row.name + '" data-staff-status="' + row.status + '" data-bs-toggle="modal" data-bs-target="#StaffChangeStatusModal">',
                            '   <i class="fas fa-toggle-on"></i> ' + l("ChangeStatus"),
                            '</button>'
                        );
                        actions.push(
                            '<button type="button" class="btn btn-sm bg-danger delete-staff" data-staff-id="' + row.id + '" data-staff-name="' + row.name + '">',
                            '   <i class="fas fa-trash"></i> ' + l("Delete"),
                            '</button>'
                        );
                    }
                    return actions.join("");
                }
            }
        ]
    });

    // Tự động điền Name, Email, Phone khi chọn User trong Create Modal
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

    // Validate create form
    _$createForm.validate({
        rules: {
            UserId: "required",
            Name: "required",
            PhoneNumber: "required",
            HiredDate: "required"
        }
    });

    // Save Staff (Create)
    _$createForm.find(".save-button").on("click", function (e) {
        e.preventDefault();
        if (!_$createForm.valid()) return;

        var staff = _$createForm.serializeFormToObject();
        // Convert gender to boolean or null
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

    // Open Edit Modal
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

    // Open Change Status Modal
    $(document).on("click", ".change-status-staff", function () {
        var staffId = $(this).attr("data-staff-id");
        var staffName = $(this).attr("data-staff-name");
        var status = $(this).attr("data-staff-status");

        $("#ChangeStatus_StaffId").val(staffId);
        $("#ChangeStatus_StaffName").text(staffName);
        $("#ChangeStatus_Select").val(status);
    });

    // Submit Change Status
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

    // Delete Staff
    $(document).on("click", ".delete-staff", function () {
        var staffId = $(this).attr("data-staff-id");
        var staffName = $(this).attr("data-staff-name");

        abp.message.confirm(
            abp.utils.formatString(l("StaffDeleteWarningMessage"), staffName),
            null,
            function (isConfirmed) {
                if (isConfirmed) {
                    _staffService.delete({ id: staffId }).done(function () {
                        abp.notify.info(l("SuccessfullyDeleted"));
                        _$staffsTable.ajax.reload();
                    });
                }
            }
        );
    });

    // Search and filters
    $(".btn-search").on("click", function () {
        _$staffsTable.ajax.reload();
    });

    $(".staff-status-filter").on("change", function () {
        _$staffsTable.ajax.reload();
    });

    $(".btn-clear-search").on("click", function () {
        $("#StaffsSearchForm")[0].reset();
        _$staffsTable.ajax.reload();
    });

    $(".txt-search").on("keypress", function (e) {
        if (e.which === 13) {
            _$staffsTable.ajax.reload();
            return false;
        }
    });

    // Reload when edited
    abp.event.on("staff.edited", function () {
        _$staffsTable.ajax.reload();
    });

})(jQuery);
