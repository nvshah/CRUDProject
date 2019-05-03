
(function ($) {
    "use strict";

    
    /*==================================================================
    [ Validate ]*/
    var input = $('.validate-input .input100');

    //Added
    var oldName = $('#input_name').val();
    var oldStandard = $('#input_standard').val();
    var oldPhone = $('#input_phone').val();
    var oldDob = $('#input_dob').val();
    var oldMedium = $("#input_medium").val();
    //Over

    //set default standard validation message
    //$(input).parent().attr('data-validate', default_standard_validate);

    $('.validate-form').on('submit', function () {
        var check = true;

        for (var i = 0; i < input.length; i++) {
            if (validate(input[i]) == false) {
                showValidate(input[i]);
                check = false;  
            }
        }

        //added
        if (check == true)
        {
            InsertRecords();
        }
        //added over

        return check;
    });

    //Added
    $("#cancelBtn").click(function () {

        $('#input_name').val(oldName);
        $('#input_standard').val(oldStandard);
        $('#input_phone').val(oldPhone);
        $('#input_dob').val(oldDob);
        $('#input_medium').val(oldMedium);
        
    });

    $("#updateBtn").click(function () {

        var check = true;

        for (var i = 0; i < input.length; i++) {
            if (validate(input[i]) == false) {
                showValidate(input[i]);
                check = false;
            }
        }

        if (check == true) {
            UpdateRecords();
        }

        return check;

    });
    //Over

    $('.validate-form .input100').each(function () {
        $(this).focus(function () {
            hideValidate(this);
        });
    });

    function validate(input) {
        var input_name = $(input).attr('name');
        var input_value = $(input).val().trim();

        if (input_value == '')
        {
            
            if (input_name == 'standard') {
                $(input).parent().attr('data-validate', "Enter Standard (1 to 12)");
            }
            if (input_name == 'phone') {
                $(input).parent().attr('data-validate', "Please Enter your phone number (10 digit)");
            }
            return false;
        }
        else if ($(input).attr('type') == 'email' || $(input).attr('name') == 'email') {
            if ($(input).val().trim().match(/^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{1,5}|[0-9]{1,3})(\]?)$/) == null) {
                return false;
            }
        }
        else if(input_name == 'standard')
        {
            if (input_value.match(/^([2-9]|1[0-2]?)$/) == null)
            {
                $(input).parent().attr('data-validate', "Standard must be in (1 , 12)");
                return false;
            }
        }
        else{
            if (input_name == 'phone') {
                if (input_value.match(/^[2-9]{1}[0-9]{9}$/) == null) {
                    $(input).parent().attr('data-validate', "Invalid Phone Number");
                    return false;
                }
            }
        }

        return true;
       
        //else {
        //    if ($(input).val().trim() == '') {
        //        return false;
        //    }
        //}
    }

    function showValidate(input) {
        var thisAlert = $(input).parent();

        $(thisAlert).addClass('alert-validate');
    }

    function hideValidate(input) {
        var thisAlert = $(input).parent();

        $(thisAlert).removeClass('alert-validate');
    }

    //Added
    function InsertRecords()
    {
        
        var name = $("#input_name");
        var standard = $("#input_standard");
        var phone = $("#input_phone");
        var dob = $("#input_dob");
        var medium = $("#input_medium");

        var _student = {};
        _student.Name = name.val();
        _student.Standard = standard.val();
        _student.Phone = phone.val();
        _student.Dob = dob.val();
        _student.Medium = medium.val();

        // Call Web API to Insert a record of Student
        $.ajax({
            url: '/api/CRUD/Insert',
            type: 'POST',
            data: JSON.stringify(_student),
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            success: function (success) {
                if (success == true) {
                    alert("Record Inserted Successfully");

                    name.val("");
                    standard.val("");
                    phone.val("");
                    dob.val("");
                    medium.val("");
                }
            },
            error: function (request, message, error) {
                handleException(request, message, error);
            }
        });
        
    }

    function UpdateRecords() {

        var id = $("#input_id");
        var name = $("#input_name");
        var standard = $("#input_standard");
        var phone = $("#input_phone");
        var dob = $("#input_dob");
        var medium = $("#input_medium");

        var _student = {};
        _student.Id = id.val();
        _student.Name = name.val();
        _student.Standard = standard.val();
        _student.Phone = phone.val();
        _student.Dob = dob.val();
        _student.Medium = medium.val();

        // Call Web API to Update a record of Student
        $.ajax({
            url: '/api/CRUD/Update',
            type: 'POST',
            data: JSON.stringify(_student),
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            success: function (success) {
                if (success == true) {
                    alert("Record Updated Successfully");

                    name.val("");
                    standard.val("");
                    phone.val("");
                    dob.val("");
                    medium.val("");

                    //redirect to Index Page After Updation
                    window.location.href = "/";

                }
                else
                {
                    alert('Unable To Update It.');
                }
            },
            error: function (request, message, error) {
                handleException(request, message, error);
            }
        });

    }


    function showMessage()
    {
        alert('you fooooooooool');
    }

    //Handle Error and display if occurs via inserting
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
    

})(jQuery);