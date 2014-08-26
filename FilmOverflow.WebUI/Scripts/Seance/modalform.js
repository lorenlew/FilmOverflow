(function ($) {
	'use strict';

	$('#seanceModal').on('shown.bs.modal', function () {
		$('#seanceDatePicker', this).datetimepicker({
			format: 'MM/DD/YYYY',
			pickTime: false,
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