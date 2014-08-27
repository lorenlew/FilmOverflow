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

		$('#seanceStartDatePicker', this).on('dp.change', function (e) {
			//Uncaught TypeError: Cannot read property 'setMinDate' of null
			$('#seanceEndDatePicker', this).data('DateTimePicker').setMinDate(e.date);
		});
		$('#seanceEndDatePicker', this).on('dp.change', function (e) {
			//Uncaught TypeError: Cannot read property 'setMaxDate' of null 
			$('#seanceStartDatePicker', this).data('DateTimePicker').setMaxDate(e.date);
		});

		$('#seanceTimePicker', this).datetimepicker({
			format: 'HH:mm',
			pickDate: false
		});
	});

	$('#SeanceManagement').on('click', 'a[data-modal]', function () {
		$('#seanceModalContent').load(this.href, function () {
			$.validator.unobtrusive.parse(this);

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