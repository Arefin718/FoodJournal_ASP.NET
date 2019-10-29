$(function () {




    $("#filterSearch").submit(function (e) {

        if ($('#searchByName').is(':checked')) {

            var resName = $("#resrSearch").val();

            window.location.href = '/restaurant/SearchByRestaurantName?id=' + resName;

            e.preventDefault();
        }

  
        if ($('#searchByLoc').is(':checked')) {

            var resLocation = $("#resrSearch").val();

            window.location.href = '/restaurant/SearchByRestaurantLocation?id=' + resLocation;

            e.preventDefault();
        }

    });






    $("#resrSearch").keyup(function () {
       
 
        if ($('#searchByName').is(':checked')) {


  
        var resName = [];

        $.ajax(
            {
                url: '/ajax/GetRestaurants',
                dataType: "json",
                data:
                {
                    term: $("#resrSearch").val(),
                },
                success: function (data) {
                    for (var i = 0, len = data.length; i < len; i++) {

                        var rest = {
                            value:  data[i].RestaurantId,
                            label: data[i].RestaurantName
                        };

                        resName.push(rest);
                    }


                    $("#resrSearch").autocomplete({
                        source: resName,
                       select: function (event, ui) {
                           window.location.href = "/restaurant/MenusAndOffers/"+ ui.item.value;                                                
                        }
                    });


                }

            });


    }

   

    });


});
