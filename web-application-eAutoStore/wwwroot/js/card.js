async function SaveCard(button) {
    const vehicleId = button.parentNode.parentNode.getAttribute("data-id");
    const userId = await getUserId();

    saveToDB(userId, vehicleId);
}
async function getUserId() {
    const url = "/User/GetUserId";
    const response = await fetch(url, { method: 'GET' });
    if (!response.ok) {
        console.log("Server response is negative");
        return null;
    }

    return await response.text();
}
async function saveToDB(userId, vehicleId) {
    if (userId == null || vehicleId == null) {
        console.log("Something gone wrong");
        return null;
    }

    const data = {
        userid: userId,
        vehicleid: vehicleId
    };

    const url = "/FavoriteVehicles/SaveFavoriteVehicle";
    const response = await fetch(url,
        {
            method: 'POST',
            body: JSON.stringify(data),
            headers: {
                'Content-Type': 'application/json'
            }
        });

    if (!response.ok) {
        console.log("Server response is negative");
    }
}