var buttonGroups, list = document.querySelectorAll(".team-list");

function onButtonGroupClick(t) {
    "list-view-button" === t.target.id || "list-view-button" === t.target.parentElement.id ? (document.getElementById("list-view-button").classList.add("active"), document.getElementById("grid-view-button").classList.remove("active"), Array.from(list).forEach(function (t) {
        t.classList.add("list-view-filter"), t.classList.remove("grid-view-filter")
    })) : (document.getElementById("grid-view-button").classList.add("active"), document.getElementById("list-view-button").classList.remove("active"), Array.from(list).forEach(function (t) {
        t.classList.remove("list-view-filter"), t.classList.add("grid-view-filter")
    }))
}

!list || (buttonGroups = document.querySelectorAll(".filter-button")) && Array.from(buttonGroups).forEach(function (t) {
    t.addEventListener("click", onButtonGroupClick)
});