


$("#comment-form").submit(function (e) {
    $("#comment-form").validate();
    if ($("#comment-form").valid()) {
        e.preventDefault(); // avoid to execute the actual submit of the form.

        var form = $(this);
        var actionUrl = form.attr('action');
        $.ajax({
            type: "POST",
            url: "/Comment/Add",
            data: form.serialize(), // serializes the form's elements.
            success: function (data) {
                $(".card-body.p-4").append(data)
            }
        });
    }

});
