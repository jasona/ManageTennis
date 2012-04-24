(function ($) {
    var emptyClass = 'field-compact-empty',
        focusClass = 'field-compact-focus';

    // Handler for blur/focus events on compact fields
    var handleFocusBlur = function (parent, label, field) {
        switch (this.type) {
            case 'focus':
                parent.addClass(focusClass);
                break;

            case 'blur':
                parent.removeClass(focusClass);
                field.val().length
            ? parent.removeClass(emptyClass)
            : parent.addClass(emptyClass);
                break;
        }

    };

    $.overLabels = function (context) {
        var compactFields = $('.field-compact', context);

        compactFields.length &&
        compactFields.each(function () {
            var container = $(this),
                label = $('label', container),
                field = $('#' + label.attr('for'));

            if (field.val() != null && !field.val().length)
                container.addClass(emptyClass);

            if (field.length && label.length) {
                field.bind('focus blur', function (e) {
                    handleFocusBlur.apply(e, [container, label, field]);
                });
            }
        });
    }
})(jQuery);