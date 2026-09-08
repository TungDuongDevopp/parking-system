(function ($) {
    var _customerService = abp.services.app.customer,
        l = abp.localization.getSource("ParkingSystem"),
        _$modal = $("#CustomerCreateModal"),
        _$form = _$modal.find("form"),
        _$table = $("#CustomersTable");

    var _$customersTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        processing: true,
        listAction: {
            ajaxFunction: _customerService.getAll,
            inputFilter: function () {
                return $("#CustomersSearchForm").serializeFormToObject(true);
            }
        },
        buttons: [
            {
                name: "refresh",
                text: '<i class="fas fa-redo-alt"></i>',
                action: () => _$customersTable.draw(false)
            }
        ],
        columnDefs: [
            { targets: 0, className: "control", defaultContent: "", orderable: false },
            { targets: 1, data: "name" },
            { targets: 2, data: "phoneNumber" },
            { targets: 3, data: "email" },
            {
                targets: 4,
                data: "creationTime",
                render: (data) => data ? moment(data).format("YYYY-MM-DD HH:mm:ss") : ""
            },
            {
                targets: 5,
                data: null,
                orderable: false,
                render: (data, type, row) => [
                    `   <button type="button" class="btn btn-sm bg-secondary edit-customer" data-customer-id="${row.id}" data-bs-toggle="modal" data-bs-target="#CustomerEditModal">`,
                    `       <i class="fas fa-pencil-alt"></i> ${l("Edit")}`,
                    "   </button>",
                    `   <button type="button" class="btn btn-sm bg-danger delete-customer" data-customer-id="${row.id}" data-customer-name="${row.name}">`,
                    `       <i class="fas fa-trash"></i> ${l("Delete")}`,
                    "   </button>"
                ].join("")
            }
        ]
    });

    _$form.validate({
        rules: {
            Name: "required",
            PhoneNumber: "required"
        }
    });

    _$form.find(".save-button").on("click", (e) => {
        e.preventDefault();
        if (!_$form.valid()) return;

        var customer = _$form.serializeFormToObject();
        abp.ui.setBusy(_$modal);
        _customerService.create(customer).done(function () {
            _$modal.modal("hide");
            _$form[0].reset();
            abp.notify.info(l("SavedSuccessfully"));
            _$customersTable.ajax.reload();
        }).always(function () {
            abp.ui.clearBusy(_$modal);
        });
    });

    $(document).on("click", ".delete-customer", function () {
        var customerId = $(this).attr("data-customer-id");
        var customerName = $(this).attr("data-customer-name");

        abp.message.confirm(
            abp.utils.formatString(l("AreYouSureWantToDelete"), customerName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    _customerService.delete({ id: customerId }).done(() => {
                        abp.notify.info(l("SuccessfullyDeleted"));
                        _$customersTable.ajax.reload();
                    });
                }
            }
        );
    });

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

    abp.event.on("customer.edited", () => _$customersTable.ajax.reload());
    $(".btn-search").on("click", () => _$customersTable.ajax.reload());
    $(".txt-search").on("keypress", (e) => {
        if (e.which == 13) {
            _$customersTable.ajax.reload();
            return false;
        }
    });
})(jQuery);
