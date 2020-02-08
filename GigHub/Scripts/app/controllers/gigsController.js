//reveling model pattern +IFFE Imeditlly invoike function expression

var GigsController = function(attendanceService) {

    var button;

    var init = function(container) {
        $(container).on("click", ".js-toggle-attendance", toggleAttendances);
        //with this implemntation we cant access events or gigs if we add load more in the dom
        //$(".js-toggle-attendance").click(toggleAttendances);

    };

    var toggleAttendances = function(e) {
        //we can replace other $(e.target) with new var button
        button = $(e.target);

        var gigId = button.attr("data-gig-id");

        //we create>>CREATEATTENDANCES SO EVERY THING CAN FOLLOW SEPERATION OF CONCERN RULE
        if (button.hasClass("btn-default")) 
            attendanceService.createAttendances(gigId,done, fail);
        else 
            attendanceService.deleteAttendances(gigId,done, fail);
        

        ////GigId is obj from gig dto so we dont have to add [from body in controler parameters]
        //$.post("/api/attendances", { GigId: $(e.target).attr("data-gig-id") })

        //    .done(function () {

        //        //we should put e.target in a sperate var so jqyery dont run twice
        //        $(e.target)
        //            .removeClass("btn-default")
        //            .addClass("btn-info")
        //            .text("Going");

        //    })


        //    .fail(function () {
        //        alert("something went wrong");
        //    });

    };

  
    var fail = function() {
        alert("something went wrong");
    };

    var done = function() {
        var text = (button.text() === "Going") ? "Going?" : "Going";

        button.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };
    return {
        init: init
    };


}(AttendanceService);



