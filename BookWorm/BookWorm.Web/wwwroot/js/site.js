// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function likeButton(likeBtnId, dislikeBtnId, likesId, reviewId) {
    var likeSelector = "#".concat(likesId);
    var likeBtnSelector = "#".concat(likeBtnId);
    var dislikeBtnSelector = "#".concat(dislikeBtnId);

    var isDisliked = false;
    if ($(dislikeBtnSelector).hasClass("disabled")) {
        isDisliked = true;
    }

    var token = $('input[name="__RequestVerificationToken"]').val();
    $.ajax({
        type: "Post",
        url: "https://localhost:7250/Review/Like",
        headers: {
            "RequestVerificationToken": token
        },
        data: {
            id: reviewId,
            isDisliked: isDisliked
        },
        success: function (data) {
            $(likeSelector).text(data.likesCount);
            $(likeBtnSelector).addClass("disabled");
            if (isDisliked) {
                $(dislikeBtnSelector).removeClass("disabled");
            }
        }
    });
}

function dislikeButton(likeBtnId, dislikeBtnId, likesId, reviewId) {
    var likeSelector = "#".concat(likesId);
    var likeBtnSelector = "#".concat(likeBtnId);
    var dislikeBtnSelector = "#".concat(dislikeBtnId);

    var isLiked = false;
    if ($(likeBtnSelector).hasClass("disabled")) {
        isLiked = true;
    }

    var token = $('input[name="__RequestVerificationToken"]').val();
    $.ajax({
        type: "Post",
        url: "https://localhost:7250/Review/Dislike",
        headers: {
            "RequestVerificationToken": token
        },
        data: {
            id: reviewId,
            isLiked: isLiked
        },
        success: function (data) {
            $(likeSelector).text(data.likesCount);
            $(dislikeBtnSelector).addClass("disabled");
            if (isLiked) {
                $(likeBtnSelector).removeClass("disabled");
            }
        }
    });
}