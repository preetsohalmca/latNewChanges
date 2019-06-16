$(function () {
    /// <summary>
    ///     The defines the NVS Custom Validations settings for NVS Web Applications.
    /// </summary>

    "use strict";

    if ($.NVS == undefined || $.NVS == null) {
        throw new Error("nvs.common is not initalized.");
    }

    if ($.NVS.isBlank($.validator)) {
        throw new Error("jquery.validate is not initalized.");
    }

    $.NVS.validator = function () {
        /// <summary>
        ///     This function returns the NVS jQuery validator object with custons settings for NVS Web based applications.
        ///     It requires nvs.common.js and should be included between jquery.validation.js and jquery.validate.unobtrusive.js.
        /// </summary>

        var defaultHighlight = $.validator.defaults.highlight;
        var defaultUnhighlight = $.validator.defaults.unhighlight;

        function getKendoPlaceholder(element) {
            /// <summary>
            ///     Based on the type of the Kendo UI widget return the jQuery placeholder of the widget.
            ///     If isn't a Kendo widget, it'll return undefined.
            /// </summary>
            /// <param name="element" type="object">
            ///     An html element.
            /// </param>
            /// <returns type="jQuery">jQuery object.</returns>

            var $element = $(element);
            // Check if the $element is invalid or if is a Kendo Widget
            if (!($element instanceof jQuery) || kendo.widgetInstance($element) == undefined)
                return undefined;

            switch ($element.data("role")) {
                case "combobox":
                    return $element.parent().find("span input");

                case "dropdownlist":
                    return $element.parent().find("span.k-input");

                case "multiselect":
                    return $element.closest(".k-multiselect");

                case "numerictextbox":
                    return $element.parent().find(".k-formatted-value");

                    // it uses the default input
                case "datepicker":
                case "datetimepicker":
                case "maskedtextbox":
                case "autocomplete":
                    return $element;

                default:
                    throw new Error("Not mapped Kendo Widget.");
            }
        }

        function unhighlight(element, errorClass, validClass) {
            /// <summary>
            ///     NVS unhighlight to handle also Kendo UI widgets.
            /// </summary>
            /// <param name="element" type="object">
            ///     An html element.
            /// </param>
            /// <param name="errorClass" type="string">
            ///     CSS class for unvalid elements.
            /// </param>
            /// <param name="validClass" type="string">
            ///     CSS class for valid elements.
            /// </param>

            var $kendoPlaceholder = getKendoPlaceholder(element);
            if ($kendoPlaceholder)
                $kendoPlaceholder.removeClass($.NVS.validator.settings.errorClassForKendo);
            else
                defaultUnhighlight(element, errorClass, validClass);
        }

        function highlight(element, errorClass, validClass) {
            /// <summary>
            ///     NVS highlight to handle also Kendo UI widgets.
            /// </summary>
            /// <param name="element" type="object">
            ///     An html element.
            /// </param>
            /// <param name="errorClass" type="string">
            ///     CSS class for unvalid elements.
            /// </param>
            /// <param name="validClass" type="string">
            ///     CSS class for valid elements.
            /// </param>

            var $kendoPlaceholder = getKendoPlaceholder(element);
            if ($kendoPlaceholder)
                $kendoPlaceholder.addClass($.NVS.validator.settings.errorClassForKendo);
            else
                defaultHighlight(element, errorClass, validClass);
        }

        function showErrors(errorList) {
            /// <summary>
            ///     This function show validation errors in the current FlashMessenger.
            /// </summary>
            /// <param name="errorList" type="Array">
            ///     An arrray of string messages.
            /// </param>

            $.NVS.selectedFlashMessenger.hide();
            var errorMessage = "";
            for (var i = 0; i < errorList.length; i++) {
                errorMessage += errorList[i].message + "<br />";
            }
            $.NVS.selectedFlashMessenger.error(errorMessage);
        }

        function hideErros() {
            /// <summary>
            ///     This function hide all current messages in the current FlashMessenger.
            /// </summary>

            $.NVS.selectedFlashMessenger.hide();
        }

        function globalizeJQueryValidations() {
            /// <summary>
            ///     Overwrite the default functions from jQuery.validator which get the date or numeric value from a html element.
            ///     It uses kendo.parse* functions to safety convert strings into numeric and date values.
            /// </summary>

            if ($.validator) {
                $.extend($.validator.methods, {
                    date: function (value, element) {
                        return this.optional(element) || kendo.parseDate(value) != null;
                    },

                    number: function (value, element) {
                        return this.optional(element) || kendo.parseFloat(value) != null;
                    },

                    min: function (value, element, param) {
                        value = kendo.parseFloat(value);
                        return this.optional(element) || value >= param;
                    },

                    max: function (value, element, param) {
                        value = kendo.parseFloat(value);
                        return this.optional(element) || value <= param;
                    },

                    range: function (value, element, param) {
                        value = kendo.parseFloat(value);
                        return this.optional(element) || (value >= param[0] && value <= param[1]);
                    }
                });
            }
        }

        return {
            init: function () {
                /// <summary>
                ///     Initiate the default configuration of a NVS Web application for jQuery validation.
                /// </summary>

                globalizeJQueryValidations();

                // This settings should be done before unobtrusive parse
                $.validator.setDefaults($.NVS.validator.settings);
                $.validator.addMethod('compareTo', $.NVS.validator.compareTo);
                $.validator.addMethod('dateTimeRange', $.NVS.validator.dateTimeRange);
                $.validator.addMethod('kendoRequired', $.NVS.validator.kendoRequired);
            }

            , compareTo: function (value, element, params) {
                /// <summary>
                ///     Adapter for CompareTo Client Validator.
                /// </summary>

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
            }

            , dateTimeRange: function (value, element, params) {
                /// <summary>
                ///     DateTimeRange Client Validator.
                /// </summary>

                if ($.NVS.isBlank(value)) {
                    return false; // not testing 'is required' here!
                }

                // parse the value from DatePicker
                var dateValue = kendo.parseDate(value);

                // if can't convert
                if (dateValue == null)
                    throw new Error("Could not convert to date with kendo.parseDate");

                return dateValue >= params.min && dateValue <= params.max;
            }

            , kendoRequired: function (value, element, params) {
                /// <summary>
                ///     KendoRequired Client Validator.
                /// </summary>

                var $currentElement = $(element);
                var widgetValue = $.NVS.getValue($currentElement);
                var widgetValueType = $.NVS.getDataType(widgetValue);

                if (widgetValueType === "numeric" || widgetValueType === "date")
                    return true;
                else
                    return !$.NVS.isBlank(widgetValue);
            }

            , settings: {
                debug: false, // set true for testing
                errorClassForKendo: "kendo-validation-error",
                ignore: ":hidden:not(input[data-role],select[data-role]),.k-formatted-value.k-input",
                highlight: highlight,
                unhighlight: unhighlight,
                onfocusin: function () { },
                onfocusout: function () { },
                onkeyup: function () { },
                showErrors: function (errorMap, errorList) {
                    // this is the current $.validator
                    this.defaultShowErrors();
                    showErrors(errorList);
                },
                success: function () {
                    hideErros();
                }
            }
        };
    }();

    $.NVS.validator.init();
});