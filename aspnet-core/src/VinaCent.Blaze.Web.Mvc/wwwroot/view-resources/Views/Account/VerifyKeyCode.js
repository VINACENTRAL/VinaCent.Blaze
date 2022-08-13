const form = document.getElementById('VerifyAndChangePasswordFrom');

const c1 = document.getElementById('digit1-input');
const c2 = document.getElementById('digit2-input');
const c3 = document.getElementById('digit3-input');
const c4 = document.getElementById('digit4-input');

form.addEventListener('submit', (e) => {
    e.preventDefault();
})

form.querySelector('button[type="button"]').addEventListener('click', () => {
    if (!$('#VerifyAndChangePasswordFrom').valid()) return;

    const key = `${c1.value}${c2.value}${c3.value}${c4.value}`;

    if (key.length !== 4) {
        return;
    }

    window.location = form.action + `&key=${key}`;
});

function moveToNext(e, t) { 0 < e.value.length && document.getElementById("digit" + t + "-input").focus() }