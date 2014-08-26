(function () {
	ko.applyBindings(new koModels.HallServiceViewModel('Hall', 5, 20));
	ko.validation.rules.pattern.message = 'Invalid.';
	ko.validation.configure({
		registerExtenders: true,
		messagesOnModified: true,
		insertMessages: true,
		parseInputAttributes: true,
		messageTemplate: null
	});
})();