document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("settings-button").addEventListener("click", function () {
        document.getElementById("modal-settings").classList.add("show-settings");
    });
    document.getElementById("close-button-main").addEventListener("click", function () {
        document.getElementById("modal-settings").classList.remove("show-settings");
    });
    document.getElementById("close-button").addEventListener("click", function () {
        document.getElementById("modal-settings").classList.remove("show-settings");
    });

    addListenerToCards();
});

function addListenerToCards() {
    const cards = document.getElementsByClassName("card");

    Array.from(cards).forEach(function (card) {
        const buttonSave = card.querySelector(".more-vehicle-btn");
        buttonSave.addEventListener("click", async function () {
            const vehicleId = card.getAttribute("data-id");
            const url = "/Vehicles/GetVehicleDetailsPartial?vehicleId=" + vehicleId;

            const response = await fetch(url, { method: 'GET' });

            if (response.ok) {
                document.getElementById("modal-carreview").classList.add("show-carreview");

                var targetDiv = document.getElementById("vehicle-review-body");

                targetDiv.innerHTML = await response.text();
            } else {
                console.log("Server response is negative");
            }
        });
    });

    document.getElementById("close-carreview").addEventListener("click", function () {
        document.getElementById("modal-carreview").classList.remove("show-carreview");
    });
    document.getElementById("close-carreview-2").addEventListener("click", function () {
        document.getElementById("modal-carreview").classList.remove("show-carreview");
    });
}

function clearCookiesFromTokens() {
    clearCookie("jwt");
    clearCookie("rt");
    location.reload();
}

function clearCookie(name) {
    document.cookie = name + "=; expires=Thu, 01 Jan 1970 00:00:00 GMT; path=/";
}

const interval = 20 * 60 * 1000; // 20 mins in milliseconds

setInterval(checkTokens, interval);

async function checkTokens() {
    const jwtToken = getCookieValue('jwt');

    if (jwtToken == null)
        return null;

    if (isTokenExpired(jwtToken)) {
        const url = "/Token/UpdateToken";
        try {
            const response = await fetch(url, { method: 'POST' });

            if (response.ok) {
                console.log("Tokens are updated");
            } else {
                console.log("Tokens are not updated");
            }
        } catch (error) {
            console.log("An error occurred while updating tokens:", error);
        }
    } else {
        console.log("Tokens do not need to be updated");
    }
}

function getCookieValue(key) {
    const cookieArray = document.cookie.split(';');
    for (let i = 0; i < cookieArray.length; i++) {
        const cookie = cookieArray[i].trim();
        if (cookie.startsWith(key + '=')) {
            return cookie.substring(key.length + 1);
        }
    }
    return null;
}

function isTokenExpired(token) {
    const expiration = getTokenExpiration(token); // time of token expiring
    const currentTimestamp = Math.floor(Date.now() / 1000); // current time in seconds

    const deadline = 20 * 60 * 1000; // 20 mins

    return expiration - deadline < currentTimestamp;
}

function getTokenExpiration(token) {
    const decodedToken = decodeJWT(token); // decode jwt
    if (!decodedToken) {
        return null;
    }

    // check the field exp
    if (!decodedToken.exp) {
        return null;
    }

    return decodedToken.exp;
}

function decodeJWT(token) {
    try {
        const tokenParts = token.split('.');
        const base64Payload = tokenParts[1];
        const payload = atob(base64Payload); // decode base64 line
        return JSON.parse(payload);
    } catch (error) {
        console.error('Decoding error:', error);
        return null;
    }
}