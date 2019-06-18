var Volvo;
(function (Volvo) {
    var LAT;
    (function (LAT) {
        var Views;
        (function (Views) {
            var CartService = (function () {
                /**
                 * Creates an instance of the cart service.
                 */
                function CartService() {
                    /**
                     * A key used in the session storage in order to keep the current cart items.
                     */
                    this.ITEM_KEY = "cartItems";
                    if (CartService.instance) {
                        throw new Error('Only one instance of the cart service should be created');
                    }
                    this.initializeCartItems();
                }
                Object.defineProperty(CartService, "current", {
                    /**
                     * Returns a single, current instance of the cart service.
                     * @returns {}
                     */
                    get: function () {
                        if (!CartService.instance) {
                            CartService.instance = new CartService();
                        }
                        return CartService.instance;
                    },
                    enumerable: true,
                    configurable: true
                });
                /**
                 * Binds a handler to be run when an item in the cart is added or removed.
                 * @param handler A custom handler to be called.
                 */
                CartService.prototype.bind = function (handler) {
                    this.items.bind("change", handler);
                };
                /**
                 * Loads the currently stored cart items and initializes the cart items collection.
                 */
                CartService.prototype.initializeCartItems = function () {
                    var _this = this;
                    var value = sessionStorage.getItem(this.ITEM_KEY);
                    if (value) {
                        try {
                            this.items = new kendo.data.ObservableArray($.parseJSON(value));
                        }
                        catch (e) {
                        }
                    }
                    if (!this.items) {
                        this.items = new kendo.data.ObservableArray([]);
                    }
                    this.bind(function () { return _this.onItemsChange(); });
                };
                /**
                 * An event handler called when any of the cart item is added or removed from the observable collection.
                 */
                CartService.prototype.onItemsChange = function () {
                    this.updateCartItems();
                };
                /**
                 * Serializes and stores the current collection of cart items in the session storage.
                 */
                CartService.prototype.updateCartItems = function () {
                    sessionStorage.setItem(this.ITEM_KEY, JSON.stringify(this.items.toJSON()));
                };
                /**
                 * Finds an item with a given part number.
                 * @param partNumber A part number for which an item should be located in the cart.
                 */
                CartService.prototype.getCartItem = function (partNumber, location) {
                    var found = $.grep(this.items, function (item, index) {
                        if (item.Part.Number == partNumber) {
                            if (location) {
                                location.index = index;
                            }
                            return true;
                        }
                        return false;
                    });
                    return found.length == 0 ? null : found[0];
                };
                /**
                 * Checks if the cart contains a part with a given number.
                 * @param partNumber A part number for which a cart should be checked.
                 */
                CartService.prototype.hasCartItem = function (partNumber) {
                    return this.getCartItem(partNumber) != null;
                };
                /**
                 * Adds a new item into the cart.
                 * @param quantity A part quantity.
                 * @param part A part for which an item should be added.
                 */
                CartService.prototype.addCartItem = function (quantity, part) {
                    var location = { index: 0 };
                    var item = this.getCartItem(part.Number, location);
                    if (item) {
                        item.Quantity += quantity;
                        this.items.trigger('change', {
                            action: 'itemchange',
                            index: location.index,
                            items: [item]
                        });
                    }
                    else {
                        item = new Views.CartItem();
                        item.Quantity = quantity;
                        item.Part = part;
                        this.items.push(item);
                    }
                };
                /**
                 * Removes an item from the cart.
                 * @param partNumber A part number of an item to be removed.
                 */
                CartService.prototype.removeCartItem = function (partNumber) {
                    var indexToRemove = -1;
                    $.each(this.items, function (index, item) {
                        if (item.Part.Number === partNumber) {
                            indexToRemove = index;
                        }
                    });
                    if (indexToRemove == -1) {
                        return;
                    }
                    this.items.splice(indexToRemove, 1);
                };
                /**
                 * Clears the cart removing all the items.
                 */
                CartService.prototype.clear = function () {
                    if (this.items.length > 0) {
                        this.items.splice(0, this.items.length);
                    }
                };
                /**
                 * Calculates a total price for all the cart items.
                 */
                CartService.prototype.getTotalPrice = function () {
                    var total = 0;
                    $.each(this.items, function (index, item) {
                        total += 0;
                    });
                    // Round in order to have only 2 decimal characters
                    return Math.round(total * 100) / 100;
                };
                return CartService;
            }());
            Views.CartService = CartService;
        })(Views = LAT.Views || (LAT.Views = {}));
    })(LAT = Volvo.LAT || (Volvo.LAT = {}));
})(Volvo || (Volvo = {}));
//# sourceMappingURL=cartService.js.map