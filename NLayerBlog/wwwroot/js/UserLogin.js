$(document).ready(() => {

    var getForm = $("#login-form")
    var loginButton = $("#login-button")

    loginButton.click((e) => {
        e.preventDefault()
        $.ajax({
            method: "POST",
            url: "/user/login",
            data: getForm.serialize(),
            dataType: "json",
            contentType: "application/x-www-form-urlencoded",
            success: function (response) {
                console.log(response)
            },
            error: function (err) {
                console.log(err)
            }
        })
    })

})