
function Json2JsDateTime(datetime) {
    if (datetime == null) return datetime;
    if (typeof (datetime == 'string')) {
        return new Date(parseInt(datetime.substr(6)));
    }
    return datetime;
}

function CorrectDate(jsDate) {
    if (jsDate == undefined || jsDate == null) return null;
    return CheckDateNumber(jsDate.getDate()) + '.' + CheckDateNumber(jsDate.getMonth() + 1) + '.' + jsDate.getFullYear();
}

function CheckDateNumber(num) {
    if (num < 10) {
        return '0' + num;
    }
    return num;
}