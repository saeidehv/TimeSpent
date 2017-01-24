var timeEntryModule = angular.module('timeEntry', ['common'])
    .config(function ($routeProvider, $locationProvider) {
        $routeProvider.when(TimeSpent.rootPath + 'timeentry/list', { templateUrl: TimeSpent.rootPath + 'Templates/TimeEntryList.html', controller: 'TimeEntryListViewModel' });
        $routeProvider.when(TimeSpent.rootPath + 'timeentry/edit', { templateUrl: TimeSpent.rootPath + 'Templates/EditTimeEntry.html', controller: 'EditTimeEntryViewModel' });
        $routeProvider.otherwise({ redirectTo: TimeSpent.rootPath + 'timeentry/list' });
        $locationProvider.html5Mode(true);
    });

timeEntryModule.controller('TimeEntryViewModel', function TimeEntryViewModel($scope, $http, $location, $window, viewModelHelper) {

    $scope.viewModelHelper = viewModelHelper;
    $scope.timeEntryModel = new TimeSpent.TimeEntryModel();
    $scope.currentItem = new TimeSpent.TimeEntryModel();
   

    $scope.previous = function () {
        $window.history.back();
    }
});

// this is a child controller of TimeEntryViewModel for displaying list
timeEntryModule.controller('TimeEntryListViewModel', function TimeEntryListViewModel($scope, $q, $http, $location, viewModelHelper, validator) {

   
    $("#addEntryButton").show();

    $scope.viewModelHelper = viewModelHelper;
    $scope.timeEntries = [];

    var loadTimeEntries = function () {

        viewModelHelper.apiGet('api/timeentry/list', null,
            function (result) {

                $scope.timeEntries = result.data;

               
            }
    );
    }

     $scope.confirmDeleteTimeEntry= function ( timeEntry) {

        $scope.currentItem = timeEntry;
        $('.deleteTimeEntryModal').modal();
    }

    $scope.deleteTimeEntry = function (timeEntry) {

        viewModelHelper.apiPost('api/timeentry/delete', timeEntry.TimeEntryId,
          function (result) {
             
              $('.deleteTimeEntryModal').modal('hide');
              $location.path(TimeSpent.rootPath + 'timeentry');
          })

    }

    $scope.editTimeEntry = function (timeEntry) {

        $scope.currentItem = $.extend($scope.currentItem, timeEntry);
        
        $location.path(TimeSpent.rootPath + 'timeentry/edit');
    }

  
    loadTimeEntries();
       
});

// this is a child controller of TimeEntryViewModel for editing
timeEntryModule.controller('EditTimeEntryViewModel', function EditTimeEntryViewModel($scope, $http, $location, $q, viewModelHelper, validator) {

    viewModelHelper.modelIsValid = true;
    viewModelHelper.modelErrors = [];
    var editEntryModelRules = [];
    
    $("#addEntryButton").hide();

    if ($scope.currentItem.TimeEntryId == '')
    {
        $window.location.href = TimeSpent.rootPath + 'timeentry';
    }

    var categories = $http({
        method: 'GET',
        url: TimeSpent.rootPath + 'api/category/list'
    });

    var projects = $http({
        method: 'GET',
        url: TimeSpent.rootPath + 'api/project/list'
    });

    $q.all([categories, projects]).then(function (data) {

        var categoryList = data[0].data;

        var projectList = data[1].data;

        viewModelHelper.apiPost('api/timeentry/get', $scope.currentItem.TimeEntryId, function (result) {

            $scope.catList = $.extend($scope.catList, categoryList);
            $scope.projects = $.extend($scope.projects, projectList);
            $scope.timeEntryModel = $.extend($scope.timeEntryModel, result.data);
           

        });
    });

    $scope.submit = function () {
        if ($scope.timeEntryModel.Date != null && $scope.timeEntryModel.Date != '')
            $scope.timeEntryModel.Date = moment($scope.timeEntryModel.Date).format("MM-DD-YYYY");
        else
            $scope.timeEntryModel.Date = '';

        validator.ValidateModel($scope.timeEntryModel, editEntryModelRules);
        viewModelHelper.modelIsValid = $scope.timeEntryModel.isValid;
        viewModelHelper.modelErrors = $scope.timeEntryModel.errors;

        if (viewModelHelper.modelIsValid) {
            viewModelHelper.apiPost('api/timeentry/add', $scope.timeEntryModel,
           function (result) {
               $location.path(TimeSpent.rootPath + 'timeentry');

           })

        }
        else{
            viewModelHelper.modelErrors = $scope.timeEntryModel.errors;
        }
    }

    $scope.openDate = function ($event) {

        $event.preventDefault();
        $event.stopPropagation();
        $scope.openedEntry = true;

    }

   
    var setupRules = function () {
        editEntryModelRules.push(new validator.PropertyRule("Description", {
            required: { message: "Entry description is required" }
        }));

        editEntryModelRules.push(new validator.PropertyRule("Duration", {
            required: { message: "Entry duration is required" },
            pattern: { message: "Entry duration is invalid", params: /[1-9][0-9]{0,2}/ }
        }));

        editEntryModelRules.push(new validator.PropertyRule("EntryDate", {
            required: { message: "Entry date is required" }
        }));

    }

    setupRules();
    
});
