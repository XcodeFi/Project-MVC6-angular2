$('#pagination-demo').twbsPagination({
    totalPages: 10,
    visiblePages: 4,
    onPageClick: function (event, page) {
        $('#data').html(_getAll());
    }
});

$host = location.origin;
var path = "/images/cms/news/";
var _id = -1;
var _imageUrl = "";
function _getAll() {
    $.ajax(
        {
            url: "/api/cardapi/admin",
            type: "GET",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                var html = '';
                $.each(result, function (key, item) {
                    //xu ly check box
                    $checked = "";
                    if (item.isPublished) {
                        $checked="checked"
                    }
                    else
                    {
                        $checked = "";
                    }
                    html += '<tr>';
                    html += '<td>' + item.title + '</td>';
                    html += '<td><img class="img-responsive" style="width:100px;" src="' + $host + path + item.imageUrl + '" data-toggle="tooltip" title="'+item.content+'" alt="' + item.content + '" /></td>';
                    html += '<td>' + new Date(item.dateCreated) + '</td>';
                    html += '<td align="center">' + '<input type="checkbox"' + $checked + '>' + '</td>';
                    html += '<td>' + item.likesNo + '</td>';
                    html += '<td>' + item.viewNo + '</td>';
                    html += '<td>' + item.rateNo + '</td>';
                    html += '<td align="center"><button class="btn btn-info btn-xs" onclick="return _edit(' + item.id + ')" ><i class="fa fa-edit"></i></button> ';
                    html += '<a class="btn btn-success btn-xs" ><i class="fa fa-eye"></i></a> ';
                    html += '<a class="btn btn-danger btn-xs" onclick="return _delete(' + item.id + ')"><i class="fa fa-trash"></i></a>';
                    html += '</td></tr>';
                })
                $('#data').html(html);
            },
            error: function (errMe) {
                alertify.error(err.responseText);
            }
        });
    $('#btnAdd').show();
    return false;
}

//btn edit
function _edit(id) {
    _id = id;
    $('#modalCreate').modal('show');
    $.ajax(
        {
            url: "/api/cardapi/" + id,
            type: "GET",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                $("#Title").attr("value", result.title);
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
    debugger;
    $.ajax(
        {
            url: "/api/cardapi/" + id,
            type: "PUT",
            data:JSON.stringify(obj),
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                $('#modalCreate').modal('hide');
                _getAll();
                alertify.success("This card was edit!");
                _id = -1;//gan lai gia tri ban dau 
            },
            error: function (e) {
                alertify.error("Something wrong");
            }
        }
        );
}
function btnCreate()
{
    if (_id === -1) {
        _add();
    }
    else {
        _put(_id);
    }
}

function _add() {
    var obj = {
        CateId: $("#CateId").val(),
        Title: $("#Title").val(),
        Content: $("#Content").val(),
        ImageUrl: $("#ImageUrl").val(),
        IsPublished: $("#IsPublished").val(),
        TextSearch: $("#TextSearch").val()
    }
    $.ajax({
        url: '/api/cardapi',
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            alertify.success("Add " + obj.Title + " was success!");
            _getAll();
            $('#modalCreate').modal('hide');
        },
        error: function (errormessage) {
            alertify.error(errormessage.responseText);
        }
    });
}

//delete
function _delete(id) {
    alertify.confirm("Are you sure that you want to delete !", function (e) {
        if (e) {
            // user clicked "ok"
            $.ajax({
                url: "/api/cardapi/" + id,
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


//xu ly up anh

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
        url: "/admin/cards/UploadFilesAjax",
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

$(document).ready(function () {
    _getAll();
    $('[data-toggle="tooltip"]').tooltip();
});