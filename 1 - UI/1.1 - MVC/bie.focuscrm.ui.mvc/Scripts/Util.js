var formatDateComplete = function (data) {

    if (typeof (data) === 'undefined' || data === "") return "";
    
    var dt = new Date(data.replace(/-/g, '/'));

    

    //var dt = new Date(data);

    var d = dt.getDate();
    var m = dt.getMonth() + 1;
    var a = dt.getFullYear();
    var hh = dt.getHours();
    var mn = dt.getMinutes();

    return (d < 10 ? "0" : "") + d + "/" + (m < 10 ? "0" : "") + m + "/" + a + " " + (hh < 10 ? "0" : "") + hh + ":" + (mn < 10 ? "0" : "") + mn;

}


var formatDate = function (data) {

    if (typeof (data) === 'undefined' || data === "") return "";

    //var dt = new Date(data);
    var dt = new Date(data.replace(/-/g, '/'));

    var d = dt.getDate();
    var m = dt.getMonth() + 1;
    var a = dt.getFullYear();


    return (d < 10 ? "0" : "") + d + "/" + (m < 10 ? "0" : "") + m + "/" + a;

}





var IsNull = function (o, ret) {
    if (typeof (o) === 'undefined') {
        return ret;
    } else {
        return o;
    }


}


function isEmptyOrSpaces(str) {
    return str === null || str.match(/^ *$/) !== null;
}

