﻿/// <reference path="../Scripts/jquery-1.10.2.js" />
/// <reference path="../Scripts/jquery.signalR-2.1.1.js" />
//$(function () {
//	'use strict';

//	$.connection.hub.logging = true;
//	$(window).unload(function () {
//		$.connection.hub.stop();
//	});
//	var seanceService = $.connection.seanceService;
//	var seanceId = $('#SeanceId').attr('data-id');

//	function init() {
//		seanceService.server.getReservedSeatsForSeance(seanceId);
//	}

//	seanceService.client.notify = function (data) {
//		$('#test').text(data);
//	};
//	$.connection.hub.start()
//		.then(init);
//});