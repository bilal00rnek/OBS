function checkAndConfirm(count) {
    var total = parseInt(count);
    var result = false;
    if (total < 3) {
        var CourseCode = $("#CourseCode").val();
        $.ajax({
            type: 'post',
            url: '/Home/ValidateAdding?CourseCode=' + CourseCode,
            contentType: "charset=utf-8",
            async: false,
            success: function (data) {
                if (data != "success") {
                    alert(data);
                }
                else {
                    result = confirm('Are you sure to add?');
                }
            }
        });
    }
    else {
        alert("You can select at most 3 lectures!!");
    }
    return result;
}
function getLectures() {
    var result = "";
    $.ajax({
        type: 'get',
        async: false,
        url: '/Home/getLectures',
        contentType: "charset=utf-8",
        success: function (data) {
            result = "<option value=\"\" disabled selected>Choose a lecture</option>";
            for (var i = 0; i < data.length; i++) {
                result += '<option value="' + data[i].LectureCode + '">' + data[i].LectureCode + " " + data[i].LectureName + " " + ' (' + data[i].Lecturer +')' + '</option>';
            }
            $("#CourseCode").html(result);
        }
    });
}
$(document).ready(function () {
    getLectures();
});