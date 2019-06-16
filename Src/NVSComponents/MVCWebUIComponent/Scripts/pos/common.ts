interface String {
    /**
     * Formats a string using a give collection of arguments.
     * @param args Arguments used to format a string value.
     * @returns {} A formatted string.
     */
    format(...args: string[]): string;

    /**
     * Determines if a current value start with a given string.
     * @param search A string to check for.
     * @param position A position to start from.
     * @returns {} 
     */
    startsWith (search: string, position?: number): boolean;
}

interface JQuery {
    /**
     * Determines if the current element is recognized as disabled or not.
     * @returns {} 
     */
    isDisabled(): boolean;

    /**
     * Disables the current button element.
     * @returns {} 
     */
    disableButton(title: string): void;

    /**
     * Enables the current button element.
     * @param title A title to be set for the enabled button element.
     * @returns {} 
     */
    enableButton(title: string): void;
}

namespace Volvo.LAT.Common {

    String.prototype.format = function (...args: string[]) {
        return this.replace(/{(\d+)}/g, (match: string, index: number) => {
            return args.length > index ? args[index] : match;
        });
    }

    if (!String.prototype.startsWith) {
        String.prototype.startsWith = function (searchString: string, position?: number) {
            position = position || 0;
            return this.substr(position, searchString.length) === searchString;
        };
    }

    $.fn.disableButton = function (title: string) {
        this.addClass("disabled");
        this.attr("title", title);
    };

    $.fn.enableButton = function (title) {
        this.removeClass("disabled");
        this.attr("title", title);
    };

    $.fn.isDisabled = function () {
        return this.hasClass("disabled");
    };

}
