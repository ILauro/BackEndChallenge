var appname = angular.module('appname', []);
appname.controller('appCtrl', ['$scope', '$http',
    function ($scope, $http) {
        $scope.coins;
        $scope.quotes;

        $scope.init = function () {
            var httpRequest = $http({
                method: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                url: "/HomeController/GetConversion",
            }).then(function (response, status) {
                debugger;
                

            });
        }

        $scope.init();

        "use strict";

        var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

        connection.on("ReceiveMessage", function (data) {
            $scope.coins = data;
            $console.log($scope.coins[0].image);
        });

        connection.start().then(function () {
            connection.invoke("SendMessage").catch(function (err) {
                return console.error(err.toString());
            });
            $scope.$apply();
        }).catch(function (err) {
            return console.error(err.toString());
        });

        setInterval(function () {
            connection.invoke("SendMessage").catch(function (err) {
                return console.error(err.toString());
            });
            $scope.$apply();
        }, 10000);

        document.getElementById("sendButton").addEventListener("click", function (event) {
            connection.invoke("SendMessage").catch(function (err) {
                return console.error(err.toString());
            });
            event.preventDefault();
            $scope.$apply();
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

        connection.onclose(function (e) {
            if (e) {
                abp.log.debug('Connection closed with error: ' + e);
            } else {
                abp.log.debug('Disconnected');
            }

            tryReconnect();
        });

        const formatToCurrency = amount => { return "$" + amount.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, "$&,"); };

    }]);