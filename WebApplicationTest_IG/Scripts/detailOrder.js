$(document).ready(function () {

    var ordersList;
    var ordersListDetails = [];
    showIndicator(true);

    if (COURENT_CLIENT_ID !== '') {
        GetDataFromServer("OrderList", COURENT_CLIENT_ID);
        GetDataFromServer("DetailOrderList", COURENT_CLIENT_ID);   
    }
    showIndicator(false);
   
});



var defaultOrder = {
    OrderId: null,
    OrderDate: null,
    //ClientID,
    TableProduct: []
};









/*
class Table {
    constructor(config) {
        this.config = config;
        this.currentPage = 1;
        this.init();
    }
    splitByPages(data) {
        let headers = data.shift(), //заголовок таблицы (id == Идентификатор и.т.д)
            container = document.querySelector(this.config.output.container), // сюда помещаются все данные
            pages = [], // матрица страниц
            numPages = Math.ceil(data.length / this.config.perPage); // подсчет кол-ва необходимых страниц

        // разбиваем все данные по страницам.
        // массив имеет следующий вид: pages[номер страницы] === [[], [], [].....]

        for (let i = 0; i < numPages; i++) {
            pages[i] = [];
            for (let j = 0; j < this.config.perPage; j++) {
                if (data.length === 0) break;
                pages[i].push(data.shift());
            }
        }

        // записываем информацию о сраницах в localStorage
        localStorage.setItem("pages", JSON.stringify(pages));

        // создаем начальную сруктуру таблицы
        let table = document.createElement('table'),
            thead = document.createElement('thead'),
            tr = document.createElement('tr');

        // генерируем заголовок таблицы
        for (let item of headers) {
            let td = document.createElement('td');
            td.setAttribute('data-field', item.field);
            td.innerHTML = item.title;
            tr.appendChild(td);
        }

        thead.appendChild(tr);
        table.appendChild(thead);

        container.appendChild(table);
    }
    // переключение между страницами (т.е генерация таблицы на основе массива pages из localStorage)
    switchPage() {
        // если данных в localStorage по какой-то причине нет, то останавливаем работу метода.
        if (!localStorage.getItem('pages')) return;

        let table = document.querySelector(this.config.output.container + ' table'), // таблица
            pages = JSON.parse(localStorage.getItem('pages')), // массив со страницами
            tbody = document.querySelector(this.config.output.container + ' table tbody'); // элемент tbody в таблице

        // если tbody не найден, то создаем его. В обратном случае обнуляем.
        if (!tbody) {
            tbody = document.createElement('tbody');
        }
        else {
            tbody.innerHTML = '';
        }

        // генерируем содержимое элемента tbody
        pages[this.currentPage - 1].forEach((row) => {
            let tr = document.createElement('tr');

            row.forEach((cell) => {
                let td = document.createElement('td');
                td.innerHTML = cell;
                tr.appendChild(td);
            });

            tbody.appendChild(tr);
        });
        table.appendChild(tbody);
    }
    pagination() {
        // если данных в localStorage по какой-то причине нет, то останавливаем работу метода.
        if (!localStorage.getItem('pages')) return;

        let numPages = JSON.parse(localStorage.getItem('pages')).length, // кол-во страниц
            container = document.querySelector(this.config.output.container), // сюда помещаются все данные
            items = document.createElement('ul'); // контейнер пунктов меню

        // генерируем необходимое кол-во элементов.
        for (let i = 0; i < numPages; i++) {
            let item = document.createElement('li');
            item.innerHTML = i + 1;
            items.appendChild(item);
        }

        // вешаем обработчик клика на элементы пагинации
        items.addEventListener('click', (e) => {
            if (e.target.tagName !== 'LI') return false;

            this.currentPage = +e.target.innerHTML; // обновляем текущую страницу
            this.switchPage(); // перерисовываем таблицу (tbody)
        });

        // добавляем пагинацию в контейнер
        container.appendChild(items);
    }
    init() {
        // запрос к серверу
        fetch(this.config.url)
            .then((response) => {
                if (response.status !== 200) {
                    console.error('Something went wrong, response status: ' + response.status);
                    return;
                }
                response.json().then((response) => {
                    this.splitByPages(response); // разделяем данные по страницам
                    this.switchPage(); // так как изначально this.currentPage = 1, то отрисовываем первую страницу
                    this.pagination(); // генерируем пагинацию.
                });
            })
            .catch((err) => {
                console.error(err);
            });
    }
}
*/