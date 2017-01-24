appMainModule.controller("ProjectListViewModel", function ($scope, $http, $element,$window, viewModelHelper, validator) {

    $scope.viewModelHelper = viewModelHelper;
    $scope.projects = [];
    $scope.selected = {};

    $scope.viewModelHelper.modelIsValid = true;
    $scope.viewModelHelper.modelErrors = [];
    var projectModelRules = [];


    var loadProjects = function() {
       
        viewModelHelper.apiGet('api/project/list', null,
            function (result) {
                
                 $scope.projects = result.data;

       });
    }

    $scope.reset = function() {
        $scope.selected = {};
    };

    $scope.cancelEdit = function() {
        loadProjects();
        $scope.reset();
    }

    angular.element(document).ready(function () {
       
        loadProjects();
    
    });

    $scope.confirmDeleteProject = function (index, project) {

        $scope.selectedProject = project;
            
        $('.deleteProjectModal').modal();
    }

    $scope.deleteProject = function () {
        
        viewModelHelper.apiPost('api/project/delete', $scope.selectedProject.ProjectId,
          function (result) {
             
              $window.location.href = TimeSpent.rootPath + 'project';
          }
          , null
          , function () {
              $('.deleteProjectModal').modal('hide');
          });

    }

    $scope.updateProject = function(project) {

        validator.ValidateModel(project, projectModelRules);
        viewModelHelper.modelIsValid = project.isValid;
        viewModelHelper.modelErrors = project.errors;

        if (viewModelHelper.modelIsValid) {
            viewModelHelper.apiPost('api/project/add', project,
            function (result) {
               
                $scope.reset();
            });
            
        }
        else {
            viewModelHelper.modelErrors = project.errors;

        }
    }

   

    var setupRules = function () {
        projectModelRules.push(new validator.PropertyRule("Name", {
            required: { message: "Name is required" }
        }));
    }

   

    $scope.getTemplate = function (project) {
       
        if ($scope.selected['ProjectId'] && project.ProjectId === $scope.selected.ProjectId) {
            return 'edit';
        }
        else return 'display'
          
    }


    $scope.editProject = function (project) {
        $scope.selected = angular.copy(project);
    }


    setupRules();
   

});


    
