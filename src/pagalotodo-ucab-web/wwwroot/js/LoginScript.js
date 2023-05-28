const form = document.querySelector('form');
const usernameInput = document.querySelector('#username');
const passwordInput = document.querySelector('#password');
const rememberMeCheckbox = document.querySelector('#remember-me');

form.addEventListener('submit', e => {
    e.preventDefault();
    const username = usernameInput.value;
    const password = passwordInput.value;
    const rememberMe = rememberMeCheckbox.checked;
    // Aqu� puedes incluir la l�gica de autenticaci�n
    console.log(`Nombre de usuario: ${username}, Contrase�a: ${password}, Recordar datos de inicio de sesi�n: ${rememberMe}`);
});