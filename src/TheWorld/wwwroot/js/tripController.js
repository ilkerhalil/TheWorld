(function () {
    "use strict";
    angular.module("app-trips")
        .controller("tripsController", tripsController);

    function tripsController($http) {

        var vm = this;

        vm.trips = [];

        vm.newTrip = {};

        vm.errorMessage = "";

        vm.isBusy = true;

        $http.get("/api/trips")
            .then(function (response) {
                //Success
                angular.copy(response.data, vm.trips);

            }, function (error) {
                //Failure
                vm.errorMessage = "Failed to load to data : " + error;
            }).finally(function () {
                vm.isBusy = false;
            });

        vm.addTrip = function () {
            //vm.trips.push({ name: vm.newTrip.name, created: new Date() });
            //vm.newTrip= {}
            vm.isBusy = true;
            $http.post("/api/trips", vm.newTrip)
                .then(function (response) {
                    //success
                    vm.trips.push(response.data);
                    vm.newTrip = {};
                }, function (error) {
                    vm.errorMessage = "Failed to save new trip" + error;
                })
                .finally(function () {
                    vm.isBusy = false;
                });
        };
    }
})();