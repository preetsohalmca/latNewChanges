var Volvo;
(function (Volvo) {
    var LAT;
    (function (LAT) {
        var Views;
        (function (Views) {
            var ShoppingCartSummary = (function () {
                /**
                 * Creates an instance of the shopping cart summary page controller.
                 * @param shoppingCartUrl An url pointing at the shopping cart view.
                 * @param service A cart service.
                 */
                function ShoppingCartSummary(shoppingCartUrl, service, basketButton) {
                    var _this = this;
                    this.shoppingCartUrl = shoppingCartUrl;
                    this.service = service;
                    this.basketButton = basketButton;
                    this.basketButton.click(function () { return _this.onBasketButtonClick(); });
                    this.service.bind(function () { return _this.onCartItemsChange(); });
                    this.updateBasketButtonText();
                }
                /**
                 * Updates a shopping cart button text displaying a summary of the basket content.
                 */
                ShoppingCartSummary.prototype.updateBasketButtonText = function () {
                    var lenght = this.service.items.length;
                    if (lenght == 0) {
                        this.basketButton.html($.NVS.localization.ShoppingCart_Cart_Empty);
                    }
                    else if (lenght == 1) {
                        this.basketButton.html($.NVS.localization.ShoppingCart_Cart_OneItem.format(this.service.getTotalPrice().toString()));
                    }
                    else {
                        this.basketButton.html($.NVS.localization.ShoppingCart_Cart_ManyItems.format(lenght.toString(), this.service.getTotalPrice().toString()));
                    }
                };
                /**
                 * An event handler called when a shopping cart button is clicked.
                 */
                ShoppingCartSummary.prototype.onBasketButtonClick = function () {
                    $.NVS.go(this.shoppingCartUrl);
                };
                /**
                 * An event handler called when an cart item changes.
                 */
                ShoppingCartSummary.prototype.onCartItemsChange = function () {
                    this.updateBasketButtonText();
                };
                return ShoppingCartSummary;
            }());
            Views.ShoppingCartSummary = ShoppingCartSummary;
        })(Views = LAT.Views || (LAT.Views = {}));
    })(LAT = Volvo.LAT || (Volvo.LAT = {}));
})(Volvo || (Volvo = {}));
//# sourceMappingURL=shoppingCartSummary.js.map