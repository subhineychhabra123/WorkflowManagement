
var myApp = angular.module('UserHomeApp', []);
myApp.controller('LogoutCtlr', ['$scope', function ($scope, $http) {
    
    $scope.Logout = function () {
       
        alert();
        $http.post("/User/Logout/", '').success(function (data, status) {
            $scope.hello = data;
        })
       
    };

}]);