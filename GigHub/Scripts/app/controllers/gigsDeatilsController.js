var gigDeatilsController= function(followingService) {

    var button;

    var init = function() {

        //js for follow an family
        $(".js-toggle-follow").click(toggleFollowing);

    };



    var toggleFollowing = function(e) {
        

        button = $(e.target);
        var userId = button.attr("data-user-id");

        if (button.hasClass("btn-default"))
            followingService.followFamily(userId, done, fail);
        else
            followingService.unFollowFamily(userId, done, fail);

    };

    var fail = function() {
        alert("something went wrong");
    };

    var done = function() {
        //change follow and unfollow text
        var text = (button.text() == "Follow") ? "Following" : "Follow";

        button.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };

    return {
        init: init
    }

}(FollowingService);
