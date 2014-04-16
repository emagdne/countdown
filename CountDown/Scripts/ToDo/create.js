$(function () {
    initDatePickers();
    assignListeners();
    correctLabelPositions();

    function initDatePickers() {
        $(".datepicker").datepicker();
    }
    
    function assignListeners() {
        $("button:contains('Cancel')").click(function(e) {
            window.location.href = $("#todo-create-home").attr("href");
            return false;
        });

        // This callback fixes the cancel button being clicked when enter is pressed.
        $(document).keypress(function (e) {
            if (e.which == 13) {
                $("input[type=submit]").click();
                return false;
            }
        });
    };

    // Fixes issue with the label positions being offset by validation errors
    // printed on top of input elements.
    function correctLabelPositions() {
        $(".form-group").each(function (index, element) {
            element = $(element);
            if (element.find("span.field-validation-error").size()) {
                element.find("label").css({
                    paddingTop: "25px"
                });
            }
        });
    }
});