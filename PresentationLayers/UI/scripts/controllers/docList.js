angular.module('sampleAngularApp').controller('documentListCtrl', ['$scope', '$filter', '$http', '$location', function ($scope, $filter, $http, $location) {

    var apiUrl = "/api/documents/menu/";
    var menuTitle = $location.path();
    $scope.documentsInfoAvailable = false;

    var menuTopageHeaderMapping = [];

    menuTopageHeaderMapping["/ExplanatoryDocs"] = "Edelman Edge Explanatory Documents";
    menuTopageHeaderMapping["/ResearchReferenceDocs"] = "Research Reference Documents";
    menuTopageHeaderMapping["/NewBusinessFiles"] = "New Business Files";
    menuTopageHeaderMapping["/RegionalSummaries"] = "Regional Summaries";
    menuTopageHeaderMapping["/MarketingMaterials"] = "Marketing Materials";

    $scope.pageSize = 14;
    $scope.currentPage = 1;
    $scope.numPages = 0;
    $scope.documentStatus = "Fetching";
    $scope.documents = [];
    $scope.pageHeader = menuTopageHeaderMapping[menuTitle];
    $http.get(apiUrl + menuTopageHeaderMapping[menuTitle])
        .then(documentFetchSuccess, documentFetchError)
        .catch(handleException);


    function documentFetchSuccess(response)
    {
        if (response.status == 204) {
            $scope.documentStatus = "No Content";
        }
        else {
            $scope.documents = response.data;
            $scope.documentStatus = "Available";
        }
    }

    function documentFetchError(response)
    {
        $scope.documentStatus = "Error";
    }

    function handleException(exception)
    {
        $scope.documentStatus = "Error";
    }


    $scope.searchTextChanged = function (searchText) {
        var filteredObjects = $filter('filter')($scope.documents, { documentTitle: searchText });
        if (filteredObjects.length > $scope.pageSize) {
            $scope.showPagination = true;
        }
        else {
            $scope.showPagination = false;
        }
    }

}]);

