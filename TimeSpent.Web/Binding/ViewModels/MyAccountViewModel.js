
appMainModule.controller("MyAccountViewModel", function ($scope, $http, $window, viewModelHelper, validator) {

    $scope.viewMode = ''; // account, success
    $scope.accountModel = null;
    $scope.viewModelHelper = viewModelHelper;

    var accountModelRules = [];

    var setupRules = function () {
        accountModelRules.push(new validator.PropertyRule("FirstName", {
            required: { message: "First name is required" }
        }));
        accountModelRules.push(new validator.PropertyRule("LastName", {
            required: { message: "Last name is required" }
        }));
        accountModelRules.push(new validator.PropertyRule("Address", {
            required: { message: "Address is required" }
        }));
      
    }

    $scope.initialize = function () {
        viewModelHelper.apiGet('api/user/account', null,
            function (result) {
                $scope.accountModel = result.data;
                $scope.viewMode = 'account';
            });
    }

    $scope.save = function () {
        validator.ValidateModel($scope.accountModel, accountModelRules);
        viewModelHelper.modelIsValid = $scope.accountModel.isValid;
        viewModelHelper.modelErrors = $scope.accountModel.errors;
        if (viewModelHelper.modelIsValid) {
            viewModelHelper.apiPost('api/user/account', $scope.accountModel,
                function (result) {
                    $scope.viewMode = 'success';
                });
        }
        else
            viewModelHelper.modelErrors = accountModel.errors;
    }

    $scope.cancel = function () {
        $window.location.href = TimeSpent.rootPath;
    }
    
    

    var validationErrors = function () {
        var errors = [];
        for (var i = 0; i < propertyBag.length; i++) {
            if (propertyBag[i].Invalid) {
                errors.push(propertyBag[i].PropertyName);
            }
        }
        return errors;
    }

    $scope.validate = function (field, invalid) {
        for (var i = 0; i < propertyBag.length; i++) {
            if (propertyBag[i].PropertyName == field) {
                propertyBag[i].Invalid = invalid;
                break;
            }
        }
    }

    setupRules();
    $scope.initialize();

});
