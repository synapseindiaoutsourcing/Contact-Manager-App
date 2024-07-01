function validateEmail(sEmail) {

    var filter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    if (filter.test(sEmail)) {
        return true;
    }
    else {
        return false;
    }
}
function validateMobile(mobile) {
    var check = /^[6-9]\d{9}$/;
    if (check.test(mobile)) {
        return true;
    }
    else {
        return false;
    }
}