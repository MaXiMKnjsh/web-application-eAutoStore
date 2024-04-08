document.addEventListener("DOMContentLoaded", function () {
    const favCardsDelBtns = document.querySelectorAll('.del-vehicle-btn');
    favCardsDelBtns.forEach(delBtn => {
        delBtn.style.display = 'block';
        delBtn.style.backgroundColor = 'rgb(255, 84, 84)';
        delBtn.addEventListener('mouseover', function () {
            delBtn.classList.add('btn-delete-hover');
        });

        delBtn.addEventListener('mouseout', function () {
            delBtn.classList.remove('btn-delete-hover');
        });
    });
});