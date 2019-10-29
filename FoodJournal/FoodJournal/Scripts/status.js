function RestaurantStatusChange(resId) {
    $.ajax({
        type: "POST",
        url: "/status/RestaurantStatus?id="+resId
    });
}


function PostStatusChange(postId) {
    $.ajax({
        type: "POST",
        url: "/status/PostStatus?id=" + postId
    });
}


function UserStatusChange(userId) {
    $.ajax({
        type: "POST",
        url: "/status/UserStatus?id=" + userId
    });
}

function RecipeStatusChange(recipeId) {
    $.ajax({
        type: "POST",
        url: "/status/RecipeStatus?id=" + recipeId
    });
}
