async function deleteAdvertisement(button) {
    const vehicleId = button.parentNode.parentNode.getAttribute("data-id");

    const url = "/FavoriteVehicles/DeleteFavoriteVehicle";

    const data = {
        vehicleId: vehicleId
    };

    const response = await fetch(url, {
        method: 'DELETE',
        body: JSON.stringify(data),
        headers: {
            'Content-Type': 'application/json'
        }
    });

    if (!response.ok) {
        console.log("Server response is negative");
    }
}