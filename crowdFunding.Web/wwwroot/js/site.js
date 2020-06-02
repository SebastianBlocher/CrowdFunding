// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



let userEditSuccessAlert = $('.js-useredit-success-alert');
userEditSuccessAlert.hide();

let userEditFailedAlert = $('.js-useredit-fail-alert');
userEditFailedAlert.hide();

let userEditButton = $('.js-useredit-submit-button');
userEditButton.on('click', () => {
    userEditSuccessAlert.hide();
    userEditFailedAlert.hide();

    let userid = document.getElementById("userId").innerHTML;

    let firstname = $('.js-useredit-firstname');
    let lastname = $('.js-useredit-lastname');
    let email = $('.js-useredit-email');
    let country = $('.js-useredit-country');
    let description = $('.js-useredit-description');

    let data = {
        userId: parseInt(userid),
        firstname: firstname.val(),
        lastname: lastname.val(),
        email: email.val(),
        country: country.val(),
        description: description.val()
    }
    debugger;
    $.ajax({
        type: 'PATCH',
        url: '/user/update',
        contentType: 'application/json',
        data: JSON.stringify(data),
        processData: false,
        dataType: 'json'
    }).done(user => {
        userEditSuccessAlert.html(`Customer with id ${user.userId} was updated.`);
        userEditSuccessAlert.show().delay(2000);
        userEditSuccessAlert.fadeOut();
    }).fail(failureResponse => {
        userEditFailedAlert.show();
        userEditFailedAlert.show().delay(2000);
        succeuserEditFailedAlertssAlert.fadeOut();
    })
});



//**************************************************
//Create User JS
//**************************************************
let userCreateSuccessAlert = $('.js-usercreate-success');
userCreateSuccessAlert.hide();

let userCreateFailedAlert = $('.js-usercreate-fail');
userCreateFailedAlert.hide();

let userCreateButton = $('.js-usercreate-submit-button');
userCreateButton.on('click', () => {
    userCreateSuccessAlert.hide();
    userCreateFailedAlert.hide();

    let firstname = $('.js-usercreate-firstname');
    let lastname = $('.js-usercreate-lastname');
    let email = $('.js-usercreate-email');
    let country = $('.js-usercreate-country');
    let description = $('.js-usercreate-description');

    let data = {
        firstname: firstname.val(),
        lastname: lastname.val(),
        email: email.val(),
        country: country.val(),
        description: description.val()
    }
    $.ajax({
        type: 'POST',
        url: '/user/create',
        contentType: 'application/json',
        data: JSON.stringify(data),
        processData: false,
        dataType: 'json'
    }).done(user => {
        userCreateSuccessAlert.html(`A customer with id ${user.userId} was created.`);
        userCreateSuccessAlert.show().delay(2000);
        userCreateSuccessAlert.fadeOut();
    }).fail(failureResponse => {
        userCreateFailedAlert.show().delay(2000);
        userCreateFailedAlert.fadeOut();
    })
});




var btnL = document.getElementById("btnLeft");
var btnR = document.getElementById("btnRight");

var content = document.getElementById("content");



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

//**************************************************
//Search Project JS
//**************************************************

let searchInput = $('#searchIn');
let searchButton = $('.searchBtn');
$(searchButton).attr("href", "test");


searchInput.on('input', () => {
    console.log("test");
    $(searchButton).attr("href", "project/search?name=" + searchInput.val());
});

//**************************************************
//User in Navbar
//**************************************************
let user = $('#usr');
let logIn = $('#jsLogIn');
let signUp = $('#jsSignUp');
let myProfile = $('#jsMyProfile');
let editProfile = $('#jsEditProfile');
let createdProjects = $('#jsCreatedProjects');
let backedProjects = $('#jsBackedProjects');



if (localStorage.getItem('userId') != null) {
    $(user).text(localStorage.getItem('userName'));
    myProfile.show();
    editProfile.show();
    createdProjects.show();
    backedProjects.show();
}
else {
    logIn.show();
    signUp.show();
}

