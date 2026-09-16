(function ($) {
    var _staffService = abp.services.app.staff,
        l = abp.localization.getSource("ParkingSystem"),
        _$modal = $("#StaffEditModal"),
        _$form = _$modal.find("form");

    _$form.validate({
        rules: {
            Name: "required",
            PhoneNumber: "required",
            HiredDate: "required"
        }
    });

    function save() {
        if (!_$form.valid()) return;

        var staff = _$form.serializeFormToObject();
        if (staff.Gender === "true") staff.Gender = true;
        else if (staff.Gender === "false") staff.Gender = false;
        else staff.Gender = null;

        abp.ui.setBusy(_$form);
        _staffService.update(staff).done(function () {
            _$modal.modal("hide");
            abp.notify.info(l("SavedSuccessfully"));
            abp.event.trigger("staff.edited", staff);
        }).always(function () {
            abp.ui.clearBusy(_$form);
        });
    }

    _$form.closest("div.modal-content").find(".save-button").click(function (e) {
        e.preventDefault();
        save();
    });

    _$form.find("input").on("keypress", function (e) {
        if (e.which === 13) {
            e.preventDefault();
            save();
        }
    });
})(jQuery);
