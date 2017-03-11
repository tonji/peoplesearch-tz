app.controller("AddPersonController",["$scope", "$http", "$rootScope",
    function($scope, $http, $rootScope) {
        $http.get("/PeopleSearch/GetAllInterests").then(function(response) {
            $scope.interests = response.data;
        });

        $scope.formModel = {Interests:[]};  //initialize blank model
        $scope.onSubmit = function() {
            $http.post("/PeopleSearch/AddPerson", $scope.formModel)
                .then(function (success) {
                    $scope.clearModal();
                    $rootScope.$broadcast("newuser", $scope.formModel);
                }, function(error) {
                    console.log("ERROR! " + error);
                });
        };


        $scope.clearModal = function () { //clear data from the modal
            $scope.formModel = { Interests: [] };
        }

    }]);