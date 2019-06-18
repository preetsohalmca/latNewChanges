namespace Volvo.LAT.Views {

    export class AddEditUsers {

        /**
         * Creates an instance of the add/edit user page controller.
         * @param homeUrl An url pointing at the main view.
         * @param manageUsersUrl An url pointing at the user management controller action.
         * @param isUserAdmin A flag telling if a current user is an administrator.
         * @param cancelUserButton An element representing the cancel user button.
         */
        constructor(
            private homeUrl: string,
            private manageUsersUrl: string,
            private isUserAdmin: boolean,
            private cancelUserButton: JQuery) {
            
            this.cancelUserButton.click(() => this.onCancelClick());
            this.cancelUserButton.enableButton($.NVS.localization.User_AddEditUser_CancelButtonTooltip);
        }

        /**
         * An event handler executed when a cancel button is clicked.
         */
        private onCancelClick(): void {
            if (this.isUserAdmin) {
                $.NVS.go(this.manageUsersUrl);
            } else {
                $.NVS.go(this.homeUrl);
            }
        }

    }

}