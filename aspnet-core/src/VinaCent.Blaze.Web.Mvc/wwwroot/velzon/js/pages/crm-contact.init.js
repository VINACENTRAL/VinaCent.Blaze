/*
Template Name: Velzon - Admin & Dashboard Template
Author: Themesbrand
Website: https://Themesbrand.com/
Contact: Themesbrand@gmail.com
File: CRM-contact Js File
*/


// list js

function timeConvert(time) {
    var d = new Date(time);
    time_s = (d.getHours() + ':' + d.getMinutes());
    var t = time_s.split(":");
    var hours = t[0];
    var minutes = t[1];
    var newformat = hours >= 12 ? 'PM' : 'AM';
    hours = hours % 12;
    hours = hours ? hours : 12;
    minutes = minutes < 10 ? '0' + minutes : minutes;
    return (hours + ':' + minutes + '' + newformat);
}

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
        "designation",
        "date",
        "email_id",
        "phone",
        "lead_score",
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
var contactList = new List("contactList", options).on("updated", function (list) {
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
        contactList.add({
            id: '<a href="javascript:void(0);" class="fw-medium link-primary">#VZ' +
                raw.id +
                "</a>",
            name: raw.name,
            company_name: raw.company_name,
            designation: raw.designation,
            date: formatDate(raw.date)+' <small class="text-muted">'+timeConvert(raw.date)+'</small>',
            email_id: raw.email_id,
            phone: raw.phone,
            lead_score: raw.lead_score
        });
    });
    contactList.remove("id", `<a href="javascript:void(0);" class="fw-medium link-primary">#VZ001</a>`);
}
xhttp.open("GET", "assets/json/contact-list.json");
xhttp.send();

isCount = new DOMParser().parseFromString(
    contactList.items.slice(-1)[0]._values.id,
    "text/html"
);

var isValue = isCount.body.firstElementChild.innerHTML;

var idField = document.getElementById("id-field"),
    customerNameField = document.getElementById("customername-field"),
    company_nameField = document.getElementById("company_name-field"),
    designationField = document.getElementById("designation-field"),
    email_idField = document.getElementById("email_id-field"),
    phoneField = document.getElementById("phone-field"),
    lead_scoreField = document.getElementById("lead_score-field"),
    addBtn = document.getElementById("add-btn"),
    editBtn = document.getElementById("edit-btn"),
    removeBtns = document.getElementsByClassName("remove-item-btn"),
    editBtns = document.getElementsByClassName("edit-item-btn");
    viewBtns = document.getElementsByClassName("view-item-btn");
refreshCallbacks();

document
    .getElementById("showModal")
    .addEventListener("show.bs.modal", function (e) {
        if (e.relatedTarget.classList.contains("edit-item-btn")) {
            document.getElementById("exampleModalLabel").innerHTML = "Edit Contact";
            document
                .getElementById("showModal")
                .querySelector(".modal-footer").style.display = "block";
            document.getElementById("add-btn").style.display = "none";
            document.getElementById("edit-btn").style.display = "block";
        } else if (e.relatedTarget.classList.contains("add-btn")) {
            document.getElementById("exampleModalLabel").innerHTML = "Add Contact";
            document
                .getElementById("showModal")
                .querySelector(".modal-footer").style.display = "block";
            document.getElementById("edit-btn").style.display = "none";
            document.getElementById("add-btn").style.display = "block";
        } else {
            document.getElementById("exampleModalLabel").innerHTML = "List Contact";
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

document.querySelector("#contactList").addEventListener("click", function () {
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
        email_idField.value !== "" &&
        phoneField.value !== "" &&
        lead_scoreField.value !== "" &&
        designationField.value !== ""
    ) {
        contactList.add({
            id: '<a href="javascript:void(0);" class="fw-medium link-primary">#VZ' +
                count +
                "</a>",
            name: customerNameField.value,
            company_name: company_nameField.value,
            designation: designationField.value,
            email_id: email_idField.value,
            phone: phoneField.value,
            lead_score: lead_scoreField.value,
        });

        document.getElementById("close-modal").click();
        clearFields();
        refreshCallbacks();
        count++;
    }
});

editBtn.addEventListener("click", function (e) {
    document.getElementById("exampleModalLabel").innerHTML = "Edit Contact";
    var editValues = contactList.get({
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
                designation: designationField.value,
                email_id: email_idField.value,
                phone: phoneField.value,
                lead_score: lead_scoreField.value,
                date: '-----'
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
            var itemValues = contactList.get({
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
                            contactList.remove("id", isElem.outerHTML);
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
            var itemValues = contactList.get({
                id: itemId,
            });

            itemValues.forEach(function (x) {
                isid = new DOMParser().parseFromString(x._values.id, "text/html");
                var selectedid = isid.body.firstElementChild.innerHTML;
                if (selectedid == itemId) {
                    idField.value = selectedid;
                    customerNameField.value = x._values.name;
                    company_nameField.value = x._values.company_name;
                    designationField.value = x._values.designation;
                    email_idField.value = x._values.email_id;
                    phoneField.value = x._values.phone;
                    lead_scoreField.value = x._values.lead_score;

                }
            });
        });
    });

    viewBtns.forEach(function (btn) {
        btn.addEventListener("click", function (e) {
            console.log('clcik');
            e.target.closest("tr").children[1].innerText;
            itemId = e.target.closest("tr").children[1].innerText;
            var itemValues = contactList.get({
                id: itemId,
            });

            itemValues.forEach(function (x) {
                isid = new DOMParser().parseFromString(x._values.id, "text/html");
                var selectedid = isid.body.firstElementChild.innerHTML;
                if (selectedid == itemId) {
                    var codeBlock = `
                        <div class="card-body text-center">
                            <div class="position-relative d-inline-block">
                                <img src="assets/images/users/avatar-10.jpg" alt=""
                                    class="avatar-lg rounded-circle img-thumbnail">
                                <span class="contact-active position-absolute rounded-circle bg-success"><span
                                        class="visually-hidden"></span>
                            </div>
                            <h5 class="mt-4 mb-1">${x._values.name}</h5>
                            <p class="text-muted">${x._values.company_name}</p>

                            <ul class="list-inline mb-0">
                                <li class="list-inline-item avatar-xs">
                                    <a href="javascript:void(0);"
                                        class="avatar-title bg-soft-success text-success fs-15 rounded">
                                        <i class="ri-phone-line"></i>
                                    </a>
                                </li>
                                <li class="list-inline-item avatar-xs">
                                    <a href="javascript:void(0);"
                                        class="avatar-title bg-soft-danger text-danger fs-15 rounded">
                                        <i class="ri-mail-line"></i>
                                    </a>
                                </li>
                                <li class="list-inline-item avatar-xs">
                                    <a href="javascript:void(0);"
                                        class="avatar-title bg-soft-warning text-warning fs-15 rounded">
                                        <i class="ri-question-answer-line"></i>
                                    </a>
                                </li>
                            </ul>
                        </div>
                        <div class="card-body">
                            <h6 class="text-muted text-uppercase fw-semibold mb-3">Personal Information</h6>
                            <p class="text-muted mb-4">Hello, I'm Tonya Noble, The most effective objective is
                                one that is tailored to the job you are applying for. It states what kind of
                                career you are seeking, and what skills and experiences.</p>
                            <div class="table-responsive table-card">
                                <table class="table table-borderless mb-0">
                                    <tbody>
                                        <tr>
                                            <td class="fw-medium" scope="row">Designation</td>
                                            <td>${x._values.designation}</td>
                                        </tr>
                                        <tr>
                                            <td class="fw-medium" scope="row">Email ID</td>
                                            <td>${x._values.email_id}</td>
                                        </tr>
                                        <tr>
                                            <td class="fw-medium" scope="row">Phone No</td>
                                            <td>${x._values.phone}</td>
                                        </tr>
                                        <tr>
                                            <td class="fw-medium" scope="row">Lead Score</td>
                                            <td>${x._values.lead_score}</td>
                                        </tr>
                                        <tr>
                                            <td class="fw-medium" scope="row">Tags</td>
                                            <td>
                                                <span class="badge badge-soft-primary">Lead</span>
                                                <span class="badge badge-soft-primary">Partner</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="fw-medium" scope="row">Last Contacted</td>
                                            <td>${x._values.date} <small class="text-muted"></small></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>`;
                    document.getElementById('contact-view-detail').innerHTML = codeBlock;
                }
            });
        });
    });
}

function clearFields() {
    customerNameField.value = "";
    company_nameField.value = "";
    designationField.value = "";
    email_idField.value = "";
    phoneField.value = "";
    lead_scoreField.value = "";
}

// Delete All Records
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
                contactList.remove("id", `<a href="javascript:void(0);" class="fw-medium link-primary">${id}</a>`);
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