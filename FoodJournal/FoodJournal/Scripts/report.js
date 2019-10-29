
    $("#popUpOpen").click(function () {
        $("#dialog").dialog({
            dialogClass: "no-close",
            buttons: [
                {
                    text: "OK",
                    click: function () {
                        $(this).dialog("close");
                    }
                }
            ]
        });
    });

    $(function () {
        //----- OPEN
        $('[data-popup-open]').on('click', function (e) {
            var targeted_popup_class = jQuery(this).attr('data-popup-open');
            $('[data-popup="' + targeted_popup_class + '"]').fadeIn(350);

            e.preventDefault();
        });

    //----- CLOSE
    $('[data-popup-close]').on('click', function (e) {
            var targeted_popup_class = jQuery(this).attr('data-popup-close');
            $('[data-popup="' + targeted_popup_class + '"]').fadeOut(350);

            e.preventDefault();
        });
    });




//report submit


    function reportSubmit() {

        $("#report").submit(function (e) {


            var Type = $("#TypeOfPost").val();

            var ReportedBy = $("#ReportedBy").val();

            var ReportedId = $("#ReportedId").val();


            var RestaurantId = $("#RestaurantId").val();
            var PostId = $("#PostId").val();
            var RecipeId = $("#RecipeId").val();



            var Details = $("#Details").val();


            var report = {
                Type: Type,
                ReportedBy: ReportedBy,
                ReportedId: ReportedId,
                RestaurantId: RestaurantId,
                PostId: PostId,
                RecipeId: RecipeId,
                Details: Details
            }

  

            $.ajax({
                type: "POST",
                url: "/report/NewReport",
                data: report,
                success: function (data) {
                    console.log("Posted");
                    $("#reportPopUp").css("display", "none");
                    $("#Details").val("");
                }
            });


            e.preventDefault();


        });

    }