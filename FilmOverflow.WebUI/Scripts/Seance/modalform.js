(function ($) {
	'use strict';

	$('#seanceModal').on('shown.bs.modal', function () {
		var $StartDatePicker = $('#seanceStartDatePicker', this);
		var $EndDatePicker = $('#seanceEndDatePicker', this);

		$StartDatePicker.datetimepicker({
			format: 'DD/MM/YYYY',
			pickTime: false,
			useCurrent: false
		});
		$EndDatePicker.datetimepicker({
			format: 'DD/MM/YYYY',
			pickTime: false,			
		});

		$('#IsMultipleDateSelect', this).on('change', function (e) {
			if (e.target.checked) {
				var startDate = $StartDatePicker.data('DateTimePicker').getDate();
				$EndDatePicker.data('DateTimePicker').setDate(startDate);

				var endDate = $EndDatePicker.data('DateTimePicker').getDate();
				$StartDatePicker.data('DateTimePicker').setMaxDate(endDate);

				$EndDatePicker.on('dp.change', function (ev) {
					$StartDatePicker.data('DateTimePicker').setMaxDate(ev.date);
				});
			}
			else {
				$StartDatePicker.data('DateTimePicker').setMaxDate(moment().add(100, 'y'));
				$EndDatePicker.off('dp.change');
			}
		});

		$StartDatePicker.on('dp.change', function (e) {
			$EndDatePicker.data('DateTimePicker').setMinDate(e.date);
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