
var _id = -1;

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



$("#btnSave").off('click').on('click', function () {
    if (_id === -1) {
        _add();
    }
    else {
        _put(_id);
        _resetForm();
    }
});

function _add() {
    var obj = {
        ParentId: $("#ParentId").val(),
        Name: $("#Name").val(),
        icon: $("#Icon").val(),
        Level: $("#Level").val(),
        ImageUrl: $("#ImageUrl").val(),
        IsPublished: $("#IsPublished").val(),
        IsMainMenu: $("#IsMainMenu").val(),
        Description: $("#Description").val()
    }

    console.log(obj);
    $.ajax({
        url: '/api/cateapi',
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            alertify.success("Add " + obj.Name + " was success!");
            _getAll();
            $('#modalCreate').modal('hide');
        },
        error: function (errormessage) {
            alertify.error(errormessage.responseText);
        }
    });
}


function _edit(id) {
    _id = id;
    $('#modalCreate').modal('show');
    $.ajax(
        {
            url: "/api/cardapi/" + id,
            type: "GET",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                document.getElementById("Title").value = result.title;
                document.getElementById("imgId").src = $host + path + result.imageUrl;
                document.getElementById("ImageUrl").value = result.imageUrl;
                document.getElementById("IsPublished").checked = result.isPublished;
                document.getElementById("CateId").value = result.cateId;
                document.getElementById("Content").value = result.content;
                var _tag = "";
                for (var t in result.tag) {
                    _tag += result.tag[t] + ",";
                }
                document.getElementById("TextSearch").value = _tag.substring(0, _tag.length - 1);
            },
            error: function (e) {
                alertify.error("Something wrong");
            }
        }
        );
};
function _put(id) {
    var obj = {
        CateId: $("#CateId").val(),
        Title: $("#Title").val(),
        Content: $("#Content").val(),
        ImageUrl: document.getElementById("ImageUrl").value,
        IsPublished: document.getElementById("IsPublished").checked,
        TextSearch: $("#TextSearch").val()
    };
    $.ajax(
        {
            url: "/api/cateapi/" + id,
            type: "PUT",
            data: JSON.stringify(obj),
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                $('#modalCreate').modal('hide');
                _getAll();
                alertify.success("This card was edit!");
                _id = -1;//gan lai gia tri ban dau 
                //resetForm();
            },
            error: function (e) {
                alertify.error("Something wrong");
            }
        }
        );
}

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
                    html += '<a class="btn btn-success btn-xs" href="/admin/categories/details/' + item.id + '" ><i class="fa fa-eye"></i></a> ';
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

function _delete(id) {
    alertify.confirm("Are you sure that you want to delete !", function (e) {
        if (e) {
            // user clicked "ok"
            $.ajax({
                url: "/api/cateapi/" + id,
                type: "DELETE",
                success: function (resutl) {
                    alertify.success("Delete Successed!");
                    _getAll();
                },
                error: function (errormessage) {
                    alertify.error(errormessage.responseText);
                }
            })
        } else {
            // user clicked "cancel"
        }
    });
}
$(document).ready(function () {
    _getAll();
})

