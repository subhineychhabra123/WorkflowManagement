var myApp = angular.module('GIGCreateApp', ['validation', 'validation.rule']);

myApp.config(['$validationProvider', function ($validationProvider) {
    $validationProvider.showSuccessMessage = false;
    $validationProvider.showErrorMessage = true;
}]);


myApp.config(['$validationProvider', function ($validationProvider) {
  

    $validationProvider
        .setExpression({
            _required: function (value, scope, element, attrs) {

                 
                var isValid = true;
                
                if (scope.$parent.LevelVM.ISDisplayContractor == true) {
                    if (!scope.$parent.IDContractor) {
                        isValid = false;
                    }
                   
                }
                return isValid;
            },
            _RequiredField: function (value, scope, element, attrs) {


                var isValid = true;
                var re = new RegExp("^[\d-]+$");
                if (scope.$parent.LevelVM.ISDisplayContractor == true) {

                    //if (!scope.$parent.IDContractor) {
                    //    isValid = false;
                    //}

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
            error: 'Contractor is required.'
        },
        CheckDate: {
            error: 'Required By Cannot be less then Date.'
        },
        _RequiredField: {
            error: 'Required'
        },
    });
}]);


$(document).ready(function () {
 //   $('#datepicker_Date').datepicker();
    $("#datepicker_Required").datepicker({ minDate: 0 });
    $("#datepicker_Date").datepicker({ minDate: 0 });
    $('#datepicker_Date').datepicker('setDate', 'today');
    //  
    var selected = null;
    $("#datepicker_Date").on("change", function () {
        selected = $(this).val();
        $("#datepicker_Required").datepicker('option', { minDate: selected });

    });

   
  
});
myApp.controller('GIGCreateCtlr', ['$scope', '$http',
  function ($scope, $http) {
      $scope.UDFValidationDefinitionVM = "";
      $scope.IDGIG = "";
      $scope.GIGCreateData = null;
      $scope.GIGData = null;
      $scope.SubmitDisable = true;
      $scope.SelectedExternalFiles = [];
      $scope.DBattachment = [];
      $scope.SelectedStateId = "0";
      $scope.IDLifeCycle = null;
      $scope.LifeCycleObj = null;
     // $('#datepicker_Required').datepicker();
      
      var myDate = new Date();
      
      $scope.DateCreated = myDate;
      var currentDate = new Date();
    //  $("#datepicker_Date").datepicker("setDate", currentDate);

      $scope.GIGInitializer = function (IDUPW, IDProject, IDLevelXFunction, IDWorkflowUserGroup) {
          $scope.IDUPW = IDUPW;
          $scope.IDProject = IDProject;
          $scope.IDLevelXFunction = IDLevelXFunction;
         // alert($scope.IDLevelXFunction)
          $scope.IDWorkflowUserGroup = IDWorkflowUserGroup;

          $scope.Getdata();

      };

    
      $scope.templateUrl = function (DataObj) {

         
          $('#datepicker_' + DataObj.IDUDFDefinition + '').datepicker();
          $('#datepicker_' + DataObj.IDUDFDefinition + '').datepicker({ minDate: 0 });
          //$('#datepicker_' + DataObj.IDUDFDefinition + '').datepicker('setDate', 'today');
          return '/Templates/' + DataObj.Name + '.html';

      }

      $scope.convertInt = function (input) {
          return parseInt(input, 10);
      }
      $scope.BindCalenderData = function (data) {


          $('#datepicker_' + data.$parent.DataObj.IDUDFDefinition + '').datepicker();
   

      };
      $scope.Getdata = function () {

          var parameters = {
              IDUPW: $scope.IDUPW,
              IDProject: $scope.IDProject,


          };
          var config = {
              params: parameters
          };

          $http.get('/User/GetGIGCreateData', config).
        success(function (data, status, headers, config) {
           
            //  $scope.GIGCreateData = data.Fields.Data.createGigVM;
            $scope.Contractors = data.Contractors;
            // $scope.UDFValidationDefinitionVM = data.Fields.Data.UDFValidationDefinitionVM;
            $scope.LevelVM = data.LevelVM;
            $scope.UserXProjectXWorkFlowUserGroupVM = data.UserXProjectXWorkFlowUserGroupVM;
            $scope.WorkFlowUserGroupVM = data.WorkFlowUserGroupVM;
            $scope.GroupName = data.GroupName;
            $scope.GIGCreateData = data.Fields.Data.createGigVM;

            $scope.UDFValidationDefinitionVM = data.Fields.Data.UDFValidationDefinitionVM;

        }).
     error(function (data, status, headers, config) {
         alert("Oops| something is wrong");
     });
      }
      $scope.cancelFile = function (file, index) {
         
       
         //CommonFunctions.showConfirmAlert('Are you sure, you want to delete this file?', function (isConfirm) {
         //    if (isConfirm) {
      
                  $scope.SelectedExternalFiles.splice(index, 1);
                  var obj = new Object();
                  obj = file;

                  if (obj.IDAttachment != "" && obj.IDAttachment != null && obj.IDAttachment != undefined) {
                      obj.IsDeleted = true;
                      $scope.DBattachment.push(obj);
                  }
                  else {
                      for (var i = 0; i < $scope.DBattachment.length; i++) {
                          if ($scope.DBattachment[i] == file) {
                              $scope.DBattachment.splice(i, 1);
                          }
                      }
                  }
               
       // }
      
       //});
          
     }
      $scope.SubmitGIGData = function () {
          
          
          $scope.SubmitDisable = false;
          var obj1 = new Object();
          obj1.IDUPW = $scope.IDUPW;
          obj1.IDContractor = $scope.IDContractor;
          obj1.Number = $scope.GIGNumber;
          obj1.ISSubmitted = true;
          obj1.AssignedToID = $scope.IDUser;
          obj1.SendTo = $scope.IDUser;
          obj1.DateCreated = $scope.DateCreated;
          obj1.DateRequired = $scope.DateRequired;

          obj1.IDGig = $scope.IDGIG;
          $scope.GIGData = obj1;


          var obj = new Object();
         // obj.IDLifeCycle = $scope.IDLifeCycle;
          obj.IDGig = $scope.IDGIG;
         
        
          obj.AssignedToID = $scope.IDUser;
          obj.ISSubmitted = true;
          obj.IDLevelXFunction = $scope.IDLevelXFunction;
          obj.Detail = $scope.LifeCycleDetail;
          $scope.LifeCycleObj = obj;

          var parameters = {
              gIGVM: $scope.GIGData,
              ObjUpdated: $scope.GIGCreateData,
            
              attachment: $scope.DBattachment,
              lifeCycleVM: $scope.LifeCycleObj,
              IDWorkflowUserGroup: $scope.IDWorkflowUserGroup,
              IDProject:$scope.IDProject
          };
          //  alert(JSON.stringify( $scope.GIGCreateData))
          $http.post('/User/SubmitGIGData', parameters).
           success(function (data, status, headers, config) {
               if(data.Success==false)
               {
                   $scope.SubmitDisable = true;
                   alert(data.Message);
               }
               else
               {
                   window.location.href = "/GIGCommunicationLog/" + $scope.IDUPW + "/" + $scope.IDProject;
               }
           
        })
      }

      $scope.SaveGIGData = function () {
       
          var obj = new Object();
          obj.IDUPW = $scope.IDUPW;
          obj.IDContractor = $scope.IDContractor;
          obj.Number = $scope.GIGNumber;
        
          obj.AssignedToID = $scope.IDUser;
          obj.SendTo = $scope.IDUser;
          obj.DateCreated = $scope.DateCreated;
          obj.DateRequired = $scope.DateRequired;

          obj.IDGig = $scope.IDGIG;
          $scope.GIGData = obj;
      
          var parameters = {
            
              
              attachmentVM: $scope.SelectedExternalFiles,
              IDProject: $scope.IDProject
          };

          $http.post('/User/SaveGIGData', parameters).
        success(function (data, status, headers, config) {
            if (data.Success == true)
            {
                //alert(data.Message);
               
            }

            else if (data.Success == false)
            {
                alert(data.Message)
               
            }

          else
      {

                alert(data.Message)
                $scope.SubmitDisable = true;
      }
            
            $scope.SelectedExternalFiles = [];
            $scope.DBattachment = [];
            $scope.SelectedExternalFiles = data.Attachments;
            angular.forEach($scope.SelectedExternalFiles, function (value, key) {
                value.IsDeleted = false;
            });

            // $scope.DBattachment = $scope.SelectedExternalFiles;
 
            $scope.IDGIG = data.IDGig;
            alert($scope.IDGIG)         //  $scope.GIGCreateData = data.Fields.Data.createGigVM;
          
          //  $scope.UDFValidationDefinitionVM = data.Fields.Data.UDFValidationDefinitionVM;
            $scope.IDLifeCycle = data.IDLifeCycle;

        })
      }


      
      $scope.CheckBoxValue = function (data) {
          if(data=="True" || data=="true")
          {
              return true;

          }

          else
          {

              return false;
          }
      }
     
      $scope.getUsersGroupData = function (IDContractor) {

          $scope.WorkFlowUserGroupVM = null;

          var parameters = {
              IDUPW: $scope.IDUPW,
              IDContractor: IDContractor

          };

          $http.post('/User/GetUserForSendTo', parameters).
        success(function (data, status, headers, config) {
           
            $scope.WorkFlowUserGroupVM = data.WorkFlowUserGroupVM;

        })
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
                      $scope.DBattachment.push(data);

                  });

              }

          },
          afterUploadAll: function (files, data, xhr) {
          },
          onError: function (files, status, errMsg) {

          }

      });


  }]);





