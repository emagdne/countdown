$(function() {
    assignListeners();
    colorRowsOwnedByMe();
    colorRowsAssignedToMe();
    colorOverdueItems();
    colorCompletedItems();

    function assignListeners() {
        $(".index-completed-checkbox").change(function (e) {
            var checkbox = $(e.target);
            var confirm = window.confirm("Are you sure that you want to mark this item as completed?");
            if (confirm) {
                markItemComplete(checkbox.parent().parent().find(".index-id-cell").text(), checkbox);
            } else {
                checkbox.removeAttr("checked");
            }
        });

        $(".index-table-row > td").click(function (e) {
            var cell = $(e.target);
            var id = cell.parent().find(".index-id-cell").text();
            window.location = editUrl + "?toDoItemId=" + id;
        });
    };

    function colorRowsOwnedByMe() {
        $(".index-owner-cell:contains('Me')").each(function(index, element) {
            var row = $(element).parent();
            row.css({
                backgroundColor: "rgb(255, 255, 187)"
            });
        });
    };

    function colorRowsAssignedToMe() {
        $(".index-assignee-cell:contains('Me')").each(function(index, element) {
            var row = $(element).parent();
            row.css({
                backgroundColor: "rgb(255, 226, 174)"
            });
        });
    };

    function colorOverdueItems() {
        $(".index-timeleft-cell:contains('Passed due date.')").each(function(index, element) {
            var row = $(element).parent();
            row.css({
                backgroundColor: "rgb(255, 203, 203)"
            });
        });
    }

    function colorCompletedItems() {
        $(".index-completed-cell > input[checked=checked]").each(function(index, element) {
            var row = $(element).parent().parent();
            row.css({
                textDecoration: "line-through",
                color: "gray",
                backgroundColor: "rgb(223, 223, 223)"
            });
        });
    };

    function markItemComplete(toDoId, checkbox) {
        $.post(completeUrl, {
            "toDoItemId":toDoId
        }, function (data, status) {
            if (data["Status"] == "Success") {
                location.reload();
            } else {
                alert("Error - " + data["Error"]);
                checkbox.removeAttr("checked");
            }
        }).fail(function() {
            alert("Error - Request to server failed.");
            checkbox.removeAttr("checked");
        });
    };
});