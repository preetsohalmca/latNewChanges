(function ($) {
    /// <deprecated type="deprecate">
    ///     This file is deprecated, use nvs.validate.js and nvs.validate.unobtrusive.adapters.js
    /// </deprecated>
    if (console)
        console.log('This file is deprecated, use nvs.validate.js and nvs.validate.unobtrusive.adapters.js.');

    $.validator.addMethod('dateTimeRange', function (value, element, params) {
        /// <deprecated type="deprecate">
        ///     DateTimeRange Client Validator
        /// </deprecated>
        if (console)
            console.log('This file is deprecated, use nvs.validate.js and nvs.validate.unobtrusive.adapters.js.');

        if ($.NVS.isBlank(value)) {
            return false; // not testing 'is required' here!
        }

        // parse the value from DatePicker
        var dateValue = kendo.parseDate(value);

        // if can't convert
        if (dateValue == null)
            throw new Error("Could not convert to date with kendo.parseDate");

        return dateValue >= params.min && dateValue <= params.max;
    });

    //overrides jquery validate date function.
    //Jquery validate date function has issue with chrome as it does not recognize the culture specific format.
    //This behaviour causes issue in validation. "The field is not valid date" message gets displayed in chrome though the date is valid date.
    //Below function returns culture specific date if the browser is chrome.
    $.validator.methods.date = function (value, element) {
        /// <deprecated type="deprecate">
        ///     validator.methods.date
        /// </deprecated>

        if (console)
            console.log('This file is deprecated, use nvs.validate.js and nvs.validate.unobtrusive.adapters.js.');

        var d = new Date();
        return this.optional(element) || !/Invalid|NaN/.test(new Date((/chrom(e|ium)/.test(navigator.userAgent.toLowerCase())) ? d.toLocaleDateString(value) : value));
    };

    $.validator.unobtrusive.adapters.add('datetimerange', ['min', 'max'], function (options) {
        /// <deprecated type="deprecate">
        ///     adapters datetimerange
        /// </deprecated>

        if (console)
            console.log('This file is deprecated, use nvs.validate.js and nvs.validate.unobtrusive.adapters.js.');

        var params = {
            min: kendo.parseDate(options.params.min),
            max: kendo.parseDate(options.params.max)
        };

        options.rules['dateTimeRange'] = params;
        options.messages['dateTimeRange'] = options.message;
    });

    $.validator.addMethod('compareTo', function (value, element, params) {
        /// <deprecated type="deprecate">
        ///     validator compareTo
        /// </deprecated>

        if (console)
            console.log('This file is deprecated, use nvs.validate.js and nvs.validate.unobtrusive.adapters.js.');

        var $currentElement = $(element);
        var currentValue = $.NVS.getTypedValue($currentElement);
        var $targetElement = $(params.targetSelector);
        var targetValue = $.NVS.getTypedValue($targetElement);

        // check if it could get the values
        if (currentValue == null || targetValue == null) {
            if (this.settings.debug && window.console) {
                console.log("CompareTo valitor doesn't validate required fields, therefore it'll be always pass.");
            }
            return true;
        }

        var currentValueType = $.NVS.getDataType(currentValue);
        var targetValueType = $.NVS.getDataType(targetValue);

        // check if it the values have the same type
        if (currentValueType != targetValueType) {
            if (this.settings.debug && window.console) {
                console.log("The values from elements have different types.");
            }
            return false;
        }

        // to avoid comparing objects
        if (currentValueType == "date") {
            currentValue = currentValue.getTime();
            targetValue = targetValue.getTime();
        }

        var isValid = false;
        switch (params.operation) {
            case "EqualTo":
                isValid = currentValue === targetValue;
                break;

            case "NotEqualTo":
                isValid = currentValue !== targetValue;
                break;

            case "LessThan":
                isValid = currentValue < targetValue;
                break;

            case "LessThanOrEqualTo":
                isValid = currentValue <= targetValue;
                break;

            case "GreaterThan":
                isValid = currentValue > targetValue;
                break;

            case "GreaterThanOrEqualTo":
                isValid = currentValue >= targetValue;
                break;
        }

        return isValid;
    });

    $.validator.unobtrusive.adapters.add('compareTo', ['targetselector', 'operation'], function (options) {
        /// <deprecated type="deprecate">
        ///     adapters compareTo
        /// </deprecated>

        if (console)
            console.log('This file is deprecated, use nvs.validate.js and nvs.validate.unobtrusive.adapters.js.');

        var params = {
            targetSelector: options.params.targetselector,
            operation: options.params.operation
        };

        options.rules['compareTo'] = params;
        options.messages['compareTo'] = options.message;
    });

})(jQuery);