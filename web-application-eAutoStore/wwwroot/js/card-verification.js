document.addEventListener("DOMContentLoaded", async function () {
    const result = await getFavoriteVehiclesAsync();
    verificationCards(result);
});
function verificationCards(favoriteVehicles) {
    const cards = document.querySelectorAll('.card');

    cards.forEach(card => {
        const dataId = parseInt(card.getAttribute('data-id'));
        const isVerificated = favoriteVehicles.some(vehicle => vehicle.vehicleId === dataId);

        if (isVerificated) {
            card.querySelector('.save-vehicle-btn').classList.add('btn-hidden');
            card.querySelector('.btn-saved').classList.remove('btn-hidden');
        };
    });
}
async function getFavoriteVehiclesAsync() {
    const url = "/FavoriteVehicles/GetFavoriteVehicles";

    const response = await fetch(url, {
        method: 'GET',
    });

    if (!response.ok) {
        console.log("Server response is negative");
        return null;
    }

    return await response.json();
}