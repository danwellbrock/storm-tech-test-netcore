// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function (e) {

    // locate each gravatar partial section.
    // if it has a URL set, load profile contents into the area.
    $(".content-gravatar-partial").each(function (index, item) {
        var emailAddress = $(item).data("email");

        if (emailAddress && emailAddress.length > 0) {
            $(item).load("GravatarProfilePartial?email=" + emailAddress);
        }
    });
}); 
