(function ($) {
	'use strict';

	$('li:first', '#DatePagination').addClass('active');

	$('#DatePagination').on('click', 'li', function () {
		$('li', '#DatePagination').removeClass('active');
		$(this).addClass('active');
	});
})(jQuery);