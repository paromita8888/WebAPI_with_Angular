'use strict';

/**
 * @ngdoc overview
 * @name sampleAngularApp
 * @description
 * # sampleAngularApp
 *
 * Main module of the application.
 */
angular
  .module('sampleAngularApp', [
     'ngRoute',
    'datamaps',
    'bsTable',
    'ui.bootstrap',
    'ngAnimate'
  ])
  .config(function ($routeProvider) {
    $routeProvider
      .when('/', {
        templateUrl: 'UI/views/main.html',
        controller: 'MainCtrl',
        controllerAs: 'main'
      })
      .when('/about', {
        templateUrl: 'UI/views/about.html',
        controller: 'AboutCtrl',
        controllerAs: 'about'
      })
        .when('/ExplanatoryDocs', {
            templateUrl: 'UI/views/docsList.html',
            controller: 'documentListCtrl',
            controllerAs: 'documents'

        })
         .when('/ResearchReferenceDocs', {
             templateUrl: 'UI/views/docsList.html',
             controller: 'documentListCtrl',
             controllerAs: 'documents'
         })
         .when('/NewBusinessFiles', {
             templateUrl: 'UI/views/docsList.html',
             controller: 'documentListCtrl',
             controllerAs: 'documents'
         })
        .when('/RegionalSummaries', {
            templateUrl: 'UI/views/docsList.html',
            controller: 'documentListCtrl',
            controllerAs: 'documents'
        })
         .when('/MarketingMaterials', {
             templateUrl: 'UI/views/docsList.html',
             controller: 'documentListCtrl',
             controllerAs: 'documents'
         })
      .otherwise({
        redirectTo: '/'
      });
  });
