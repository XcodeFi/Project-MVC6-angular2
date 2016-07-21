$host = location.origin;
var path = "/images/cms/news/";
var _id = -1;
var _imageUrl = "";
var _itemPerPage = 7;

function _resetForm()
{
    document.getElementById("Title").value="";
    document.getElementById("Content").value="";
    document.getElementById("ImageUrl").value="";
    document.getElementById("TextSearch").value = "";
    document.getElementById("imgId").src = "/images/logo/image_default.png";
}


$("#btnCreate").off('click').on('click', function () {
    _id = -1;
    _resetForm();
});

function _getAll(page,itemPerpage) {
    $.ajax(
        {
            url: "/api/cardapi/admin/"+page+"/"+itemPerpage,
            type: "GET",
            contentType: "application/json;charset=utf-8",
            success: function (_result) {
                _totalPages = _result.totalPages;
                _totalCount = _result.totalCount;
                _count = _result.count;
                _page = _result.page;
                var result = _result.items;
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
                    html += '<td>' + new Date(item.dateCreated) + '<br/>' + new Date(item.dateEdited) + '</td>';
                    html += '<td align="center">' + '<input type="checkbox"' + $checked + '>' + '</td>';
                    html += '<td>' + item.likesNo + '</td>';
                    html += '<td>' + item.viewNo + '</td>';
                    html += '<td>' + item.rateNo + '</td>';
                    html += '<td align="center"><button class="btn btn-info btn-xs" onclick="return _edit(' + item.id + ')" ><i class="fa fa-edit"></i></button> ';
                    html += '<a class="btn btn-success btn-xs" href="/admin/cards/details/'+item.id+'" ><i class="fa fa-eye"></i></a> ';
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
                document.getElementById("Title").value= result.title;
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
//search
//sua
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
            url: "/api/cardapi/" + id,
            type: "PUT",
            data:JSON.stringify(obj),
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                $('#modalCreate').modal('hide');
                _getAll(0,_itemPerPage);
                alertify.success("This card was edit!");
                _id = -1;//gan lai gia tri ban dau 
                resetForm();

            },
            error: function (e) {
                alertify.error(e);
            }
        }
        );
}
//save
function btnCreate()
{
    if (_id === -1) {
        _add();
    }
    else {
        _put(_id);
    }
}
//them
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
            _getAll(0,_itemPerPage);
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
                    _getAll(0,_itemPerPage);
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
$("#txtUploadFile").off('click').on('click',(function (evt) {
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
}));
$("#btnSearch").off('click').on('click', function (e) {
    e.preventDefault();
    if ($("#txtSearch").val().trim() == '') {
        alertify.error("You must enter any string in this field!");
        return;
    }
    $.ajax(
        {
            url: "/api/cardapi/search/" + document.getElementById("txtSearch").value,
            type: "GET",
            contentType: "application/json;charset=utf-8",
            success: function (result) {
                var html = '';
                $.each(result, function (key, item) {
                    //xu ly check box
                    $checked = "";
                    if (item.isPublished) {
                        $checked = "checked"
                    }
                    else {
                        $checked = "";
                    }
                    html += '<tr>';
                    html += '<td>' + item.title + '</td>';
                    html += '<td><img class="img-responsive" style="width:100px;" src="' + $host + path + item.imageUrl + '" data-toggle="tooltip" title="' + item.content + '" alt="' + item.content + '" /></td>';
                    html += '<td>' + new Date(item.dateCreated)+'/n'+ new Date(item.dateEdited)+ '</td>';
                    html += '<td align="center">' + '<input type="checkbox"' + $checked + '>' + '</td>';
                    html += '<td>' + item.likesNo + '</td>';
                    html += '<td>' + item.viewNo + '</td>';
                    html += '<td>' + item.rateNo + '</td>';
                    html += '<td align="center"><button class="btn btn-info btn-xs" onclick="return _edit(' + item.id + ')" ><i class="fa fa-edit"></i></button> ';
                    html += '<a class="btn btn-success btn-xs" href="/admin/cards/details/' + item.id + '" ><i class="fa fa-eye"></i></a> ';
                    html += '<a class="btn btn-danger btn-xs" onclick="return _delete(' + item.id + ')"><i class="fa fa-trash"></i></a>';
                    html += '</td></tr>';
                })
                $('#data').html(html);
            },
            error: function (e) {
                alertify.error("Something wrong");
            }
        }
        );
});
$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
    $_totalPages=1;
    $_totalCount=1;
    $_count=1;
    $_page = 1;
    $.ajax(
        {
            async:false,
            url: "/api/cardapi/admin/0/7",
            type: "GET",
            contentType: "application/json;charset=utf-8",
            success: function (_result) {
                $_totalPages = _result.totalPages;
                $_totalCount = _result.totalCount;
                $_count = _result.count;
                $_page = _result.page;
            },
            error: function (errMe) {
                alertify.error(err.responseText);
            }
        }
        );
    $('#pagination-demo').twbsPagination({
        totalPages: $_totalPages,
        visiblePages: 3,
        onPageClick: function (event, page) {
            _getAll(page - 1, 7)
        }
    });
    $("#_totalItemId").html($_totalCount);
    $("#_totalPageId").html($_totalPages);


    
});
