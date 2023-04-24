$(function () {

    $.validator.addMethod("onlyletters", function (value, element) {
        return this.optional(element) || /^[a-z\s]+$/i.test(value);
    }, "Only alphabetical characters");
    $.validator.addMethod("strongPassword", function (value, element) {
        return this.optional(element) || /^(?=.*[!@#$%^&*(),.?":{}|<>])(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}$/.test(value);
    }, "Your password must contain at least one special character, one number, one lowercase letter, and one uppercase letter.");
    $.validator.addMethod("validphone", function (value, element) {
        return this.optional(element) || /^[\(\)\.\- ]{0,}[0-9]{3}[\(\)\.\- ]{0,}[0-9]{3}[\(\)\.\- ]{0,}[0-9]{4}[\(\)\.\- ]{0,}$/.test(value);
    },"Invalid phone number!")
    $.validator.setDefaults({
        errorClass: 'text-danger',
        highlight: function (element) {
            $(element)
                .closest('.form-control')
                .addClass('is-invalid')
        },
        unhighlight: function (element) {
            $(element)
                .closest('.form-control')
                .removeClass('is-invalid')
        },
        errorPlacement: function (error, element) {
            if (element.prop('type') === 'checkbox') {
                error.insertAfter(element.parent());
            }
            else {
                error.insertAfter(element);
            }
        }
    });
    $("#edit-form").validate({
        rules: {
            email: {
                required: true,
                email: true
            },
            title: {
                required: true,
         
            },
            url: {
                required: true
            },
            name: {
                required: true,
                onlyletters: true
            },
            postedDate: "required",
            count: {
                min: 0
            },
            description: "required",
            content: "required",
            ratecount: {
                min: 0
            },
            viewcount: {
                min: 0
            },
            totalrate: {
                min: 0
            }
        },
        messages: {
            email: {
                required: 'Please enter an email address.',
                email: 'Please enter a <em>valid</em> email address.'
            },


        }
    });

    $("#add-form").validate({
        rules: {
            email: {
                required: true,
                email: true
            },
            title: {
                required: true,
                lettersonly: true
            },
            url: {
                required: true
            },
            postedDate: "required",
            name: {
                required: true,
                lettersonly: true
            },
            count: {
                min: 0,
                required: true
            },
            description: "required",
            content: "required",
            ratecount: {
                min: 0,
                required: true
            },
            viewcount: {
                min: 0,
                required: true
            },
            totalrate: {
                min: 0,
                required: true
            }
        },
        messages: {
            email: {
                required: 'Please enter an email address',
                email: 'Please enter a <em>valid</em> email address'
            },


        }
    });


    $("#comment-form").validate({
        rules: {
            email: {
                required: true,
                email: true
            },
            title: {
                required: true,
            },
            name: {
                required: true,
                lettersonly: true
            },
            comment:{
                required:true
            }
        },
        messages: {
            email: {
                required: 'Please enter an email address',
                email: 'Please enter a <em>valid</em> email address'
            },


        }
    });

    $('#add-user-form').validate({
        rules: {
            username: {
                required: true,
            },
            email: {
                required: true,
                email: true
            },
            password: {
                required: true,
                minlength: 6,
                strongPassword: true
            }
        }
    });

    $('#register-form').validate({
        rules: {
            username: {
                required: true,
            },
            email: {
                required: true,
                email: true
            },
            password: {
                required: true,
                minlength: 6,
                strongPassword: true
            },
            phone: {
                validphone: true
            },
            repassword: {
                required: true,
                equalTo: "#password"
            }
        }
    })
});