﻿$(function () {
    assignListeners();
    correctLabelPositions();
    focusInputElement();

    function assignListeners() {
        $("button:contains('Clear')").click(function(e) {
            $(".col-sm-9 > input").val("");
            return false;
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

    function focusInputElement() {
        var errors = $("span.field-validation-error");
        if (!errors.size()) {
            $("#reg-fn").focus();
        } else {
            errors.eq(0).next().focus();
        }
    }
});