// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function likeButton(likeBtnId, dislikeBtnId,likesId) {
    var likeSelector = "#".concat(likesId);
    var likeBtnSelector = "#".concat(likeBtnId);
    var dislikeBtnSelector = "#".concat(dislikeBtnId);

    //if ($(dislikeBtnSelector).hasClass("disabled")) {

    //}

    $.ajax({
        type: "Post",
        url: "https://localhost:7250/Review/Like",
        headers:
        {
            "RequestVerificationToken": /* the request verification anti forgery token value should be here! */
        },
        success: function (data) {
            console.log(data)
        }
        });

    $(likeSelector).text("Works");
}