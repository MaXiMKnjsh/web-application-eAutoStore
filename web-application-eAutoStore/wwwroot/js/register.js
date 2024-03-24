document.getElementById('register-form').addEventListener('submit', function (event) {
    var passwordField = document.getElementById('passwordfield');
    var confirmPasswordField = document.getElementById('confirm-passwordfield');

    var password = passwordField.value;
    var confirmPassword = confirmPasswordField.value;

    if (password !== confirmPassword) {
        passwordField.style.borderColor = confirmPasswordField.style.borderColor = 'red';

        alert("The password doesn't match!");
        event.preventDefault();
    }
});