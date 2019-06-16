namespace Volvo.LAT.Views {

    export class ManageUsers {

        /**
        * A current, single instance of the ManageUsers page control.
        */
        public static current: ManageUsers;

        /**
         * An entity representing a currently selected user in the user grid.
         */
        private selectedUser: Models.UserModel;

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
        constructor(
            private editUserUrl: string,
            private addNewUserUrl: string,
            private deleteUserUrl: string,
            private notAuthorizedUrl: string,
            private editUserButton: JQuery,
            private addUserButton: JQuery,
            private deleteUserButton: JQuery,
            private grid: JQuery) {

            // We need to have access into the current instance from all the static event handlers.
            // We do not keep the instance on elements raising events but we prefer static like field.
            ManageUsers.current = this;

            this.editUserButton.click(() => this.onEditUserClick());
            this.addUserButton.click(() => this.onAddUserClick());
            this.deleteUserButton.click(() => this.onDeleteUserClick());
            this.grid.delegate("tbody > tr", "dblclick", () => this.onEditUserClick()); // grid row double click

            this.enableOrDisableToolbarButtons();
        }

        /**
         * Gives access into the kendo grid object from the current grid element.
         * @returns {} A kendo grid object.
         */
        private get kendoGrid(): kendo.ui.Grid {
            return this.grid.data("kendoGrid");
        }

        /**
         * Enables or disables the current toolbar buttons.
         */
        private enableOrDisableToolbarButtons(): void {

            // Add new user button is always enabled.
            this.addUserButton.enableButton($.NVS.localization.User_ManageUsers_AddUserButtonTooltip);

            if (!this.selectedUser) {
                this.disableToolbarButtons();
                return;
            }
            this.editUserButton.enableButton($.NVS.localization.User_ManageUsers_EditUserButtonTooltip);
            this.deleteUserButton.enableButton($.NVS.localization.User_ManageUsers_DeleteUserButtonTooltip);
        }

        /**
         * Disables the current toolbar buttons.
         */
        private disableToolbarButtons(): void {
            this.editUserButton.disableButton($.NVS.localization.User_ManageUsers_ButtonDisabledTooltip);
            this.deleteUserButton.disableButton($.NVS.localization.User_ManageUsers_ButtonDisabledTooltip);
        }

        /**
         * An event handler executed when an edit user button is clicked.
         */
        private onEditUserClick(): void {
            if (this.editUserButton.isDisabled()) {
                return;
            }
            if (!this.selectedUser) {
                return;
            }
            $.NVS.go(this.editUserUrl + "?userName=" + this.selectedUser.UserName);
        }

        /**
         * An event handler executed when an add user button is clicked.
         */
        private onAddUserClick(): void {
            $.NVS.go(this.addNewUserUrl);
        }

        /**
         * An event handler executed when a delete user button is clicked.
         */
        private onDeleteUserClick(): void {
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
                .post(this.deleteUserUrl, { userName: this.selectedUser.UserName }, (data: any, textStatus: string, jqXHR: JQueryXHR) => this.onActionSuccess(data))
                .fail(() => this.onActionError())
        }

        /**
         * An event handler executed when the user action has completed successfully.
         * @param data A data received from the order controller action.
         */
        private onActionSuccess(data: NVSRequestResult): void {
            $.NVS.summaryFlashMessenger.hide();
            $.NVS.displayFlashMessages(data, $.NVS.summaryFlashMessenger);
            if (data.Success) {
                this.kendoGrid.dataSource.read();
            }
        }

        /**
         * An event handler executed when the order action has ended with errors.
         */
        private onActionError(): void {
            $.NVS.summaryFlashMessenger.hide();
            $.NVS.summaryFlashMessenger.error($.NVS.localization.Order_ActionError);
            this.enableOrDisableToolbarButtons();
        }

        /**
         * An event handler executed when an error is detected on the grid data source.
         * @param event A data source error event object.
         */
        public static onGridError(event: kendo.data.DataSourceErrorEvent): void {
            $.NVS.summaryFlashMessenger.error($.NVS.localization.Error_GenericAjax);
        }

        /**
         * An event handler executed when a grid row selection changes.
         */
        public static onGridRowSelected(event: kendo.ui.GridChangeEvent): void {
            let that: ManageUsers = ManageUsers.current;

            // Get the currently selected order entity.
            var selectedRows: JQuery = that.kendoGrid.select();
            that.selectedUser = selectedRows.length == 0 ? null : <Models.UserModel><any>that.kendoGrid.dataItem(selectedRows[0]);

            that.enableOrDisableToolbarButtons();
        }

    }

}
