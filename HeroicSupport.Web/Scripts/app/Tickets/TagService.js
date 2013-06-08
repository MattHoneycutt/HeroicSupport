var TagService = function (serverMethodUrl) {
	var me = this;

	me.save = function (ticketID, tags) {
		return $.post(serverMethodUrl, { ticketID: ticketID, tags: tags });
	};
};
