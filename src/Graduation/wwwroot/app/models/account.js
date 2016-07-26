"use strict";
var UserLogin = (function () {
    function UserLogin(email, password, rememberMe) {
        this.email = email;
        this.password = password;
        this.rememberMe = rememberMe;
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