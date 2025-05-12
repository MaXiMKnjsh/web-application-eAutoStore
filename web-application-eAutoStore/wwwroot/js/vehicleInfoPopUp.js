document.getElementById('btnCancelVehicleInfo').addEventListener('click', function () {
    const modal = document.getElementById('modal-vehicle-info');
    modal.classList.remove('show-stats'); // Убираем класс, чтобы скрыть попап
});

document.getElementById('vehicleInfoForm').addEventListener('submit', async function (event) {
    event.preventDefault(); // Останавливаем стандартное поведение формы

    const infoModal = document.getElementById('modal-vehicle-info');
    const vehicleDataString = infoModal.getAttribute('data-vehicle');
    let vehicleData;
    if (vehicleDataString) {
        try {
            vehicleData = JSON.parse(vehicleDataString); // Преобразуем обратно в объект
            console.log("Полученные данные:", vehicleData);
        } catch (error) {
            console.error("Ошибка при обработке данных:", error);
        }
    } else {
        console.error("Данные отсутствуют.");
        return;
    }

    try {
        const request = {
            wayOfAttraction: document.getElementById('platformSource').value,
            wayOfSelling: document.getElementById('saleType').value,
            wayDescription: document.getElementById('feedback').value,
            quality: vehicleData.quality,
            brand: vehicleData.brand,
            model: vehicleData.model,
            type: vehicleData.type
        };

        url = "/Vehicles/SubmitVehicleInfo";
        const response = await fetch(url, {
            method: 'POST',
            body: JSON.stringify(request),
            headers: {
                'Content-Type': 'application/json'
            }
        });

        if (!response.ok) {
            console.error("Ошибка при отправке данных на сервер.");
            alert('Ошибка при отправке данных.');
            return;
        }

        // Закрываем попап после успешной отправки
        document.getElementById('modal-vehicle-info').classList.remove('show-stats');

    } catch (error) {
        console.error("Произошла ошибка:", error);
        alert('Произошла ошибка при отправке данных.');
    }
    infoModal.classList.remove('show-stats');
});
