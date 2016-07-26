export class UserLogin {
    constructor(
        public email: string,
        public password: string,
        public rememberMe: boolean
    ) {
    }
}

export class ChangePassword {
    constructor(
        public oldPassword: string,
        public newPassword: string,
        public confirmPassword: string
    ) {
    }
}