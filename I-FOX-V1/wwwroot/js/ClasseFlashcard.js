class Flashcard {

    constructor(listaCodigo, listaFrente, listaVerso) {
        this.mobileMenu = document.querySelector(mobileMenu);
        this.navList = document.querySelector(navList);
        this.navLinks = document.querySelectorAll(navLinks);
        this.activeClass = "active";

        //método do javascript para reconhecer o this dentro de um método handleClick
        this.handleClick = this.handleClick.bind(this);
    }
}