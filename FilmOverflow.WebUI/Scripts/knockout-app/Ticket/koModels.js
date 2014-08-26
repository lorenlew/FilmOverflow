﻿(function (koModels) {
	'use strict';

	koModels.HallViewModel = function () {
		var self = this;
		var seanceId = $('#SeanceId').attr('data-id');
		var urlGetSeats = '/Seance/GetSeanceSeats?seanceId=' + seanceId;
		var urlReserveSeat = '/Seance/ToogleReservationStatus?seanceId=' + seanceId;
		var seanceService = $.connection.seanceService;
		self.Seats = ko.observableArray([]);
		self.ReservedSeats = ko.observableArray([]);
		self.SelectedSeats = ko.observableArray([]);

		self.PaymentMethodsVisable = ko.computed(function () {
			return self.SelectedSeats().length > 0;
		});

		self.NumberOfSelectedSeats = ko.computed(function ()
		{
			return self.SelectedSeats().length;
		});

		$.connection.hub.logging = true;
		$.connection.hub.start()
			.then(init);

		function init() {
			$.getJSON(urlGetSeats, function (data) {
				ko.mapping.fromJS(data, {}, self.Seats);
			});
			seanceService.server.getReservedSeatsForSeance(seanceId, true);
		}

		seanceService.client.notify = function (data) {
			ko.mapping.fromJSON(data, {}, self.ReservedSeats);
		};

		self.isSeatFree = function (seat) {
			var isSeatReserved = self.isSeatReserved(seat);
			return !isSeatReserved;
		};

		self.isSeatReserved = function (seat) {
			var reservedSeanceSeat = ko.utils.arrayFirst(self.ReservedSeats(), function (reservedSeat) {
				return (reservedSeat.RowNumber() === seat.RowNumber() &&
					reservedSeat.ColumnNumber() === seat.ColumnNumber());
			});
			if (reservedSeanceSeat == null) {
				return false;
			}
			return true;
		};

		self.toogleSeatStatus = function (seat) {
			if (self.isSeatReserved(seat)) {
				return true;
			}
			var seatModel = ko.mapping.toJSON(seat);
			if (self.isSeatSelected(seat)) {
				self.SelectedSeats.remove(seat);
			} else {
				self.SelectedSeats.push(seat);
			}
			$.ajax({
				type: 'POST',
				url: urlReserveSeat,
				data: { seatViewModel: seatModel },
				success: function () {
					seanceService.server.getReservedSeatsForSeance(seanceId, false);
				}
		});

			return true;
		};

		self.isSeatSelected = function (selectedSeat) {
			var seats = ko.utils.arrayFirst(self.SelectedSeats(), function (seat) {
				return (selectedSeat == seat);
			});
			if (seats == null) {
				return false;
			}
			return true;
		};

		self.submit = function () {
			//post
		};

	};

}(window.koModels = window.koModels || {}));