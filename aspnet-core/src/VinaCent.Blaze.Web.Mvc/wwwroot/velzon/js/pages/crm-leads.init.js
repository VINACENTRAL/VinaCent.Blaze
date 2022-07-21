/*
Template Name: Velzon - Admin & Dashboard Template
Author: Themesbrand
Website: https://Themesbrand.com/
Contact: Themesbrand@gmail.com
File: CRM-leads Js File
*/


// list js

function formatDate(date) {
    var monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun",
        "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"
    ];
    var d = new Date(date),
        month = '' + monthNames[(d.getMonth())],
        day = '' + d.getDate(),
        year = d.getFullYear();
    if (month.length < 2)
        month = '0' + month;
    if (day.length < 2)
        day = '0' + day;
    return [day + " " + month, year].join(', ');
};

var checkAll = document.getElementById("checkAll");
if (checkAll) {
    checkAll.onclick = function () {
        var checkboxes = document.querySelectorAll(
            '.form-check-all input[type="checkbox"]'
        );
        for (var i = 0; i < checkboxes.length; i++) {
            checkboxes[i].checked = this.checked;
            if (checkboxes[i].checked) {
                checkboxes[i].closest("tr").classList.add("table-active");
            } else {
                checkboxes[i].closest("tr").classList.remove("table-active");
            }
        }
    };
}

var perPage = 8;

//Table
var options = {
    valueNames: [
        "id",
        "name",
        "company_name",
        "date",
        "leads_score",
        "phone",
        "location",
    ],
    page: perPage,
    pagination: true,
    plugins: [
        ListPagination({
            left: 2,
            right: 2
        })
    ]
};
// Init list
var leadsList = new List("leadsList", options).on("updated", function (list) {
    list.matchingItems.length == 0 ?
        (document.getElementsByClassName("noresult")[0].style.display = "block") :
        (document.getElementsByClassName("noresult")[0].style.display = "none");
    var isFirst = list.i == 1;
    var isLast = list.i > list.matchingItems.length - list.page;
    // make the Prev and Nex buttons disabled on first and last pages accordingly
    (document.querySelector(".pagination-prev.disabled")) ? document.querySelector(".pagination-prev.disabled").classList.remove("disabled"): '';
    (document.querySelector(".pagination-next.disabled")) ? document.querySelector(".pagination-next.disabled").classList.remove("disabled"): '';
    if (isFirst) {
        document.querySelector(".pagination-prev").classList.add("disabled");
    }
    if (isLast) {
        document.querySelector(".pagination-next").classList.add("disabled");
    }
    if (list.matchingItems.length <= perPage) {
        document.querySelector(".pagination-wrap").style.display = "none";
    } else {
        document.querySelector(".pagination-wrap").style.display = "flex";
    }

    if (list.matchingItems.length > 0) {
        document.getElementsByClassName("noresult")[0].style.display = "none";
    } else {
        document.getElementsByClassName("noresult")[0].style.display = "block";
    }
});

const xhttp = new XMLHttpRequest();
xhttp.onload = function () {
    var json_records = JSON.parse(this.responseText);
    json_records.forEach(function (raw){
        leadsList.add({
            id: '<a href="javascript:void(0);" class="fw-medium link-primary">#VZ' +
                raw.id +
                "</a>",
            name: raw.name,
            company_name: raw.company_name,
            date: formatDate(raw.date),
            leads_score: raw.leads_score,
            phone: raw.phone,
            location: raw.location
        });
        refreshCallbacks();
    });
    leadsList.remove("id", `<a href="javascript:void(0);" class="fw-medium link-primary">#VZ2101</a>`);
}
xhttp.open("GET", "assets/json/leads-list.json");
xhttp.send();

isCount = new DOMParser().parseFromString(
    leadsList.items.slice(-1)[0]._values.id,
    "text/html"
);

var isValue = isCount.body.firstElementChild.innerHTML;

var idField = document.getElementById("id-field"),
    customerNameField = document.getElementById("customername-field"),
    company_nameField = document.getElementById("company_name-field"),
    dateField = document.getElementById("date-field"),
    leads_scoreField = document.getElementById("leads_score-field"),
    phoneField = document.getElementById("phone-field"),
    locationField = document.getElementById("location-field"),
    addBtn = document.getElementById("add-btn"),
    editBtn = document.getElementById("edit-btn"),
    removeBtns = document.getElementsByClassName("remove-item-btn"),
    editBtns = document.getElementsByClassName("edit-item-btn");
refreshCallbacks();

document
    .getElementById("showModal")
    .addEventListener("show.bs.modal", function (e) {
        if (e.relatedTarget.classList.contains("edit-item-btn")) {
            document.getElementById("exampleModalLabel").innerHTML = "Edit Lead";
            document
                .getElementById("showModal")
                .querySelector(".modal-footer").style.display = "block";
            document.getElementById("add-btn").style.display = "none";
            document.getElementById("edit-btn").style.display = "block";
        } else if (e.relatedTarget.classList.contains("add-btn")) {
            document.getElementById("exampleModalLabel").innerHTML = "Add Lead";
            document
                .getElementById("showModal")
                .querySelector(".modal-footer").style.display = "block";
            document.getElementById("edit-btn").style.display = "none";
            document.getElementById("add-btn").style.display = "block";
        } else {
            document.getElementById("exampleModalLabel").innerHTML = "List Lead";
            document
                .getElementById("showModal")
                .querySelector(".modal-footer").style.display = "none";
        }
    });
ischeckboxcheck();

document
    .getElementById("showModal")
    .addEventListener("hidden.bs.modal", function () {
        clearFields();
    });

document.querySelector("#leadsList").addEventListener("click", function () {
    refreshCallbacks();
    ischeckboxcheck();
});

var table = document.getElementById("customerTable");
// save all tr
var tr = table.getElementsByTagName("tr");
var trlist = table.querySelectorAll(".list tr");

var count = Number(isValue.replace(/[^0-9]/g, "")) + 1;
addBtn.addEventListener("click", function (e) {
    if (
        customerNameField.value !== "" &&
        company_nameField.value !== "" &&
        dateField.value !== "" &&
        leads_scoreField.value !== "" &&
        phoneField.value !== "" &&
        locationField.value !== ""
    ) {
        leadsList.add({
            id: '<a href="javascript:void(0);" class="fw-medium link-primary">#VZ' +
                count +
                "</a>",
            name: customerNameField.value,
            company_name: company_nameField.value,
            date: formatDate(dateField.value),
            leads_score: leads_scoreField.value,
            phone: phoneField.value,
            location: locationField.value,
        });

        document.getElementById("close-modal").click();
        clearFields();
        refreshCallbacks();
        count++;
    }
});

editBtn.addEventListener("click", function (e) {
    document.getElementById("exampleModalLabel").innerHTML = "Edit Contact";
    var editValues = leadsList.get({
        id: idField.value,
    });
    editValues.forEach(function (x) {
        isid = new DOMParser().parseFromString(x._values.id, "text/html");
        var selectedid = isid.body.firstElementChild.innerHTML;
        if (selectedid == itemId) {
            x.values({
                id: '<a href="javascript:void(0);" class="fw-medium link-primary">' +
                    idField.value +
                    "</a>",
                name: customerNameField.value,
                company_name: company_nameField.value,
                date: formatDate(dateField.value),
                leads_score: leads_scoreField.value,
                phone: phoneField.value,
                location: locationField.value,
            });
        }
    });
    document.getElementById("close-modal").click();
    clearFields();
});


function ischeckboxcheck() {
    document.getElementsByName("checkAll").forEach(function (x) {
        x.addEventListener("click", function (e) {
            if (e.target.checked) {
                e.target.closest("tr").classList.add("table-active");
            } else {
                e.target.closest("tr").classList.remove("table-active");
            }
        });
    });
}

function refreshCallbacks() {
    removeBtns.forEach(function (btn) {
        btn.addEventListener("click", function (e) {
            e.target.closest("tr").children[1].innerText;
            itemId = e.target.closest("tr").children[1].innerText;
            var itemValues = leadsList.get({
                id: itemId,
            });

            itemValues.forEach(function (x) {
                deleteid = new DOMParser().parseFromString(x._values.id, "text/html");

                var isElem = deleteid.body.firstElementChild;
                var isdeleteid = deleteid.body.firstElementChild.innerHTML;

                if (isdeleteid == itemId) {
                    document
                        .getElementById("delete-record")
                        .addEventListener("click", function () {
                            leadsList.remove("id", isElem.outerHTML);
                            document.getElementById("deleteRecordModal").click();
                        });
                }
            });
        });
    });

    editBtns.forEach(function (btn) {
        btn.addEventListener("click", function (e) {
            e.target.closest("tr").children[1].innerText;
            itemId = e.target.closest("tr").children[1].innerText;
            var itemValues = leadsList.get({
                id: itemId,
            });

            itemValues.forEach(function (x) {
                isid = new DOMParser().parseFromString(x._values.id, "text/html");
                var selectedid = isid.body.firstElementChild.innerHTML;
                if (selectedid == itemId) {
                    idField.value = selectedid;
                    customerNameField.value = x._values.name;
                    company_nameField.value = x._values.company_name;
                    dateField.value = x._values.date;
                    leads_scoreField.value = x._values.leads_score;
                    phoneField.value = x._values.phone;
                    locationField.value = x._values.location;
                    flatpickr("#date-field", {
                        dateFormat: "d M, Y",
                        defaultDate: x._values.date,
                    });
                }
            });
        });
    });
}

function clearFields() {
    customerNameField.value = "";
    company_nameField.value = "";
    dateField.value = "";
    leads_scoreField.value = "";
    phoneField.value = "";
    locationField.value = "";
}


function deleteMultiple(){
    ids_array = [];
    var items = document.getElementsByName('chk_child');
    for (var i = 0; i < items.length; i++) {
        if (items[i].checked == true){
            var trNode = items[i].parentNode.parentNode.parentNode;
            var id = trNode.querySelector('.id a').innerHTML;
            ids_array.push(id);
        }
    }
    if(typeof ids_array !== 'undefined' && ids_array.length > 0){
        if(confirm('Are you sure you want to delete this?')){
            ids_array.forEach(function (id){
                leadsList.remove("id", `<a href="javascript:void(0);" class="fw-medium link-primary">${id}</a>`);
            });
            document.getElementById('checkAll').checked = false;
        }else{
            return false;
        }
    }else{
        alert("Please Select Atleast One Checkbox");
    }
}

document.querySelector(".pagination-next").addEventListener("click", function () {
    (document.querySelector(".pagination.listjs-pagination")) ? (document.querySelector(".pagination.listjs-pagination").querySelector(".active")) ?
    document.querySelector(".pagination.listjs-pagination").querySelector(".active").nextElementSibling.children[0].click(): '': '';
});
document.querySelector(".pagination-prev").addEventListener("click", function () {
    (document.querySelector(".pagination.listjs-pagination")) ? (document.querySelector(".pagination.listjs-pagination").querySelector(".active")) ?
    document.querySelector(".pagination.listjs-pagination").querySelector(".active").previousSibling.children[0].click(): '': '';
});