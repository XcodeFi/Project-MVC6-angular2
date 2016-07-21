
function GetAllUse() {
    $.get("/api/accountapi/GetAllUsers/0/5", function (data) {
        document.getElementById("txtTotalUsers").innerHTML = data.totalCount;
    });
}

function GetAllCard() {
    $.get("/api/cardapi/admin/0/10", function (data) {
        document.getElementById("txtTotalCards").innerHTML = data.totalCount;
    });
}
function GetViews() {
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

var _init = function () {
    GetViews();
    GetAllUse();
    GetAllCard();
}

_initCardChart = function () {

    $.ajax(
            {
                url: "/api/cardapi/chart",
                type: "GET",
                contentType: "application/json;charset=utf-8",
                success: function (_result) {
                    Morris.Bar({
                        // ID of the element in which to draw the chart.
                        element: 'card-view-chart',
                        // Chart data records -- each entry in this array corresponds to a point on
                        // the chart.
                        data: _result,
                        xkey: 'title',
                        ykeys: ['viewNo'],
                        labels: ['viewNo'],
                        barRatio: 0.4,
                        xLabelAngle: 35,
                        hideHover: 'auto'
                    });
                }
            }
            );
}

$(document).ready(function () {
    _init();
    _initCardChart();
})



