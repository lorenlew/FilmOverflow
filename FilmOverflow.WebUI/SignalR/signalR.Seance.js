/// <reference path="../Scripts/jquery-1.10.2.js" />
/// <reference path="../Scripts/jquery.signalR-2.1.1.js" />
$(function () {
	'use strict';

	$.connection.hub.logging = true;
	$(window).unload(function () {
		$.connection.hub.stop();
	});
	var seanceService = $.connection.seanceService;
	var reservedSeats;
	var seanceId;

	function init() {
		seanceId = $('#SeanceId').attr('data-id');
		return seanceService.server.getReservedSeatsForSeance(seanceId).done(function (seats) {
			$('#test').text(seats);
			var test;
		});
	}

	$.connection.hub.start()
		.then(init);
});