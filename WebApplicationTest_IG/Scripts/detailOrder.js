$(document).ready(function () {
   
    showIndicator(true);

    if (COURENT_CLIENT_ID !== '') {
        GetDataFromServer("OrderList", COURENT_CLIENT_ID);
        GetDataFromServer("DetailOrderList", COURENT_CLIENT_ID); 
        var tableData = {
            parentRows: getDataFromStorage("OrderList"),
            childRows: getDataFromStorage("DetailOrderList")
        };
        CreateDoubleTable(tableData);
    }
    showIndicator(false);
});

function CreateDoubleTable(data) {
    var j = 10;
    var pRow = data.parentRows;
    let headerTitles = ["", "№ Операции", "Дата", "Сумма"];

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
   
    for (var i = 0; i < pRow.length; i++) {
        let tr = document.createElement('tr');
        tr.className = 'clickable';
        tr.setAttribute('data-toggle', "collapse");
        tr.setAttribute('data-target', ".row" + (i + 1).toString());
        tr.id = "row" + (i + 1).toString();

        let td = document.createElement('td');
        td.innerHTML = " ";
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
    }
    table.appendChild(tbody);


    var container = document.querySelector("#tableContainer");
   
    container.appendChild(table);
}

