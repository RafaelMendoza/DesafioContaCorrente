// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    var now = new Date();

    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);

    var today = now.getFullYear() + "-" + (month) + "-" + (day);
    var firstDayOfTheMonth = now.getFullYear() + "-" + (month) + "-" + "01";

    $('#data-inicial-ipt').val(firstDayOfTheMonth);
    $('#data-final-ipt').val(today);

});

function loading() {
    $("body").loading('toggle');
}

function extratoCompleted() {
    $("body").loading('toggle');
}

function depositoCompleted()
{
    $("body").loading('toggle');
    $("#deposito-input").val('');
}

function saqueCompleted()
{
    $("body").loading('toggle');
    $("#saque-input").val('');
}


function saqueSuccess()
{
    $("#alert-div").html('<div class="alert alert-success" role="alert"> Saque efetuado com sucesso! </div>');
}

function saqueError(err)
{
console.log(err)

    var erro = err.responseText;
    $("#alert-div").html('<div class="alert alert-danger" role="alert"> ' + erro + '</div>');
}

function depositoSuccess()
{
    $("#alert-div").html('<div class="alert alert-success" role="alert"> Depósito efetuado com sucesso! </div>');
}

function depositoError()
{
    $("#alert-div").html('<div class="alert alert-danger" role="alert"> Houve um erro ao efetuar o Depósito! Caso o problema persista, contate um administrador. </div>');
}