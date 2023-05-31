//Adicionar cards->
var novos = 0;
console.log(novos);
$('.botaoAzul').on('click', function () {
    var totalCards = $('.container_flashcard #totalCartoes')

    var bloco = $('.container:last').clone(),
        indice = parseInt(bloco.find('span').text()) + 1

    //Mudando as propriedades de acordo com o inice
    bloco.find('span').text(indice);
    bloco.find('textarea[name^="frente-"]').attr('name', 'frente-' + indice)
    bloco.find('textarea[name^="verso-"]').attr('name', 'verso-' + indice)
    bloco.find('input[name^="codigo-"]').attr('name', 'codigo-naoDef' + indice)
    bloco.find('textarea').val('')

    $('#totalCartoes').val(indice)
    bloco.insertAfter('.container:last')
    $('#info-card').text('')
    novos++;
})

//deletar 
$('.botaoVermelho').on('click', function () {
    var totalCartoes = parseInt($('#totalCartoes').val())

    if(totalCartoes >= 4){
        $('.container:last').remove()
        $('#info-card').text('')
        $('#totalCartoes').val((totalCartoes - 1))
    } else {
        $('#info-card').text('Você precisa de pelo menos 3 cards!')
    }
})