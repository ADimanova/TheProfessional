// Source: http://www.asp.net/signalr/overview/getting-started/tutorial-getting-started-with-signalr-and-mvc

$(function () {
    // Reference the auto-generated proxy for the hub.  
    var chat = $.connection.chatHub;

    // Create a function that the hub can call back to display messages.
    chat.client.addNewMessageToPage = function (name, message) {
        // Add the message to the page. 
        if (message.length > 0) {
            $('#discussion').append(
            '<li><strong>' + htmlEncode(name)
            + '</strong>: ' + htmlEncode(message) + '</li>');
        }
    };
    chat.client.log = function (message) {
        console.log(message);
    };
    // Get the user name and store it to prepend to messages.
    // Set initial focus to message input box.  
    $('#message').focus();
    var currentChatPartner;
    // Start the connection.
    $.connection.hub.start().done(function () {
        $('.chat-open').click(function () {
            $('#chat').toggleClass('hide');
            //var messages = $(this).attr('data-messages')
            //for (var message in messages) {
            //    addNewMessageToPage('Other', messages[message]);
            //}
            currentChatPartner = $(this).attr('data-value');
            $(this).addClass('seen');
            $('#chat #toId').attr('value', currentChatPartner);
            $('#discussion').text('');
            $('.chat-header').text($(this).attr('data-discution-with'));
            chat.server.startConversation(currentChatPartner);
        });
        $('#sendmessage').click(function () {
            // Call the Send method on the hub. 
            chat.server.send(currentChatPartner, $('#message').val());
            // Clear text box and reset focus for next comment. 
            $('#message').val('').focus();
        });
    });
});

// This function html-encodes messages for display in the page.
function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}

// For debug purposes 
//function addNewMessageToPage(name, message) {
//    // Add the message to the page. 
//    if (message.length > 0) {
//        $('#discussion').append(
//        '<li><strong>' + htmlEncode(name)
//        + '</strong>: ' + htmlEncode(message) + '</li>');
//    }
//};