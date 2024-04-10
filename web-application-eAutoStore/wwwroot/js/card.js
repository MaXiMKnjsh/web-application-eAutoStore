async function saveCard(button) {
    const vehicleId = button.parentNode.parentNode.getAttribute("data-id");
    const userId = await getUserId();
    if (await saveToDB(userId, vehicleId)) {
        button.parentNode.querySelector('.save-vehicle-btn').classList.add('btn-hidden');
        button.parentNode.querySelector('.btn-saved').classList.remove('btn-hidden');
    }

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
    try {
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
            return false;
        }
    }
    catch { return false; }
    return true;
}

async function deleteFavoriteCard(button) {
    const card = button.parentNode.parentNode;
    const vehicleId = card.getAttribute('data-id');
    const url = "/FavoriteVehicles/DeleteFavoriteVehicle?vehicleId="+vehicleId;
    try {
        const response = await fetch(url,
            {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json'
                }
            });
        if (!response.ok) {
            console.log("Server response is negative");
            return false;
        }
        card.remove();
    }
    catch { console.log("Something went wrong"); }
}