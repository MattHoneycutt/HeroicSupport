/// <reference path="../../jasmine.js" />
/// <reference path="../../jquery-1.9.1.js" />
/// <reference path="../../knockout-2.2.1.js" />
/// <reference path="../../app/Tickets/TicketModel.js" />
describe("TicketModel", function () {
	var model,
	    serviceMock,
	    pageMock;

	beforeEach(function () {
		serviceMock = jasmine.createSpyObj("service", ["save"]);
		pageMock = jasmine.createSpyObj("page", ["success", "error"]);
		model = new TicketModel("some-id", serviceMock, pageMock);
	});

	describe("when saving tags successfully", function () {
		beforeEach(function () {
			serviceMock.save.andCallFake(function () {
				var d = $.Deferred();
				d.resolve();
				return d.promise();
			});

			model.tags("abc");

			model.saveTags();
		});
		
		it("saves the tags to the server", function () {
			expect(serviceMock.save).toHaveBeenCalledWith("some-id", "abc");
		});

		it("displays a success message", function () {
			expect(pageMock.success).toHaveBeenCalled();
		});
	});

	describe("when saving tags fails", function () {
		beforeEach(function () {
			serviceMock.save.andCallFake(function () {
				var d = $.Deferred();
				d.reject();
				return d.promise();
			});

			model.tags("abc");

			model.saveTags();
		});
		
		it("tries to save the tags to the server", function () {
			expect(serviceMock.save).toHaveBeenCalledWith("some-id", "abc");
		});

		it("displays an error message", function () {
			expect(pageMock.error).toHaveBeenCalled();
		});
	});
});