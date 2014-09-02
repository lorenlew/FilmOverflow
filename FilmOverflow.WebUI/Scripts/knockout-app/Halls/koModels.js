(function (koModels) {
	'use strict';

	koModels.HallServiceViewModel = function (name, rowAmount, columnAmount) {
		var self = this;

		var $error = $('#error');
		var initRow = null;
		var initCol = null;

		self.Name = ko.observable(name).extend({ required: { message: 'Please enter hall name' }, maxLength: 10 });
		self.RowAmount = ko.observable(rowAmount).extend({ min: 1, max: 10, required: { message: 'Please enter row number' } });
		self.ColumnAmount = ko.observable(columnAmount).extend({ min: 1, max: 20, required: { message: 'Please enter column number' } });
		self.HallTemplate = ko.observableArray([]);
		self.Seats = ko.observableArray([]);
		self.IsProcessing = ko.observable(false);

		self.Errors = ko.validation.group(self);

		self.seatNumber = ko.computed(function () {
			return self.Seats().length;
		});

		self.isHallConfigured = ko.computed(function () {
			return self.Errors().length == 0;
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
			$error.text('');
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
			initRow = self.RowAmount();
			initCol = self.ColumnAmount();
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

		self.removeAllSeats = function () {
			self.Seats([]);
		};

		self.addAllSeats = function () {
			self.Seats(self.HallTemplate.slice(0));
		};

		self.submit = function () {
			if (initRow !== self.RowAmount() || initCol !== self.ColumnAmount()) {
				$error.text('Regenerate hall before submit!');
				return;
			}
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