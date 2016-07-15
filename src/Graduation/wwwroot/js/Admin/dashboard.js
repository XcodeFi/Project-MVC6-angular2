
function GetAllUse()
{
    $.get("/api/accountapi/GetAllUsers/0/5", function (data) {

        document.getElementById("txtTotalUsers").innerHTML = data.totalCount;
    });
}

function GetAllCard()
{
    $.get("/api/cardapi/admin/0/10", function (data) {
        document.getElementById("txtTotalCards").innerHTML = data.totalCount;
    });
}


function GetViews()
{

    $.ajax(
        {
            url: "/api/viewapi",
            type: "GET",
            contentType: "application/json;charset=utf-8",
            success: function (_result) {
                document.getElementById("txtTotalViews").innerHTML = _result.totalViews;
            }
        }
        );
}
var _init=function()
{
    GetViews();
    GetAllUse();
    GetAllCard();
    
}


$(document).ready(function () {
    _init();
})