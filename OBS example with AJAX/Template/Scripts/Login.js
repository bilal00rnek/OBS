function dologin() {
    var studentNumber = $("#studentNumber").val();
    var password = $("#password").val();
    var result = false;
    $.ajax({
        type: 'post',
        url: '/Home/CheckLogin?studentNumber=' + studentNumber + '&password=' + password,
        contentType: "charset=utf-8",
        async: false,
        success: function (data) {
            if (data == "error") {
                $("#message").show();
                setTimeout(
                    function () {
                        document.getElementById('message').style.display = 'none';
                    }, 3000);
                result = false;
            }
            else {
                $("#message").hide();
                result = true;
            }
        }
    });
    return result;
}