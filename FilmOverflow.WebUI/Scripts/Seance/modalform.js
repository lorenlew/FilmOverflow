(function ($) {
	'use strict';

	$('#seanceModal').on('shown.bs.modal', function () {
		$('#seanceStartDatePicker', this).datetimepicker({
			format: 'DD/MM/YYYY',
			pickTime: false,
		});
		$('#seanceEndDatePicker', this).datetimepicker({
			format: 'DD/MM/YYYY',
			pickTime: false,
		});

		$('#IsMultipleDateSelect', this).on('change', function (e) {
			if (e.target.checked) {
				var startDate = $('#seanceStartDatePicker').data('DateTimePicker').getDate();
				$('#seanceEndDatePicker').data('DateTimePicker').setDate(startDate);

				var endDate = $('#seanceEndDatePicker').data('DateTimePicker').getDate();
				$('#seanceStartDatePicker').data('DateTimePicker').setMaxDate(endDate);

				$('#seanceEndDatePicker').on('dp.change', function (ev) {
					$('#seanceStartDatePicker').data('DateTimePicker').setMaxDate(ev.date);
				});
			}
			else {
				$('#seanceStartDatePicker').data('DateTimePicker').setMaxDate(moment().add(100, 'y'));
				$('#seanceEndDatePicker').off('dp.change');
			}
		});
		$('#seanceStartDatePicker', this).on('dp.change', function (e) {
			$('#seanceEndDatePicker').data('DateTimePicker').setMinDate(e.date);
		});
		$('#seanceEndDatePicker', this).on('dp.change', function (e) {
			$('#seanceStartDatePicker').data('DateTimePicker').setMaxDate(e.date);
		});

		$('#seanceTimePicker', this).datetimepicker({
			format: 'HH:mm',
			pickDate: false
		});
	});

	$('#SeanceManagement').on('click', 'a[data-modal]', function () {
		$('#seanceModalContent').load(this.href, function () {
			$.validator.unobtrusive.parse(this);

			var element = $('#seanceModalContent')[0];
			ko.cleanNode(element);
			ko.applyBindings(new koModels.CreateSeanceViewModel(), document.getElementById('seanceModalContent'));

			$('#seanceModal').modal({
				keyboard: true
			}, 'show');
		});

		return false;
	});

	$('#seanceModalContent').on('submit', 'form', function (e) {
		e.preventDefault();
		var data = $(this).serialize();

		$.ajax({
			url: this.action,
			type: this.method,
			data: data,
			success: function (result) {
				if (result.success) {
					$('#seanceModal').modal('hide');
					$(result.replaceTarget).load(result.url);   // Load data from the server and place the returned HTML into the matched element
				} else {
					$('#seanceModalContent').html(result);
				}
			}
		});
	});
})(jQuery);