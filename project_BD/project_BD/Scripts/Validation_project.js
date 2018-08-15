$("button[name='rec']").click(function () {
    // собираем данные в один массив
    var name = $("input[name='_name']").val().trim();
    var id_lead = $("select[name='_id_lead']").val().trim();
    var id_company_custoner = $("select[name='_id_company_custoner']").val().trim();
    var id_company_performer = $("select[name='_id_company_performer']").val().trim();
    var date_begin = $("input[name='_date_begin']").val().trim();
    var date_end = $("input[name='_date_end']").val().trim();
    
    //обрабатываю данные
    var error_arr = [];   
    if (name.length < 2) error_arr.push('Название (нужно >1 знака)');
    if (id_lead == 0) error_arr.push('Руководитель (не выбран)');
    if (id_company_custoner == 0) error_arr.push('Заказчик (не выбран)');
    if (id_company_performer == 0) error_arr.push('Исполнитель (не выбран)');
    if (date_begin > date_end) error_arr.push('Дата начала > даты завершения');
    if (date_begin == 0 || date_end == 0) error_arr.push('Даты не заполнены');
    

    // проверка на наличие ошибок
    if (error_arr.length > 0) {
        alert("Некорректные данные:\n" + error_arr.join('\n'));
        // блокировка перехода на другую страницу
        return false;
    } else {
        console.log("Ошибок нет!");
    }
});