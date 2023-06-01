//guardando elementos do dom
const codigo = $('#codigo').val()
const frente = $('.front p')
const verso = $('.back p')
const totalCards = $('#cardstotais')

//pegando os inputs das respostas
const respostas = document.querySelectorAll('input[name="opcao-resposta"]')
console.log(respostas)
//variaveis que vão guardar as listas
let listaFrente = []
let listaVerso = []
let listaCodigo = []
//lista que vai inserir as respostas
let listaRespostas = []


//Achando oq foi selecionado
function acharselecionado(){
    let selecionado = document.querySelector('input[name="opcao-resposta"]:checked').value;
    return selecionado
}

respostas.forEach(respostas => {
    respostas.addEventListener("change", acharselecionado)
})


// Realizando a requisição e guardando em uma variável
listarCards(codigo).then(objeto => {
    const flashcards = JSON.parse(objeto);

    const arrayInterno = flashcards[0];

    listaFrente = arrayInterno.ListaFrente;
    listaVerso = arrayInterno.ListaVerso;
    listaCodigo = arrayInterno.CodigoCard;

    frente.text(listaFrente[0]);
    verso.text(listaVerso[0]);
    var i = 0;
    const total = listaCodigo.length
    totalCards.text((1) + "/" + total);

   //eveneto para navegar para o próximo card
    $("#next").on('click', function () {
        if (i < total) {
            //add na lista oq a pessoa achou do card
            listaRespostas[i] = acharselecionado();
            
            i++;

            totalCards.text((i + 1) + "/" + total)
            frente.text(listaFrente[i]);
            verso.text(listaVerso[i]);
        } else if (i + 1 === total) {
            
            totalCards.text(i + "/" + total)
            //add um botão de finalizar revisão
        }
        
    });

    //eveneto para retornar para o card anterior
    //$("#last").on('click', function () {
    //    if (i > 0) {
    //        i--;
    //        if (i < total) {
    //            totalCards.text((i + 1) + "/" + total)
    //            frente.text(listaFrente[i]);
    //            verso.text(listaVerso[i]);
    //        }
    //    }
    //});
    
    
}).catch(error => {
    console.error(error); // Trate erros da requisição
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
