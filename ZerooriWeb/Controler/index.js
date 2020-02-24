﻿angular.module("ZerooriApp", ['ngCookies']).controller("Index", function ($scope, $http, $cookies) {
    $scope.urlArray = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    $scope.Page = 'Index';
    $scope.CurrentURL = $scope.urlArray[0];
    $scope.PrevPage = '';
    $scope.SessionId = '';
    $scope.LoginStatusCaption = "SIGN IN";
    $scope.UserName = "Welcome";
    $scope.ISLogin = true;
    $scope.ISSignINStatus = false;
    $scope.ISSignOutStatus = false;
    $scope.EnablePostAds = false;
    $scope.ViewData = {
        FistNam: "",
        LastNam: "",
        Email: "",
        Mob: "",
        Passwd: "",
        Cpasswd: "",
        ZaBase: {
            SessionId: ''
        }
    }
    if ($scope.urlArray.length > 1) {
        $scope.PrevPage = $scope.urlArray[0].replace('url=', '');
    }
    $scope.SetStatus = function (ISSignIN) {
        if (ISSignIN) {
            $scope.LoginStatusCaption = "Account";
            $scope.ISLogin = true;
            $scope.ISSignINStatus = true;
            $scope.ISSignOutStatus = false;
            //$scope.EnablePostAds = true;
        }
        else {
            $scope.LoginStatusCaption = "Sign IN";
            $scope.ISLogin = false;
            $scope.ISSignINStatus = false;
            $scope.ISSignOutStatus = true;
            $scope.UserName = "Welcome";
            //$scope.EnablePostAds = false;
        }
    }
    $scope.SetStatus(false);
    $http({
        method: 'POST',
        url: 'Index.ashx',
        params: {
            GetC: JSON.stringify($scope.ViewData)
        }
    }).then(function successCallback(response) {
        if ($scope.isValidSave(response)) {
            $scope.ZaKey = response.data.UserData.ZaBase.ZaKey;
            $scope.SessionId = $cookies.get($scope.ZaKey);
            $scope.ViewData.ZaBase.SessionId = $cookies.get($scope.ZaKey);
            $scope.GetUserInfo("FL");
        }
    }, function errorCallback(response) {
        alert(response);
    });
    $scope.ShowPwd = function (Name) {
        if ('password' == $('#' + Name).attr('type')) {
            $('#' + Name).prop('type', 'text');
        } else {
            $('#' + Name).prop('type', 'password');
        }
    }
    $scope.ShowE = function (search) {
        //Navigate to the corresponding page with search string
        $cookies.put("searchID", search);
        var category = $scope.CategorySearch;
        
        if (category == 'Motors')
            $scope.navigate("motorlist");
        else if (category == 'Classifieds')
            $scope.navigate("classifiedslist");
        else if (category == 'Properties')
            $scope.navigate("propertieslist");
        else if (category == 'Jobs')
            $scope.navigate("jobs-hiring-listing");
        else if (category == 'Deals')
            $scope.navigate("deals-list");
        else if (category == 'Brands')
            $scope.navigate("brandslist");
        else if (category == 'Freelance')
            $scope.navigate("frelnc-hiring-listing");
        else if (category == 'Directory')
            $scope.navigate("directorylist");
        else if (category == 'Malls')
            $scope.navigate("malls");
        else if (category == undefined)
            alert("Please select category");
    }
    $scope.GetUserInfo = function (Mode) {
        if (($scope.SessionId != null && $scope.SessionId != "") || (Mode == "UL" && $scope.isValidLoginData())) {
            try {
                $http({
                    method: 'POST',
                    url: 'Index.ashx',
                    params: {
                        LoadUser: JSON.stringify($scope.ViewData)
                    }
                }).then(function successCallback(response) {
                    if ($scope.isValidSave(response) && response.data.UserData.ZaBase.SessionId != "") {
                        $scope.SetStatus(true);
                        //  $scope.LoginStatusCaption = 
                        $scope.UserName = "Hi, " + response.data.UserData.ZaBase.UserName;
                        $scope.ViewData.Email = '';
                        $scope.ViewData.Passwd = '';
                        $scope.ViewData.FistNam = response.data.UserData.ZaBase.UserName;
                        $scope.SessionId = response.data.UserData.ZaBase.SessionId;
                        $scope.ViewData.ZaBase.SessionId = response.data.UserData.ZaBase.SessionId;
                        $cookies.remove($scope.ZaKey);
                        $cookies.put($scope.ZaKey, $scope.SessionId);
                        $('#myModal').modal("hide");
                        if ($scope.EnablePostAds && $scope.ISSignINStatus) {
                            $scope.navigate('postyouradd')
                        }
                    } // User Login Mode
                    else if (Mode == "UL" && response.data.UserData.ZaBase.SessionId == "") {
                        alert('Invalid User Name Or password');
                    }
                }, function errorCallback(response) {
                    alert(response);
                    $scope.SetStatus(false);
                });
            }
            catch (err) {
                $scope.SetStatus(false);
                alert(err);
            }
        }
        if ($scope.EnablePostAds && $scope.ISSignINStatus) {
            $scope.navigate('postyouradd')
        }
    }
    $scope.PostAdd = function () {
        $scope.EnablePostAds = true;
        if (!$scope.ISLogin) {
            $('#myModal').modal('show');
        }
        else {
            $scope.navigate('postyouradd')
        }
    }
    $scope.SignIN = function () {
        if (!$scope.ISLogin) {
            $('#myModal').modal('show');
        }
    }
    $scope.SignOut = function () {

        if ($scope.ISLogin) {
            try {
                $http({
                    method: 'POST',
                    url: 'Index.ashx',
                    params: {
                        SignOut: JSON.stringify($scope.ViewData)
                    }
                }).then(function successCallback(response) {
                    if ($scope.isValidSave(response)) {
                        $scope.ViewData.FistNam = "";
                        $scope.SessionId = "";
                        $cookies.remove($scope.ZaKey);
                        $scope.SetStatus(false);
                    }
                }, function errorCallback(response) {
                    alert(response);
                });
            }
            catch (err) {
                alert(err);
            }
        }
    }
    $scope.isValidLoginData = function () {
        if ($scope.ViewData.Email == undefined || $scope.ViewData.Email == null || $scope.ViewData.Email.trim() == "") {
            alert('Please Enter Email Address');
            return false;
        }
        else if ($scope.ViewData.Email.trim() == "") {
            alert('Please Enter Email Address');
            return false;
        }
        if ($scope.ViewData.Passwd.trim() == "") {
            alert('Please Enter Password');
            return false;
        }
        return true
    }
    $scope.isValidSave = function (response) {

        if (response.data.UserData.ZaBase == undefined
            || response.data.UserData.ZaBase.ErrorMsg == undefined
            || response.data.UserData.ZaBase.ErrorMsg.trim() != "") {
            alert(response.data.UserData.ZaBase.ErrorMsg);
            return false;
        }
        return true
    }

    $scope.navigate = function (URL) {
        url = URL + '.html?url=' + $scope.Page;
        $(location).attr('href', url);
    };
})
