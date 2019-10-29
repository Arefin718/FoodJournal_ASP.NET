$(function () {

    var resName = [];

    $.ajax(
        {
            url: '/ajax/GetRestaurants',
            dataType: "json",
            data:
            {
                term: $("#search").val(),
            },
            success: function (data) {
                for (var i = 0, len = data.length; i < len; i++) {

                    var rest = {
                        value: "/restaurant/MenusAndOffers/" + data[i].RestaurantId,
                        label: data[i].RestaurantName
                    };

                    resName.push(rest);
                }

                
                $("#search").autocomplete({
                    source: resName,
                    select: function (event, ui) {
                        window.location.href = ui.item.value;
                    }
                });


                $("#msearch").autocomplete({
                    source: resName,
                    select: function (event, ui) {
                        window.location.href = ui.item.value;
                    }
                });
            
            }

        });


});
