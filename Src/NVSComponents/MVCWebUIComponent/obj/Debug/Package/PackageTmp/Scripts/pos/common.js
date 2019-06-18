var Volvo;
(function (Volvo) {
    var LAT;
    (function (LAT) {
        var Common;
        (function (Common) {
            String.prototype.format = function () {
                var args = [];
                for (var _i = 0; _i < arguments.length; _i++) {
                    args[_i - 0] = arguments[_i];
                }
                return this.replace(/{(\d+)}/g, function (match, index) {
                    return args.length > index ? args[index] : match;
                });
            };
            if (!String.prototype.startsWith) {
                String.prototype.startsWith = function (searchString, position) {
                    position = position || 0;
                    return this.substr(position, searchString.length) === searchString;
                };
            }
            $.fn.disableButton = function (title) {
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
        })(Common = LAT.Common || (LAT.Common = {}));
    })(LAT = Volvo.LAT || (Volvo.LAT = {}));
})(Volvo || (Volvo = {}));
//# sourceMappingURL=common.js.map