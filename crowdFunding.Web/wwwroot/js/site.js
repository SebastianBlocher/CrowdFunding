// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var btnL = document.getElementById("btnLeft");
var btnR = document.getElementById("btnRight");

var content = document.getElementById("content");

let searchButton = $('.searchBtn');

btnR.addEventListener("click", goRight);
btnL.addEventListener("click", goLeft);

var clickedIndex = 0;

function goRight() {
    if (clickedIndex < 2) {
        clickedIndex = clickedIndex + 1;
        content.style.marginLeft = -190 * clickedIndex + "px";
    }
}

function goLeft() {
    if (clickedIndex > 0) {
        clickedIndex = clickedIndex - 1;
        content.style.marginLeft = -190 * clickedIndex + "px";
    }
}

$('.nav-link').on('click', (event) => {
    debugger;
    //event.target
});

searchButton.on('click', () => {
    let searchInput = $('#searchIn').val();
    if (searchInput == "") {
        return;
    }
    else {
        //$.ajax({
        //    type: 'POST',
        //    url: '/project',
        //    contentType: 'application/json',
        //    data: JSON.stringify(data)
        //}).done(customer => {
        //    successAlert.html(`Customer ${customer.firstname} ${customer.lastname} with id ${customer.customerId} was created`);
        //    successAlert.show();
        //}).fail(failureResponse => {
        //    failedAlert.show();
        //});
        console.log("input OK");
    }

});