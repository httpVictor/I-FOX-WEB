

const button = document.querySelectorAll(".descricao_resumo button")
const modal = document.querySelectorAll("dialog")
const buttonClose = document.querySelectorAll("dialog button")

console.log(button)
console.log(modal)
console.log(buttonClose)

//Colocar popup dos
for (i = 0; i < button.length; i++) {
    let modalClicado = modal[i]
    button[i].onclick = function () {
        modalClicado.showModal()
    }
}

for (i = 0; i < button.length; i++) {
    let modalClicadoFechar = modal[i]
    buttonClose[i].onclick = function () {
        modalClicadoFechar.close()
    }
}
