var Volvo;
(function (Volvo) {
    var LAT;
    (function (LAT) {
        var Views;
        (function (Views) {
            var SearchPOs = (function () {
                /**
                 * Creates an instance of the search POs page controller.
                 * @param service A cart service to be used.
                 * @param container A parent element which should be displayed when search results should be presented.
                 * @param searchInput A search input element used as the PO of the grid search criteria.
                 * @param addToCartButton An 'add to cart' button element related to the current grid items.
                 * @param searchButton A search button element which executes the PO search operation.
                 */
                function SearchPOs(service, grid, container, searchInput, addToCartButton, searchButton) {
                    var _this = this;
                    this.service = service;
                    this.grid = grid;
                    this.container = container;
                    this.searchInput = searchInput;
                    this.addToCartButton = addToCartButton;
                    // We need to have access into the current instance from all the static event handlers.
                    // We do not keep the instance on elements raising events but we prefer static like field.
                    SearchPOs.current = this;
                    // Attach event handlers to desired elements wrapping and keeping the this instance.
                    this.searchInput.keypress(function (e) { return _this.onInputKeypress(e); });
                    this.addToCartButton.click(function (e) { return _this.onAddPOToCartClick(); });
                    searchButton.click(function (e) { return _this.onSearchPOsClick(e); });
                }
                Object.defineProperty(SearchPOs.prototype, "kendoGrid", {
                    /**
                     * Gives access into the kendo grid object from the current grid element.
                     * @returns {} A kendo grid object.
                     */
                    get: function () {
                        return this.grid.data("kendoGrid");
                    },
                    enumerable: true,
                    configurable: true
                });
                /**
                 * An event handler executed when a change of value is detected for the search input filed.
                 * @param eventObject A current event object.
                 */
                SearchPOs.prototype.onInputKeypress = function (eventObject) {
                    if (eventObject.which == 13) {
                        this.searchPOs();
                    }
                };
                /**
                 * An event handler executed when a search POs button is clicked.
                 * @param eventObject A current event object.
                 */
                SearchPOs.prototype.onSearchPOsClick = function (eventObject) {
                    this.searchPOs();
                };
                /**
                 * Searches for POs according to the currently entered criteria on the search input field.
                 */
                SearchPOs.prototype.searchPOs = function () {
                    this.kendoGrid.dataSource.filter({ field: "Name", operator: "contains", value: this.searchInput.val() });
                    this.container.css("display", "block");
                };
                /**
                 * Adds a single PO into the cart and notifies about the addition.
                 * @param PO A PO to be added into the cart.
                 */
                SearchPOs.prototype.onAddPOToCartClick = function () {
                    if (this.addToCartButton.isDisabled()) {
                        return;
                    }
                    var rows = this.kendoGrid.select();
                    var selectedPO = this.kendoGrid.dataItem(rows[0]);
                    this.addItemToCart(selectedPO);
                };
                /**
                 * Adds a single PO into the cart and notified about the addition.
                 * @param PO A PO to be added into the cart.
                 */
                SearchPOs.prototype.addItemToCart = function (PO) {
                    // Present a different notification message depending if we already have such an item in the cart or not
                    $.NVS.staticflashMessager.hide();
                    $.NVS.staticflashMessager.info(this.service.hasCartItem(PO.Number) ? 1 : 0);
                    this.service.addCartItem(1, PO);
                };
                /**
                 * An event handler executed when a grid row selection changes.
                 */
                SearchPOs.onGridRowSelected = function (event) {
                    var that = SearchPOs.current;
                    var row = that.kendoGrid.select();
                    //if (row.length == 0) {
                    //    //that.addToCartButton.disableButton($.NVS.localization.PO_SearchPOs_AddToCart_ButtonDisabledTooltip);
                    //} else {
                    //    //that.addToCartButton.enableButton($.NVS.localization.PO_SearchPOs_AddToCartButtonTooltip);
                    //}
                };
                /**
                 * An event handler executed when an error is detected on the grid data source.
                 * @param event A data source error event object.
                 */
                SearchPOs.onGridError = function (event) {
                    $.NVS.summaryFlashMessenger.error($.NVS.localization.Error_GenericAjax);
                };
                return SearchPOs;
            }());
            Views.SearchPOs = SearchPOs;
        })(Views = LAT.Views || (LAT.Views = {}));
    })(LAT = Volvo.LAT || (Volvo.LAT = {}));
})(Volvo || (Volvo = {}));
//# sourceMappingURL=searchPOs.js.map