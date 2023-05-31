//verificando se existe um arquivo
const arquivo = document.querySelector('#arq-1')
const imagem = document.querySelector('#arq-novo');
const div = document.querySelector('.add-input')
var textoSituacao = '+'
imagem.innerHTML = textoSituacao;


arquivo.addEventListener('change', function (e) {
    console.log("aq chegou");
    const inputTarget = e.target;
    const arquivoSelecionado = inputTarget.files[0];
    console.log(arquivoSelecionado)


    if (arquivoSelecionado) {
        $('form p').text('Arquivo preenchido')
        const reader = new FileReader()

        reader.addEventListener('load', function (e) {
            const readerTarget = e.target

            console.log("e aq??");

            const img = document.createElement('img');
            img.src = readerTarget.result
            img.classList.add('imagem_resumo');
            imagem.innerHTML = ''

            div.appendChild(img)


        })
        reader.readAsDataURL(arquivoSelecionado)

    } else {
        imagem.innerHTML = textoSituacao;
    }
})





$('form p').on('click', function () {
    let tamanho = $('#arq-1').val()
    if (tamanho != "") {
        $('form p').text('Arquivo preenchido')
    }
    console.log(tamanho)
})