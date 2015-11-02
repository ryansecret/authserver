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
    
    $("#sendAuthCode2").on('click', function (e) {
        validatePhone();
        sendUpdateAuthCode($('#PhoneNumber').val().trim());
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

    var validatePhone = function() {
        var phoneNumber = $('#PhoneNumber').val().trim();
        if (phoneNumber == '') {
            alert("请输入您的手机号");
            return false;
        }
        ;
        return true;
    };

    //修改密码 
    var sendUpdateAuthCode = function (telephone) {
        $.ajaxAntiForgery({
            url: '/Account/SendVerificateCode2',
            global: false,
            type: 'POST',
            dataType: "json",
            data: {
                phoneNumber: telephone
            },
            success: function (result) {
                if (result.State) {
                    return;
                }
                alert(result.Message);
            }
        });
    };
});


