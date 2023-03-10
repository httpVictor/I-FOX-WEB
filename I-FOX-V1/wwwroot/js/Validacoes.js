//Variáveis de validações que vamos realizar
var numeros = /[0-9]/;

/* função para ver se a senha esta válida*/

function validarSenha(senha) {

// verificar se a senha tem no minimo 8 letras
    if (senha.length < 8) {
        document.getElementById(sit_senha).innerHTML = "Senha muito curta, digite pelo menos 8 digitos";
}

//verificar se excede 8 letras
if (senha.length >= 20) {
    return "";
}

// verificar se a senha contém pelo menos um caractere especial
if (!/[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]/.test(senha)) {
    return false;
}

//obrigado a ter pelo menos um numero 
if (/0123456789/.test(senha)) {
    return false;
}

// se passar pelas validações acima, retorna true
    return true;

}

document.getElementById("nome")
function validarNomeUsuario(){

    //verificar se não tem espaço

    //verificar
}
