"use strict";
var UserLogin = (function () {
    function UserLogin(id, email, username) {
        this.id = id;
        this.email = email;
        this.username = username;
    }
    return UserLogin;
}());
exports.UserLogin = UserLogin;
var ChangePassword = (function () {
    function ChangePassword(oldPassword, newPassword, confirmPassword) {
        this.oldPassword = oldPassword;
        this.newPassword = newPassword;
        this.confirmPassword = confirmPassword;
    }
    return ChangePassword;
}());
exports.ChangePassword = ChangePassword;
//# sourceMappingURL=account.js.map