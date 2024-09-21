"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/NotificationHub").build();

connection.on
(
	"SendToPOSClerck",
	(MessageSubject, MessageContent) =>
	{
		var element = document.getElementById("notificationContainer");
		console.log(element)
		element.style.backgroundColor = "red";
		console.log("done")
	}
);

connection.start().catch
(
	function (err)
	{
		return console.error(err.toString());
	}
);
