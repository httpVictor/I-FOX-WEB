//verificando se existe um arquivo


$('form p').on('click', function () {
    let tamanho = $('#arq-1').val()
    if (tamanho != "") {
        $('form p').text('Arquivo preenchido')
    }
    console.log(tamanho)
})


//Fazendo uma prévia da img (https://horadecodar.com.br/javascript-preview-de-imagem-carregada-em-input-file//)
/*
function readImage() {
    if (this.files && this.files[0]) {
        var file = new FileReader();
        file.onload = function (e) {
            document.getElementById("preview").src = e.target.result;
        };
        file.readAsDataURL(this.files[0]);
    }
}
document.getElementById("img-input").addEventListener("change", readImage, false);
*/