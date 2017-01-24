appMainModule.controller("CategoryListViewModel", function ($scope, $http, $element, $attrs,$window, viewModelHelper,validator) {

    $scope.viewModelHelper = viewModelHelper;
    $scope.categories = [];
    $scope.selected = {};
   

    $scope.viewModelHelper.modelIsValid = true;
    $scope.viewModelHelper.modelErrors = [];
    var categoryModelRules = [];

    var loadCategories = function () {
        
        viewModelHelper.apiGet('api/category/list', null,
            function (result) {

                $scope.categories = result.data;
              
            });
    
    }

     $scope.reset = function () {
        $scope.selected = {};
     };

     $scope.cancelEdit= function(){
         loadCategories();
         $scope.reset();
     }


    angular.element(document).ready(function () {
       
        loadCategories();
       
   
    });

    $scope.confirmDeleteCategory = function (index, category) {

        $scope.selectedCategory = category;
             
        $('.deleteCategoryModal').modal();
    }

    $scope.deleteCategory = function () {
        
        viewModelHelper.apiPost('api/category/delete', $scope.selectedCategory.CategoryId,
          function (result) {
              
              $window.location.href = TimeSpent.rootPath + 'category';
          }
          , null
          , function () {
              $('.deleteCategoryModal').modal('hide');
          });


    }

    $scope.updateCategory = function (category) {

        validator.ValidateModel(category, categoryModelRules);
        viewModelHelper.modelIsValid = category.isValid;
        viewModelHelper.modelErrors = category.errors;

        if (viewModelHelper.modelIsValid) {
            viewModelHelper.apiPost('api/category/add', category,
            function (result) {
               
                $scope.reset();
            });
            
        }
        else {
            viewModelHelper.modelErrors = category.errors;

        }
    }

   

    var setupRules = function () {
        categoryModelRules.push(new validator.PropertyRule("Name", {
            required: { message: "Name is required" }
        }));
    }

  

    $scope.getTemplate = function (category) {
       
        if ($scope.selected['CategoryId'] && category.CategoryId === $scope.selected.CategoryId) {
            return 'edit';
        }
        else return 'display'
          
    }


    $scope.editCategory = function (category) {
        $scope.selected = angular.copy(category);
    }


    setupRules();

    });
    
  