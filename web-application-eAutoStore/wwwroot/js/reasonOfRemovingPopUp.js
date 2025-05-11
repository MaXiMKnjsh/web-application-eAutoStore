// Обработчик для кнопки DELETE на карточке
document.querySelectorAll('.deleteVehButton').forEach(button => {
    button.addEventListener('click', function (event) {
        event.preventDefault(); // Останавливаем стандартное поведение кнопки

        // Получение родительского элемента с классом .card-inline
        const cardElement = event.target.closest('.card-inline');
        const vehicleId = cardElement ? cardElement.getAttribute('data-id') : null;

        if (!vehicleId) {
            alert('Не удалось определить ID транспортного средства.');
            return;
        }

        // Передача vehicleId в попап
        const modal = document.getElementById('modal-delete'); // Элемент попапа
        modal.setAttribute('data-vehicle-id', vehicleId); // Устанавливаем ID в атрибут попапа

        // Открытие попапа
        modal.classList.add('active'); // Добавляем класс active для отображения попапа
        console.log(`Vehicle ID передан на попап: ${vehicleId}`);
    });
});

document.getElementById('btnDeleteVeh').addEventListener('click', async function (event) {
    event.preventDefault(); // Останавливаем стандартное поведение кнопки

    // Получение vehicleId из атрибута data-vehicle-id попапа
    const modal = document.getElementById('modal-delete');
    const vehicleId = modal.getAttribute('data-vehicle-id');

    if (!vehicleId) {
        alert('Не удалось получить ID транспортного средства из попапа.');
        return;
    }

    // Получение выбранной радиокнопки
    const selectedReason = document.querySelector('input[name="reason"]:checked');

    // Получение текста из текстовой области
    const removingDescription = document.getElementById('removingDescription').value;

    // Сбор данных для запроса
    const request = {
        vehicleId: parseInt(vehicleId, 10), // Преобразуем ID в число
        reason: removingDescription,       // Строка с описанием причины
        reasonEnum: selectedReason.value   // Значение перечисления из радиокнопки
    };

    try {
        const url = "/Vehicles/DeleteVehicleWithReason";
        const response = await fetch(url, {
            method: 'POST',
            body: JSON.stringify(request),
            headers: {
                'Content-Type': 'application/json'
            }
        });

        if (!response.ok) {
            console.error("Server response is negative");
            alert('Ошибка на сервере при удалении.');
            return;
        }

        console.log('Успешно удалено:');

        // Закрываем попап после успешного удаления
        modal.classList.remove('show-delete');

        // Удаляем элемент из DOM
        const deletedElement = document.querySelector(`.card-inline[data-id="${vehicleId}"]`);
        if (deletedElement) {
            deletedElement.remove();
            console.log(`Элемент с ID ${vehicleId} удален из DOM.`);
        }

    } catch (error) {
        console.error("Error occurred during the request:", error);
        alert('Произошла ошибка при удалении.');
    }
});
