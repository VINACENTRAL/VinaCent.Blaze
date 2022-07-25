/*
Template Name: Velzon - Admin & Dashboard Template
Author: Themesbrand
Website: https://Themesbrand.com/
Contact: Themesbrand@gmail.com
File: mailbox init Js File
*/


// favourite btn
document.querySelectorAll(".favourite-btn").forEach(function (item) {
    item.addEventListener("click", function () {
        this.classList.toggle("active");
    });
});

document.addEventListener("DOMContentLoaded", function () {
    var bodyElement = document.getElementsByTagName('body')[0];
    document.querySelectorAll(".col-mail a").forEach(function (item) {
        item.addEventListener("click", function () {
            bodyElement.classList.add("email-detail-show");
        });
    });

    document.querySelectorAll(".close-btn-email").forEach(function (item) {
        
        item.addEventListener("click", function () {
            bodyElement.classList.remove("email-detail-show");
        });
    });
});

var isShowMenu = false;
var emailMenuSidebar = document.getElementsByClassName('email-menu-sidebar');
document.querySelectorAll(".email-menu-btn").forEach(function (item) {
    item.addEventListener("click", function () {
        emailMenuSidebar.forEach(function (elm) {
            elm.classList.add("menubar-show");
            isShowMenu = true;
        });

    });
});

window.addEventListener('click', function (e) {
    if (document.querySelector(".email-menu-sidebar").classList.contains('menubar-show')) {
        if (!isShowMenu) {
            document.querySelector(".email-menu-sidebar").classList.remove("menubar-show");
        }
        isShowMenu = false;

    }
});

// editor
ClassicEditor
    .create(document.querySelector('#email-editor'))
    .then(function (editor) {
        editor.ui.view.editable.element.style.height = '200px';
    })
    .catch(function (error) {
        console.error(error);
    });


// checkbox-wrapper-mail
document.querySelectorAll(".message-list .checkbox-wrapper-mail input").forEach(function (element) {
    element.addEventListener('click', function (el) {
        if (el.target.checked == true) {
            el.target.closest('li').classList.add("active");
        } else {
            el.target.closest('li').classList.remove("active");
        }
    });
});

var checkAll = document.getElementById("checkAll");
if (checkAll) {
    checkAll.onclick = function () {
        var checkboxes = document.querySelectorAll('.message-list .checkbox-wrapper-mail input[type="checkbox"]');
        for (var i = 0; i < checkboxes.length; i++) {
            if (checkboxes[i].checked == false) {
                checkboxes[i].parentNode.parentNode.parentNode.classList.add("active");
                checkboxes[i].checked = this.checked;
            } else {
                checkboxes[i].parentNode.parentNode.parentNode.classList.remove("active");
                checkboxes[i].checked = false;
            }
        }
    };
}

// Delete All Records
function deleteMultiple() {
    ids_array = [];
    var items = document.querySelectorAll('.col-mail [type=checkbox]');
    for (var i = 0; i < items.length; i++) {
        if (items[i].checked == true){
            (items[i].parentNode.parentNode.parentNode).remove();
        }
    }
    document.getElementById('checkAll').checked = false;
}