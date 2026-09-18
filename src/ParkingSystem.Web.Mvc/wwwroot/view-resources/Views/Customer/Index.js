(function ($) {
    // 1. SERVICES & LOCALIZATION
    var _customerService = abp.services.app.customer,
        l = abp.localization.getSource("ParkingSystem");

    // 2. DOM ELEMENTS
    var _$createModal = $("#CustomerCreateModal"),
        _$createForm = _$createModal.find("form"),
        _$table = $("#CustomersTable"),
        _$searchForm = $("#CustomersSearchForm");

    // 3. PERMISSIONS (UX only)
    var canEdit = abp.auth.isGranted("Pages.Customers.ModifyAll") || abp.auth.isGranted("Pages.Customers"),
        canDelete = abp.auth.isGranted("Pages.Customers.ModifyAll") || abp.auth.isGranted("Pages.Customers");

    // 4. DATATABLE INITIALIZATION
    var _$customersTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        processing: true,
        listAction: {
            ajaxFunction: _customerService.getAll,
            inputFilter: function () {
                return _$searchForm.serializeFormToObject(true);
            }
        },
        buttons: [
            {
                name: "refresh",
                text: '<i class="fas fa-redo-alt"></i>',
                action: function () {
                    _$customersTable.draw(false);
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
                targets: 4,
                data: "creationTime",
                render: function (data) {
                    return data ? moment(data).format("YYYY-MM-DD HH:mm:ss") : "";
                }
            },
            {
                targets: 5,
                data: null,
                orderable: false,
                autoWidth: false,
                defaultContent: "",
                render: function (data, type, row) {
                    var actions = [];

                    if (canEdit) {
                        actions.push(
                            '<button type="button" class="btn btn-sm bg-secondary edit-customer me-1 mr-1" data-customer-id="' + row.id + '" data-bs-toggle="modal" data-bs-target="#CustomerEditModal">',
                            '    <i class="fas fa-pencil-alt"></i> ' + l("Edit"),
                            '</button>'
                        );
                    }

                    if (canDelete) {
                        actions.push(
                            '<button type="button" class="btn btn-sm bg-danger delete-customer" data-customer-id="' + row.id + '" data-customer-name="' + row.name + '">',
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
            Name: "required",
            PhoneNumber: "required"
        }
    });

    // 6. CREATE (SAVE)
    _$createForm.find(".save-button").on("click", function (e) {
        e.preventDefault();
        if (!_$createForm.valid()) {
            return;
        }

        var customer = _$createForm.serializeFormToObject();

        abp.ui.setBusy(_$createModal);
        _customerService.create(customer).done(function () {
            _$createModal.modal("hide");
            _$createForm[0].reset();
            abp.notify.info(l("SavedSuccessfully"));
            _$customersTable.ajax.reload();
        }).always(function () {
            abp.ui.clearBusy(_$createModal);
        });
    });

    // 7. EDIT MODAL OPEN
    $(document).on("click", ".edit-customer", function (e) {
        var customerId = $(this).attr("data-customer-id");
        e.preventDefault();
        abp.ajax({
            url: abp.appPath + "Customer/EditModal?customerId=" + customerId,
            type: "POST",
            dataType: "html",
            success: function (content) {
                $("#CustomerEditModal div.modal-content").html(content);
            }
        });
    });

    // 8. DELETE
    $(document).on("click", ".delete-customer", function () {
        var customerId = $(this).attr("data-customer-id");
        var customerName = $(this).attr("data-customer-name");

        deleteCustomer(customerId, customerName);
    });

    function deleteCustomer(id, name) {
        abp.message.confirm(
            abp.utils.formatString(l("AreYouSureWantToDelete"), name),
            null,
            function (isConfirmed) {
                if (isConfirmed) {
                    _customerService.delete({ id: id }).done(function () {
                        abp.notify.info(l("SuccessfullyDeleted"));
                        _$customersTable.ajax.reload();
                    });
                }
            }
        );
    }

    // 9. SEARCH & FILTERS
    $(".btn-search").on("click", function () {
        _$customersTable.ajax.reload();
    });

    $(".txt-search").on("keypress", function (e) {
        if (e.which === 13) {
            _$customersTable.ajax.reload();
            return false;
        }
    });

    $(".btn-clear-search").on("click", function () {
        _$searchForm[0].reset();
        _$customersTable.ajax.reload();
    });

    // 10. MODAL EVENTS & ABP EVENT LISTENERS
    _$createModal.on("shown.bs.modal", function () {
        _$createModal.find("input:not([type=hidden]):first").focus();
    }).on("hidden.bs.modal", function () {
        _$createForm[0].reset();
    });

    abp.event.on("customer.edited", function () {
        _$customersTable.ajax.reload();
    });

})(jQuery);
