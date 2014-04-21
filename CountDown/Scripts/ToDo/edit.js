$(function() {
    initDatePickers();
    if (allowModification) unlockModificationElements();
    if (allowModification && openEditing) unlockForm();
    assignListeners();
    correctLabelPositions();

    function initDatePickers() {
        $(".datepicker").datepicker();
    }

    // Shows Delete and Edit buttons.
    function unlockModificationElements() {
        $('.todo-edit-mod-button').show();
    }

    function assignListeners() {
        $("button:contains('Cancel')").click(function(e) {
            window.location = cancelEditUrl;
            return false;
        });

        $("button:contains('Edit')").click(function(e) {
            unlockForm();
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

    // Opens the to-do item for editing
    function unlockForm() {
        $("button:contains('Delete')").add("button:contains('Edit')").hide();
        $("input[type=submit]").show();
        $("input").add("textarea").add("select").removeAttr("disabled");
    };
});