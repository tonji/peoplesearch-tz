app.controller("AddPersonController", ["$scope", "$http", "$rootScope", 
    function ($scope, $http, $rootScope) {
        $http.get("/PeopleSearch/GetAllInterests").then(function(response) {
            $scope.interests = response.data;
        });

        $scope.formModel = { Interests: []};  //initialize blank model

        $scope.onSubmit = function () {

            var formData = new FormData();

            for (var key in $scope.formModel) {
                var data = $scope.formModel[key];
                if (angular.isArray(data)) {
                    formData.append(key, angular.$$stringify(data));
                } else {
                    formData.append(key, data);
                }
            }

            $http.post("/PeopleSearch/AddPerson", formData,
                    {
                        transformRequest: angular.identity,
                        headers: { "Content-Type": undefined }
                    }
                )
                .then(function (success) {
                    $scope.clearModal();
                    $rootScope.$broadcast("newuser", $scope.formModel);
                }, function(error) {
                    console.log("ERROR! " + error);
                });
        };

        $scope.clearModal = function () { //clear data from the modal
            $scope.formModel = {};
            $scope.personForm.$setPristine();
        }

    }]);