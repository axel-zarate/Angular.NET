(function () {
	'use strict';

	var app = angular.module('AxSoft');

	app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
		var base = 'Home/', defaultRoute = {
			title: 'Home',
			templateUrl: base + 'Home',
			controller: 'HomeController'
		};

		$routeProvider.when('/', defaultRoute)
			.when('/Home', defaultRoute)
			.when('/Customers', {
				title: 'Customers',
				templateUrl: base + 'Customers',
				controller: 'CustomersController'
			})
			.when('/AddCustomer', {
				title: 'Add Customer',
				templateUrl: base + 'AddEditCustomer',
				controller: 'CustomerEditController'
			})
			.when('/EditCustomer/:id', {
				title: 'Edit Customer',
				templateUrl: base + 'AddEditCustomer',
				controller: 'CustomerEditController'
			})
			.when('/Contact', {
				title: 'Contact Us',
				templateUrl: base + 'Contact',
				controller: 'HomeController'
			})
			.otherwise({
				redirectTo: '/'
			});

		// Specify HTML5 mode (using the History APIs) or HashBang syntax.
		$locationProvider.html5Mode(false).hashPrefix('!');
	}]);
})();