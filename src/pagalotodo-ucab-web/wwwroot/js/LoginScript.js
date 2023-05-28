const form = document.querySelector('form');
const usernameInput = document.querySelector('#username');
const passwordInput = document.querySelector('#password');
const rememberMeCheckbox = document.querySelector('#remember-me');

form.addEventListener('submit', e => {
    e.preventDefault();
    const username = usernameInput.value;
    const password = passwordInput.value;
    const rememberMe = rememberMeCheckbox.checked;
    // Aquí puedes incluir la lógica de autenticación
    console.log(`Nombre de usuario: ${username}, Contraseña: ${password}, Recordar datos de inicio de sesión: ${rememberMe}`);
});