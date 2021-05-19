
var myApp = angular.module('GIGLogApp', ["bw.paging"]);
myApp.controller('GIGLogCtlr', ['$scope', '$http',
  function ($scope, $http) {



      var lastRequest = null;
      $scope.readyToPrint = false;
      $scope.OrganizationList = [];
      $scope.OrganizationPrintList = [];
      $scope.Search = "";
      $scope.IsActive = "";
      var uploadObj = null;
      /* Paging parameters*/
      $scope.PageNumber = 1;
      $scope.PageSize;
      $scope.TotalRecords = 0;
      $scope.TotalPages = null;
      $scope.Pages = [];
      $scope.currentPage = 1;
      $scope.nextCount = 0;
           /* Sorting parameters */
      $scope.Action = "";
      $scope.NoRecordfound = false;
      $scope.sortParameter = "";
      $scope.sortOrder = false;
      $scope.IDGIG ='';

      $scope.GIGInitializer = function (IDUPW, IDProject, IDWorkflowUerGroup) {
         
          $scope.hdnIDUPW = IDUPW;
          $scope.IDProject = IDProject;
          
          $scope.Actions = []
        
          $scope.GetAllActions();
          $scope.getdata();
         
          $scope.GetFirstLevelActions();
          

      };
      $scope.GetdataWithStatus = function (Status) {
          $scope.Actions = [];
          $scope.Status = Status;
          $scope.TotalRecords = 0;
          $scope.PageNumber = 1;

          getdataForStatus();
      };

      $scope.GetFirstLevelActions = function () {

          $scope.Pages = [];
          var parameters = {
              IDUPW: $scope.hdnIDUPW,
              IDProject: $scope.IDProject,
              IDGIG: $scope.IDGIG
          };
          var config = {
              params: parameters
          };

          $http.get('/User/getFirstLevelActions', config).
        success(function (data, status, headers, config) {

            $scope.intialFirstlevelBtn = data.FunctionVM;
            $scope.levelXFunctionID = data.levelXFunctionID;
         
        }).
     error(function (data, status, headers, config) {
       //  window.location.href = "/login"
     });
      }

      $scope.getdata = function () {
          var search = $scope.Search;
          $scope.Pages = [];
          var parameters = {
              IDUPW: $scope.hdnIDUPW,
              IDProject: $scope.IDProject,
              PageNumber: $scope.PageNumber,
              SortParameter: $scope.sortParameter,
              Search: $scope.Search,
              sortOrder: $scope.sortOrder,
              Searchby:  $scope.Searchby
          };
          var config = {
              params: parameters    
          };

          $http.get('/User/GetGIGLog', config).
        success(function (data, status, headers, config) {



            $scope.TotalRecords = parseInt(data.TotalRecords / data.pageSize);
            if (data.TotalRecords % data.pageSize != 0) {

                $scope.TotalRecords = $scope.TotalRecords + 1;
            }

            $scope.workflow = [];
            $scope.Projects = data.GIGVM;
            $scope.TabsList = data.TabsList;

            // $scope.PageSize = data.pageSize;
            $scope.tabOpenCount = data.tabOpenCount;
            $scope.tabCloseCount = data.tabCloseCount;
            $scope.AllGIGCount = data.AllGIGCount;
            $scope.Status = data.TabsList[0].IDTab;
            if (!data.GIGVM.Length) {

                $scope.IDGIG = data.GIGVM[0].IDGig;
                $scope.IDFunctionTrigger = data.GIGVM[0].IDFunctionTrigger;

                $scope.setClickedRow(0, $scope.IDGIG, $scope.IDFunctionTrigger);
            }
            $scope.NoRecordfound = true;
        }).
     error(function (data, status, headers, config) {
       //  window.location.href = "/login"
     });
      }

      function getdataForStatus() {

          var parameters = {
              IDUPW: $scope.IDProject,
              Status: $scope.Status,
              PageNumber: $scope.PageNumber,
              Search: $scope.Search,
              sortParameter: $scope.sortParameter,
              sortOrder: $scope.sortOrder,
            Searchby:  $scope.Searchby
          };
          var config = {
              params: parameters
          };

          $http.get('/User/GetCommunications', config).
        success(function (data, status, headers, config) {



            $scope.TotalRecords = parseInt(data.TotalRecords / data.pageSize);

            if (data.TotalRecords % data.pageSize != 0) {

                $scope.TotalRecords = $scope.TotalRecords + 1;
            }



            $scope.workflow = [];

            if (data.Length != 0) {
                $scope.Projects = data.GIGVM;
                $scope.IDGIG = data.GIGVM[0].IDGig;
                $scope.IDFunctionTrigger = data.GIGVM[0].IDFunctionTrigger;
                $scope.setClickedRow(0, $scope.IDGIG, $scope.IDFunctionTrigger);
            }

        }).
     error(function (data, status, headers, config) {
         alert("Oops| something is wrong");
     });
      }

      function getActions() {
          $scope.Actions = [];
          var parameters = {
              IDProject: $scope.IDProject,
              IDGIG: $scope.IDGIG,
              IDUPW: $scope.hdnIDUPW,
              IDFunctionTrigger:  $scope.IDFunctionTrigger
              
          };
          var config = {
              params: parameters
          };

          $http.get('/User/GetActionsForLoggedInUser', config).
        success(function (data, status, headers, config) {
            $scope.workflow = [];
            //   $scope.Projects = data;   
            $scope.ShowCreate = false;
            $scope.Actions = data;

            
            angular.forEach(data, function (value, key) {
                if (value.Enable === true) {

                    angular.forEach($scope.AllActions, function (valueAllAction, key) {

                        if (valueAllAction.Name == value.Name) {

                            valueAllAction.Enable = true
                            valueAllAction.IDLevelXFunction = value.IDLevelXFunction;
                        }



                    });
                }
            
            });
            
        }).
     error(function (data, status, headers, config) {
         alert("Oops| something is wrong");
     });
      }
      $scope.selectedRow = null;  // initialize our variable to null
      $scope.setClickedRow = function (index, IDGig, IDFunctionTrigger) {
          //function that sets the value of selectedRow to current index


          angular.forEach($scope.AllActions, function (valueAllAction, key) {
              valueAllAction.Enable = false;
          });

          
          $scope.IDFunctionTrigger = IDFunctionTrigger;

          $scope.selectedRow = index;
          $scope.IDGIG = IDGig;
          getActions();
          $scope.GetFirstLevelActions();
      }

      $scope.GetAllActions = function () {



          $http.get('/User/GetAllActions').
      success(function (data, status, headers) {


          $scope.AllActions = data;
      }).
   error(function (data, status, headers, config) {
       alert("Oops| something is wrong");
   });

      }


      $scope.GetOrganizationByPage = function (text, page, pageSize, total) {

          $scope.PageNumber = page;
          getdataForStatus()
      };

      $scope.getPageData = function (pageNo) {
          $scope.PageNumber = pageNo;
          $scope.currentPage = pageNo;
          getdataForStatus()
      }

      $scope.SearchGIGLogs = function () {
          if ($scope.Search != "") {
              $scope.FilterGIGLogs();
          }
      }
      $scope.FilterGIGLogs = function () {

          $scope.PageNumber = 1;
    
          if (!$scope.Searchby)
          {
              $scope.Searchby = 'Number1';
          }
          getdataForStatus();
      }


      $scope.Sort = function (sortParameter, sortOrder) {

          if ($scope.sortParameter == sortParameter) {
              $scope.sortOrder = sortOrder;
          }
          else
              $scope.sortOrder == false;
          $scope.sortParameter = sortParameter;

          getdataForStatus();
      }


  }]);