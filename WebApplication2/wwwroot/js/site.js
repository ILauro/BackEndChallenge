var app = angular.module("myApp", []);
app.controller("myCtrl", function ($scope) {
    $scope.coins = [];

    $scope.getData = () => {
        var url = 'Home/GetData';

        $.ajax({
            type: "POST",
            url: url,
            success: function (result) {
                if (result != null) {
                    $scope.coins = result;
                    console.log($scope.coins);
                    alert('successfuly inserted');
                }
                else {
                    alert('some error occured');
                }
            }
        });
    }
});