document.querySelector("#load-team-button")
    .addEventListener("click", async function () {
        var response = await fetch("load-team", { method: "GET" });
        var responseBody = await response.text();
        var teamContent = document.querySelector("#load-team-content");
        teamContent.classList.add('box');
        teamContent.innerHTML = responseBody;
    });