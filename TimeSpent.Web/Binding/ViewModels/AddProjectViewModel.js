

appMainModule.controller("AddProjectViewModel", function ($scope, $window, $location,$http, viewModelHelper, validator) {

    $scope.projectModel = new TimeSpent.ProjectModel();
    $scope.viewModelHelper = viewModelHelper;

    $scope.viewModelHelper.modelIsValid = true;
    $scope.viewModelHelper.modelErrors = [];

    $scope.previous = function()
    {
        window.history.back();
    }

    $scope.submit = function () {
    
        validator.ValidateModel($scope.projectModel, projectModelRules);
        viewModelHelper.modelIsValid = $scope.projectModel.isValid;
        viewModelHelper.modelErrors = $scope.projectModel.errors;

        if(viewModelHelper.modelIsValid)
        {
            viewModelHelper.apiPost('api/project/add', $scope.projectModel,
            function (result) {
               
                $window.location.href = TimeSpent.rootPath + 'project';
            });
           
           
        }
        else
        {
            viewModelHelper.modelErrors = $scope.projectModel.errors;
        }
    }

    
    var projectModelRules = [];

    var setupRules = function () {
        projectModelRules.push(new validator.PropertyRule("Name", {
            required: { message: "Name is required" }
        }));
    }

    setupRules();

});