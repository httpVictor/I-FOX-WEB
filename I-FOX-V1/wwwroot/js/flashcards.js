
//Adicionar cards->
//$(function () {

var imgs
$('.botaoAzul').on('click', function () {

    var bloco = $('.container:last').clone(),
        indice = parseInt(bloco.find('span').text()) + 1

    bloco.find('span').text(indice);
    bloco.find('textarea[name^="frente-"]').attr('name', 'frente-' + indice)
    bloco.find('textarea[name^="verso-"]').attr('name', 'verso-' + indice)
    bloco.find('img[id^="img-"]').attr('id', 'img-' + indice)
    // Se ouver outros inputs, altera o atributo name deles com o índice

    bloco.insertAfter('.container:last')
    imgs = document.querySelectorAll('.container img')
    console.log(imgs)
});

//deletar 
function deletarCard(card) {
    var bloco = $(card.remove())
}

//});
