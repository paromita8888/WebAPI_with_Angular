(function () {
    'use strict';

    /**
     * @ngdoc function
     * @name sampleAngularApp.controller:MainCtrl
     * @description
     * # MainCtrl
     * Controller of the sampleAngularApp
     */
    angular.module('sampleAngularApp')
        .controller('MainCtrl', ['$scope', '$http', '$templateRequest',  function ($scope, $http, $templateRequest) {

            var mainCtrl = this;

            $scope.countries = [];

            $scope.countriesInfoAvailable = "Fecthing";
            $scope.popupVM = {
                pageSize: 5,
                currentPage: 1,
                numPages: 0,
                docAvailable: false,
                contextCountry: ''
            };
            $scope.countryDocAvailable = false;
            $scope.popupTemplate = "<p>working</p>";
            $templateRequest("UI/views/popuptemplate.html").then(function (html) {
                $scope.popupTemplate = html;
            });

            this.getMapData = function () {
                var fillKey = { "fillKey": "Available" };
                var data = {};
                for (var i = 0; i < $scope.countries.length; ++i) {
                    data[$scope.countries[i].aplha3_Code] = fillKey;
                }

                return data;
            };

            $http.get("/api/countries").then(countriesFecthSucess, countriesFecthError).catch(countriesFecthException);

            function countriesFecthSucess(response) {
                if (response.status == 204) {
                    $scope.countries = [];
                    $scope.countriesInfoAvailable = "Error";
                }
                else {
                    $scope.countries = response.data;
                    $scope.mapObject.data = mainCtrl.getMapData();
                    $scope.countriesInfoAvailable = "Available";
                }
            }

            function countriesFecthError(response) {
                $scope.countriesInfoAvailable = "Error";
            }

            function countriesFecthException(exception) {
                $scope.countriesInfoAvailable = "Error";
            }


            $scope.selectCountry = function (alphaCode, name) {
                var newCountryCode = alphaCode;
                var updatedData = [];

                $scope.documents = [];

                if ($scope.selectedCountryCode == alphaCode) {
                    newCountryCode = null;
                }

                var oldSelection = $scope.selectedCountryCode;



                $scope.selectedCountryCode = newCountryCode;
                if (newCountryCode) {

                    updatedData[newCountryCode] = { "fillKey": "Selected" };
                    $scope.selectedCountry = name;

                }
                else {
                    $scope.selectedCountry = null;

                }

                if (oldSelection) {
                    var fillkey = "defaultFill";

                    for (var i = 0; i < $scope.countries.length; ++i) {
                        if ($scope.countries[i].aplha3_Code == oldSelection) {
                            fillkey = "Available";
                            break;
                        }
                    }

                    updatedData[oldSelection] = { "fillKey": fillkey };
                }

                $scope.mapAPI.updateWithData(updatedData);
                $scope.mapAPI.setSelectedCountry(newCountryCode, oldSelection);

                if (newCountryCode) {
                    $scope.popupVM.docAvailable = false;

                    $http({
                        method: "GET",
                        url: "/api/documents/" + newCountryCode,

                    }).success(function (data, status, header, config) {
                        console.log("Data requested for:" + config.url.substring(15) + "     cuurent Selection:" + newCountryCode);
                        if (config.url.substring(15) == newCountryCode) {
                            $scope.popupVM.docAvailable = true;
                            $scope.documents = data;
                            $scope.documentCount = $scope.documents.length;
                        }

                    }).error(function (data, status, header, config) {
                        $scope.popupVM.docAvailable = false;
                        $scope.IsEdelCountry = false;
                    });
                }
            };


            $scope.mapAPI = {};
            $scope.geoSelected = function (geography) {

                var newCountryCode = geography.id;
                var newCountryName = geography.properties.name;


                this.selectCountry(newCountryCode, newCountryName);

                $scope.$apply();
            };



            $scope.selectCounty = function (country) {
                this.selectCountry(country.aplha3_Code, country.name);
            };




            $scope.mapObject = {
                scope: 'world',
                //responsive : true,
                options: {
                    width: 1024,
                    selectable: true,
                    staticGeoData: true,
                    legendHeight: 60 // optionally set the padding for the legend
                },
                geographyConfig: {
                    hideHawaiiAndAlaska: true,
                    highlighBorderColor: '#B7B7B7',
                    highlighBorderWidth: 2,
                    borderColor: '#001a59',
                    highlightFillColor: function (geo) {
                        return geo['fillColor'] || '#01b0f3';

                    },
                    popupTemplate: function (geography, data) {

                        var hoverTemplate = '<div class="hoverinfo"><strong>' + geography.properties.name + '</strong> </div>';

                        if ($scope.selectedCountry) {
                            $scope.popupVM.IsEdelCountry = false;
                            for (var i = 0; i < $scope.countries.length; ++i) {
                                if ($scope.countries[i].aplha3_Code == geography.id) {
                                    $scope.popupVM.IsEdelCountry = true;
                                    break;
                                }
                            }
                            return $scope.popupTemplate;

                        }
                        else {
                            return hoverTemplate;
                        }
                    }

                },
                fills: {
                    'Available': '#2279fa',
                    'defaultFill': '#2b3781',
                    'Selected': '#24ffa1'
                }
                ,
                data: this.getMapData()
            };
        }]);
})();
