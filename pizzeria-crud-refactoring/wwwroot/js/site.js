﻿const deleteBtn = document.querySelectorAll(".deleteBtn");
const modal = document.getElementById('exampleModalCenter');
const confermaEliminaBtn = document.getElementById('confermaEliminaBtn');
const close = document.getElementById('close');
const ics = document.getElementById('ics');
const modalInstance = new bootstrap.Modal(modal);
const confirmationMessage = document.getElementById('confirmationMessage');


document.addEventListener("DOMContentLoaded", function () {
    const messageElement = document.getElementById("message");
    setTimeout(function () {
        messageElement.style.opacity = "0";
    }, 1000); 
});

deleteBtn.forEach(function (btn) {
    btn.addEventListener('click', function (event) {
        event.preventDefault();
        modalInstance.show();
    });
});

ics.addEventListener('click', function () {
    modalInstance.hide();
});

close.addEventListener('click', function () {
    modalInstance.hide();
});

window.addEventListener('click', function (event) {
        modalInstance.hide();
    
});


confermaEliminaBtn.addEventListener('click', function () {
    event.preventDefault();
    const form = document.querySelector('form');
    form.submit();

});

