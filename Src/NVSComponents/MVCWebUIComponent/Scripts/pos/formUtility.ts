interface JQuery {
    /**
    * Scans the element (e.g. form) and finds all descendants according to the given selector. Adds each descendant's element value into a new, result object.
    * Each located descendant must have a name. This name is used in order to create a final property on the result object. Nested objects are supported as the
    * name may contain a '.' character. A value is determined according to the default implementation given by the Volvo.LAT.Common.FormUtility.
    * @param selector A selector saying which elements from our parent, form should be included.
    * @param filter An additional filter which may decide if an element should be included or not.
    */
    toObject(selector: string, filter?: (element: JQuery, name: string) => boolean): any;
}

namespace Volvo.LAT.Common {

    export class FormUtility {

        /**
        * Prepare the target object setting fields according to the given name and using the '.' in the name field.
        * @param current A current target to be updated. It will receive new properties according to name.
        * @param name A name which should be used to create new properties. It may contain '.' characters for nested objects.
        */
        private static prepareTarget(current: any, name: string): any {
            let parts: string[] = name.split('.');

            // No more parts so we should use the current object as the target.
            if (parts.length < 2) {
                return current;
            }

            let target: any = current[parts[0]];
            if (!target) {
                target = {};
                current[parts[0]] = target;
            }

            return this.prepareTarget(target, name.substring(name.indexOf('.') + 1));
        }

        /**
         * Gets a name of the final property on which a value should be set.
         * @param name A name of the property on which a value should be set.
         */
        private static getFinalName(name: string): string {
            let index: number = name.lastIndexOf('.');
            if (index == -1) {
                return name;
            }
            return name.substring(index + 1);
        }

        /**
         * Scans the form (parent) element and finds all descendants according to the given selector. Adds each descendant's element value into a new, result object.
         * Each located descendant must have a name. This name is used in order to create a final property on the result object. Nested objects are supported as the
         * name may contain a '.' character. A value is determined according to the given value function.
         * @param form A form (parent element) on which descendants should be found according to the given selector.
         * @param selector A selector saying which elements from our parent, form should be included.
         * @param valueReader A function reading value from a located descendant. This value will be added to the final object.
         * @param filter An additional filter which may decide if an element should be included or not.
         */
        public static formToObjectWithReader(form: JQuery, selector: string, valueReader: (input: JQuery) => any, filter?: (element: JQuery, name: string) => boolean): any {

            let result: any = {};

            form.find(selector).each((index: number, element: Element) => {
                let input: JQuery = $(element);

                // We are only handling inputs having the name set as the name will determine the property on the object.
                let name: string = input.attr('name');
                if (!name) {
                    return;
                }

                // If a custom filter is added then skip not matching elements.
                if (filter && !filter(input, name)) {
                    return;
                }

                // Update the result object with the structure following the name and adding nested object according to name '.' characters
                let target: any = this.prepareTarget(result, name);
                let propertyName: string = this.getFinalName(name);

                // Get a value from the located field.
                target[propertyName] = valueReader(input);
            });

            return result;
        }

        /**
         * Reads a value from a given element assuming that an element may represent a kendo widget.
         * @param element An element from which a value should be read.
         */
        private static getValue(element: JQuery): any {
            switch (element.data("role")) {

                case "autocomplete":
                    let autoComplete: kendo.ui.AutoComplete = element.data("kendoAutoComplete");
                    return autoComplete.value();

                case "datepicker":
                    let datePicker: kendo.ui.DatePicker = element.data("kendoDatePicker");
                    return datePicker.value().toISOString();

                case "datetimepicker":
                    let dateTimepicker: kendo.ui.DateTimePicker = element.data("kendoDateTimePicker");
                    return dateTimepicker.value().toISOString();

                case "combobox":
                    let comboBox: kendo.ui.ComboBox = element.data("kendoComboBox");
                    if (!comboBox.dataItem()) return null;
                    return comboBox.value();

                case "dropdownlist":
                    let dropDownList: kendo.ui.DropDownList = element.data("kendoDropDownList");
                    if (!dropDownList.dataItem()) return null;
                    return dropDownList.value();

                case "maskedtextbox":
                    let maskedTextBox: kendo.ui.MaskedTextBox = element.data("kendoMaskedTextBox");
                    return maskedTextBox.value();

                case "multiselect":
                    let multiSelect: kendo.ui.MultiSelect = element.data("kendoMultiSelect");
                    return multiSelect.value();

                case "numerictextbox":
                    let numericTextBox: kendo.ui.NumericTextBox = element.data("kendoNumericTextBox");
                    return numericTextBox.value();

                default:
                    return element.val();
            }    
        }

        /**
         * Scans the form (parent) element and finds all descendants according to the given selector. Adds each descendant's element value into a new, result object.
         * Each located descendant must have a name. This name is used in order to create a final property on the result object. Nested objects are supported as the
         * name may contain a '.' character. A value is determined according to the given value function.
         * @param form A form (parent element) on which descendants should be found according to the given selector.
         * @param selector A selector saying which elements from our parent, form should be included.
         * @param filter An additional filter which may decide if an element should be included or not.
         */
        public static formToObject(form: JQuery, selector: string, filter?: (element: JQuery, name: string) => boolean): any {
            return this.formToObjectWithReader(form, selector, (input: JQuery) => { return input.data('role') ? this.getValue(input) : input.val() }, filter);
        }

    }

    $.fn.toObject = function toObject(selector: string, filter?: (element: JQuery, name: string) => boolean): any {
        return FormUtility.formToObject(this, selector, filter);
    };

}