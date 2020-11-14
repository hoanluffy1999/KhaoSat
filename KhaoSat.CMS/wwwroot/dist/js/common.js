﻿function removeAccents(str) {
    str = stringToSlug(str);
   var s1 =  str.normalize('NFD')
       .replace(/[`“”"~!@#$%^&*()_|+\-=?;:'",.<>\{\}\[\]\\\/]/gi,' ')
        .replace(/đ/g, 'd').replace(/Đ/g, 'D');
    s = s1.split(' ')
    var value = ''
    for (var i = 0; i < s.length; i++) {
        
        if (s[i] != '-'&& s[i]!='') {
            value = value == '' ? s[i]: value + '-' + s[i]
        }
        //s = value;
    }
    return value+".html";
}
function removeAccents2(str) {
    str = stringToSlug(str);
    var s1 = str.normalize('NFD')
        .replace(/[`“”"~!@#$%^&*()_|+\-=?;:'",.<>\{\}\[\]\\\/]/gi, ' ')
        .replace(/đ/g, 'd').replace(/Đ/g, 'D');
    s = s1.split(' ')
    var value = ''
    for (var i = 0; i < s.length; i++) {

        if (s[i] != '-' && s[i] != '') {
            value = value == '' ? s[i] : value + '-' + s[i]
        }
        //s = value;
    }
    return value;
}
function CheckPass(pass) {
    var error = false;
    if (pass.length < 6) {
        showAlert("Mật khẩu phải lớn hơn 6 ký tự");
        error = true;
    }
    return error;
}