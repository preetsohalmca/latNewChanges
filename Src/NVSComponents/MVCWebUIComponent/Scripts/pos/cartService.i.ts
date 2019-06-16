namespace Volvo.LAT.Views {

    export interface ICartService {

        /**
         * A collection of current cart items.
         */
        items: kendo.data.ObservableArray;

        /**
         * Binds a handler to be run when an item in the cart is added or removed.
         * @param handler A custom handler to be called.
         */
        bind(handler: Function): void;

        /**
         * Checks if the cart contains a part with a given number.
         * @param partNumber A part number for which a cart should be checked.
         */
        hasCartItem(partNumber: number): boolean;

        /**
         * Adds a new item into the cart.
         * @param quantity A part quantity.
         * @param part A part for which an item should be added.
         */
        addCartItem(quantity: number, part: Models.PoSelection): void;

        /**
         * Removes an item from the cart.
         * @param partNumber A part number of an item to be removed.
         */
        removeCartItem(partNumber: number): void;

        /**
         * Clears the cart removing all the items.
         */
        clear(): void;

        /**
         * Calculates a total price for all the cart items.
         */
        getTotalPrice(): number;

    }

}
