document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("settings-button").addEventListener("click", function () {
        document.getElementById("modal").classList.add("show");
    });
    document.getElementById("close-button-main").addEventListener("click", function () {
        document.getElementById("modal").classList.remove("show");
    });
    document.getElementById("close-button").addEventListener("click", function () {
        document.getElementById("modal").classList.remove("show");
    });
});

const interval = 20 * 60 * 1000; // 20 mins in millisecs

setInterval(checkTokens, interval);
async function checkTokens() {
    const jwtToken = getCookieValue('jwt');

    if (jwtToken == null)
        return null;

    if (isTokenExpired(jwtToken)) {
        var url = "/Token/UpdateToken";
        const response = await fetch(url, { method: 'POST' });
        if (response.ok)
            console.log("Tokens are updated");
        else
            console.log("Token's aren't updated");
    }
    else console.log("Tokens don't need to be updated");
};

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
    const currentTimestamp = Math.floor(Date.now() / 1000); // current time in secs

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