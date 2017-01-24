

appMainModule.controller("AddEntryViewModel", function ($scope, $window, $location,$http, viewModelHelper, validator) {

    $scope.addEntryModel = new TimeSpent.TimeEntryModel();
    $scope.addEntryModel.Date = new Date();
    $scope.viewModelHelper = viewModelHelper;

    $scope.categories = [];
    $scope.projects = [];

    $scope.viewModelHelper.modelIsValid = true;
    $scope.viewModelHelper.modelErrors = [];

    $scope.previous = function()
    {
        window.history.back();
    }

    $scope.submit = function () {
        if ($scope.addEntryModel.Date != null && $scope.addEntryModel.Date != '')
            $scope.addEntryModel.Date = moment($scope.addEntryModel.Date).format("MM-DD-YYYY");
        else
            $scope.addEntryModel.Date = '';

        validator.ValidateModel($scope.addEntryModel, addEntryModelRules);
        viewModelHelper.modelIsValid = $scope.addEntryModel.isValid;
        viewModelHelper.modelErrors = $scope.addEntryModel.errors;

        if(viewModelHelper.modelIsValid)
        {
            viewModelHelper.apiPost('api/timeentry/add', $scope.addEntryModel,
           function (result) {
               $window.location.href = TimeSpent.rootPath + 'timeentry';
               
           })
            
        }
        else
        {
            viewModelHelper.modelErrors = $scope.addEntryModel.errors;
        }
    }

    $scope.openDate = function ($event) {

        $event.preventDefault();
        $event.stopPropagation();
        $scope.openedEntry = true;
        
     }

    var addEntryModelRules = [];

    var setupRules = function () {
       
        addEntryModelRules.push(new validator.PropertyRule("Description", {
            required: { message: "Description is required" }
        }));

        addEntryModelRules.push(new validator.PropertyRule("Duration", {
            required: { message: "Duration is required" },
            //pattern: { message: "Entry duration is invalid", params: /[1-9][0-9]{0,2}/ }
            range: {  message: "Duration must be between 1 and 24.", params: { min: 1, max: 25 } }
        }));

        addEntryModelRules.push(new validator.PropertyRule("EntryDate", {
            required: { message: "Date is required" }
        }));      

    }

    var loadCategories = function(){
        viewModelHelper.apiGet('api/category/list', null,
             function (result) {
                 $scope.categories = result.data;
                 if($scope.categories.length > 0)
                    $scope.addEntryModel.CategoryId = $scope.categories[0].CategoryId;
             })
    }
    
    var loadProjects = function () {
        viewModelHelper.apiGet('api/project/list', null,
             function (result) {
                 $scope.projects = result.data;
                 if ($scope.projects.length > 0)
                     $scope.addEntryModel.ProjectId = $scope.projects[0].ProjectId;
             })
    }

    setupRules();
    loadCategories();
    loadProjects();
   

});