
var path = "/images/cms/cates/";
$("#txtUploadFile").click(function (evt) {
    evt.preventDefault();
    var fileUpload = $("#files").get(0);
    var files = fileUpload.files;
    var data = new FormData();
    for (var i = 0; i < files.length ; i++) {
        data.append(files[i].name, files[i]);
    }
    $.ajax({
        type: "POST",
        url: "/admin/categories/UploadFilesAjax",
        contentType: false,
        processData: false,
        data: data,
        success: function (message) {
            alertify.success(message + "was UPLOADED");
            $("#imgId").attr("src", path + message);
            document.getElementById("ImageUrl").value = message;
            _imageUrl = message;
        },
        error: function () {
            alertify.error("There was error uploading files!");
        }
    });
});

function _getAll() {
    $.ajax(
        {
            url: "/api/cateapi/getall",
            type: "GET",
            contentType: "application/json;charset=utf-8",
            success: function (_result) {
                //_totalPages = _result.totalPages;
                //_totalCount = _result.totalCount;
                //_count = _result.count;
                //_page = _result.page;
                //var result = _result.items;
                var html = '';
                $.each(_result, function (key, item) {
                    //xu ly check box
                    $_isPublished = item.isPublished==true?"checked":"";
                    $_isMainMenu = item.isMainMenu == true ? "checked" : "";
                    html += '<tr>';
                    html += '<td> <i class= "' + item.icon + '"></i></td>';
                    html += '<td>' + item.level + '</td>';
                    html += '<td>' + item.parentId + '</td>';
                    html += '<td>' + item.name + '<br/>' + new Date(item.dateCreated) + '</td>';
                    html += '<td><img class="img-responsive" style="width:100px;" src="' + location.origin + path + item.imageUrl + '" data-toggle="tooltip" title="' + item.description + '" alt="' + item.urlSlug + '" /></td>';
                    html += '<td align="center">' + '<input type="checkbox"' + $_isPublished + '>' + '</td>';
                    html += '<td align="center">' + '<input type="checkbox"' + $_isMainMenu + '>' + '</td>';
                    html += '<td align="center"><button class="btn btn-info btn-xs" onclick="return _edit(' + item.id + ')" ><i class="fa fa-edit"></i></button> ';
                    html += '<a class="btn btn-success btn-xs" href="/admin/cards/details/' + item.id + '" ><i class="fa fa-eye"></i></a> ';
                    html += '<a class="btn btn-danger btn-xs" onclick="return _delete(' + item.id + ')"><i class="fa fa-trash"></i></a>';
                    html += '</td></tr>';
                })

                $('#data').html(html);
            },
            error: function (errMe) {
                alertify.error(err.responseText);
            }
        });
    return false;
}

$(document).ready(function () {
    _getAll();
})

