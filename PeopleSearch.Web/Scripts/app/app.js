var app = angular.module("SearchAppModule", ["checklist-model", "angular-loading-bar"]);

app.controller("PersonController", [
  "$scope","$http",
        function ($scope, $http) {

            $scope.$on("newuser", function(event, data) {
                load(true);
            });
            $scope.searchText = "";
            var load = function(isEvent){
            $http.get("/PeopleSearch/GetPersonList").then(function(response) {
                $scope.personList = response.data;

                $scope.filterPeople = function () {
                    return $scope.personList.filter(function (item) {
                        if (item.FirstName != ""|| "" != item.LastName){
                        return (item.FirstName.toLowerCase().indexOf($scope.searchText.toLowerCase()) > -1 ||
                            item.LastName.toLowerCase().indexOf($scope.searchText.toLowerCase()) > -1);
                        }
                        return true;
                    });
                }; //end of filterPeople
            });
        };
        load();
    }]);
