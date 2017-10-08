var COURENT_CLIENT_ID = '';
//localStorage.clear();

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

function getDataFromStorage(key) {
    var data = null;
    if (localStorage[key] !== null) {
        try {
            data = JSON.parse(localStorage[key]);
        }
        catch (e) {
            data = null;
        }

    }
    return data;
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
