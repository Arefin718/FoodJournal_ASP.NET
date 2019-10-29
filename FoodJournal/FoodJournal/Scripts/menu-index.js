$("#mc4wp-form-1").submit(function (e) {

    var SubscriberMail = $("#subscribeemailaddress").val();

    var Subscriber = {
        SubscriberMail: SubscriberMail
    }


    $.ajax({
        type: "POST",
        url: "/other/subscribe",
        data: Subscriber,
        success: function (data) {
             $("#subscribeemailaddress").val("");

        }
    });


    e.preventDefault();




});