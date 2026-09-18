(function ($) {
    // 1. SERVICES & LOCALIZATION
    var _vehicleService = abp.services.app.vehicle,
        l = abp.localization.getSource("ParkingSystem");

    // 2. DOM ELEMENTS
    var _$createModal = $("#VehicleCreateModal"),
        _$createForm = _$createModal.find("form"),
        _$table = $("#VehiclesTable"),
        _$searchForm = $("#VehiclesSearchForm");

    // 3. PERMISSIONS (UX only)
    var canEdit = abp.auth.isGranted("Pages.Vehicles.ModifyAll") || abp.auth.isGranted("Pages.Vehicles"),
        canDelete = abp.auth.isGranted("Pages.Vehicles.ModifyAll") || abp.auth.isGranted("Pages.Vehicles");

    // 4. DATATABLE INITIALIZATION
    var _$vehiclesTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        processing: true,
        listAction: {
            ajaxFunction: _vehicleService.getAll,
            inputFilter: function () {
                return _$searchForm.serializeFormToObject(true);
            }
        },
        buttons: [
            {
                name: "refresh",
                text: '<i class="fas fa-redo-alt"></i>',
                action: function () {
                    _$vehiclesTable.draw(false);
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
            { targets: 1, data: "vehicleCode" },
            { targets: 2, data: "vehicleTypename", name:"vehicleType" },
            { targets: 3, data: "licensePlate" },
            { targets: 4, data: "brand" },
            { targets: 5, data: "color" },
            { targets: 6, data: "customerName", defaultContent: "" },
            {
                targets: 7,
                data: "creationTime",
                render: function (data) {
                    return data ? moment(data).format("YYYY-MM-DD HH:mm:ss") : "";
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
                            '<button type="button" class="btn btn-sm bg-secondary edit-vehicle me-1 mr-1" data-vehicle-id="' + row.id + '" data-bs-toggle="modal" data-bs-target="#VehicleEditModal">',
                            '    <i class="fas fa-pencil-alt"></i> ' + l("Edit"),
                            '</button>'
                        );
                    }

                    if (canDelete) {
                        actions.push(
                            '<button type="button" class="btn btn-sm bg-danger delete-vehicle" data-vehicle-id="' + row.id + '" data-vehicle-name="' + (row.licensePlate || row.vehicleCode) + '">',
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
            VehicleType: "required",
            Color: "required"
        }
    });

    // 6. CREATE (SAVE)
    _$createForm.find(".save-button").on("click", function (e) {
        e.preventDefault();
        if (!_$createForm.valid()) {
            return;
        }

        var vehicle = _$createForm.serializeFormToObject();

        abp.ui.setBusy(_$createModal);
        _vehicleService.create(vehicle).done(function () {
            _$createModal.modal("hide");
            _$createForm[0].reset();
            abp.notify.info(l("SavedSuccessfully"));
            _$vehiclesTable.ajax.reload();
        }).always(function () {
            abp.ui.clearBusy(_$createModal);
        });
    });

    // 7. EDIT MODAL OPEN
    $(document).on("click", ".edit-vehicle", function (e) {
        var vehicleId = $(this).attr("data-vehicle-id");
        e.preventDefault();
        abp.ajax({
            url: abp.appPath + "Vehicle/EditModal?vehicleId=" + vehicleId,
            type: "POST",
            dataType: "html",
            success: function (content) {
                $("#VehicleEditModal div.modal-content").html(content);
            }
        });
    });

    // 8. DELETE
    $(document).on("click", ".delete-vehicle", function () {
        var vehicleId = $(this).attr("data-vehicle-id");
        var vehicleName = $(this).attr("data-vehicle-name");

        deleteVehicle(vehicleId, vehicleName);
    });

    function deleteVehicle(id, name) {
        abp.message.confirm(
            abp.utils.formatString(l("AreYouSureWantToDelete"), name),
            null,
            function (isConfirmed) {
                if (isConfirmed) {
                    _vehicleService.delete({ id: id }).done(function () {
                        abp.notify.info(l("SuccessfullyDeleted"));
                        _$vehiclesTable.ajax.reload();
                    });
                }
            }
        );
    }

    // 9. SEARCH & FILTERS
    $(".btn-search").on("click", function () {
        _$vehiclesTable.ajax.reload();
    });

    $(".txt-search").on("keypress", function (e) {
        if (e.which === 13) {
            _$vehiclesTable.ajax.reload();
            return false;
        }
    });

    $(".vehicle-type-filter").on("change", function () {
        _$vehiclesTable.ajax.reload();
    });

    $(".btn-clear-search").on("click", function () {
        _$searchForm[0].reset();
        _$vehiclesTable.ajax.reload();
    });

    // 10. MODAL EVENTS & ABP EVENT LISTENERS
    _$createModal.on("shown.bs.modal", function () {
        _$createModal.find("input:not([type=hidden]):first").focus();
    }).on("hidden.bs.modal", function () {
        _$createForm[0].reset();
    });

    abp.event.on("vehicle.edited", function () {
        _$vehiclesTable.ajax.reload();
    });

})(jQuery);
