var dashboardModule = angular.module('dashboard', ['common','angularCharts']);

dashboardModule.controller("DashboardViewModel", function ($scope, $http, $window, $element, viewModelHelper) {

    var categories = [];
    var projects = [];
    $scope.chartType = 'bar';

    $scope.configProjectCategory = {
        title: 'Activities Per Project',
        tooltips: true,
        labels: false,
        mouseover: function () { },
        mouseout: function () { },
        click: function () { },
        legend: {
            display: true,
            //could be 'left, right'
            position: 'right'
        },
        lineLegend: "traditional"
        
    };

    $scope.configProject = {
        title: "Project Participation ",
        tooltips: true,
        labels: false,
        mouseover: function () { },
        mouseout: function () { },
        click: function () { },
        legend: {
            display: true,
            position: "right"
        },
        innerRadius: '1%'
    };

    var loadCategories = function () {
        viewModelHelper.apiGet('api/category/list', null,
            function (result) {
                for (var j = 0 ; j < result.data.length ; j++) {
                    categories.push(result.data[j].Name);
                }
            });
    }

    var loadProjects = function () {
        viewModelHelper.apiGet('api/project/list', null,
            function (result) {
                for (var j = 0 ; j < result.data.length ; j++) {
                    projects.push(result.data[j].Name);
                }
            });
    }

    var loadProjectCategoryReport = function () {


        viewModelHelper.apiGet('api/timeEntry/dashboard/projectcategoryreport', null,

            function (result) {

                var chartData = result.data;

                loadCategories();

                $scope.dataProjectCategoryReport = {
                    series: categories,
                    data: chartData


                };
                viewModelHelper.apiGet('api/timeentry/dashboard/projectreport', null,

                    function (result2) {

                        var chartDataProjectReport = result2.data;
                      
                      loadProjects();

                        $scope.dataProjectReport = {
                            series: projects,
                            data: chartDataProjectReport

                        };

                    });

            });

    }


    var LoadProjectReport = function () {
        viewModelHelper.apiGet('api/timeentry/dashboard/projectreport', null,

          function (result) {

              var chartDataProjectReport = result.data;

              loadProjects();

              $scope.dataProjectReport = {
                  series: projects,
                  data: chartDataProjectReport

              };

            

          });



      


    }
        

        loadProjectCategoryReport();
     //   LoadProjectReport();

    });
   
   



