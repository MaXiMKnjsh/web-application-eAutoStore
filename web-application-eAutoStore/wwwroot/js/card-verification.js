document.addEventListener("DOMContentLoaded", async function () {
    const result = await getFavoriteVehiclesAsync();
    verificationCards(result);
});

function verificationCards(favoriteVehicles) {
    const cards = document.querySelectorAll('.card');

    cards.forEach(card => {
        const dataId = +card.getAttribute('data-id');
        const isVerified = favoriteVehicles.some(vehicle => vehicle.vehicleId === dataId);

        if (isVerified) {
            card.querySelector('.save-vehicle-btn').classList.add('btn-hidden');
            card.querySelector('.btn-saved').classList.remove('btn-hidden');
        }
    });
}

async function getFavoriteVehiclesAsync() {
    const url = "/FavoriteVehicles/GetFavoriteVehicles";

    try {
        const response = await fetch(url, {
            method: 'GET',
        });

        if (!response.ok) {
            console.error("Server response is negative");
            return null;
        }

        const contentType = response.headers.get('Content-Type');
        if (!contentType || !contentType.includes('application/json')) {
            console.error("Invalid response format");
            return null;
        }

        return await response.json();
    } catch (error) {
        console.error("Error occurred during the request:", error);
        return null;
    }
}