(function () {
	'use strict';
	
	var app = angular.module('AxSoft');

	app.controller('CustomersController', ['$scope', 'customerService', function ($scope, customerService) {
		$scope.customers = customerService.query();
	}]);

	app.controller('CustomerEditController', ['$scope', '$location', '$routeParams', 'customerService', function ($scope, $location, $routeParams, customerService) {

		if ($location.$$path == '/AddCustomer') {
			$scope.isAdd = true;
			$scope.customer = {};
		} else {
			$scope.isAdd = false;
			var id = parseInt($routeParams.id);
			if (isNaN(id) || id <= 0) throw new Error('Invalid customer Id');
			$scope.customer = customerService.get({id: id});
		}

		$scope.isEdit = !$scope.isAdd;

		$scope.save = function (form) {
			if (form.$invalid) {
				return;
			}

			$scope.errorMessage = null;

			var fn = $scope.isAdd ? customerService.save : customerService.update;
			fn($scope.customer, function () {
				$location.url('/Customers');
			}, function (response) {
				$scope.errorMessage = response.Message || 'Error';
			});
		};

		$scope.cancel = function () {
			$location.url('/Customers');
		};
	}]);

	app.factory('customerService', ['$resource', function ($resource) {
		var customers = $resource('/api/customers/:id', {}, {
			update: {
				method: 'PUT'
			}
		});

		return customers;
	}]);
})();