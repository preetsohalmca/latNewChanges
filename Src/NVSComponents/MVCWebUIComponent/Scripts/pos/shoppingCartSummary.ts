namespace Volvo.LAT.Views {

    export class ShoppingCartSummary {

        /**
         * Creates an instance of the shopping cart summary page controller.
         * @param shoppingCartUrl An url pointing at the shopping cart view.
         * @param service A cart service.
         */
        constructor(
            private shoppingCartUrl: string,
            private service: ICartService,
            private basketButton: JQuery) {
            
            this.basketButton.click(() => this.onBasketButtonClick());
            this.service.bind(() => this.onCartItemsChange());

            this.updateBasketButtonText();
        }

        /**
         * Updates a shopping cart button text displaying a summary of the basket content.
         */
        private updateBasketButtonText(): void {
            var lenght: number = this.service.items.length;
            if (lenght == 0) {
                this.basketButton.html($.NVS.localization.ShoppingCart_Cart_Empty);
            }
            else if (lenght == 1) {
                this.basketButton.html($.NVS.localization.ShoppingCart_Cart_OneItem.format(this.service.getTotalPrice().toString()));
            }
            else {
                this.basketButton.html($.NVS.localization.ShoppingCart_Cart_ManyItems.format(lenght.toString(), this.service.getTotalPrice().toString()));
            }   
        }

        /**
         * An event handler called when a shopping cart button is clicked.
         */
        private onBasketButtonClick(): void {
            $.NVS.go(this.shoppingCartUrl);
        }

        /**
         * An event handler called when an cart item changes.
         */
        private onCartItemsChange(): void {
            this.updateBasketButtonText();
        }

    }

}
