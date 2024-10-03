"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/ConnectedHub").build();

connection.on("ReceiveMessage", function (user, message) {
    var li = document.createElement("li");
    li.textContent = `${user} says: ${message}`;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var message = document.getElementById("messageInput").value;

    connection.invoke("SendMessage", message).catch(function (err) {
        return console.error(err.toString());
    });

    event.preventDefault();
});


connection.on("UpdateUsers", function (userList) {
    var usersListElement = document.getElementById("usersList");
    usersListElement.innerHTML = ""; 

    userList.forEach(function (user) {
        var li = document.createElement("li");
        li.textContent = user;
        usersListElement.appendChild(li);
    });
});
