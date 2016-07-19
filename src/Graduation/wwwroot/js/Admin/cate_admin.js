
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