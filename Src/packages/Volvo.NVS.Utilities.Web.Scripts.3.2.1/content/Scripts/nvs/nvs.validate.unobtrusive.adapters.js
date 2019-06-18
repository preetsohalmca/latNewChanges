(function ($) {
    "use strict";

    $.validator.unobtrusive.adapters.add('datetimerange', ['min', 'max'], function (options) {
        /// <summary>
        ///     Adapter for DateTimeRange Client Validator.
        /// </summary>

        var params = {
            min: kendo.parseDate(options.params.min),
            max: kendo.parseDate(options.params.max)
        };

        options.rules['dateTimeRange'] = params;
        options.messages['dateTimeRange'] = options.message;
    });

    $.validator.unobtrusive.adapters.add('compareto', ['targetselector', 'operation'], function (options) {
        /// <summary>
        ///     Adapter for CompareTo Client Validator.
        /// </summary>

        var params = {
            targetSelector: options.params.targetselector,
            operation: options.params.operation
        };

        options.rules['compareTo'] = params;
        options.messages['compareTo'] = options.message;
    });

    $.validator.unobtrusive.adapters.add('kendorequired', function (options) {
        /// <summary>
        ///     Adapter for KendoRequired Client Validator.
        /// </summary>

        options.rules['kendoRequired'] = true;
        options.messages['kendoRequired'] = options.message;
    });
})(jQuery);
