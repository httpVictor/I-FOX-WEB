//VALIDAÇÕES DA PÁGINA CADASTRO

//ELEMENTOS DA PAGINA
const form = document.getElementById('formulario');
const campos = document.querySelectorAll('.campo-input');
const spans = document.querySelectorAll('.span-obrigatorio');

//Caracteres e Regex para validar
const numeros = /[0123456789]/;
const caractereEspecial = /[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]/

campos[0].addEventListener("keypress", function(e) {
    checkChar(e)

})

function checkChar(e) {
    const char = String.fromCharCode(e.keyCode)
    console.log(e.keycode)
    console.log(char)
}

//Setando evento no form para
//form.addEventListener('submit', event => {
//    event.preventDefault()

//    validarNomeUsuario()
//    validacoes += validarSenha()
    
//    //caso as duas validações forem verdadeiras, subimitta
//    if (validacoes) {
//        event.submitter();
//    }
   
//})
//Caso de erro...
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
    if (campos[0].value.length < 6) {
        setarErro(0)
       
    } else {
        tirarErro(0);
        return true
    }

    //validar caracteres especiais (@, #,. e _)
}

//Validando a senha
function validarSenha() {
    //validar o tamanho da senha
    if (campos[2].value.length < 8 || campos[2].value.length > 30) {
        setarErro(2)
    } else {
        //validar se possui números
        if (numeros.test(campos[2].value)) {
            //validar se existe pelo menos um caractere especial
            if (caractereEspecial.test(campos[2])) {
                tirarErro(2)
                return true
            } else {
                setarErro(2)
            }
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






