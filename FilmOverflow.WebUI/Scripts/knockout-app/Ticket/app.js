(function () {
	ko.applyBindings(new koModels.HallViewModel());
	ko.validation.rules.pattern.message = 'Invalid.';
	ko.validation.configure({
		registerExtenders: true,
		messagesOnModified: true,
		insertMessages: true,
		parseInputAttributes: true,
		messageTemplate: null
	});
})();