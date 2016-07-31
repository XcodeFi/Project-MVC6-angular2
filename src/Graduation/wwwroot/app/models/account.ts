export class UserLogin {
    constructor(
        public id: string,
        public email: string,
        public username:string
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