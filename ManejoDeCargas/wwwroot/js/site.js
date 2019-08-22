// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $('#success').css('visibility', 'hidden');
});

function cancel() {
    window.location.replace("index");
}

function changeState() {
    var data = { "pesadaID": $('#pesadaID').val(), "estado": $('#estado').val() }
    var url = "/Home/Store";
    var headers = GetToken();
    $.ajax({
        url: url,
        type: "POST",
        data: JSON.stringify(data),
        Accept: "application/json",
        contentType: "application/json",
        headers: headers,
        dataType: "JSON",
        async: 1,
        success: function (response) {

        },
        failure: function (response) {
            console.log("Failure" + response);
        },
        error: function (response) {
            console.log('Something went wrong', response);
        },
        complete: function (response) {
            console.log(response)
            $('#estado').attr("disabled", true);
            if (response.responseJSON == -1) {
                $("#success").css("visibility", "visible").delay(1000);
            } else {
                $("#success").text("Ocurrió un problema").css({ "color": "red", "visibility": "visible" }).delay(1000);
            }
            setTimeout(function () {
                cancel();
            }, 1010);
        }
    });
    console.log(data);
}

function GetToken() {
    var token = $('input[name="__RequestVerificationToken"]').val();
    var headers = {};
    headers["MY-XSRF-TOKEN"] = token;
    return headers;
}