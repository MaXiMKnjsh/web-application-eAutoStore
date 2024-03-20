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