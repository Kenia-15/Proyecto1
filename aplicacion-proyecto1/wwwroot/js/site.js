window.setTimeout(function () {
    $(".alert").fadeTo(500, 0).slideUp(500, function () {
        $(this).remove();
    });
}, 3000);


/*
if (history.forward(1))
    location.replace(history.forward(1));
*/