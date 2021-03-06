﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

//**************************************************
//Login User JS
//**************************************************
let loginButton = $('#jsLogIn');
loginButton.on('click', () => {

    $('#myLoginModal').modal('show');
})

let userLoginSuccessAlert = $('.js-userlogin-success-alert');
userLoginSuccessAlert.hide();

let userLoginFailedAlert = $('.js-userlogin-fail-alert');
userLoginFailedAlert.hide();

$("#login-FirstName").keyup(function (event) {
    if (event.keyCode === 13) {
        $("#jsLoginButton").click();
    }
});

$("#login-Email").keyup(function (event) {
    if (event.keyCode === 13) {
        $("#jsLoginButton").click();
    }
});

let userLoginButton = $('#jsLoginButton');
userLoginButton.on('click', () => {

    userLoginSuccessAlert.hide();
    userLoginFailedAlert.hide();

    let firstname = $('.js-userlogin-firstname');
    let email = $('.js-userlogin-email');

    let data = {
        firstName: firstname.val(),
        email: email.val(),
    }

    $.ajax({
        type: 'POST',
        url: '/user/login',
        contentType: 'application/json',
        data: JSON.stringify(data),
        processData: false,
        dataType: 'json'
    }).done(user => {
        userLoginSuccessAlert.html(`Login successful.`);
        userLoginSuccessAlert.show().delay(3000);
        userLoginSuccessAlert.fadeOut();

        localStorage.removeItem('userId');
        localStorage.removeItem('userName');

        localStorage.setItem('userId', user.userId);
        localStorage.setItem('userName', user.firstName);

        location.reload();
    }).fail(failureResponse => {
        userLoginFailedAlert.html('No user was found.');
        userLoginFailedAlert.show().delay(2000);
        userLoginFailedAlert.fadeOut();
    })
});

//**************************************************
//Log Out User JS
//**************************************************
let userLogOutButton = $('#jsLogOut');
userLogOutButton.on('click', () => {

    localStorage.removeItem('userId');
    localStorage.removeItem('userName');

    var url = "/Home/Index/";
    window.location.href = url;
});

//**************************************************
//Create New Project Check for Logged In User
//**************************************************
let homePageCreateButton = $('#js-createNewProject');
homePageCreateButton.on('click', () => {

    if (!localStorage.getItem("userId")) {
        $('#myCreateLoginModal').modal('show');
    }
    else {
        var url = $("#RedirectTo").val();
        location.href = url;
    }
});


//**************************************************
//Edit Profile JS
//**************************************************
let editprofileButton = $('#jsEditProfile');
editprofileButton.on('click', () => {

    let userid = localStorage.getItem('userId');

    var url = "/User/Edit/" + parseInt(userid);
    window.location.href = url;
});


//**************************************************
//Disable User JS
//**************************************************
let disableUserButton = $('#jsDisableUser');
disableUserButton.on('click', () => {

    let userid = localStorage.getItem('userId');

    $.ajax({
        type: 'DELETE',
        url: '/user/' + parseInt(userid),
    }).done(user => {

        localStorage.removeItem('userId');
        localStorage.removeItem('userName');

        var url = "/Home/Index/";
        window.location.href = url;

    }).fail(failureResponse => {

    })
})

//**************************************************
//My Profile JS
//**************************************************
let myprofileButton = $('#jsMyProfile');
myprofileButton.on('click', () => {

    let userid = localStorage.getItem('userId');

    var url = "/User/MyProfile/" + parseInt(userid);
    window.location.href = url;
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

    let userid = localStorage.getItem('userId');

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

        localStorage.setItem('userName', user.firstName);

        location.reload();

    }).fail(failureResponse => {
        userEditFailedAlert.show();
        userEditFailedAlert.show().delay(2000);
        userEditFailedAlert.fadeOut();
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
        userCreateSuccessAlert.html(`A user with id ${user.userId} was created.`);
        userCreateSuccessAlert.show().delay(2000);
        userCreateSuccessAlert.fadeOut();

        localStorage.removeItem('userId');
        localStorage.removeItem('userName');

        localStorage.setItem('userId', user.userId);
        localStorage.setItem('userName', user.firstName);

        var url = "/Home/Index/";
        window.location.href = url;

    }).fail(failureResponse => {
        userCreateFailedAlert.html(`${failureResponse.responseCode} - User creation failed, ${failureResponse.responseText}.`);
        userCreateFailedAlert.show().delay(2000);
        userCreateFailedAlert.fadeOut();
    })
});




var btnL = document.getElementById("btnLeft");
var btnR = document.getElementById("btnRight");

var content = document.getElementById("content");
if (btnL) {
    btnL.addEventListener("click", goLeft);
}

if (btnR) {
    btnR.addEventListener("click", goRight);
}

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

    //event.target
});

//**************************************************
//Search Project JS
//**************************************************

let searchInput = $('#searchIn');
let searchButton = $('.searchBtn');
//$(searchButton).attr("href", "");
$(searchButton).attr("href", "project/search?name= ");

searchInput.keyup(function (event) {
    if (event.keyCode === 13) {
        searchButton.click();
    }
});

searchButton.on('click', () => {
    var url = "/project/search?name=" + searchInput.val();
    window.location.href = url;
});


//**************************************************
//User in Navbar
//**************************************************
let user = $('#usr');
let logIn = $('#jsLogIn');
let signUp = $('#jsSignUp');
let myProfile = $('#jsMyProfile');
let editProfile = $('#jsEditProfile');
let logOutButton = $('#jsLogOut');


if (localStorage.getItem('userId') != null) {
    $(user).text(localStorage.getItem('userName'));
    myProfile.show();
    editProfile.show();
    logOutButton.show();
}
else {
    logIn.show();
    signUp.show();
}

//**************************************************
//Post Status Update
//**************************************************
let addStatusUpdateButton = $('#addStatusUpdateButton');
addStatusUpdateButton.on('click', () => {
    $('#modalCreateStatusUpdate').modal('show');
});

let addStatusButton = $('#addStatusButton');
addStatusButton.on('click', () => {
    let statusUpdate = $('#statusUpdate');
    let projectid = $('#prid').val();

    let data = {
        projectId: projectid,
        post: statusUpdate.val()
    };
    debugger;
    $.ajax({
        type: 'POST',
        url: '/post/create',
        contentType: 'application/json',
        data: JSON.stringify(data),
        dataType: 'json'
    }).done(_project => {

        $('#statusUpdate').val('');

        $('#modalCreateStatusUpdate').modal('hide');

    }).fail(_failureResponse => {

    });

    $('#modalCreateStatusUpdate').modal('hide');
});

//**************************************************
//Add Reward Pagkages with Rewards - name description amount
//**************************************************
var rewardOptions = [];

let addRewardPackageButton = $('#addRewardPackageButton');
addRewardPackageButton.on('click', () => {
    $('#modalCreateRewardPackages').modal('show');
});

let addRewardsButton = $('#addRewardsButton');
addRewardsButton.on('click', () => {
    let rew1 = $('#reward1').val();
    let rew2 = $('#reward2').val();
    let rew3 = $('#reward3').val();
    let rew4 = $('#reward4').val();
    let rew5 = $('#reward5').val();
    let projectid = $('#prid').val();

    if (rew1) {
        let reward1 = {
            name: rew1
        }
        rewardOptions.push(reward1);
    }
    if (rew2) {
        let reward2 = {
            name: rew2
        }
        rewardOptions.push(reward2);
    }
    if (rew3) {
        let reward3 = {
            name: rew3
        }
        rewardOptions.push(reward3);
    }
    if (rew4) {
        let reward4 = {
            name: rew4
        }
        rewardOptions.push(reward4);
    }
    if (rew5) {
        let reward5 = {
            name: rew5
        }
        rewardOptions.push(reward5);
    }

    let data = {
        projectId: parseInt(projectid),
        name: $('#rewardName').val(),
        description: $('#rewardDescription').val(),
        amount: parseFloat($('#rewardAmount').val()),
        rewardOptions: rewardOptions
    };

    $.ajax({
        type: 'POST',
        url: '/rewardpackage/create',
        contentType: 'application/json',
        data: JSON.stringify(data),
        dataType: 'json'
    }).done(_project => {
        rew1 = '';
        rew2 = '';
        rew3 = '';
        rew4 = '';
        rew5 = '';

        $('#reward1').val('');
        $('#reward2').val('');
        $('#reward3').val('');
        $('#reward4').val('');
        $('#reward5').val('');
        $('#rewardName').val('');
        $('#rewardDescription').val('');
        $('#rewardAmount').val('');

        $('#modalCreateRewardPackages').modal('hide');
        location.reload();
        
    }).fail(_failureResponse => {

    });

    rewardPackages.push(JSON.stringify(data));
    $('#modalCreateRewardPackages').modal('hide');
});

// ----- Create project----//
let createSuccessAlert = $('.js-create-success-alert');
createSuccessAlert.hide();

let createFailedAlert = $('.js-create-failed-alert');
createFailedAlert.hide();

let projectCreateButton = $('.js-projectcreate-submit-button');
projectCreateButton.on('click', () => {
    createSuccessAlert.hide();
    createFailedAlert.hide();

    let userid = localStorage.getItem("userId");
    let name = $('.js-projectcreate-projectname');
    let description = $('.js-projectcreate-description');
    let category = $('.js-project-create-category');
    let amountrequired = $('.js-projectcreate-amountrequired');

    let dueto = new Date($('#js-projectcreate-dueto').val());

    let photos = $('#js-projectcreate-photos').val();
    let videos = $('#js-projectcreate-videos').val();



    let data = {
        userId: parseInt(userid),
        name: name.val(),
        description: description.val(),
        category: parseInt(category.val()),
        amountRequired: parseFloat(amountrequired.val()),
        dueTo: dueto,
        photos: [(photos)],
        videos: [(videos)],

    };

    $.ajax({
        type: 'POST',
        url: '/project/create',
        contentType: 'application/json',
        data: JSON.stringify(data),
        dataType: 'json'
    }).done(_project => {
        createSuccessAlert.show().delay(2000);
        createSuccessAlert.fadeOut();
    }).fail(_failureResponse => {
        createFailedAlert.show().delay(2000);
        createFailedAlert.fadeOut();
    });
});

//-------Edit Project-------//
let projectEditSuccessAlert = $('.js-projectedit-success-alert');
projectEditSuccessAlert.hide();

let projectEditFailedAlert = $('.js-projectedit-fail-alert');
projectEditFailedAlert.hide();

let projectEditButton = $('.js-projectedit-submit-button');
projectEditButton.on('click', () => {
    projectEditSuccessAlert.hide();
    projectEditFailedAlert.hide();

    let projectid = $('#prid');
    let name = $('.js-projectedit-projectname');
    let description = $('.js-projectedit-description');
    let category = $('.js-projectedit-category');
    let amountrequired = $('.js-projectedit-amountrequired');
    let dueto = new Date($('#js-projectedit-dueto').val());
    let photos = $('#js-projectedit-photos').val();
    let videos = $('#js-projectedit-videos').val();

    debugger;

    let data = {
        projectId: projectid.val(),
        name: name.val(),
        description: description.val(),
        category: parseInt(category.val()),
        amountRequired: parseFloat(amountrequired.val()),
        dueTo: dueto,
        photos: [(photos)],
        videos: [(videos)]
    }
    debugger;
    $.ajax({
        type: 'PATCH',
        url: '/project/update',
        contentType: 'application/json',
        data: JSON.stringify(data),
        dataType: 'json'
    }).done(project => {
        projectEditSuccessAlert.show().delay(2000);
        projectEditSuccessAlert.fadeOut();
    }).fail(failureResponse => {
        projectEditFailedAlert.show().delay(2000);
        projectEditFailedAlert.fadeOut();

    })
});
debugger;
//**************************************************
//Disable Project JS
//**************************************************
let disableProjectButton = $('#jsDisableProject');
disableProjectButton.on('click', () => {

    let projectid = $('#prid');
    debugger;
    $.ajax({
        type: 'DELETE',
        url: '/project/delete?id=' +parseInt(projectid.val())
    }).done(project => {

       


        var url = "/User/MyProfile/" + parseInt(localStorage.getItem('userId'))
        window.location.href = url;

    }).fail(failureResponse => {

    })
    debugger;
})

//-------Back Project-------//
let backProjectButton = $('.js-back-project-button');
let backedSuccessAlert = $('.js-back-success-alert');
//backedSuccessAlert.hide();
let backedFailedAlert = $('.js-back-failed-alert');
//backedFailedAlert.hide();
successModal = $('#successModal');
failureModal = $('#failureModal');

backProjectButton.on('click', e => {
    console.log(e.target.id)

    if (!localStorage.getItem("userId")) {
        $('#myCreateLoginModal').modal('show');
    } else {
        let userid = localStorage.getItem('userId');
        let projectid = $('#js-back-projectId');
        let amount = $('.js-back-project-amount-' + parseInt(e.target.id));
        debugger
        let data = {
            userid: parseInt(userid),
            projectid: projectid.text(),
            amount: parseFloat(amount.text().slice(0, -2))
        }

        $.ajax({
            type: 'POST',
            url: '/backedProject/create',
            contentType: 'application/json',
            data: JSON.stringify(data),
            dataType: 'json'
        }).done(_project => {
            successModal.modal('show');
            successModal.data('hideInterval', setTimeout(function () {
                successModal.modal('hide');
            }, 3000));
            setTimeout(function () {
                location.reload();
            }, 3000);
        }).fail(_failureResponse => {
            failureModal.modal('show');
            failureModal.data('hideInterval', setTimeout(function () {
                failureModal.modal('hide');
            }, 3000));
        });
    }
});
////------- User Profile--------//

let aboutRow = $('#js-about-row');
aboutRow.hide();

let footer = $('#js-footer');
let header = $('#js-header');

let createdRow = $('#js-created-list-row');
createdRow.hide();

let backedRow = $('#js-backed-list-row');
backedRow.hide();

let aboutButton = $('#js-about-button');
aboutButton.on('click', () => {
    aboutRow.show();
    createdRow.hide();
    backedRow.hide();
    footer.show();

})

let backedListButton = $('#js-backedlist-button');
backedListButton.on('click', () => {

    backedRow.show();
    aboutRow.hide();
    createdRow.hide();
    footer.hide();
})

let createdListButton = $('#js-createdlist-button');
createdListButton.on('click', () => {

    createdRow.show();
    backedRow.hide();
    aboutRow.hide();
    footer.hide();
})

//----------------------------------//