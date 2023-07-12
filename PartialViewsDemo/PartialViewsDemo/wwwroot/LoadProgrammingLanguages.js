document.querySelector("#button-load-programming-languages")
    .addEventListener("click", async function () {
        var response = await fetch("programming-languages");
        var languages = await response.text();
        var languagesContent = document.querySelector(".programming-languages-content");
        languagesContent.classList.add('box');
        languagesContent.innerHTML = languages;
    });