const codigo = $('#codigo').val()
const codigoLista = parseInt(codigo)
const frente = $('.front p')
const verso = $('.back p')


//variaveis que vão guardar as listas
var listaFrente
var listaVerso
var listaCodigo

//realizando a requisição e guardando em uma variavel
    listarCards(codigo).then(objeto => {
        const flashcards = JSON.parse(objeto)
        listaFrente = flashcards[0].ListaFrente
        listaVerso = flashcards[0].ListaVerso
        listaCodigo = flashcards[0].ListaCodigo

        var i = 0
        frente.text(listaFrente[i])
        verso.text(listaVerso[i])

        $("#next").on('click', function () {
            frente.text(listaFrente[i])
            verso.text(listaVerso[i])
            i++
        })

    }).catch(error => {
        console.error(error); // trate erros da requisição
    });


//Requisição para pegar os cards
function listarCards(codigooo) {
    return new Promise((resolve, reject) => {
        axios.get('../../api/ServicosApi/listar-cartoes', { params: { cod_resumo: codigooo } })
            .then(response => {
                const lista = response.data;
                resolve(lista); // resolva a Promise com o objeto retornado
            })
            .catch(error => {
                reject(error); // rejeite a Promise em caso de erro na requisição
            });
    });
}
