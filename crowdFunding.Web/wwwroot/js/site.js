// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


//**************************************************
//Login User JS
//**************************************************
let loginButton = $('.jsLogIn');
loginButton.on('click', () => {

    $('#js-modal-login-dialog').modal('show'); 
})

let userLoginSuccessAlert = $('.js-useredit-success-alert');
userLoginSuccessAlert.hide();

let userLoginFailedAlert = $('.js-useredit-fail-alert');
userLoginFailedAlert.hide();

let userLoginButton = $('.js-modal-login-login');
userLoginButton.on('click', () => {
    userLoginSuccessAlert.hide();
    userLoginFailedAlert.hide();

    let firstname = $('.js-useredit-firstname');
    let email = $('.js-useredit-email');

    let data = {
        firstname: firstname.val(),
        email: email.val(),
    }

    $.ajax({
        type: 'GET',
        url: '/user/search',
        contentType: 'application/json',
        data: JSON.stringify(data),
        processData: false,
        dataType: 'json'
    }).done(user => {
        userLoginSuccessAlert.html(`Login successful.`);
        userLoginSuccessAlert.show().delay(2000);
        userLoginSuccessAlert.fadeOut();

        localStorage.removeItem('userId');
        localStorage.removeItem('userName');

        localStorage.setItem('userId', user.userId);
        localStorage.setItem('userName', user.firstname);

        $('#js-modal-login-dialog').modal('hide');
        location.reload();
    }).fail(failureResponse => {
        userLoginFailedAlert.html(`Login failed.`);
        userLoginFailedAlert.show().delay(2000);
        userLoginFailedAlert.fadeOut();
    })
});


//**************************************************
//Edit User JS
//**************************************************
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
        userEditFailedAlert.fadeOut();
    })
});



//**************************************************
//Create User JS
//**************************************************
//let signUpButton = $('.georgepap123');
//signUpButton.on('click', () => {
    
//    $('#popup-user-create').modal('show'); 
//})
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
        userCreateSuccessAlert.html(`A user with id ${user.userId} was created.`);
        userCreateSuccessAlert.show().delay(2000);
        userCreateSuccessAlert.fadeOut();
        
        localStorage.removeItem('userId');
        localStorage.removeItem('userName');

        localStorage.setItem('userId', user.userId);
        localStorage.setItem('userName', user.firstname);
    }).fail(failureResponse => {
        userCreateFailedAlert.html(`${failureResponse.responseCode} - User creation failed, ${failureResponse.responseText}.`);
        userCreateFailedAlert.show().delay(2000);
        userCreateFailedAlert.fadeOut();
    })
});

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