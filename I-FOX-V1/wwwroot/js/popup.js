

const button = document.getElementsByClassName("lixin")
const modal = document.querySelector("dialog")
const buttonClose = document.querySelector("dialog button")

button.onclick = function() {
    modal.showModal()
}


buttonClose.onclick = function() {
    modal.close()
}