var FollowingService= function() {
    
    var followFamily = function(userId, done, fail) {

        $.post("/api/Followings", { followeeId: userId })
            .done(done)

            .fail(fail);
    };

    var unFollowFamily = function(userId, done, fail) {
        
        $.ajax({
                url: "/api/Followings/" + userId,
                method: "DELETE"
            })
            .done(done)
            .fail(fail);
    };

    return {
        followFamily: followFamily,
        unFollowFamily: unFollowFamily
    }
}();