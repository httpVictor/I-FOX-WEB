//verificando se existe um arquivo
const arquivo = document.querySelector('#arq-1')
const imagem = document.querySelector('#arq-novo');
const div = document.querySelector('.add-input')
var textoSituacao = '+'
imagem.innerHTML = textoSituacao;


//Exibir a imagem antes de salvar ela
arquivo.addEventListener('change', function (e) {
    const inputTarget = e.target;
    const arquivoSelecionado = inputTarget.files[0];
    console.log(arquivoSelecionado)


    if (arquivoSelecionado) {

        if (arquivo.files[0].type != "image/png" || arquivo.files[0].type != "image/jpeg") {

            alert("Selecione apenas imagens!")
            arquivo.val('')

        } else {
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
        }

    } else {
        imagem.innerHTML = textoSituacao;
    }
})

