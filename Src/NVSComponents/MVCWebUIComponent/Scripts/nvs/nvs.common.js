$(function () {
    /// <summary>
    /// This file contains the $.NVS object definition.
    /// Please, check https://teamplace.volvo.com/sites/volvoit-dotNET/Web%20client%20wiki/NVS%20Javascript%20Object.aspx for more information.
    /// </summary>

    "use strict";

    $.NVS = function () {
        /// <summary>
        /// The $.NVS object contains all utilities functions regarding NVS Web Applications.
        /// </summary>

        // Private Members

        function setupSplitter() {
            /// <summary>
            ///     Set up Kendo UI Splitter to resize main footer and content area properly.
            /// </summary>

            var headerHeight = $("#nvsHeader").outerHeight();

            $("#nvsMainWindow").kendoSplitter({
                orientation: "vertical",
                panes: [
                    { resizable: false, size: headerHeight + "px" },
                    { resizable: false, size: "0", scrollable: false, },
                    { resizable: false },
                    { resizable: false, size: "25px" }
                ]
            });
        }

        function getInitialSummaryMessages() {
            /// <summary>
            ///     Set up the summary block to NVS template. By default, the validation summary from MVC3 framework must be inside of a form.
            /// </summary>

            var requestResult = { ErrorMessages: [], InfoMessages: [], WarningMessages: [] };

            var $validatioSummary = $("form .validation-summary-errors");
            if ($validatioSummary.length > 0) {
                $("ul li", $validatioSummary).each(function (i, item) {
                    requestResult.ErrorMessages.push($(item).text());
                });

                $validatioSummary.remove();
            }

            return requestResult;
        }

        function popupFlashMessenger_show(e) {
            /// <summary>
            /// The align the notification at the center of the browser.
            /// </summary>

            if (!$("." + e.sender._guid)[1]) {
                var element = e.element.parent(),
                    eWidth = element.width(),
                    eHeight = element.height(),
                    wWidth = $(window).width(),
                    wHeight = $(window).height(),
                    newTop,
                    newLeft;

                newLeft = Math.floor(wWidth / 2 - eWidth / 2);
                newTop = Math.floor(wHeight / 2 - eHeight / 2);

                e.element.parent().css({ top: newTop, left: newLeft, zIndex: 11000 });
            }
        }

        function mergeRequestResultObjects(requestResult1, requestResult2) {
            /// <summary>
            /// Merge two RequestResult FlashMessages into one RequestResult.
            /// </summary>

            if ($.NVS.isBlank(requestResult1))
                return requestResult2;

            if ($.NVS.isBlank(requestResult2))
                return requestResult1;

            requestResult1.ErrorMessages = $.merge(requestResult1.ErrorMessages, requestResult2.ErrorMessages);
            requestResult1.WarningMessages = $.merge(requestResult1.WarningMessages, requestResult2.WarningMessages);
            requestResult1.InfoMessages = $.merge(requestResult1.InfoMessages, requestResult2.InfoMessages);

            return requestResult1;
        }

        function summaryFlashMessenger_show(e) {
            /// <summary>
            /// Update the height of the Summary due the new messages.
            /// </summary>

            var flashMessengerHeight = 0;
            $("#nvsMainWindow .k-pane:eq(1) .k-notification").each(function (index, item) {
                var $item = $(item);
                if ($item.attr("ignore") === undefined && $item.attr("ignore") !== "true")
                    flashMessengerHeight += $item.outerHeight();
            });
            $("#nvsMainWindow").data("kendoSplitter").size(".k-pane:eq(1)", flashMessengerHeight + "px");
        }

        function summaryFlashMessenger_hide(e) {
            /// <summary>
            /// Update the height of the Summary due the messages removed.
            /// </summary>

            e.element.attr("ignore", true);
            $("#nvsMainWindow").data("kendoSplitter").size(".k-pane:eq(1)", "0");
        }

        // Public Members

        return {

            init: function () {
                /// <summary>
                ///     Initiate the default configuration of a NVS Web application.
                /// </summary>

                setupSplitter();
            }



            , displayFlashMessages: function (requestResult, flashMessenger) {
                /// <summary>
                ///     Display the Error, Info and Warning messages from requestResult in the target FlashMessager.
                /// </summary>
                /// <param name="requestResult" type="plain javascript object version of Volvo.NVS.Utilities.Web.Models.RequestResult">
                ///     The object containing the flash messages to be displayed. See the RequestResult C# object in Utilities.Web project.
                ///     The most important properties of this type are InfoMessages, ErrorMessages and WarningMessages, each one a string array.
                /// </param>
                /// <param name="flashMessenger" type="html object">
                ///     (OPTINAL) The flashMessenger object used for displaying the messages. This is an optional parameter, if you do not specify it the default
                ///     FlashMessenger object will be used (or the one previously set via $.NVS.selectedFlashMessenger)
                /// </param>

                if (!requestResult)
                    throw new Error("flashMessages is invalid.");

                var targetFlashMessenger = this.selectedFlashMessenger;
                if (flashMessenger)
                    targetFlashMessenger = flashMessenger;

                if (!this.isBlank(requestResult.ErrorMessages))
                    targetFlashMessenger.error(requestResult.ErrorMessages.join("<br />"));

                if (!this.isBlank(requestResult.WarningMessages))
                    targetFlashMessenger.warning(requestResult.WarningMessages.join("<br />"));

                if (!this.isBlank(requestResult.InfoMessages))
                    targetFlashMessenger.info(requestResult.InfoMessages.join("<br />"));
            }

            , getWeekNumber: function (date) {
                /// <summary>
                ///     Gets the week number of a date.
                /// </summary>
                /// <param name="date" type="Date">
                ///     A date.
                /// </param>
                /// <returns type="numeric">The week number.</returns>

                if (!(date instanceof Date))
                    return null;

                var time, checkDate = new Date(date.getTime());

                // Find Thursday of this week starting on Monday
                checkDate.setDate(checkDate.getDate() + 4 - (checkDate.getDay() || 7));

                time = checkDate.getTime();

                checkDate.setMonth(0); // Compare with Jan 1
                checkDate.setDate(1);

                return Math.floor(Math.round((time - checkDate) / 86400000) / 7) + 1;
            }

            , getYearForWeek: function (date) {
                /// <summary>
                ///     Returns the year number for the week based date (e.g. YYYYWWD).
                /// </summary>
                /// <param name="date" type="Date">
                ///     The date for which the year number (related into the iso8601 Week) should be returned.
                /// </param>
                /// <returns type="numeric">The year for the week.</returns>

                var target = new Date(date);
                target.setDate(target.getDate() - ((date.getDay() + 6) % 7) + 3);
                return target.getFullYear();
            }

            , getDataType: function (value) {
                /// <summary>
                ///     Identifies the type of the value.
                ///     The return values can be (as string):
                ///         undefined
                ///         array
                ///         date
                ///         stringdate (using kendo)
                ///         function
                ///         numeric
                ///         plainobject
                ///         emptyobject
                ///         string
                /// </summary>
                /// <param name="value" type="any">
                ///     The value.
                /// </param>
                /// <returns type="string">The string representing the type of value.</returns>

                if ($.isArray(value))
                    return "array";

                if ($.isFunction(value))
                    return "function";

                if (value instanceof Date)
                    return "date";

                if (typeof (value) == "string") {
                    if (kendo.parseDate(value) != null)
                        return "stringdate";
                    else
                        return "string";
                }

                if ($.isNumeric(value) || kendo.parseFloat(value) != null)
                    return "numeric";

                if ($.isPlainObject(value)) {
                    if ($.isEmptyObject(value))
                        return "emptyobject";
                    else
                        return "plainobject";
                }

                return "undefined";
            }

            , getValue: function ($element) {
                /// <summary>
                ///     Gets current value from a jQuery object or from a Kendo Wiget.
                ///     If it's not possible to get the value, returns null.
                /// </summary>
                /// <param name="$element" type="jQuery">
                ///     The jQuery object.
                /// </param>
                /// <returns type="string">The found value as string.</returns>

                // Check if the $element is invalid
                if (!($element instanceof jQuery))
                    return null;

                if (kendo.widgetInstance($element) == undefined)
                    return $element.val();

                switch ($element.data("role")) {
                    case "autocomplete":
                        var autoComplete = $element.data("kendoAutoComplete");
                        return autoComplete.value();

                    case "datepicker":
                        var datePicker = $element.data("kendoDatePicker");
                        return datePicker.value();

                    case "datetimepicker":
                        var dateTimepicker = $element.data("kendoDateTimePicker");
                        return dateTimepicker.value();

                    case "combobox":
                        var comboBox = $element.data("kendoComboBox");
                        if (comboBox.dataItem() == undefined) return null;
                        return comboBox.dataItem().Value;

                    case "dropdownlist":
                        var dropDownList = $element.data("kendoDropDownList");
                        if (dropDownList.dataItem() == undefined) return null;
                        return dropDownList.dataItem().Value;

                    case "maskedtextbox":
                        var maskedTextBox = $element.data("kendoMaskedTextBox");
                        return maskedTextBox.value();

                    case "multiselect":
                        var multiSelect = $element.data("kendoMultiSelect");
                        return multiSelect.dataItems();

                    case "numerictextbox":
                        var numericTextBox = $element.data("kendoNumericTextBox");
                        return numericTextBox.value();

                    default:
                        return null;
                }
            }

            , getTypedValue: function ($element) {
                /// <summary>
                ///     Gets current value from a jQuery object or from a Kendo Wiget in the type of the value.
                ///     Supports numeric, string and date types. Does not support arrays.
                /// </summary>
                /// <param name="$element" type="jQuery">
                ///     The jQuery object.
                /// </param>
                /// <returns type="any">Could return string, numeric or date depending on the data type of the value.</returns>

                var value = $.NVS.getValue($element);

                if (value == null)
                    return null;

                // Typing the Value
                switch ($.NVS.getDataType(value)) {
                    case "numeric":
                        value = kendo.parseFloat(value);
                        break;

                    case "stringdate":
                        value = kendo.parseDate(value);
                        break;
                }

                return value;
            }

            , go: function (url) {
                /// <summary>
                ///     Go to another URL.
                /// </summary>
                /// <param name="url" type="string">
                ///     An url.
                /// </param>

                window.location = url;
            }

            , globalizeJQueryValidations: function () {
                /// <deprecated type="deprecate">
                ///     Overwrite the default functions from jQuery.validator which get the date or numeric value from a html element.
                ///     It uses kendo.parse* functions to safety convert strings into numeric and date values.
                /// </deprecated>
                if (console)
                    console.log('This function is deprecated. Include nvs.validate.js instead.');

                if (jQuery.validator) {
                    jQuery.extend(jQuery.validator.methods, {
                        date: function (value, element) {
                            return this.optional(element) || kendo.parseDate(value) != null;
                        },
                        number: function (value, element) {
                            return this.optional(element) || kendo.parseFloat(value) != null;
                        }
                    });
                }
            }

            , isClickEventAttached: function (element) {
                /// <summary>
                ///     Checks if the click event is already attached to a given element.
                /// </summary>
                /// <param name="element" type="html object">
                ///     An html element.
                /// </param>
                /// <returns type="bool">Return true if the element has the click event binded, otherwise false.</returns>

                var events = $._data(element, "events");
                return events != undefined && events.click != undefined;
            }

            , isBlank: function (obj) {
                /// <summary>
                ///     Checks if an object is empty/blank (only data types, not widgets).
                ///     Data types supported are: array, string, stringdate.
                /// </summary>
                /// <param name="obj" type="Object">
                ///     The object to be checked.
                /// </param>
                /// <returns type="bool">Return true if the obj is blank, otherwise false.</returns>

                if (obj === undefined || obj === null)
                    return true;

                switch ($.NVS.getDataType(obj)) {
                    case "date":
                    case "numeric":
                    case "undefined": // Could not discover the data type
                    default:
                        throw new Error("Data type not supported");

                    case "function":
                    case "plainobject":
                        return false;

                    case "array":
                        return obj.length == 0;

                    case "emptyobject":
                        return true;

                    case "string":
                    case "stringdate":
                        return $.trim(obj) === "";
                }
            }

            , localization: {
                /// <summary>
                ///     Localization object, all fields of this object are overridden by LocalizationHelper.cs with the contents of Javacript.Resource.resx file.
                /// </summary>
            }

            , selectedFlashMessenger: null

            , summaryFlashMessenger: null

            , popupFlashMessenger: null

            /// <deprecated type="deprecate">
            ///     This property is deprecated, use popupFlashMessenger instead.
            /// </deprecated>
            , popupflashMessager: null

            /// <deprecated type="deprecate">
            ///     This property is deprecated, use summaryFlashMessenger instead.
            /// </deprecated>
            , staticflashMessager: null

            , setupAndDisplayFlashMessengers: function (requestResult, summaryFlashMessengerSelector, popupFlashMessengerSelector) {
                /// <summary>
                ///     This method sets up the summaryFlashMessenger and popupFlashMessenger objects and also displays the flashMessages
                ///     through the selectedFlashMessenger object. By default, the selectedFlashMessenger points to summaryFlashMessenger.
                /// </summary>
                /// <param name="flashMrequestResultessages" type="plain javascript object version of Volvo.NVS.Utilities.Web.Models.RequestResult">
                ///     The object containing the flash messages to be displayed. See the RequestResult C# object in Utilities.Web project.
                ///     The most important properties of this type are InfoMessages, ErrorMessages and WarningMessages, each one a string array.
                /// </param>
                /// <param name="summaryFlashMessengerSelector" type="string">
                ///     The jQuery selector string that represents the summaryFlashMessenger object in the DOM.
                /// </param>
                /// <param name="popupFlashMessengerSelector" type="string">
                ///     The jQuery selector string that represents the popupFlashMessenger object in the DOM.
                /// </param>

                this.summaryFlashMessenger = $(summaryFlashMessengerSelector).data("kendoNotification");

                if (this.summaryFlashMessenger == null)
                    throw new Error("Couldn't initiate the Summary FlashMessenger.");

                this.summaryFlashMessenger.bind("show", summaryFlashMessenger_show);
                this.summaryFlashMessenger.bind("hide", summaryFlashMessenger_hide);
                this.selectedFlashMessenger = this.summaryFlashMessenger;

                this.popupFlashMessenger = $(popupFlashMessengerSelector).data("kendoNotification");

                if (this.popupFlashMessenger == null)
                    throw new Error("Couldn't initiate the Popup FlashMessenger.");

                this.popupFlashMessenger.bind("show", popupFlashMessenger_show);

                var initialMessages = mergeRequestResultObjects(getInitialSummaryMessages(), requestResult);
                this.displayFlashMessages(initialMessages);

                // legacy compliance
                $.NVS.staticflashMessager = $.NVS.summaryFlashMessenger;
                $.NVS.popupflashMessager = $.NVS.popupFlashMessenger;
            }

            , setupCollapsibleElements: function () {
                /// <summary>
                ///     Adds the close and open behavior for all elements containing .collapsible CSS class.
                /// </summary>

                $(".collapsible").each(function () {
                    var $element = $(this);
                    $element.find("h3:first").bind("click", function () {
                        $element.toggleClass("closed2");
                    });
                });
            }

            , setupFlashMessagers: function () {
                /// <deprecated type="deprecate">
                ///     This method sets up the summaryFlashMessenger and popupFlashMessenger objects and also displays the flashMessages
                ///     through the selectedFlashMessenger object. By default, the selectedFlashMessenger points to summaryFlashMessenger.
                /// </deprecated>
                if (console)
                    console.log('This function is deprecated. This remove this call, @Html.Summary() cover it.');

                if ($.NVS.summaryFlashMessenger == null || $.NVS.popupFlashMessenger == null)
                    $.NVS.setupAndDisplayFlashMessengers(null, "#staticNotification", "#popupNotification");


            }
            , setupSplitter: function () {
                /// <deprecated type="deprecate">
                ///     Set up  Kendo UI Splitter to resize main footer and content area properly.
                /// </deprecated>
                if (console)
                    console.log('This function is deprecated. User $.NVS.init() instead.');

                setupSplitter();
            }

            , setupSummary: function () {
                /// <deprecated type="deprecate">
                ///     Set up the summary block to NVS template. By default, the validation summary from MVC3 framework must be inside of a form.
                /// </deprecated>
                if (console)
                    console.log('This function is deprecated. This remove this call, @Html.Summary() cover it.');
            }

            , setupValidation: function ($form) {
                /// <deprecated type="deprecate">
                ///     Setup the jQuery validation for the givem form jQuery element.
                /// </deprecated>
                if (console)
                    console.log('This function is deprecated. Use nvs.validate.js to setup the validation settings and include the jquery.validate.unobtrusive.js to apply the validation in the current form.');

                $form.bind("invalid-form.validate", function (event, validator) {
                    $.NVS.staticflashMessager.hide();
                    var errorMessage = "";
                    for (var i = 0; i < validator.errorList.length; i++) {
                        errorMessage += validator.errorList[i].message + "<br />";
                    }
                    $.NVS.staticflashMessager.error(errorMessage);
                });
            }
        }
    }();

    window.jQuery.NVS = window.$.NVS = $.NVS;
});