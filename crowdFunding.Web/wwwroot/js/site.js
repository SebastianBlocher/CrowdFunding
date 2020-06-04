// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// ----- Create project----//
let createSuccesAlert = $('.js-create-success-alert');
createSuccesAlert.hide();

let createFailedAlert = $('.js-create-failed-alert');
createFailedAlert.hide();

let projectCreateButton = $('.js-projectcreate-submit-button');
projectCreateButton.on('click', () => {
    createSuccesAlert.hide();
    createFailedAlert.hide();
    debugger;
    let userid = localStorage.getItem("userid");
    let name = $('.js-projectcreate-projectname');
    let description = $('.js-projectcreate-description');
    let category = $('.js-project-create-category');
    let amountrequired = $('.js-projectcreate-amountrequired');

    debugger;


    let data = {
        userid: parseInt(userid),
        name: name.val(),
        description: description.val(),
        category: parseInt(category.val()),
        amountrequired: parseFloat(amountrequired.val())
    }
    debugger;
    $.ajax({
        type: 'POST',
        url: '/project/create',
        contentType: 'application/json',
        data: JSON.stringify(data),
        dataType: 'json'
    }).done(_project => {
        createSuccesAlert.show();
    }).fail(_failureResponse => {
        createFailedAlert.show();
    });
    debugger;

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

  
    let projectid = localStorage.getItem("projectid");
    let name = $('.js-projectedit-projectname');
    let description = $('.js-projectedit-description');
    let category = $('.js-projectedit-category');
    let amountrequired = $('.js-projectcreate-amountrequired');

    let data = {
        projectid: projectid.val(),
        name: name.val(),
        description: description.val(),
        category: parseInt(category.val()),
        amountrequired: parseFloat(amountrequired.val()),
    }

    $.ajax({
        type: 'PATCH',
        url: '/project/update',
        contentType: 'application/json',
        data: JSON.stringify(data),
        dataType: 'json'
    }).done(project => {
      projectEditSuccessAlert.show().delay(2000);
      }).fail(failureResponse => {
        projectEditFailedAlert.show();
        
    })
});



