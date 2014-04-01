$(function () {
    assignListeners();
    $("#reg-fn").focus();

    function assignListeners() {
        $("button:contains('Clear')").click(function(e) {
            $(".col-sm-9 > input").val("");
            return false;
        });
    };
});