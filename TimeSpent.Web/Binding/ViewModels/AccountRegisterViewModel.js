 
var accountRegisterModel = angular.module('accountRegister', ['common'])
.config(function ($routeProvider, $locationProvider) {
    $routeProvider.when(TimeSpent.rootPath + 'account/register/step1', { templateUrl: TimeSpent.rootPath + 'Templates/RegisterStep1.html', controller: 'AccountRegisterStep1ViewModel' });
    $routeProvider.when(TimeSpent.rootPath + 'account/register/step2', { templateUrl: TimeSpent.rootPath + 'Templates/RegisterStep2.html', controller: 'AccountRegisterStep2ViewModel' });
    $routeProvider.when(TimeSpent.rootPath + 'account/register/confirm', { templateUrl: TimeSpent.rootPath + 'Templates/RegisterConfirm.html', controller: 'AccountRegisterConfirmViewModel' });
    $routeProvider.otherwise({ redirectTo: TimeSpent.rootPath + 'account/register/step1' });
    $locationProvider.html5Mode(true);
});

accountRegisterModel.controller('AccountRegisterViewModel', function AccountRegisterViewModel($scope, $http, $location, $window, viewModelHelper) {

    $scope.viewModelHelper = viewModelHelper;

    $scope.accountModelStep1 = new TimeSpent.AccountRegisterModelStep1();
    $scope.accountModelStep2 = new TimeSpent.AccountRegisterModelStep2();


    $scope.previous = function(){

        $window.history.back();
    }
});

accountRegisterModel.controller('AccountRegisterStep1ViewModel',function($scope, $http,$location, $window,viewModelHelper, validator){

    viewModelHelper.modelIsValid = true;
    viewModelHelper.modelErrors = [];

    var accountModelStep1Rules = [];

    
        var setupRules = function () {
            accountModelStep1Rules.push(new validator.PropertyRule("FirstName", {
                required: { message: "First name is required" }
            }));
            accountModelStep1Rules.push(new validator.PropertyRule("LastName", {
                required: { message: "Last name is required" }
            }));
            accountModelStep1Rules.push(new validator.PropertyRule("Address", {
                required: { message: "Address is required" }
            }));
            //accountModelStep1Rules.push(new validator.PropertyRule("City", {
            //    required: { message: "City is required" }
            //}));
            //accountModelStep1Rules.push(new validator.PropertyRule("State", {
            //    required: { message: "State is required" }
            //}));
            //accountModelStep1Rules.push(new validator.PropertyRule("ZipCode", {
            //    required: { message: "Zip code is required" },
            //    pattern: { message: "Zip code is in invalid format", params: /^\d{5}$/ }
            //}));
        }
    

    $scope.step2 = function () {

        validator.ValidateModel($scope.accountModelStep1, accountModelStep1Rules);
        viewModelHelper.modelIsValid = $scope.accountModelStep1.isValid;
        viewModelHelper.modelErrors = $scope.accountModelStep1.errors;
        if (viewModelHelper.modelIsValid) {
            // server side validation
            viewModelHelper.apiPost('api/account/register/validate1', $scope.accountModelStep1,
                function (result) {
                    $scope.accountModelStep1.Initialized = true;
                    $location.path(TimeSpent.rootPath + 'account/register/step2');

                });
        }
        else
            viewModelHelper.modelErrors = $scope.accountModelStep1.errors;
    }

    setupRules();
});

accountRegisterModel.controller('AccountRegisterStep2ViewModel', function ($scope, $http, $location, $window, viewModelHelper, validator) {
    //go to this controller before going to step1
    if (!$scope.accountModelStep1.Initialized)
        $location.path(TimeSpent.rootPath + 'account/register/step1');

    viewModelHelper.modelIsValid = true;
    viewModelHelper.modelErrors = [];

    var accountModelStep2Rules = [];

    var setupRules = function () {
        accountModelStep2Rules.push(new validator.PropertyRule("LoginEmail", {
            required: { message: "Login Email is required" }
        }));
        accountModelStep2Rules.push(new validator.PropertyRule("Password", {
            required: { message: "Password is required" },
            minLength: { message: "Password must be at least 6 characters", params: 6 }
        }));
        accountModelStep2Rules.push(new validator.PropertyRule("PasswordConfirm", {
            required: { message: "Password confirmation is required" },
            custom: {
                validator: TimeSpent.mustEqual,
                message: "Password do not match",
                params: function () { return $scope.accountModelStep2.Password; } // must be function so it can be obtained on-demand
            }
        }));
    }

    $scope.confirm = function () {

        validator.ValidateModel($scope.accountModelStep2, accountModelStep2Rules);
        viewModelHelper.modelIsValid = $scope.accountModelStep2.isValid;
        viewModelHelper.modelErrors = $scope.accountModelStep2.errors;

        
        if (viewModelHelper.modelIsValid) {
            viewModelHelper.apiPost('api/account/register/validate2',$scope.accountModelStep2,
                function (result) {
                    $scope.accountModelStep2.Initialized = true;
                    $location.path(TimeSpent.rootPath + 'account/register/confirm');
                })
        }
        else
            viewModelHelper.modelErrors = $scope.accountModelStep2.errors;
    }
    setupRules();

});

accountRegisterModel.controller("AccountRegisterConfirmViewModel", function ($scope, $http, $location, $window, viewModelHelper, validator) {

    //go to this controller before going to step1
    if (!$scope.accountModelStep2.Initialized)
        $location.path(TimeSpent.rootPath + 'account/register/step2');


    $scope.createAccount = function () {

        // merge client side models and send to server
        var accountModel;
        accountModel = $.extend(accountModel, $scope.accountModelStep1);
        accountModel = $.extend(accountModel, $scope.accountModelStep2);

        viewModelHelper.apiPost('api/account/register', accountModel, 
            function (result) {
                $window.location.href = TimeSpent.rootPath;
            })
    }
});