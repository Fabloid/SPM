$("button[name='record']").click(function () {
    // собираем данные в один массив
    var _family = $("input[name='_family']").val().trim();
    var _name = $("input[name='_name']").val().trim();
    var _patronymic = $("input[name='_patronymic']").val().trim();
    var _email = $("input[name='_email']").val().trim();

    var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
    var num = /[\d]/;

    //обрабатываю данные
    var error_arr = [];
    if (num.test(_family) == true || num.test(_name) == true || num.test(_patronymic) == true) error_arr.push('В ФИО не должно быть цифр');
    if (_family.length < 2) error_arr.push('Фамилия (нужно >1 знака)');
    if (_name.length < 2) error_arr.push('Имя (нужно >1 знака)');
    if (_patronymic.length < 2) error_arr.push('Отчество (нужно >1 знака)');
    if (reg.test(_email) == false) error_arr.push('e-mail (шаблон - name@domain.extension)');


    // проверка на наличие ошибок
    if (error_arr.length > 0) {
        alert("Некорректные данные:\n" + error_arr.join('\n'));
        // блокировка перехода на другую страницу
        return false;
    }
    else {
        console.log("Ошибок нет!");
    }
});