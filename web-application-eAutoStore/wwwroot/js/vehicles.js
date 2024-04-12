const nextLink = document.getElementById("next-page-link");
const previousLink = document.getElementById("previous-page-link");
const totalPageSpan = document.getElementById("total-page");
const currentPageSpan = document.getElementById("current-page");

nextLink.addEventListener("click", function (event) {
    event.preventDefault();

    var currentUrl = window.location.href;
    var totalPage = parseInt(totalPageSpan.textContent);
    var currentPage = parseInt(currentPageSpan.textContent);

    if (currentUrl.indexOf("portion=") > -1) {
        var updatedUrl = currentUrl.replace(/(portion=)(\d+)/, function (match, p1, p2) {
            var newValue = parseInt(p2) + 1;
            if (newValue <= totalPage) {
                return p1 + newValue;
            } else {
                return p1 + totalPage;
            }
        });
        getVehicles(updatedUrl, newValue);
    } else {
        var separator = currentUrl.indexOf("?") > -1 ? "&" : "?";
        var updatedUrl = currentUrl + separator + "portion=2";
        getVehicles(updatedUrl, 2);
    }
});

previousLink.addEventListener("click", function (event) {
    event.preventDefault();

    var currentUrl = window.location.href;
    var totalPage = parseInt(totalPageSpan.textContent);
    var currentPage = parseInt(currentPageSpan.textContent);

    if (currentUrl.indexOf("portion=") > -1) {
        var updatedUrl = currentUrl.replace(/(portion=)(\d+)/, function (match, p1, p2) {
            var newValue = parseInt(p2) - 1;
            if (newValue >= 1) {
                return p1 + newValue;
            } else {
                return p1 + "1";
            }
        });
        getVehicles(updatedUrl, newValue);
    } else {
        var separator = currentUrl.indexOf("?") > -1 ? "&" : "?";
        var updatedUrl = currentUrl + separator + "portion=1";
        getVehicles(updatedUrl, 1);
    }
});

async function getVehicles(query, newValue) {
    const response = await fetch(query, { method: 'GET' });
    if (!response.ok) {
        console.log("Vehicles aren't updated");
        return;
    }

    const cardsToDelete = document.querySelectorAll(".card");
    cardsToDelete.forEach(function (card) {
        card.remove();
    });

    const responseHtml = await response.text();

    const tempDiv = document.createElement('div');
    tempDiv.innerHTML = responseHtml;

    const cardsToShow = tempDiv.querySelectorAll('.card');
    const vehiclesBlockDiv = document.querySelector('.vehicles-block');

    cardsToShow.forEach(card => {
        vehiclesBlockDiv.appendChild(card);
    });

    addListenerToCards();

    currentPageSpan.textContent = newValue;
}