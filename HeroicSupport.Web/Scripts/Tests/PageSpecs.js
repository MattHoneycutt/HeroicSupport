/// <reference path="../jquery-1.9.1.js" />
/// <reference path="../jasmine.js" />
/// <reference path="../jasmine-jquery.js" />
/// <reference path="../app/Page.js" />

describe("Page", function () {
	var page;

	beforeEach(function () {
		setFixtures(sandbox({ id: "main-body" }));
		page = new Page();
	});

	describe("when displaying a sucess message", function () {
		beforeEach(function () {
			page.success("Victory!");
		});

		it("adds a sucess message to the page", function () {
			expect($("#main-body > div")).toHaveClass("alert alert-block alert-success");
			expect($("#main-body > div").text()).toContain("Victory!");
		});
	});

	describe("when displaying an error message", function () {
		beforeEach(function () {
			page.error("Defeat!");
		});

		it("adds a failure message to the page", function () {
			expect($("#main-body > div")).toHaveClass("alert alert-block alert-error");
			expect($("#main-body > div").text()).toContain("Defeat!");
		});
	});
});