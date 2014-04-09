$(function() {
    colorRowsOwnedByMe();
    colorRowsAssignedToMe();
    colorOverdueItems();
    colorCompletedItems();

    function colorRowsOwnedByMe() {
        $(".index-owner-cell:contains('Me')").each(function(index, element) {
            var row = $(element).parent();
            row.css({
                backgroundColor: "rgb(255, 255, 187)"
            });
        });
    };

    function colorRowsAssignedToMe() {
        $(".index-assignee-cell:contains('Me')").each(function (index, element) {
            var row = $(element).parent();
            row.css({
                backgroundColor: "rgb(255, 226, 174)"
            });
        });
    };

    function colorOverdueItems() {
        $(".index-timeleft-cell:contains('Passed due date.')").each(function (index, element) {
            var row = $(element).parent();
            row.css({
                backgroundColor: "rgb(255, 203, 203)"
            });
        });
    }

    function colorCompletedItems() {
        $(".index-completed-cell > input[checked=checked]").each(function (index, element) {
            var row = $(element).parent().parent();
            row.css({
                textDecoration: "line-through",
                color: "gray",
                backgroundColor: "rgb(223, 223, 223)"
            });
        });
    };
});