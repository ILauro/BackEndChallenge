var appname = angular.module('appname', []);
appname.controller('appCtrl', ['$scope', '$http',
    function ($scope, $http) {
        $scope.coins;
        $scope.quotes;

        $scope.init = function () {
            var httpRequest = $http({
                method: "POST",
                url: "Home/GetConversion",
            }).then(function (response, status) {
                var data = response.data;

                if (data.success == true) {
                    $scope.quotes = data.quotes;
                    //I Didn't understand 2º request in introduction so the conversion code ends here. 
                    //Seems like a pointless conversion to me from FIAT then back to USD
                }

            }, function (response) {
                console.log("Connection failed => GetConverison");
            });
        };

        $scope.formatToCurrency = amount => { return "$" + amount.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, "$&,"); };

        $scope.init();

        // #region SignalR

        "use strict";
        var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

        connection.start().then(function () {
            getData();
            $scope.$apply();
        }).catch(function (err) {
            return console.error(err.toString());
        });

        function getData() {
            connection.invoke("SendMessage").catch(function (err) {
                return console.error(err.toString());
            });
        };

        connection.on("ReceiveMessage", function (data) {
            $scope.coins = data;
            $scope.$apply();
        });

        setInterval(function () { //Not familiar with SignalR, so it's most likely incorrect
            connection.invoke("SendMessage").catch(function (err) {
                return console.error(err.toString());
            });
            $scope.$apply();
        }, 10000);

        connection.onclose(function (e) {
            if (e) {
                abp.log.debug('Connection closed with error: ' + e);
            } else {
                abp.log.debug('Disconnected');
            }

            tryReconnect();
        });

        function tryReconnect() {
            if (tries > 5) {
                return;
            } else {
                connection.start()
                    .then(() => {
                        reconnectTime = abp.signalr.reconnectTime;
                        tries = 1;
                        console.log('Reconnected to SignalR server!');
                    }).catch(() => {
                        tries += 1;
                        reconnectTime *= 2;
                        setTimeout(() => tryReconnect(), reconnectTime);
                    });
            }
        }

        // #endregion

    }]);