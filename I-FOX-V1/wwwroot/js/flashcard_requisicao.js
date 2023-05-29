const codigo = $('#codigo').val()
const codigoofc = parseInt(codigo)

listarCards(codigoofc)

function listarCards(codigooo) {
    axios.get('api/cards/listar-cartoes', { params: { cod_resumo: codigooo } }).then(function (resposta) {
        console.log(resposta);
    })
}
