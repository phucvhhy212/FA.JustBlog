$(function () {

    $.validator.addMethod("onlyletters", function (value, element) {
        return this.optional(element) || /^[a-z\s]+$/i.test(value);
    }, "Only alphabetical characters");

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
                lettersonly: true
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
});