
var myApp = angular.module('GIGProcessApp', ['validation', 'validation.rule']);

myApp.config(['$validationProvider', function ($validationProvider) {
    $validationProvider.showSuccessMessage = false;
    $validationProvider.showErrorMessage = true;
}]);


myApp.config(['$validationProvider', function ($validationProvider) {


    $validationProvider
        .setExpression({
            _required: function (value, scope, element, attrs) {

               
                var isValid = true;
                var re = new RegExp("^[\d-]+$");
                if (scope.$parent.ValidationTrue == true) {
                    isValid = false;


                }
                return isValid;
            },
            _RequiredAnswerField: function (value, scope, element, attrs) {


                var isValid = true;
                var re = new RegExp("^[\d-]+$");
                if (scope.$parent.ShowDivValue == true) {
                    isValid = false;


                }
                return isValid;
            },

            CheckDate: function (value, scope, element, attrs) {


                var isValid = true;
                if (scope.$parent.DateCreated > value) {
                    isValid = false;


                }
                return isValid;
            }

        })
    .setDefaultMsg({
        _required: {
            error: 'Please Select Receiver.'
        },
        _RequiredField: {
            error: 'Please Select Action.'
        },
        _RequiredAnswerField: {
            error: 'Required'
        },
    });
}]);




myApp.controller('GIGProcessCtlr', ['$scope', '$http', function ($scope, $http) {

    $scope.SelectedStateId = "0";
    $scope.IDLifeCycle = null;
    $scope.LifeCycleObj = null;
    $scope.Leveldata = null;
    $scope.IDUPW = 0;
    $scope.IDProject = 0;
    $scope.GIG = 0;
    $scope.ShowDivValue = false;
    $scope.IDLevelXFunction = 0;
    $scope.NewAttachments = [];
    $scope.WorkFlowUserGroupVM = null;
    $scope.ShowActionandreceiver = true;
    $scope.SelectedExternalFiles = [];
    $scope.GIGInitializer = function (IDUPW, IDWorkFlowUserGroup, IDProject, GIG, IDLevelXFunction, Leveldata, ShowActionandreceiver, Direction, IDFunctionTrigger) {

     
        if (ShowActionandreceiver == 1) {
            $scope.ShowActionandreceiver=false
        }
     
        $scope.IDUPW = IDUPW;
        $scope.IDFunctionTrigger = IDFunctionTrigger;



        $scope.Direction = Direction;
        $scope.IDProject = IDProject;
        $scope.IDWorkFlowUserGroup = IDWorkFlowUserGroup;
        $scope.GIG = GIG;
        $scope.IDLevelXFunction = IDLevelXFunction;
        $scope.Leveldata = Leveldata;
        
        $scope.Getdata();
        //$scope.GetRecivers();
        $scope.BindActions();
        if (IDLevelXFunction != "" || IDLevelXFunction != null) {
            $scope.GetAnswerData();
        }
        
        $scope.BindSendTo($scope.Direction);
    };


   

    $scope.BindSendTo = function (data) {

        var parameters = {
            ActionType: $scope.Direction,
                 IDProject: $scope.IDProject,
                 IDWorkFlowUserGroup: $scope.IDWorkFlowUserGroup,
                 IDGIG: $scope.GIG
        };

        $http.post('/User/BindSendTo', parameters).
      success(function (data, status, headers, config) {

          $scope.WorkFlowUserGroupVM = data.UserVm;
         

          if (data.SelectedUserID != null && data.SelectedUserID != '') {

              $scope.DDdisable = true;

          }

          $scope.IDUser = data.SelectedUserID;
       
      })

    }
    $scope.BindActions = function () {

       
        if ($scope.Leveldata == "1") {
            $scope.DDActions = [


            {

                Id: 1,
                Name: 'Forward'
            }, {
                Id: 0,
                Name: 'Backward'
            }];
        }

        if ($scope.Leveldata == "0") {
            $scope.DDActions = [
            {
                Id: 0,
                Name: 'Backward'
            }];
        }
    }


    $scope.Getdata = function () {

        var parameters = {
            IDUPW: $scope.IDUPW,
            IDProject: $scope.IDProject,
            IDGIG: $scope.GIG,
            IDFunctionTrigger:$scope.IDFunctionTrigger
        };
        var config = {
            params: parameters
        };

        $http.get('/User/GetGIGViewData', config).
      success(function (data, status, headers, config) {
         
          

          $scope.GIGView = data.Fields.Data.createGigVM;
          $scope.UserXProjectXWorkFlowUserGroupVM = data.UserXProjectXWorkFlowUserGroupVM;
          $scope.GIGVM = data.GIGVM.UserXProjectXWorkFlowUserGroupModel;
          $scope.GIGData = data.GIGVM;
          $scope.functionVM = data.FunctionVM;
          $scope.Attachments = data.Attachments;
          $scope.Logs = data.LifeCycle;

         // $scope.WorkFlowUserGroupVMReciver = data.WorkFlowUserGroupVM;

      }).
   error(function (data, status, headers, config) {
       window.location.href = "/login"

   });
    }

    $scope.GetAnswerData = function () {

        var parameters = {
            IDGIG: $scope.GIG,
            IDLevelXFunction: $scope.IDLevelXFunction


        };
        var config = {
            params: parameters
        };

        $http.get('/User/getGIGAnswerData', config).
      success(function (data, status, headers, config) {
          
          $scope.LiFeCycle = data.LiFeCycle;
          $scope.IDLifeCycle = data.LiFeCycle.IDLifeCycle;
          $scope.SelectedExternalFiles = $scope.LiFeCycle.AttachmentVM;

      }).
   error(function (data, status, headers, config) {
      
       alert("Oops| something is wrong ");
   });
    }

    $scope.SaveGIGData = function (data) {


        var Attachmentobj = new Object();




        var obj = new Object();
        obj.IDLifeCycle = $scope.IDLifeCycle;
        obj.IDGig = $scope.GIG;
        obj.AssignedToID = $scope.IDUser;
        obj.OwnedByID = $scope.IDUser;
        obj.ISSubmitted = data;
        obj.IDLevelXFunction = $scope.IDLevelXFunction;

        obj.Detail = $scope.LiFeCycle.Detail;
        $scope.LifeCycleObj = obj;

        var parameters = {
            IDAction: $scope.Direction,
            IDWorkFlowUserGroup: $scope.IDWorkFlowUserGroup,
            attachment: $scope.SelectedExternalFiles,

            lifeCycleVM: $scope.LifeCycleObj,
          IDProject:  $scope.IDProject 
        };
        //  alert(JSON.stringify( $scope.GIGCreateData))
        $http.post('/User/SaveGIGActionData', parameters).
      success(function (data, status, headers, config) {

          if (data.Success == true) {
            
              window.location.href = "/GIGCommunicationLog/" + $scope.IDUPW + "/" + $scope.IDProject;

          }
          if (data.Success == false) {
              alert("Next Level Not Available");
          }


          $scope.IDLifeCycle = data.IdLifeCycle;
      })
    };
    $scope.cancelFile = function (file, index) {


        //bootbox.confirm("Are you sure, you want to delete this file?", function (ask) {
        //    if (ask) {
       $scope.SelectedExternalFiles.splice(index, 1);

        //$scope.NewAttachments.splice(index, 1);
        //    }
        //});



    }
    $scope.ShowDiv = function (data) {
        $scope.ActionName = data;
        $scope.ShowDivValue = true
        $scope.ValidationTrue = true;
    }

    $scope.GetRecivers = function () {

        var parameters = {
            IDUPW: $scope.IDUPW,

            IDGIG: $scope.GIG
        };
        var config = {
            params: parameters
        };

        $http.get('/User/GetUsersallSendTo', config).
      success(function (data, status, headers, config) {

          $scope.WorkFlowUserGroupVM = data.workFlowUserGroupVMObj;

      }).
   error(function (data, status, headers, config) {
      
       alert("Oops| something is wrong ");
   });
    }


    $scope.GetAttcachments = function (Data) {

        var parameters = {
            IDLifeCycle: Data

        };
        var config = {
            params: parameters
        };

        $http.get('/User/GetAttachments', config).
      success(function (data, status, headers, config) {

          $scope.AttachmentsForLC = data;


      }).
   error(function (data, status, headers, config) {
      
       alert("Oops| something is wrong ");
   });
    }



    $scope.templateUrl = function (DataObj) {

        //alert(DataObj.Val1);
        $('#datepicker_' + DataObj.IDUDFInstance + '').datepicker();

        return '/Templates/' + DataObj.Name + '.html';

    }

    $scope.AttachSelectedEmployees = function () {

        $scope.SelectedExternalFiles = [];
        $scope.GetSelectedEmployees();
        EmployeesManagementService.UploadFile($scope.SelectedFiles, function (response) {

        });
        $("#lbl_To").text('');
        $("#lbl_Subject").text('');
        $("#To_Address").val('');
        $("#Subject").val('');
        $("#TextBody").val('');
        $('#forwordEmployee').modal('show');
    }

    $("#uploadResume").uploadFile({
        url: "/User/UploadDocuments",
        fileName: "file",
        multiple: true,
        allowedTypes: "png,jpeg,jpg,tif,gif,bmp,bpg,pdf,doc,docx",
        autoSubmit: true,
        maxFileSize: 1024 * 10000,
        showProgress: false,
        showStatusAfterSuccess: false,
        showDone: false,
        showFileCounter: false,
        //showDelete: true,
        afterUploadAll: false,
        onSubmit: function (files) {
           
        },
        onDelete: function (files, pd) {
         
        },
        onSuccess: function (files, data) {

            if (data != '') {
                $scope.$apply(function () {

                    $scope.SelectedExternalFiles.push(data);
                 //   $scope.NewAttachments.push(data);
                });
            }
            // $scope.SelectedFiles.push($scope.DocumentPath+data.UploadFile);
        },
        afterUploadAll: function (files, data, xhr) {
        },
        onError: function (files, status, errMsg) {

        }

    });


}]);