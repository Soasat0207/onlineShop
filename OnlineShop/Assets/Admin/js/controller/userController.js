var user = { //khai báo đối tượng user
    init: function () { //phương thức của thằng user
        user.registerEvents();
    },
    registerEvents: function () { //method
        $('.btn-active').off('click').on('click', function (e) { //thêm class off click truyền vào 1 cái event
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({ 
                url: "/Admin/User/ChangeStatus",
                data: { id: id },

                dataType: "json",
                type: "POST",
                success: function (response) {
                    console.log(response);
                    if (response.status == true) {
                        btn.text('Kích hoạt');
                    }
                    else {
                        btn.text('Khoá');
                    }
                }
            });
        });
    }
}
user.init();