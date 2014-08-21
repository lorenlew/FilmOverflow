(function ($) {
	'use strict';

	$('#SeanceManagement').on('click', 'a[data-modal]', function (e) {
		$('#seanceModalContent').load(this.href, function () {
			$('#seanceModal').modal({
				keyboard: true
			}, 'show');
			bindForm(this);
		});
		return false;
	});

	function bindForm(dialog) {
		$(dialog).on('submit', 'form', function (e) {
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
						bindForm();
					}
				}
			});
		});
	}

})(jQuery);