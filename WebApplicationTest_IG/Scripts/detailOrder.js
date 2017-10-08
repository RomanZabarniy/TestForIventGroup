$(document).ready(function () {
   
    showIndicator(true);

    if (COURENT_CLIENT_ID !== '') {
        GetDataFromServer("DetailOrderList", COURENT_CLIENT_ID); 
    }
    showIndicator(false);
});

function checkLocalStorage() {
    if (!localStorage.getItem('OrderList') || !localStorage.getItem('DetailOrderList'))
        return false;
    return true;
}

function CreateDoubleTable() {
    var cRow = getDataFromStorage("DetailOrderList");
    var pRow = getDataFromStorage("OrderList");
    let headerTitles = ["", "№ ОПЕРАЦИИ", "ДАТА", "СУММА"];
    let productsHeaderTitles = ["- НАИМЕНОВАНИЕ ", "ЦЕНА", "КОЛИЧЕСТВО", "СУММА"];

    // создаем начальную сруктуру таблицы
    let table = document.createElement('table'),
        thead = document.createElement('thead'),
        tr = document.createElement('tr');

    table.className = "table table-responsive table-hover";

    // генерируем заголовок таблицы
    for (let item of headerTitles ) {
        let th = document.createElement('th');
        //td.setAttribute('data-field', item.field);
        th.innerHTML = item;
        tr.appendChild(th);
    }

    thead.appendChild(tr);
    table.appendChild(thead);

    // генерируем и заполняем тело таблицы
    tbody = document.createElement('tbody');

    // перечисление заказов
    for (var i = 0; i < pRow.length; i++) {
        let tr = document.createElement('tr');
        tr.className = 'clickable';
        tr.setAttribute('data-toggle', "collapse");
        tr.setAttribute('data-target', ".row" + (i + 1).toString() + ",.h-row" + (i + 1).toString());
        tr.id = "row" + (i + 1).toString();

        let td = document.createElement('td');
        td.innerHTML = "<i class='glyphicon glyphicon-plus'></i>";
        tr.appendChild(td);

         td = document.createElement('td');
         td.innerHTML = pRow[i].OrderId;
        tr.appendChild(td);

         td = document.createElement('td');
         td.innerHTML = pRow[i].Date;
         tr.appendChild(td);
        
         td = document.createElement('td');
         td.innerHTML = pRow[i].SumByOrder;
         tr.appendChild(td);
        
         tbody.appendChild(tr);

        // таблица с товарами в заказе

         tr = document.createElement('tr');
         tr.className = "collapse h-row" + (i + 1).toString();

         for (let item of productsHeaderTitles) {
             let th = document.createElement('th');
             //td.setAttribute('data-field', item.field);
             th.innerHTML = item;
             tr.appendChild(th);
         }
         tbody.appendChild(tr);
        //таблица с товарами
         let count = 0;
         for (let j = 0; j < cRow.length; j++) {
             if (cRow[j].OrderID === pRow[i].OrderId) {
                tr = document.createElement('tr');
                tr.className = "collapse row" + (i + 1).toString();
                
                let td = document.createElement('td');
                td.innerHTML = "- " + (++count) + ") " + cRow[j].ProductName;
                tr.appendChild(td);

                td = document.createElement('td');
                td.innerHTML = cRow[j].Price;
                tr.appendChild(td);

                td = document.createElement('td');
                td.innerHTML = cRow[j].Quantity;
                tr.appendChild(td);

                td = document.createElement('td');
                td.innerHTML = cRow[j].Sum;
                tr.appendChild(td);

                tbody.appendChild(tr);               

             }
         }
        //пустая строка в конце подтаблицы для разделения
         tr = document.createElement('tr');
         tr.className = "collapse row" + (i + 1).toString();
         td = document.createElement('td');
         td.setAttribute('colspan', 4);
         td.innerHTML = " ";
         tr.appendChild(td);
         tbody.appendChild(tr);
    }
    table.appendChild(tbody);

    var container = document.querySelector("#tableContainer");
    container.appendChild(table);
}

       // загружаем данные
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
                save_to_storage(methodName, data['BodyHtml']);

                if (methodName === "DetailOrderList") {
                    GetDataFromServer("OrderList", COURENT_CLIENT_ID);
                }
                else if (methodName === "OrderList") {
                    if (!checkLocalStorage()) return;
                    CreateDoubleTable();
                }
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


