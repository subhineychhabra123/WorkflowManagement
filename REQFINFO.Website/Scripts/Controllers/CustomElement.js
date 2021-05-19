/// <reference path="../../Templates/checkbox.html" />





//myApp.directive('customElement', function ($http, $compile) {

  
//    //var renderHandler = function ($elem, $scope) {
//    //    var scopeElement = $scope.$new(true);
//    //    angular.forEach(data, function (value, key) {
//    //        scopeElement[key] = value;
//    //    });

    
    
       
  

//    return {
//        restrict: 'E',
//        scope: true,
//        link: function (scope, elem, attrs) {
//            alert(JSON.stringify(attrs))
//            $http.get('/Templates/' + scope + '.html').then(function (response) {
//                $elem.html(response.data);
//                $compile($elem.contents())(scopeElement);
//            }, function (e, a) {
//                console.error('template', data.ParameterType, 'not found.');
//            });

//            var $elem = $(elem);
//            scope.$watch(attrs.element, function (newValue, oldValue) {
//                if (!angular.isArray(newValue)) {
//                    renderHandler(elem, scope, newValue);
//                }
//            });
//        }
//    }
//});







myApp.directive('customElement', function ($http, $compile) {
    alert(data.Name);
    var renderHandler = function ($elem, $scope, data) {
            var scopeElement = $scope.$new(true);
            angular.forEach(data, function (value, key) {
                scopeElement[key] = value;
            
            });
           
         
        $http.get('/Templates/' + data.Name + '.html').then(function (response) {
            $elem.html(response.data);
         //   $compile($elem.contents())(scopeElement);
        }, function (e,a) {
            console.error('template', data.ParameterType, 'not found.');
        });
    };

    return {
        restrict: 'E',
        scope: true,
        link: function ($scope, elem, attrs) {
            var $elem = $(elem);
            $scope.$watch(attrs.element, function (newValue, oldValue) {
                if (!angular.isArray(newValue)) {
                    renderHandler($elem, $scope, newValue);
                }
            });
        }
    }


   
});