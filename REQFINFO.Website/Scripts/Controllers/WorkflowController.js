
var myApp = angular.module('workflowApp', []);
myApp.controller('WorkflowCtlr', ['$scope', '$http',
  function ($scope, $http) {


   
    $http({ method: 'GET', url: '/User/GetUserWorkFlowList' }).
    success(function (data, status, headers, config) {         
        $scope.workflow = [];
     
        $scope.workflow = data;
        
    }).
 error(function (data, status, headers, config) {
     alert('error');
 });

   

  }
  
  


]);



