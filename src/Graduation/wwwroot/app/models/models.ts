export class Card {
    constructor(
        public id: number,
        public cateId: number,
        public title: string,
        public urlSlug: string,
        public content: string,
        public viewNo: number,
        public likesNo: number,
        public cardSize: string,
         public cardType: string,
        public isDeleted: boolean,
        public imageUrl: string,
        public rateNo: number,
        public dateCreated: Date,
        public isPublished: boolean,
        public tag: string[],
        public applycationUserId: string) { }
}

export class Cate {
    constructor(
        public icon: string,
        public id: number,
        public parentId: number,
        public level: number,
        public name: string,
        public urlSlug: string,
        public imageUrl: string,
        public dateCreated: Date,
        public description: string,
        public status: boolean,
        public cateChilds: Cate[]
        
    ) { }
}

export class CateDetail {
    constructor(
        public icon: string,
        public id: number,
        public parentId: number,
        public level: number,
        public name: string,
        public urlSlug: string,
        public imageUrl: string,
        public dateCreated: Date,
        public description: string,
        public status: boolean,
        public cateParent: CateDetail,
        public cards:Card[]
    ) { }
}




export class Slide {
    constructor(
        public id: number,
        public name: string,
        public imageUrl: string,
        public urlSlug:string
    ) { }
}

