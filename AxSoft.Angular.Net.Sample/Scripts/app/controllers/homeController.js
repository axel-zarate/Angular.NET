(function () {
	'use strict';
	
	var app = angular.module('AxSoft');

	app.controller('HomeController', ['$scope', function ($scope) {
		$scope.message = 'Angular.NET Sample code';
	}]);

})();