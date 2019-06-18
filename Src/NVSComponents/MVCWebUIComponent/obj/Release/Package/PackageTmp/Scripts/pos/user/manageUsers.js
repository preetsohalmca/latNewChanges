var Volvo;
(function (Volvo) {
    var LAT;
    (function (LAT) {
        var Views;
        (function (Views) {
            var ManageUsers = (function () {
                /**
                 * Creates an instance of the manage user page controller.
                 * @param editUserUrl An url pointing at the user edit controller action.
                 * @param addNewUserUrl An url pointing at the add new user controller action.
                 * @param deleteUserUrl An url pointing at the remove user controller action.
                 * @param notAuthorizedUrl An url pointing at the not authorization controller action.
                 * @param editUserButton An element representing the edit user button.
                 * @param addUserButton An element representing the add user button.
                 * @param deleteUserButton An element representing the delete user button.
                 * @param grid An element representing the grid presenting a list of users.
                 */
                function ManageUsers(editUserUrl, addNewUserUrl, deleteUserUrl, notAuthorizedUrl, editUserButton, addUserButton, deleteUserButton, grid) {
                    var _this = this;
                    this.editUserUrl = editUserUrl;
                    this.addNewUserUrl = addNewUserUrl;
                    this.deleteUserUrl = deleteUserUrl;
                    this.notAuthorizedUrl = notAuthorizedUrl;
                    this.editUserButton = editUserButton;
                    this.addUserButton = addUserButton;
                    this.deleteUserButton = deleteUserButton;
                    this.grid = grid;
                    // We need to have access into the current instance from all the static event handlers.
                    // We do not keep the instance on elements raising events but we prefer static like field.
                    ManageUsers.current = this;
                    this.editUserButton.click(function () { return _this.onEditUserClick(); });
                    this.addUserButton.click(function () { return _this.onAddUserClick(); });
                    this.deleteUserButton.click(function () { return _this.onDeleteUserClick(); });
                    this.grid.delegate("tbody > tr", "dblclick", function () { return _this.onEditUserClick(); }); // grid row double click
                    this.enableOrDisableToolbarButtons();
                }
                Object.defineProperty(ManageUsers.prototype, "kendoGrid", {
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
                 * Enables or disables the current toolbar buttons.
                 */
                ManageUsers.prototype.enableOrDisableToolbarButtons = function () {
                    // Add new user button is always enabled.
                    this.addUserButton.enableButton($.NVS.localization.User_ManageUsers_AddUserButtonTooltip);
                    if (!this.selectedUser) {
                        this.disableToolbarButtons();
                        return;
                    }
                    this.editUserButton.enableButton($.NVS.localization.User_ManageUsers_EditUserButtonTooltip);
                    this.deleteUserButton.enableButton($.NVS.localization.User_ManageUsers_DeleteUserButtonTooltip);
                };
                /**
                 * Disables the current toolbar buttons.
                 */
                ManageUsers.prototype.disableToolbarButtons = function () {
                    this.editUserButton.disableButton($.NVS.localization.User_ManageUsers_ButtonDisabledTooltip);
                    this.deleteUserButton.disableButton($.NVS.localization.User_ManageUsers_ButtonDisabledTooltip);
                };
                /**
                 * An event handler executed when an edit user button is clicked.
                 */
                ManageUsers.prototype.onEditUserClick = function () {
                    if (this.editUserButton.isDisabled()) {
                        return;
                    }
                    if (!this.selectedUser) {
                        return;
                    }
                    $.NVS.go(this.editUserUrl + "?userName=" + this.selectedUser.UserName);
                };
                /**
                 * An event handler executed when an add user button is clicked.
                 */
                ManageUsers.prototype.onAddUserClick = function () {
                    $.NVS.go(this.addNewUserUrl);
                };
                /**
                 * An event handler executed when a delete user button is clicked.
                 */
                ManageUsers.prototype.onDeleteUserClick = function () {
                    var _this = this;
                    if (this.deleteUserButton.isDisabled()) {
                        return;
                    }
                    if (!this.selectedUser) {
                        return;
                    }
                    if (!confirm($.NVS.localization.Action_DeleteConfirm)) {
                        return;
                    }
                    this.disableToolbarButtons();
                    $.NVS.summaryFlashMessenger.hide();
                    $.NVS.summaryFlashMessenger.info($.NVS.localization.Action_Deleting);
                    $
                        .post(this.deleteUserUrl, { userName: this.selectedUser.UserName }, function (data, textStatus, jqXHR) { return _this.onActionSuccess(data); })
                        .fail(function () { return _this.onActionError(); });
                };
                /**
                 * An event handler executed when the user action has completed successfully.
                 * @param data A data received from the order controller action.
                 */
                ManageUsers.prototype.onActionSuccess = function (data) {
                    $.NVS.summaryFlashMessenger.hide();
                    $.NVS.displayFlashMessages(data, $.NVS.summaryFlashMessenger);
                    if (data.Success) {
                        this.kendoGrid.dataSource.read();
                    }
                };
                /**
                 * An event handler executed when the order action has ended with errors.
                 */
                ManageUsers.prototype.onActionError = function () {
                    $.NVS.summaryFlashMessenger.hide();
                    $.NVS.summaryFlashMessenger.error($.NVS.localization.Order_ActionError);
                    this.enableOrDisableToolbarButtons();
                };
                /**
                 * An event handler executed when an error is detected on the grid data source.
                 * @param event A data source error event object.
                 */
                ManageUsers.onGridError = function (event) {
                    $.NVS.summaryFlashMessenger.error($.NVS.localization.Error_GenericAjax);
                };
                /**
                 * An event handler executed when a grid row selection changes.
                 */
                ManageUsers.onGridRowSelected = function (event) {
                    var that = ManageUsers.current;
                    // Get the currently selected order entity.
                    var selectedRows = that.kendoGrid.select();
                    that.selectedUser = selectedRows.length == 0 ? null : that.kendoGrid.dataItem(selectedRows[0]);
                    that.enableOrDisableToolbarButtons();
                };
                return ManageUsers;
            }());
            Views.ManageUsers = ManageUsers;
        })(Views = LAT.Views || (LAT.Views = {}));
    })(LAT = Volvo.LAT || (Volvo.LAT = {}));
})(Volvo || (Volvo = {}));
//# sourceMappingURL=manageUsers.js.map