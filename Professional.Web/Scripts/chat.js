// Source: http://www.asp.net/signalr/overview/getting-started/tutorial-getting-started-with-signalr-and-mvc
$(document).ready(function () {
    var messagesToTake = 5;
    var startTime;
    var firstUserId;

    function htmlEncode(value) {
        var encodedValue = $('<div />').text(value).html();
        return encodedValue;
    }

    function resetChat() {
        enableFurtherLoad();
        var requestUrl = "/UserArea/Profile/ResetMessageCount";
        $.ajax({
            url: requestUrl,
            context: document.body
        });
    }

    function prependMessage(name, message, userId) {
        var encodedName = htmlEncode(name);
        var encodedMessage = htmlEncode(message);

        var isMe = firstUserId === userId;
        var className = isMe ? "first-partner" : "second-partner";

        if (message.length > 0) {
            var li = $('<li>', { 'class': className }).html('<strong>' + encodedName +
                '</strong>: ' + encodedMessage + '</li>')
            $('#discussion').prepend(li);
        }
        $(".chat-content").scrollTop($(".chat-content")[0].scrollHeight);
    }

    function disableFurtherLoad() {
        var loadMoreLink = $('.load-messages');
        $(loadMoreLink).text('All messages loaded.');
        $(loadMoreLink).addClass('less-visible');
        $(loadMoreLink).on('click', function () {
            e.preventDefault();
        });
    }

    function enableFurtherLoad() {
        var loadMoreLink = $('.load-messages');
        $(loadMoreLink).text('Load older messages...');
        $(loadMoreLink).removeClass('less-visible');
        $(loadMoreLink).on('click', function () {});
    }

    // Active chat funcionality
    $(function () {
        var chat = $.connection.chatHub;

        chat.client.setSetting = function (time, id) {
            startTime = time;
            firstUserId = id;
        };

        chat.client.addNewMessageToPage = function (name, message, userId) {
            var encodedName = htmlEncode(name);
            var encodedMessage = htmlEncode(message);

            var isMe = firstUserId === userId;
            var className = isMe ? "first-partner" : "second-partner";

            if (message.length > 0) {
                var li = $('<li>', { 'class': className }).html('<strong>' + encodedName +
                    '</strong>: ' + encodedMessage + '</li>')
                $('#discussion').append(li);
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
                resetChat();
                // Show chat window
                $('#chat').removeClass('hide');
                $('.load-messages').addClass('hide');
                // Remove new message red dot notification
                $('.glyphicon-envelope').removeClass('active-updates');
                // Mark update as checked
                $(this).addClass('seen');
                // Clear chat window
                $('#discussion').text('');

                // Set chat partner
                currentChatPartner = $(this).attr('data-value');
                $('.chat-header').attr("data-id", currentChatPartner);
                $('.chat-header').text($(this).attr('data-discution-with'));

                // Disable load older button if no older messages exist
                var id = $('.chat-header').attr("data-id");
                var requestUrl = "/UserArea/Profile/LoadMoreMessages/";
                var postDate = { Id: id, Loaded: false, Time: startTime };

                $.ajax({
                    type: 'POST',
                    dataType: 'json',
                    url: requestUrl,
                    contentType: 'application/json',
                    data: JSON.stringify(postDate),
                    context: document.body
                }).done(function (res) {
                    if (res.length !== 0) {
                        $('.load-messages').removeClass('hide');
                    }
                    else {
                        disableFurtherLoad();
                    }
                });

                // Start conversation
                chat.server.startConversation(currentChatPartner);
            });
            $('#sendmessage').on('click', function () {
                chat.server.send(currentChatPartner, $('#message').val());
                $('#message').val('').focus();
            });
        });
    });
    // Active chat funcionality

    // Load older chat messages
    $(document).on("click", ".load-messages", function () {
        var id = $('.chat-header').attr("data-id");
        var requestUrl = "/UserArea/Profile/LoadMoreMessages/";
        var postDate = { Id: id, Loaded: true, Time: startTime };

        $.ajax({
            type: "POST",
            dataType: 'json',
            url: requestUrl,
            contentType: 'application/json',
            data: JSON.stringify(postDate),
            context: document.body
        }).done(function (res) {
            if (res.length < messagesToTake) {
                disableFurtherLoad();
            }
            for (var i = 0; i < res.length; i++) {
                prependMessage(res[i].Sender, res[i].Content, res[i].FirstUserId);
            }
            $(".chat-content").scrollTop(0);
        });
    });
    // Load older chat messages
});