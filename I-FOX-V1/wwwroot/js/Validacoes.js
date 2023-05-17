//VALIDAÇÕES DA PÁGINA CADASTRO

//ELEMENTOS DA PAGINA
const form = document.getElementById('formulario');
const campos = document.querySelectorAll('.campo-input');
const spans = document.querySelectorAll('.span-obrigatorio');
const titulo = document.querySelector('.titulo')

//Caracteres e Regex para validar
const numeros = /[0123456789]/;


function setarErro(index) {
    campos[index].style.border = '1px solid #e63636';
    spans[index].style.display = 'block';
}

//Tirar o erro caso atenda as validações
function tirarErro(index) {
    campos[index].style.border = 'none';
    spans[index].style.display = 'none';
}

//Validando o nome do usuário
function validarNomeUsuario() {

    //validar o tamanho primeiro
    if (campos[0].value.length < 6 || campos[2].value.length > 20) {
        setarErro(0)
       
    } else {
        tirarErro(0);
        return true
    }
}

//Validando a senha
function validarSenha() {
    //validar o tamanho da senha
    if (campos[2].value.length < 8 || campos[2].value.length > 30) {
        setarErro(2)
    } else {
        //validar se possui números
        if (numeros.test(campos[2].value)) {
            tirarErro(2)
            return true
        } else {
            setarErro(2)
        }
    }
}

//validando o email da pessoa
function validarEmail() {
    //Regex com a formatção de um email
    (email.test(campos[1])) ? tirarErro(1) : setarErro(1)
}

function validarTitulo{
    titulo > 80 || titulo < 1 ? starErro(1) : tirarErro(1);
}