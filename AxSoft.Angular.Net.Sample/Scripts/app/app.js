(function () {
	'use strict';

	// Register Main app module
	var app = angular.module('AxSoft', ['ngRoute']);

	app.run(['$rootScope', '$window', function ($rootScope, $window) {
		
		$rootScope.$on('$routeChangeSuccess', function (event, current, previous) {
			if (current.$$route.title) {
				$window.document.title = current.$$route.title;
			}
		});
		
	}]);
})();