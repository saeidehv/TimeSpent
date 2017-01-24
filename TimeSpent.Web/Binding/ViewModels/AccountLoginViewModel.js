﻿appMainModule.controller("AccountLoginViewModel", function ($scope, $http, viewModelHelper, validator) {

    $scope.viewModelHelper = viewModelHelper;
    $scope.accountModel = new TimeSpent.AccountLoginModel();
    $scope.returnUrl = '';

    var accountModelRules = [];

    var setupRules = function () {
        accountModelRules.push(new validator.PropertyRule("LoginEmail", {
            required: { message: "Login is required" }
        }));
        accountModelRules.push(new validator.PropertyRule("Password", {
            required: { message: "Password is required" }
          //,  minLength: { message: "Password must be at least 6 characters", params: 6 }
        }));
    }

    $scope.Login = function() {
        validator.ValidateModel($scope.accountModel, accountModelRules);
        viewModelHelper.modelIsValid = $scope.accountModel.isValid;
        viewModelHelper.modelErrors = $scope.accountModel.errors;
        if (viewModelHelper.modelIsValid) {
            viewModelHelper.apiPost('api/account/login', $scope.accountModel,
                function (result) {
                    if ($scope.returnUrl != '' && $scope.returnUrl.length > 1) {
                        window.location.href = TimeSpent.rootPath + $scope.returnUrl.substring(1);
                    }
                    else {
                        window.location.href = TimeSpent.rootPath;
                    }
                });
        }
        else
            viewModelHelper.modelErrors = $scope.accountModel.errors;

    }

    $scope.PasswordKeyPress = function (keyEvent) {
        if(keyEvent.which === 13)
        {
            $scope.Login();
        }
    }

    setupRules();
});