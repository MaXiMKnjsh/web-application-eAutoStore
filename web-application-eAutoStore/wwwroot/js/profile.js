document.addEventListener("DOMContentLoaded", function () {
    const favCardsDelBtns = document.querySelectorAll('.del-vehicle-btn');
    favCardsDelBtns.forEach(delBtn => {
        delBtn.classList.remove('btn-hidden');
    });

    const favCardsSaveBtns = document.querySelectorAll('.save-vehicle-btn');
    favCardsSaveBtns.forEach(saveBtn => {
        saveBtn.classList.add('btn-hidden');
    });
});