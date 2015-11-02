$(function() {
    //「发送验证码」单击事件
    $('#sendAuthCode').on('click', function(e) {
        var phoneNumber = $('#PhoneNumber').val().trim();
        if (phoneNumber == '') {
            alert("请输入您的手机号");
            return false;
        }

        sendAuthCode(phoneNumber);
    });

    function sendAuthCode(telephone) {
        $.ajaxAntiForgery({
            url: '/Account/SendVerificateCode',
            global: false,
            type: 'POST',
            dataType: "json",
            data: {
                type: 1,
                isVoice:false,
                phoneNumber: telephone
            },
            success: function(result) {
                if (result.State) {
                    return;
                }

                alert(result.Message);
            }
        });
    }
});
