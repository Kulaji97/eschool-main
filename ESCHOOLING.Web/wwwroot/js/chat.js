"use strict";

function GetChatHistory() {

    const chatHistory = {
        users: [],
        history: []
    }
    var jqxhr = $.get("/api/chat/GetChatHistory/" + window.UserId)
        .done(function (data, textStatus, jqXHR) {


            chatHistory.history = data.chatMessages;
            chatHistory.users = data.chatUsers;
            LoadChatHistory(chatHistory);

        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            console.log(textStatus);
            console.log(errorThrown);
            alert("error");
        });
    // .always(function () {
    //     alert("finished");
    // });

    // Perform other work here ...

    // Set another completion function for the request above
    // jqxhr.always(function () {
    //     alert("second finished");
    // });
    return chatHistory;
}

function GetLastMessageFirstPart(history, userId) {

    const messagesFromUser = history.filter(function (u) { return u.fromUserId == userId; });
    if (messagesFromUser.length > 0) {
        const lastMsg = messagesFromUser.findLast(h => {
            return h.fromUserId == userId;
        });
        return lastMsg.messageText.substr(0, 50);
    }
    else
        return '';
}

function SortByTimeSent(a, b) {
    var timeA = a.timeSent;
    var timeB = b.timeSent;
    return ((timeA < timeB) ? -1 : ((timeA > timeB) ? 1 : 0));
}


function LoadChatHistory(chatHistory) {
    $('#ContactsList').empty();
    window.chatHistory = chatHistory;
    const you = window.chatHistory.users.find(function (u) { return u.isYou == true; });
    window.you = you;
    $('#profile .wrap').data('userId', you.userId);
    // $('#profile .wrap img').attr('src', you.ImageUrl);
    //$('#profile .wrap p').text(you.UserName);



    const chatUsers = window.chatHistory.users.filter(function (u) { return u.isYou != true; });
    $.each(chatUsers, function (index, user) {
        var onlineStatus = user.isOnline ? 'online' :'offline'
        var li = '<li id="user' + user.userId + '" class="contact" onclick="setActiveChat(' + user.userId +')" data-userId="' + user.userId + '">' +
            '<div class="wrap" >' +
            '<span class="contact-status ' + onlineStatus +'"></span>' +
            '<img src="' + user.imageUrl + '" alt="" />' +
            '<div class="meta">' +
            '<p class="name">' + user.userName + '</p>' +
            '<p class="preview">' + GetLastMessageFirstPart(chatHistory.history, user.userId) + '</p>' +
            '</div>' +
            '</div >' +
            '</li>';
        $('#ContactsList').append(li);
        SetMessagesForUser(user.userId);

    });
    let lastActiveUserId = 0;
    const lastMsgIndex = window.chatHistory.history.length - 1;
    const lastMsg = window.chatHistory.history.sort((a, b) => a.Id - b.Id)[lastMsgIndex];
    if (lastMsg != null) {
        lastActiveUserId = lastMsg.fromUserId == window.you.userId ? lastMsg.toUserId : lastMsg.fromUserId;

    }
    else {
        lastActiveUserId = chatUsers[chatUsers.length - 1].userId;
    }

    setActiveChat(lastActiveUserId);
}

function setActiveChat(userId) {
    $('#contacts ul li').removeClass('active');
    $('#' + userId).addClass('active');
    const user = window.chatHistory.users.find(u => u.userId == userId);
    $('.contact-profile img').attr('src', user.imageUrl);
    $('.contact-profile p').text(user.userName);
    window.ActiveChatUserId = userId;
    SetMessagesForUser(userId);
    $(".messages").animate({ scrollTop: $(document).height() }, "fast");
}

function SetMessagesForUser(userId) {
    const messageList = $('.messages ul');
    messageList.empty();
    const you = window.chatHistory.users.find(u => u.isYou == true);
    const other = window.chatHistory.users.find(u => u.userId == userId);
    const messages = window.chatHistory.history.filter(m => m.fromUserId == userId || m.toUserId == userId);
    const sortedMessages = messages;//.sort(SortByTimeSent);
    //console.log(sortedMessages);

    $.each(sortedMessages, function (index, msg) {

        if (msg.fromUserId == you.userId) {
            const li = '<li class="sent">' +
                '<img src = "' + you.imageUrl + '" alt = "" />' +
                '<p>' + msg.messageText + '</p>' +
                '</li >';

            messageList.append(li);
        }
        else {
            const li = '<li class="replies">' +
                '<img src = "' + other.imageUrl + '" alt = "" />' +
                '<p>' + msg.messageText + '</p>' +
                '</li > ';
            messageList.append(li);
        }

    });

}

function SetChatUserOnline(userId) {
    $('#ContactsList li #user' + userId +' span .contact-status').removeClass('offline').addClass('online');
    const objIndex = window.chatHistory.users.findIndex(u => u.userId == userId);
    if (objIndex !== undefined)
    window.chatHistory.users[objIndex].isOnline = true;
}

function newMessage() {
    const message = $("#txtMessageInput").val();
    if ($.trim(message) == '') {
        return false;
    }
    SendMessage(message); 
    $('<li class="sent"><img src="' + window.ImageUrl + '" alt="" /><p>' + message + '</p></li>').appendTo($('.messages ul'));
    $('.message-input input').val(null);
    $('.contact.active .preview').html('<span>You: </span>' + message);
    $("#ulMessages").animate({ scrollTop: $(document).height() }, "fast");
    $(".messages").animate({ scrollTop: $(document).height() }, "fast");
};

/////////////////////// SignalR chat ///////////////////////////////////////

window.connection = {};
function StartConnection() {
    window.connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable the send button until connection is established.


    window.connection.start().then(function () {
        $(".submit").prop("disabled",false);
        BindFunctionsToEvents(); 
    }).catch(function (err) {
        return console.error(err.toString());
    });
}

function BindFunctionsToEvents() {
    window.connection.on("ReceiveMessage",ReceiveMessage);
    connection.on("UserConnected", UserConnected);
    $(window).on("unload", Disconnect);
}

function Disconnect() {
    window.connection.hub.stop().done(function () {
        alert('stopped');
    });
}
function SendMessage(message) {
    window.connection.invoke("SendMessage",window.UserId, window.ActiveChatUserId, message).catch(function (err) {
        return console.error(err.toString());
    });
}
function ReceiveMessage(msg) {
    {
        console.log(msg);
        var chatMsg = new ChatMessage(msg.id, msg.fromUserId, msg.toUserId, msg.message, msg.timeSent, msg.hasRead)
        const other = window.chatHistory.users.find(u => u.userId == msg.fromUserId);
        window.chatHistory.history.push(chatMsg)
        const li = '<li class="replies">' +
            '<img src = "' + other.imageUrl + '" alt = "" />' +
            '<p>' + msg.messageText + '</p>' +
            '</li > ';
        $('.messages ul').append(li);
        $("#ulMessages").animate({ scrollTop: li.position().top }, "fast");
        console.log(message);
    }
}



function UserConnected(userId) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.textContent = `${user} says ${message}`;
}




