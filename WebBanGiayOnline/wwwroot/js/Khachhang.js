$(document).ready(function () {
    // Cập nhật thông tin khách hàng
    $("#customerForm").submit(function (e) {
        e.preventDefault();
        $.post("/Admin/Khachhang/UpdateCustomer", $(this).serialize(), function (res) {
            if (res.success) location.reload();
        });
    });

    // Hiển thị modal thêm địa chỉ
    $("#addAddress").click(function () {
        $("#addressForm")[0].reset();
        $("#addressId").val("");
        $("#addressModal").show();
    });

    // Đóng modal khi nhấn bên ngoài
    $(document).mouseup(function (e) {
        if (!$("#addressModal").is(e.target) && $("#addressModal").has(e.target).length === 0) {
            $("#addressModal").hide();
        }
    });

    // Gọi API lấy danh sách tỉnh
    $.get("https://provinces.open-api.vn/api/p/", function (data) {
        let options = data.map(p => `<option value="${p.name}" data-id="${p.code}">${p.name}</option>`);
        $("#province").append(options);
    });

    // Gọi API lấy danh sách huyện khi chọn tỉnh
    $("#province").change(function () {
        let provinceCode = $("#province option:selected").data("id");
        $.get(`https://provinces.open-api.vn/api/p/${provinceCode}?depth=2`, function (data) {
            let options = data.districts.map(d => `<option value="${d.name}" data-id="${d.code}">${d.name}</option>`);
            $("#district").html(options).trigger("change");
        });
    });

    // Gọi API lấy danh sách xã khi chọn huyện
    $("#district").change(function () {
        let districtCode = $("#district option:selected").data("id");
        $.get(`https://provinces.open-api.vn/api/d/${districtCode}?depth=2`, function (data) {
            let options = data.wards.map(w => `<option value="${w.name}">${w.name}</option>`);
            $("#ward").html(options);
        });
    });

    // Lưu địa chỉ (thêm hoặc sửa)
    $("#addressForm").submit(function (e) {
        e.preventDefault();
        $.post("/Admin/Khachhang/UpdateAddress", $(this).serialize(), function (res) {
            if (res.success) location.reload();
        });
    });

    // Sửa địa chỉ
    $(".editAddress").click(function () {
        let id = $(this).data("id");
        $.get(`/GetAddress/${id}`, function (address) {
            $("#addressId").val(address.ID);
            $("#province").val(address.tinh).trigger("change");

            $("#province").one("change", function () {
                $("#district").val(address.huyen).trigger("change");

                $("#district").one("change", function () {
                    $("#ward").val(address.xa);
                });
            });

            $("#diaChi").val(address.dia_chi);
            $("#addressModal").show();
        });
    });

    // Xóa địa chỉ
    $(".deleteAddress").click(function () {
        let id = $(this).data("id");
        if (confirm("Bạn có chắc muốn xóa địa chỉ này?")) {
            $.post("/Admin/Khachhang/DeleteAddress", { id }, function (res) {
                if (res.success) location.reload();
            });
        }
    });

    // Chọn địa chỉ mặc định
    $(".setDefault").click(function () {
        let addressId = $(this).data("id");
        let customerId = $("input[name='ID']").val();
        $.post("/Admin/Khachhang/SetDefaultAddress", { addressId, customerId }, function (res) {
            if (res.success) location.reload();
        });
    });
});
