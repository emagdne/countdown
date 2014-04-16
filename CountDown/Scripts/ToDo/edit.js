$(function() {
    initDatePickers();
    if (allowModification) unlockModificationElements();
    assignListeners();
    correctLabelPositions();

    function initDatePickers() {
        $(".datepicker").datepicker();
    }

    // See comment above the "allowModification" variable.
    function unlockModificationElements() {
        $('#todo-edit-assign').removeAttr("disabled", "disabled");
        $('.todo-edit-mod-button').show();
    }

    function assignListeners() {
        $("button:contains('Cancel')").click(function (e) {
            window.alert("No item was updated.");
            window.location.href = $("#todo-edit-home").attr("href");
            return false;
        });

        // This callback disables button clicks when enter is pressed.
        $(document).keypress(function(e) {
            if (e.which == 13) {
                return false;
            }
        });
    };

    // Fixes issue with the label positions being offset by validation errors
    // printed on top of input elements.
    function correctLabelPositions() {
        $(".form-group").each(function(index, element) {
            element = $(element);
            if (element.find("span.field-validation-error").size()) {
                element.find("label").css({
                    paddingTop: "25px"
                });
            }
        });
    }
});