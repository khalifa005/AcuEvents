//reveling model pattern +IFFE Imeditlly invoike function expression

var AttendanceService = function() {
    var createAttendances = function(gigId, done, fail) {

        //GigId is obj from gig dto so we dont jave to add [from body in controler parameters]
        $.post("/api/attendances", { GigId:gigId})
            .done(done)
            .fail(fail);
    };


    
    var deleteAttendances = function(gigId, done, fail) {
        $.ajax({
                url: "/api/attendances/" + gigId,
                method: "DELETE"
            })
            .done(done)
            .fail(fail);

    };
    return {
        createAttendances:createAttendances,
        deleteAttendances:deleteAttendances
    }

}();
