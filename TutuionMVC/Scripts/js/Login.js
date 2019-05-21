$('.message a').click(function(){
   $('form').animate({height: "toggle", opacity: "toggle"}, "slow");
});

$('.login-form').on('submit', function () {

    var inputs = $(this).find('input');
    var check = false;

    var userCredential = {};
    inputs.each(function () {
        userCredential[this.name] = $(this).val()
        //alert(userCredential[this.name]);
    });
    //userCredential.name = "";
    
    $.ajax({
        url: '/api/CRUD/AuthenticateUser',
        type: 'POST',
        data: JSON.stringify(userCredential),
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        //200 Ok Success 
        success: function (data) {
            if (data == true) {

                //$('.login-form .errorMessage').css("display", "none");
                window.location.href = "/Home/Display?page=Option.html";

            }
            else {
                $('.login-form .errorMessage').css("display", "block");
            }
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });

});

$('.register-form').on('submit', function () {

    var check = true;
    
    if (check == true) {
        InsertRecords();
    }   
});


$('.login-form .message a').on('click', function () {
    //alert('Hiofdsjoif');
    $('.login-form').css("display", "none");
    $('.register-form').css("display", "block");
});

$('.register-from .message a').on('click', function () {
    $('.register-form').css("display", "none");
    $('.login-form').css("display", "block");
});

function InsertRecords() {

    var name = $("#name");
    var password = $("#password");
    var email = $("#email");


    var _user = {};
    _user.name = name.val();
    _user.password = password.val();
    _user.email = email.val();
    

    // Call Web API to Insert a record of Student
    $.ajax({
        url: '/api/CRUD/InsertUser',
        type: 'POST',
        data: JSON.stringify(_user),
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (success) {
            if (success.status == true) {
                alert("Registered");

                window.location.reload();
            }
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });

}

//Error in WebApi Call Handle 
function handleException(request, message,
    error) {
    var msg = "";
    msg += "Code: " + request.status + "\n";
    msg += "Text: " + request.statusText + "\n";
    if (request.responseJSON != null) {
        msg += "Message" +
            request.responseJSON.Message + "\n";
    }
    alert(msg);
}



