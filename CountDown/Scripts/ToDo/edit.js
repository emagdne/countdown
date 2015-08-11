$(function() {
    initDatePickers();
    if (allowModification) unlockModificationElements();
    if (allowModification && openEditing) unlockForm();
    assignListeners();
    correctLabelPositions();

    function initDatePickers() {
        $(".datepicker").datepicker({
            orientation: 'top left',
            autoclose: true
        });
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

        $("button:contains('Delete')").click(function(e) {
            var confirm = window.confirm("Are you sure that you want to delete this item?");
            if (confirm) {
                post(deleteUrl, {
                    toDoItemId: $("input[name=id]").val()
                });
            }
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

    // Taken from: http://stackoverflow.com/questions/133925/javascript-post-request-like-a-form-submit
    function post(path, parameters) {
        var form = $('<form></form>');

        form.attr("method", "post");
        form.attr("action", path);

        $.each(parameters, function(key, value) {
            var field = $('<input></input>');

            field.attr("type", "hidden");
            field.attr("name", key);
            field.attr("value", value);

            form.append(field);
        });

        // The form needs to be a part of the document in
        // order for us to be able to submit it.
        $(document.body).append(form);
        form.submit();
    }
});