const botoes_play = document.querySelectorAll(".botoes .play")
const botoes_pause = document.querySelectorAll(".botoes .pause")
const audios = document.querySelectorAll("audio")
console.log(audios)

//add as funcoes de play
for (let i = 0; i < botoes_play.length; i++) {
    let botaop = botoes_play[i];
    botaop.onclick = function () {
        let audio = audios[i];
        audio.play();
        console.log(botaop);
    };
}

//add as funcoes de pause
for (i = 0; i < botoes_play.length; i++) {https://localhost:44390/img/icones_resumo/play_icon.png
    botoes_play[i].onclick = function () {
        audios[i].play();
    }
}

function playAudio() {
    
}
function pauseAudio() {
    let x = document.getElementById("myAudio");
    x.pause();
}       
