$("#anotacoes").keydown(function () {
    let tamanho = $('#anotacoes').val()
    console.log(tamanho.length)
    $('#caracteres').text(tamanho.length)

    if (tamanho.length > 10000) {
        $('#caracteres').text("Seu resumo está passando 10.000 caracteres! Para ser eficiente diminua ele!")
    }
});