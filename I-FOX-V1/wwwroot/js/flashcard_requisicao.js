const codigo = $('#codigo').val()
const codigoofc = parseInt(codigo)
const paragrafo = $('.escrita p')
listarCards(codigoofc)

function listarCards(codigooo) {
    axios.get('../../api/ServicosApi/listar-cartoes', { params: { cod_resumo: codigooo } })
        .then(response => {
            const data = response.data
            paragrafo.text(data)
        })
}

//fazendo a classe que vai receber a requisição
//https://pt.stackoverflow.com/questions/231609/converter-json-em-array-javascript