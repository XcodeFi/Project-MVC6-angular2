"use strict";
var Card = (function () {
    function Card(id, cateId, title, urlSlug, content, viewNo, likesNo, cardSize, cardType, isDeleted, imageUrl, rateNo, dateCreated, isPublished, tag, applycationUserId) {
        this.id = id;
        this.cateId = cateId;
        this.title = title;
        this.urlSlug = urlSlug;
        this.content = content;
        this.viewNo = viewNo;
        this.likesNo = likesNo;
        this.cardSize = cardSize;
        this.cardType = cardType;
        this.isDeleted = isDeleted;
        this.imageUrl = imageUrl;
        this.rateNo = rateNo;
        this.dateCreated = dateCreated;
        this.isPublished = isPublished;
        this.tag = tag;
        this.applycationUserId = applycationUserId;
    }
    return Card;
}());
exports.Card = Card;
var Cate = (function () {
    function Cate(icon, id, parentId, level, name, urlSlug, imageUrl, dateCreated, description, status, cateChilds) {
        this.icon = icon;
        this.id = id;
        this.parentId = parentId;
        this.level = level;
        this.name = name;
        this.urlSlug = urlSlug;
        this.imageUrl = imageUrl;
        this.dateCreated = dateCreated;
        this.description = description;
        this.status = status;
        this.cateChilds = cateChilds;
    }
    return Cate;
}());
exports.Cate = Cate;
var Account = (function () {
    function Account(id, name) {
        this.id = id;
        this.name = name;
    }
    return Account;
}());
exports.Account = Account;
//# sourceMappingURL=models.js.map