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
    };

    $('.chat-close-btn').click(function () {
        $('#chat').addClass('hide');
    });
 
    $('#message').focus();

    var currentChatPartner;
    $.connection.hub.start().done(function () {
        $('.chat-open').click(function () {
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
        $('#sendmessage').click(function () {
            chat.server.send(currentChatPartner, $('#message').val());
            $('#message').val('').focus();
        });
    });
});

function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}