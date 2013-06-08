var TicketModel = function (ticketID, dataService, page) {
	var me = this;

	me.tags = ko.observable("");

	me.saveTags = function () {
		dataService.save(ticketID, me.tags())
			.done(function () {
				page.success("Tags saved!");
			})
			.fail(function () {
				page.error("There was a problem saving the new tags.");
			});
	};
};
