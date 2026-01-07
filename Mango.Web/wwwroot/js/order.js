/*
var dataTable; 

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: "/order/getall" },
        "columns": [
            { data: 'orderheaderid', "width": "5%" },
            { data: 'email', "width": "25%" },
        ]
    });
}

*/
/*
document.addEventListener("DOMContentLoaded", function () {
    loadData();
});*/

/*
$(document).ready(function () {
    loadData();
});


function loadData() {
    fetch("/order/getall")
        .then(response => response.json())
        .then(data => {
            const tableBody = document.querySelector("#tblData tbody");
            tableBody.innerHTML = "";
            data.forEach(item => {
                const row = document.createElement("tr");
                row.innerHTML = `
                    <td>${item.orderheaderid}</td>
                    <td>${item.email}</td>
                `;
                tableBody.appendChild(row);
            });
        })
        .catch(error => console.error("Error fetching data:", error));
}

*/
/*
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    $('#tblData').DataTable({
        "ajax": { url: "/order/getall" },
        "columns": [
            { data: 'orderheaderid', "width": "5%" },
            { data: 'email', "width": "25%" },
        ]
    });
}
*/


/*
$(document).ready(function () {
    loadDataTable();
});


function loadData() {
    fetch("/order/getall")
        .then(response => response.json())
        .then(data => {
            const table = $('#tblData').DataTable({
                "data": data,
                "columns": [
                    { "data": "orderheaderid", "title": "ID", "width": "5%" },
                    { "data": "email", "title": "Email", "width": "25%" },
                    // Add more columns here as needed
                ]
            });
        })
        .catch(error => console.error("Error fetching data:", error));
}

*/
/*
var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            url: "/order/getall",
            error: function (jqXHR, textStatus, errorThrown) {
                console.error("Error fetching data:", errorThrown);
                // Handle the error appropriately, e.g., display an error message to the user
            }
        },
        "columns": [
            { "data": 'orderheaderid', "width": "5%" },
            { "data": 'email', "width": "25%" }
        ],
        // Add other essential DataTables options here, such as:
        "processing": true, // Show a loading indicator while data is fetched
        "serverSide": true, // Enable server-side processing for large datasets

        // Consider adding sorting, searching, pagination, and other features as needed
    });
}
*/



            
var dataTable;

$(document).ready(function () {
    loadDataTable();
});
function loadDataTable() {
    fetch("/order/getall")
        .then(response => response.json())
        .then(data => {
            const tableBody = document.querySelector("#tblData tbody");
            tableBody.innerHTML = "";

            if (Array.isArray(data) && data.length > 0) {
                let tableRows = "";
                data.forEach(item => {
                    tableRows += `
            <tr>
              <td>${item.orderheaderid}</td>
              <td>${item.email}</td>
            </tr>
          `;
                });
                tableBody.innerHTML = tableRows;
            } else {
                console.warn("No data received from server");
            }
        })
        .catch(error => console.error("Error fetching data:", error));
}
