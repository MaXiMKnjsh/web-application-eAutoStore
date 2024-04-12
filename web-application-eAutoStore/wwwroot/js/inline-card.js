async function deleteAdvertisement(button) {
    const card = button.parentNode.parentNode;
    const vehicleId = card.getAttribute("data-id");

    const url = "/Vehicles/DeleteVehicle?vehicleId=" + encodeURIComponent(vehicleId);

    const data = {
        vehicleId: vehicleId
    };
    try {
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
        card.remove();
    } catch (error) {
        console.log("Something went wrong", error);
    }
}