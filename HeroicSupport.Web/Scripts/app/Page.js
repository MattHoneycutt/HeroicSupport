var Page = function () {
	var me = this;

	me.success = function (message) {
		var content = $("<div>").addClass("alert alert-block alert-success")
			.text(message).append('<a class="close" data-dismiss="alert" href="#">x</a>');
		$("#main-body").prepend(content);
	};

	me.error = function (message) {
		var content = $("<div>").addClass("alert alert-block alert-error")
			.text(message).append('<a class="close" data-dismiss="alert" href="#">x</a>');
		$("#main-body").prepend(content);
	};
};
