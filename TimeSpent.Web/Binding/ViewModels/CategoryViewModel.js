

appMainModule.controller("CategoryViewModel", CategoryViewModel);

CategoryViewModel.$inject = ['$scope', '$window', '$location', '$http', 'viewModelHelper', 'validator'];

function CategoryViewModel($scope, $window, $location, $http, viewModelHelper, validator) {

    $scope.categoryModel = new TimeSpent.CategoryModel();
    $scope.viewModelHelper = viewModelHelper;

    $scope.viewModelHelper.modelIsValid = true;
    $scope.viewModelHelper.modelErrors = [];

    $scope.previous = function()
    {
        window.history.back();
    }

    $scope.submit = function () {
    
        validator.ValidateModel($scope.categoryModel, categoryModelRules);
        viewModelHelper.modelIsValid = $scope.categoryModel.isValid;
        viewModelHelper.modelErrors = $scope.categoryModel.errors;

        if(viewModelHelper.modelIsValid)
        {
            viewModelHelper.apiPost('api/category/add', $scope.categoryModel,
            function (result) {
               
                $window.location.href = TimeSpent.rootPath + 'category'; 
               
            });
           
           
        }
        else
        {
            viewModelHelper.modelErrors = $scope.categoryModel.errors;
        }
    }

    
    var categoryModelRules = [];

    var setupRules = function () {
        categoryModelRules.push(new validator.PropertyRule("Name", {
            required: { message: "Name is required" }
        }));
    }

    setupRules();

};