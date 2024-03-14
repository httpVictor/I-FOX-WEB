
//Classe para o menu hamburguer, que vai receber de parâmetros o menu em si, a lista de itens do menu e os links respectivamente
class MobileNavbar {

    //Construtor da classe
    constructor(mobileMenu, navList, navLinks) {
        this.mobileMenu = document.querySelector(mobileMenu);
        this.navList = document.querySelector(navList);
        this.navLinks = document.querySelectorAll(navLinks);
        this.activeClass = "active";

        //método do javascript para reconhecer o this dentro de um método handleClick
        this.handleClick = this.handleClick.bind(this);
    }

    //Adicionando a animação no menu hamburguer
    animateLinks() {
        this.navLinks.forEach((link, index) => {
            link.style.animation ? (link.style.animation = ""): (link.style.animation = `navLinkFade 0.5s ease forwards ${index / 7 + 0.3}s`);
        });
    }

    //método para o eveneto de clique do botão
    handleClick() {
        this.navList.classList.toggle(this.activeClass);
        this.mobileMenu.classList.toggle(this.activeClass);
        this.animateLinks();
    }

    //Método para adicionar o evento de clique no botão do menu hamburguer
    addClickEvent() {
        this.mobileMenu.addEventListener("click", this.handleClick);
    }

    //Método para iniciar a classe
    init() {
        if (this.mobileMenu) {
            this.addClickEvent();
        }
        return this;
    }
}

//Objeto do menu
const mobileNavbar = new MobileNavbar(".menu-hamburguer",".nav-filhos",".nav-filhos li");

//Iniciando o código
mobileNavbar.init();