// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

$('form input').keydown(function (e) {
    if (e.keyCode == 13) {
        var form = $(this).parents("form").eq(0);

        if (form.data('entersubmit') === "yes") {
            return true;
        }

        var inputs = form.find(":input");
        if (inputs[inputs.index(this) + 1] != null) {
            inputs[inputs.index(this) + 1].focus();
        }
        e.preventDefault();
        return false;
    }
});