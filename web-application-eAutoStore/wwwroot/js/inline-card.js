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

            // Сохраняем кнопку для дальнейших действий
            modal.dataset.targetButton = deleteButton;
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

        const button = modal.dataset.targetButton; // Кнопка, вызвавшая модальное окно
        const reason = document.querySelector('input[name="reason"]:checked').value;

        await deleteAdvertisementWithReason(button, reason, descriptionInput.value); // Передача данных
        modal.classList.remove("show-delete");
    };
});

async function deleteAdvertisementWithReason(button, reason, description) {
    const card = button.parentNode.parentNode;
    const vehicleId = card.getAttribute("data-id");

    const url = "/Vehicles/DeleteVehicleWithReason";

    const data = {
        vehicleId: vehicleId,
        reasonOfRemoving: reason,
        removingDescription: description
    };

    try {
        const response = await fetch(url, {
            method: 'DELETE',
            body: JSON.stringify(data),
            headers: {
                'Content-Type': 'application/json'
            }
        });

        if (response.ok) {
            card.remove(); // Удаляем карточку из DOM
        } else {
            console.error("Server response is negative");
        }
    } catch (error) {
        console.error("Something went wrong:", error);
    }
}
