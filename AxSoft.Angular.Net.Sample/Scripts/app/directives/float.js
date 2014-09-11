(function (angular, undefined) {
	'use strict';

	var app = angular.module('AxSoft');

	app.directive('float', function () {
		var floatRegexp = /^\-?\d{0,10}(\.\d+)?$/,
			// Min and max values constrained by JavaScript
			// but they should be more than enough for almost all cases
			minFloatValue = -9007199254740992,
			maxFloatValue = 9007199254740992;
		return {
			require: 'ngModel',
			link: function (scope, elm, attrs, ctrl) {
				var options = angular.extend({
					min: minFloatValue,
					max: maxFloatValue
				}, scope.$eval(attrs.float));

				ctrl.$parsers.unshift(function (viewValue) {
					if (viewValue === '') {
						ctrl.$setValidity('float', true);
						return viewValue;
					}
					var floatValue;
					if (floatRegexp.test(viewValue) && !isNaN((floatValue = parseFloat(viewValue))) && floatValue >= options.min && floatValue <= options.max) {
						// it is valid
						ctrl.$setValidity('float', true);
						return floatValue;
					} else {
						// it is invalid, return undefined (no model update)
						ctrl.$setValidity('float', false);
						return viewValue;
					}
				});
			}
		};
	});
})(angular);