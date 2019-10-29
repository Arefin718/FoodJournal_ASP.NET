$(function () {

    var resName = [];
    var ajaxdata = {};

$.ajax(
    {
        url: '/ajax/GetRestaurants',
        dataType: "json",
        data:
        {
            term: $("#restaurantName").val(),
        },
        success: function (data) {
            for (var i = 0, len = data.length; i < len; i++) {
                resName.push(data[i].RestaurantName);
            }
            $("#restaurantName").autocomplete({
                source: resName,
                select: function (event, ui) {
                    $("#restaurantName").val(ui.item.value);

                    for (var i = 0, len = data.length; i < len; i++) {                
                        if ($("#restaurantName").val() == data[i].RestaurantName) {
                            $("#restaurantLocation").val(data[i].Location);
                            $("#restId").val(data[i].RestaurantId);
                            $("#restaurantLocation").prop('readonly', true);
                            console.log(data);
                        }

                    }

                }
            });

            ajaxdata = data;
             }
        });


    $("#restaurantName").keyup(function () {

        for (var i = 0, len = ajaxdata.length; i < len; i++) {
            if ($("#restaurantName").val() != ajaxdata[i].RestaurantName) {
                $("#restaurantLocation").val("");
                $("#restId").val("0");
                $("#restaurantLocation").prop('readonly', false);
            }
        }

    });
      

});
