var Volvo;
(function (Volvo) {
    var LAT;
    (function (LAT) {
        var Common;
        (function (Common) {
            var FormUtility = (function () {
                function FormUtility() {
                }
                /**
                * Prepare the target object setting fields according to the given name and using the '.' in the name field.
                * @param current A current target to be updated. It will receive new properties according to name.
                * @param name A name which should be used to create new properties. It may contain '.' characters for nested objects.
                */
                FormUtility.prepareTarget = function (current, name) {
                    var parts = name.split('.');
                    // No more parts so we should use the current object as the target.
                    if (parts.length < 2) {
                        return current;
                    }
                    var target = current[parts[0]];
                    if (!target) {
                        target = {};
                        current[parts[0]] = target;
                    }
                    return this.prepareTarget(target, name.substring(name.indexOf('.') + 1));
                };
                /**
                 * Gets a name of the final property on which a value should be set.
                 * @param name A name of the property on which a value should be set.
                 */
                FormUtility.getFinalName = function (name) {
                    var index = name.lastIndexOf('.');
                    if (index == -1) {
                        return name;
                    }
                    return name.substring(index + 1);
                };
                /**
                 * Scans the form (parent) element and finds all descendants according to the given selector. Adds each descendant's element value into a new, result object.
                 * Each located descendant must have a name. This name is used in order to create a final property on the result object. Nested objects are supported as the
                 * name may contain a '.' character. A value is determined according to the given value function.
                 * @param form A form (parent element) on which descendants should be found according to the given selector.
                 * @param selector A selector saying which elements from our parent, form should be included.
                 * @param valueReader A function reading value from a located descendant. This value will be added to the final object.
                 * @param filter An additional filter which may decide if an element should be included or not.
                 */
                FormUtility.formToObjectWithReader = function (form, selector, valueReader, filter) {
                    var _this = this;
                    var result = {};
                    form.find(selector).each(function (index, element) {
                        var input = $(element);
                        // We are only handling inputs having the name set as the name will determine the property on the object.
                        var name = input.attr('name');
                        if (!name) {
                            return;
                        }
                        // If a custom filter is added then skip not matching elements.
                        if (filter && !filter(input, name)) {
                            return;
                        }
                        // Update the result object with the structure following the name and adding nested object according to name '.' characters
                        var target = _this.prepareTarget(result, name);
                        var propertyName = _this.getFinalName(name);
                        // Get a value from the located field.
                        target[propertyName] = valueReader(input);
                    });
                    return result;
                };
                /**
                 * Reads a value from a given element assuming that an element may represent a kendo widget.
                 * @param element An element from which a value should be read.
                 */
                FormUtility.getValue = function (element) {
                    switch (element.data("role")) {
                        case "autocomplete":
                            var autoComplete = element.data("kendoAutoComplete");
                            return autoComplete.value();
                        case "datepicker":
                            var datePicker = element.data("kendoDatePicker");
                            return datePicker.value().toISOString();
                        case "datetimepicker":
                            var dateTimepicker = element.data("kendoDateTimePicker");
                            return dateTimepicker.value().toISOString();
                        case "combobox":
                            var comboBox = element.data("kendoComboBox");
                            if (!comboBox.dataItem())
                                return null;
                            return comboBox.value();
                        case "dropdownlist":
                            var dropDownList = element.data("kendoDropDownList");
                            if (!dropDownList.dataItem())
                                return null;
                            return dropDownList.value();
                        case "maskedtextbox":
                            var maskedTextBox = element.data("kendoMaskedTextBox");
                            return maskedTextBox.value();
                        case "multiselect":
                            var multiSelect = element.data("kendoMultiSelect");
                            return multiSelect.value();
                        case "numerictextbox":
                            var numericTextBox = element.data("kendoNumericTextBox");
                            return numericTextBox.value();
                        default:
                            return element.val();
                    }
                };
                /**
                 * Scans the form (parent) element and finds all descendants according to the given selector. Adds each descendant's element value into a new, result object.
                 * Each located descendant must have a name. This name is used in order to create a final property on the result object. Nested objects are supported as the
                 * name may contain a '.' character. A value is determined according to the given value function.
                 * @param form A form (parent element) on which descendants should be found according to the given selector.
                 * @param selector A selector saying which elements from our parent, form should be included.
                 * @param filter An additional filter which may decide if an element should be included or not.
                 */
                FormUtility.formToObject = function (form, selector, filter) {
                    var _this = this;
                    return this.formToObjectWithReader(form, selector, function (input) { return input.data('role') ? _this.getValue(input) : input.val(); }, filter);
                };
                return FormUtility;
            }());
            Common.FormUtility = FormUtility;
            $.fn.toObject = function toObject(selector, filter) {
                return FormUtility.formToObject(this, selector, filter);
            };
        })(Common = LAT.Common || (LAT.Common = {}));
    })(LAT = Volvo.LAT || (Volvo.LAT = {}));
})(Volvo || (Volvo = {}));
//# sourceMappingURL=formUtility.js.map