//ShowComment
function SuccessComment() {
    $("#Name").val("");
    $("#Email").val("");
    $("#Comment").val("");
    $("#ParnetID").val("");
}
function ReplayComment(id) {
    $("#ParnetID").val(id);
    const element = document.getElementById("comments-Parent");
    $("html,body").delay(100).animate({ scrollTop: element.offsetTop - 100 }, 1000);
}
//EndShowComment
//ProductFeature
function DeleteFeature(id) {
    if (confirm("آیا از حذف مطمئن هستید")) {
        $.get("/Admin/Products/DeleteFeature/" + id, function () {
            $("#Feature_" + id).hide("slow");
        });
    }
}
//EndProductFeature
//ProductEdit
function readURL(input) {
    if (input.files && input.files[0]) {

        var reader = new FileReader();
        reader.onload = function (e) {
            $('#ImgPreviewProduct').attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    }
}
$("#ImageProduct").change(function () {
    readURL(this);
});
//EndProductEdit
//ProductCreate
function readURL(input) {

    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#imgPreviewProduct').attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    }
}

$("#imageProduct").change(function () {
    readURL(this);
});
//EndProductCreate
//ProductGroupIndex
function CreateHeadrGroup(parentId) {
    $.get("/Admin/Product_Groups/Create/" + parentId, function (result) {
        $("#MyModal").modal();
        $("#MyModalLabel").html("افزودن گروه جدید");
        $("#MyModalBody").html(result);
    });
}
function Edit(id) {
    $.get("/Admin/Product_Groups/Edit/" + id, function (result) {
        $("#MyModal").modal("show");
        $("#MyModalLabel").html("ویرایش گروه");
        $("#MyModalBody").html(result);
    });
}
function Delete(id) {
    if (confirm("آیا از حذف مطمئن هستید؟")) {

        $.get("/Admin/Product_Groups/Delete/" + id, function (result) {
            $("#Group_" + id).hide("slow");
        });
    }
}
function Success() {
    $("#MyModal").modal("hide");
}
//EndProductGroupIndex

