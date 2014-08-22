﻿(function (koModels) {
	'use strict';

	koModels.HallServiceViewModel = function (name, rowAmount, columnAmount) {
		var self = this;

		self.Name = ko.observable(name);
		self.RowAmount = ko.observable(rowAmount);
		self.ColumnAmount = ko.observable(columnAmount);
		self.HallTemplate = ko.observableArray([]);
		self.Seats = ko.observableArray([]);
		self.IsProcessing = ko.observable(false);

		self.seatNumber = ko.computed(function () {
			var size = self.Seats().length;
			return size;
		});

		self.isHallConfigured = ko.computed(function () {
			var isHallConfigured = self.RowAmount() > 0 && self.ColumnAmount() > 0 && self.Name().length > 0;
			return isHallConfigured;
		});

		self.isSeatIncluded = function (seatTemplate) {
			var seatForPost = ko.utils.arrayFirst(self.Seats(), function (seat) {
				return (seatTemplate == seat);
			});
			if (seatForPost == null) {
				return false;
			}
			return true;
		};

		self.createHallTemplate = function () {
			self.IsProcessing(true);
			self.HallTemplate.removeAll();
			setTimeout(function () {
				for (var i = 1; i <= self.RowAmount(); i++) {
					for (var j = 1; j <= self.ColumnAmount(); j++) {
						self.HallTemplate.push(new koModels.Seat(i, j));
					}
				}
				self.addAllSeats();
				self.IsProcessing(false);
			}, 1000);

		};

		self.toogleSeatStatus = function (seatTemplate) {
			var seatForPost = ko.utils.arrayFirst(self.Seats(), function (seat) {
				return (seatTemplate == seat);
			});
			if (seatForPost != null) {
				self.Seats.remove(seatForPost);
			} else {
				self.Seats.push(seatTemplate);
			}
		};

		self.removeAllSeats = function ()
		{
			self.Seats([]);
		};

		self.addAllSeats = function () {
			self.Seats(self.HallTemplate.slice(0));
		};

		self.submit = function () {
			var koHallModel = {};
			koHallModel.Name = self.Name();
			koHallModel.RowAmount = self.RowAmount();
			koHallModel.ColumnAmount = self.ColumnAmount();
			koHallModel.CinemaId = $('#CinemaId').attr('data-id');
			koHallModel.Seats = self.Seats();
			var hallModel = ko.mapping.toJS(koHallModel);
			ko.utils.postJson(' ', { hallViewModel: hallModel });
		};
	};


	koModels.Seat = function (rowNumber, columnNumber) {
		var self = this;

		self.rowNumber = ko.observable(rowNumber);
		self.columnNumber = ko.observable(columnNumber);
	};

}(window.koModels = window.koModels || {}));