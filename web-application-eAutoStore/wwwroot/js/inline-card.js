document.addEventListener("DOMContentLoaded", function () {
    // Получаем текстовое поле по ID
    const descriptionInput = document.getElementById("removingDescription");

    // Находим все радиокнопки
    const reasonInputs = document.querySelectorAll('input[name="reason"]');

    // Обрабатываем изменение для каждой радиокнопки
    reasonInputs.forEach(input => {
        input.addEventListener("change", function () {
            if (input.value === "Another" && input.checked) {
                descriptionInput.disabled = false; // Включаем текстовое поле
            } else {
                descriptionInput.disabled = true; // Отключаем текстовое поле
                descriptionInput.value = ""; // Очищаем поле
            }
        });
    });

    // Выбираем все кнопки с классом .deleteVehButton
    document.querySelectorAll(".deleteVehButton").forEach(deleteButton => {
        deleteButton.addEventListener("click", function () {
            const modal = document.getElementById("modal-delete");
            modal.classList.add("show-delete");

            // Сохраняем кнопку напрямую в объекте modal (НЕ dataset)
            modal.targetButton = deleteButton;
            console.log("Кнопка сохранена:", deleteButton); // Проверяем, сохраняется ли кнопка
        });
    });

    // Обработка кнопки "Отмена" и закрытия модального окна
    const cancelButton = document.getElementById("cancel-button");
    const modal = document.getElementById("modal-delete");

    cancelButton.addEventListener("click", function () {
        modal.classList.remove("show-delete");
    });

    // Обработка формы удаления
    const form = document.getElementById("deleteForm");
    form.onsubmit = async function (e) {
        e.preventDefault();

        const button = modal.targetButton; // Теперь это объект DOM, а не строка

        console.log("Переданная кнопка:", button); // Проверяем, передается ли объект

        if (!button) {
            console.error("Ошибка: кнопка не была сохранена перед отправкой формы.");
            return;
        }

        const reason = document.querySelector('input[name="reason"]:checked').value;
        await deleteAdvertisementWithReason(button, reason, descriptionInput.value);
        modal.classList.remove("show-delete");
    };
});

async function deleteAdvertisementWithReason(button, reason, description) {
    if (!button || !(button instanceof HTMLElement)) {
        console.error("Ошибка: переданная кнопка не является HTML-элементом.");
        return;
    }

    const card = button.closest('.card-inline'); // Используем closest вместо parentNode.parentNode
    if (!card) {
        console.error("Ошибка: карточка не найдена.");
        return;
    }

    const vehicleId = card.getAttribute("data-id");
    if (!vehicleId) {
        console.error("Ошибка: data-id отсутствует.");
        return;
    }
    try {
        const url = `/Vehicles/${vehicleId}`;

        const response = await fetch(url, {
            method: 'GET',
            headers: {
                'Accept': 'application/json'
            }
        });

        if (!response.ok) {
            console.error("Server response is negative");
            alert('Ошибка при получении данных.');
            return;
        } 

        const vehicleData = await response.json();
        console.log('Полученные данные о транспортном средстве:', vehicleData);

        const data = {
            vehicleId: vehicleId,
            reasonOfRemoving: reason,
            removingDescription: description
        };
        const url = "/Vehicles/DeleteVehicleWithReason";

        const infoModal = document.getElementById('modal-vehicle-info');
        infoModal.classList.add('show-stats');

        const response = await fetch(url, {
            method: 'POST',
            body: JSON.stringify(data),
            headers: {
                'Content-Type': 'application/json'
            }
        });

        if (response.ok) {
            card.remove(); // Удаляем карточку из DOM
            console.log(`Транспортное средство с ID ${vehicleId} удалено.`);

        } else {
            console.error("Ошибка: сервер вернул отрицательный ответ.");
        }

    }
    catch (error) {
        console.error("Ошибка", error);
    }

}
