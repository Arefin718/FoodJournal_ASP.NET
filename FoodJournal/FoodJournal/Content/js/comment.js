$("#commentform").submit(function (e) {


    if ($("#UserId").val() == 0) {
        window.location.href = "/User/Login";
    }

    var Postid = $("#PostId").val();
    var UserID = $("#UserId").val();
    var Time = $("#Time").val();
    var CommentDescription = $("#CommentDescription").val();

    var comment = {
        Postid: Postid,
        UserID: UserID,
        Time: Time,
        CommentDescription: CommentDescription
    }


    $.ajax({
        type: "POST",
        url: "/Comment/CommentSubmit",
        data: comment,  
        success: function (data) {
            console.log(data.comment.CommentDescription);


            var name = "Shuvro";
            var time = "31 July 2008";
            var description = "The Review is paid";
            var image = "asda";

            var comment =

                "  <div class=\"entry-comments mt-30\">\n" +
                "\n" +
                "\n" +
                "                                        <ul class=\"comment-list\">\n" +
                "                                            <li class=\"comment\">\n" +
                "                                                <div class=\"comment-body\">\n" + "<a href=\"/Profile/view/" + data.comment.UserId  + "\"><div class=\"comment-avatar\">\n" +
                "<img style=\"height:80px; width:80px;\"  alt=\"\" src=\"" + data.comment.Image + "\">\n" +
                "</div>\n" +
                "</a>" +
                "                                                    <div class=\"comment-text\">\n" +
                "                                                       <a href=\"/profile/view/" + data.comment.UserId + "\"><h6 class=\"comment-author\">" + data.comment.Name + "</h6></a>" +
                "                                                        <div class=\"comment-metadata\">\n" +
                "                                                            <a href=\"#\" class=\"comment-date\">" + data.comment.Time + "</a>\n" +
                "                                                        </div>\n" +
                "                                                        <p>" + data.comment.CommentDescription + "</p>\n" +
                "                                                    </div>\n" +
                "                                                </div>\n" +
                "\n" +
                "                                            </li> <!-- end 1-2 comment -->\n" +
                "\n" +
                "                                        </ul>\n" +
                "                                    </div> <!-- end comments -->";

            $("#comments").append(comment);
            $("#CommentDescription").val("");
            

        }
    });


    e.preventDefault(); 


    

});