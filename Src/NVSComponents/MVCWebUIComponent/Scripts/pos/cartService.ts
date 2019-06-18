namespace Volvo.LAT.Views {

    export class CartService implements ICartService {

        /**
         * A key used in the session storage in order to keep the current cart items.
         */
        private ITEM_KEY: string = "cartItems";

        /**
         * A collection of current cart items.
         */
        public items: kendo.data.ObservableArray;

        /**
         * A current, single instance of the cart service.
         */
        private static instance: CartService;

        /**
         * Creates an instance of the cart service.
         */
        constructor() {
            if (CartService.instance) {
                throw new Error('Only one instance of the cart service should be created');
            }
            this.initializeCartItems();
        }

        /**
         * Returns a single, current instance of the cart service.
         * @returns {} 
         */
        public static get current(): CartService {
            if (!CartService.instance) {
                CartService.instance = new CartService();
            }
            return CartService.instance;
        }

        /**
         * Binds a handler to be run when an item in the cart is added or removed.
         * @param handler A custom handler to be called.
         */
        public bind(handler: Function): void {
            this.items.bind("change", handler);
        }

        /**
         * Loads the currently stored cart items and initializes the cart items collection.
         */
        private initializeCartItems(): void {
            let value: string = sessionStorage.getItem(this.ITEM_KEY);

            if (value) {
                try {
                    this.items = new kendo.data.ObservableArray($.parseJSON(value));
                }
                catch(e)
                {
                    // If a value is not a correct JSON we simply ignore it.
                }
            }

            if (!this.items) {
                this.items = new kendo.data.ObservableArray([]);
            }

            this.bind(() => this.onItemsChange());
        }

        /**
         * An event handler called when any of the cart item is added or removed from the observable collection.
         */
        private onItemsChange() {
            this.updateCartItems();
        }

        /**
         * Serializes and stores the current collection of cart items in the session storage.
         */
        private updateCartItems() {
            sessionStorage.setItem(this.ITEM_KEY, JSON.stringify(this.items.toJSON()));
        }

        /**
         * Finds an item with a given part number.
         * @param partNumber A part number for which an item should be located in the cart.
         */
        private getCartItem(partNumber: number, location?: { index: number }): CartItem {
            let found: Array<CartItem> = $.grep<CartItem>(this.items, (item: CartItem, index: number) => {
                if (item.Part.Number == partNumber) {
                    if (location) {
                        location.index = index;
                    }
                    return true;
                }
                return false;
            });
            return found.length == 0 ? null : found[0];
        }

        /**
         * Checks if the cart contains a part with a given number.
         * @param partNumber A part number for which a cart should be checked.
         */
        public hasCartItem(partNumber: number): boolean {
            return this.getCartItem(partNumber) != null;
        }

        /**
         * Adds a new item into the cart.
         * @param quantity A part quantity.
         * @param part A part for which an item should be added.
         */
        public addCartItem(quantity: number, part: Models.PoSelection): void {
            let location: { index: number } = { index: 0 };
            let item: CartItem = this.getCartItem(part.Number, location);

            if (item) {
                item.Quantity += quantity;
                this.items.trigger('change',
                {
                    action: 'itemchange',
                    index: location.index,
                    items: [ item ]
                });
            } else {
                item = new CartItem();
                item.Quantity = quantity;
                item.Part = part;
                this.items.push(item);
            }
        }

        /**
         * Removes an item from the cart.
         * @param partNumber A part number of an item to be removed.
         */
        public removeCartItem(partNumber: number): void {
            let indexToRemove = -1;

            $.each(this.items, function(index: number, item: CartItem) {
                if (item.Part.Number === partNumber) {
                    indexToRemove = index;
                }
            });

            if (indexToRemove == -1) {
                return;
            }

            this.items.splice(indexToRemove, 1);
        }

        /**
         * Clears the cart removing all the items.
         */
        public clear(): void {
            if (this.items.length > 0) {
                this.items.splice(0, this.items.length);
            }
        }

        /**
         * Calculates a total price for all the cart items.
         */
        public getTotalPrice(): number {
            var total = 0;

            $.each(this.items, function(index: number, item: CartItem) {
                total += 0;
            });

            // Round in order to have only 2 decimal characters
            return Math.round(total * 100) / 100;
        }

    }

}