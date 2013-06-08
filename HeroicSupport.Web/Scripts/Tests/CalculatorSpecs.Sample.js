/// <reference path="../jasmine.js" />

var Calculator = function() {
	var me = this;

	me.add = function(x, y) {
		return x + y;
	};
};

describe("Calculator", function () {
	var calc;
	
	beforeEach(function () {
		calc = new Calculator();
	});

	it("adds correctly", function () {
		expect(calc.add(1, 1)).toBe(3);
	});
});