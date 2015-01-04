// Source: http://www.asp.net/signalr/overview/getting-started/tutorial-getting-started-with-signalr-and-mvc

$(function () {
    var chat = $.connection.chatHub;

    chat.client.addNewMessageToPage = function (name, message) {
        var encodedName = htmlEncode(name); 
        var encodedMessage = htmlEncode(message);
        if (message.length > 0) {
            $('#discussion').append('<li><strong>' + encodedName +
                '</strong>: ' + encodedMessage + '</li>');
        }
        $(".chat-content").scrollTop($(".chat-content")[0].scrollHeight);
    };

    $('.chat-close-btn').on("click", function () {
        $('#chat').addClass('hide');
    });
 
    $('#message').focus();

    var currentChatPartner;
    $.connection.hub.start().done(function () {
        $('.glif-updates.chat').on('click', '.chat-open', function () {
            // Show chat window
            $('#chat').removeClass('hide');
            // Mark update as checked
            $(this).addClass('seen');
            // Clear chat window
            $('#discussion').text('');

            // Set chat partner
            currentChatPartner = $(this).attr('data-value');
            $('.chat-header').text($(this).attr('data-discution-with'));
            // Start conversation
            chat.server.startConversation(currentChatPartner);
        });
        $('#sendmessage').on('click', function () {
            chat.server.send(currentChatPartner, $('#message').val());
            $('#message').val('').focus();
        });
    });
});

function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}