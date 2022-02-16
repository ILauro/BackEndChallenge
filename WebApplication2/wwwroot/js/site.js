var app = angular.module("myApp", []);

app.controller("myCtrl", function ($scope, $http) {
    $scope.coins = [];

    $scope.init = function () {
        $scope.getData();
    }

    $scope.getData = function () {
        var httpRequest = $http({
            method: 'POST',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: 'Home/GetData',
            //data: JSON.stringify({ data: id })
        }).then(function onSuccess(response) {
            // Handle success
            debugger;

            if (response.success == true) {
                $scope.coins = data.Data;
                alert('Successfuly inserted');
            }

        }).catch(function onError(response) {
            // Handle error
            alert('Error occured');
        });
    }

});