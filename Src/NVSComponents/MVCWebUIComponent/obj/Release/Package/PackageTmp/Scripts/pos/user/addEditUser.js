var Volvo;
(function (Volvo) {
    var LAT;
    (function (LAT) {
        var Views;
        (function (Views) {
            var AddEditUsers = (function () {
                /**
                 * Creates an instance of the add/edit user page controller.
                 * @param homeUrl An url pointing at the main view.
                 * @param manageUsersUrl An url pointing at the user management controller action.
                 * @param isUserAdmin A flag telling if a current user is an administrator.
                 * @param cancelUserButton An element representing the cancel user button.
                 */
                function AddEditUsers(homeUrl, manageUsersUrl, isUserAdmin, cancelUserButton) {
                    var _this = this;
                    this.homeUrl = homeUrl;
                    this.manageUsersUrl = manageUsersUrl;
                    this.isUserAdmin = isUserAdmin;
                    this.cancelUserButton = cancelUserButton;
                    this.cancelUserButton.click(function () { return _this.onCancelClick(); });
                    this.cancelUserButton.enableButton($.NVS.localization.User_AddEditUser_CancelButtonTooltip);
                }
                /**
                 * An event handler executed when a cancel button is clicked.
                 */
                AddEditUsers.prototype.onCancelClick = function () {
                    if (this.isUserAdmin) {
                        $.NVS.go(this.manageUsersUrl);
                    }
                    else {
                        $.NVS.go(this.homeUrl);
                    }
                };
                return AddEditUsers;
            }());
            Views.AddEditUsers = AddEditUsers;
        })(Views = LAT.Views || (LAT.Views = {}));
    })(LAT = Volvo.LAT || (Volvo.LAT = {}));
})(Volvo || (Volvo = {}));
//# sourceMappingURL=addEditUser.js.map