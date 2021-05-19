
var myApp = angular.module('ProjectApp', []);
myApp.controller('ProjectCtlr', ['$scope', '$http',
  function ($scope, $http) {
     
     

      $scope.projectInitializer = function (WorkFlowID) {
         
          $scope.hdnWorkflowid = WorkFlowID;
          getdata();

      };

      function getdata() {

          var parameters = {

              WorkFLowID: $scope.hdnWorkflowid
          };
          var config = {
              params: parameters
          };

          $http.get('/User/GetProjectsList', config).
        success(function (data, status, headers, config) {

            $scope.workflow = [];
           
            $scope.Projects = data.ProjectVM;
            $scope.WorkFlowName = data.WorkflowName


        }).
     error(function (data, status, headers, config) {
         alert(data);
     });
      }



}]);