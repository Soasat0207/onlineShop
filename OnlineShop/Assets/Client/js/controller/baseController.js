var common = {
    init: function () {
        common.registerEvent();
    },
    registerEvent: function () {
        $("#txtKeyword").autocomplete({
            minLength: 0,
            source: function (request, response) {
                $.ajax({
                    url: "/Product/ListName", //sau đó vào product controller để viết cái phương thức listname này
                    dataType: "json",
                    data: {
                        q: request.term
                    },
                    success: function (res) {
                        response(res.data); //trả về data ở đây
                    }
                });
            },
            focus: function (event, ui) {
                $("#txtKeyword").val(ui.item.label);
                return false;
            },
            select: function (event, ui) {
                $("#txtKeyword").val(ui.item.label);
                return false;
            }
        })
     .autocomplete("instance")._renderItem = function (ul, item) { //lấy ra xong đấy vào
         return $("<li>")
           .append("<a>" + item.label + "</a>")
           .appendTo(ul);
     };
    }
}
common.init();