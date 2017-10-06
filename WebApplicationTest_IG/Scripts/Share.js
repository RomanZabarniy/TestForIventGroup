var COURENT_CLIENT_ID = '';

function InitCurrentClient(id){
    COURENT_CLIENT_ID = id;
}

function save_to_storage(key, val) {
    if (web_storage()) {
        localStorage.setItem(key, val);
    } else {
        alert("Your browser don't support local storage!!!");
    }
}

function showIndicator(isShow) {
    if (isShow)
        $('#loadPict').show('slow');
    else
        $('#loadPict').hide('slow');
}

function web_storage() {
    try {
        return 'localStorage' in window && window['localStorage'] !== null;
    } catch (e) {
        return false;
    }
}

function GetDataFromServer(methodName, data) {
    $.ajax({
        url: '/Home/' + methodName,
        type: "POST",
        async: true,
        cache: false,
        dataType: "json",
        data: "{'data':'" + data + "'}",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data['Code'] === "200") {
               // alert(data['BodyHtml']);
                save_to_storage( methodName, data['BodyHtml']);
            }
            else
                alert('On load city success error code ' + data['Message']);
        },
        error: function (data) {
            showIndicator(false);
            var info = '';
            info = data.responseText;
            console.warn('Error read from server ' + info);
        },
        accept: 'application/json'
    });
}