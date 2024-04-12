async function saveCard(button) {
    const vehicleId = button.parentNode.parentNode.getAttribute("data-id");
    const userId = await getUserId();
    if (userId != null && vehicleId != null && await saveToDB(userId, vehicleId)) {
        button.parentNode.querySelector('.save-vehicle-btn').classList.add('btn-hidden');
        button.parentNode.querySelector('.btn-saved').classList.remove('btn-hidden');
    }
}

async function getUserId() {
    const url = "/User/GetUserId";
    try {
        const response = await fetch(url, { method: 'GET' });
        if (!response.ok) {
            console.error("Server response is negative");
            return null;
        }
        return await response.text();
    } catch (error) {
        console.error("Error occurred during the request:", error);
        return null;
    }
}

async function saveToDB(userId, vehicleId) {
    if (userId == null || vehicleId == null) {
        console.error("Something gone wrong");
        return false;
    }

    const data = {
        userid: userId,
        vehicleid: vehicleId
    };
    try {
        const url = "/FavoriteVehicles/SaveFavoriteVehicle";
        const response = await fetch(url, {
            method: 'POST',
            body: JSON.stringify(data),
            headers: {
                'Content-Type': 'application/json'
            }
        });
        if (!response.ok) {
            console.error("Server response is negative");
            return false;
        }
        return true;
    } catch (error) {
        console.error("Error occurred during the request:", error);
        return false;
    }
}

async function deleteFavoriteCard(button) {
    const card = button.parentNode.parentNode;
    const vehicleId = card.getAttribute('data-id');
    const url = "/FavoriteVehicles/DeleteFavoriteVehicle?vehicleId=" + encodeURIComponent(vehicleId);
    try {
        const response = await fetch(url, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json'
            }
        });
        if (!response.ok) {
            console.error("Server response is negative");
            return false;
        }
        card.remove();
        return true;
    } catch (error) {
        console.error("Error occurred during the request:", error);
        return false;
    }
}