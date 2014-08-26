(function ($) {
    'use strict';

    ko.bindingHandlers.fadeVisible = {
        init: function (element, valueAccessor) {
            var shouldDisplay = valueAccessor();
            $(element).toggle(shouldDisplay);
        },
        update: function (element, valueAccessor) {
            var shouldDisplay = valueAccessor();
            shouldDisplay ? $(element).fadeIn(2000) : $(element).fadeOut(500);
        }
    };

    ko.bindingHandlers.slideVisible = {
        init: function (element, valueAccessor) {
            var value = ko.utils.unwrapObservable(valueAccessor());
            $(element).toggle(value);
        },

        update: function (element, valueAccessor, allBindingsAccessor) {
            var value = valueAccessor(), allBindings = allBindingsAccessor();
            var valueUnwrapped = ko.utils.unwrapObservable(value);
            var duration = allBindings.slideDuration || 400;
            if (valueUnwrapped == true)
                $(element).slideDown(duration);
            else
                $(element).slideUp(duration);
        }
    };
})(jQuery);           