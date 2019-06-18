namespace Volvo.LAT.Views {

    export class SearchPOs {

        /**
         * A current, single instance of the SearchPOs page control.
         */
        public static current: SearchPOs;

        /**
         * Creates an instance of the search POs page controller.
         * @param service A cart service to be used.
         * @param container A parent element which should be displayed when search results should be presented.
         * @param searchInput A search input element used as the PO of the grid search criteria.
         * @param addToCartButton An 'add to cart' button element related to the current grid items.
         * @param searchButton A search button element which executes the PO search operation.
         */
        constructor(
            private service: ICartService,
            private grid: JQuery,
            private container: JQuery,
            private searchInput: JQuery,
            private addToCartButton: JQuery,
            searchButton: JQuery) {

            // We need to have access into the current instance from all the static event handlers.
            // We do not keep the instance on elements raising events but we prefer static like field.
            SearchPOs.current = this;

            // Attach event handlers to desired elements wrapping and keeping the this instance.
            this.searchInput.keypress((e) => this.onInputKeypress(e));
            this.addToCartButton.click((e) => this.onAddPOToCartClick());
            searchButton.click((e) => this.onSearchPOsClick(e));
        }

        /**
         * Gives access into the kendo grid object from the current grid element.
         * @returns {} A kendo grid object.
         */
        private get kendoGrid(): kendo.ui.Grid {
            return this.grid.data("kendoGrid");
        }

        /**
         * An event handler executed when a change of value is detected for the search input filed.
         * @param eventObject A current event object.
         */
        private onInputKeypress(eventObject: JQueryKeyEventObject): void {
            if (eventObject.which == 13) {
                this.searchPOs();
            }
        }

        /**
         * An event handler executed when a search POs button is clicked.
         * @param eventObject A current event object.
         */
        private onSearchPOsClick(eventObject: JQueryKeyEventObject): void {
            this.searchPOs();
        }

        /**
         * Searches for POs according to the currently entered criteria on the search input field.
         */
        private searchPOs(): void {
            this.kendoGrid.dataSource.filter({ field: "Name", operator: "contains", value: this.searchInput.val() });
            this.container.css("display", "block");
        }

        /**
         * Adds a single PO into the cart and notifies about the addition.
         * @param PO A PO to be added into the cart.
         */
        private onAddPOToCartClick(): void {
            if (this.addToCartButton.isDisabled()) {
                return;
            }
            var rows: JQuery = this.kendoGrid.select();
            var selectedPO: Models.PoSelection = <Models.PoSelection><any>this.kendoGrid.dataItem(rows[0]);
            this.addItemToCart(selectedPO);
        }

        /**
         * Adds a single PO into the cart and notified about the addition.
         * @param PO A PO to be added into the cart.
         */
        private addItemToCart(PO: Models.PoSelection): void {

            // Present a different notification message depending if we already have such an item in the cart or not
            $.NVS.staticflashMessager.hide();
            $.NVS.staticflashMessager.info(this.service.hasCartItem(PO.Number) ?  1 :  0);

            this.service.addCartItem(1, PO);
        }

        /**
         * An event handler executed when a grid row selection changes.
         */
        public static onGridRowSelected(event: kendo.ui.GridChangeEvent): void {
            let that: SearchPOs = SearchPOs.current;

            var row: JQuery = that.kendoGrid.select();
            //if (row.length == 0) {
            //    //that.addToCartButton.disableButton($.NVS.localization.PO_SearchPOs_AddToCart_ButtonDisabledTooltip);
            //} else {
            //    //that.addToCartButton.enableButton($.NVS.localization.PO_SearchPOs_AddToCartButtonTooltip);
            //}
        }

        /**
         * An event handler executed when an error is detected on the grid data source.
         * @param event A data source error event object.
         */
        public static onGridError(event: kendo.data.DataSourceErrorEvent): void {
            $.NVS.summaryFlashMessenger.error($.NVS.localization.Error_GenericAjax);   
        }

    }

}
