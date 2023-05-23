const botoes_play = document.querySelectorAll(".botoes .play")
const botoes_pause = document.querySelectorAll(".botoes .pause")
const audios = document.querySelectorAll("audio")

//add as funcoes de play
for (i = 0; i < botoes_play.length; i++) {
    botoes_play[i].onclick = function () {
        audio[i].play();
    }
}


//add as funcoes de pause
for (i = 0; i < botoes_play.length; i++) {
    botoes_play[i].onclick = function () {
        audio[i].play();
    }
}

function playAudio() {
    
}
function pauseAudio() {
    let x = document.getElementById("myAudio");
    x.pause();
}       
