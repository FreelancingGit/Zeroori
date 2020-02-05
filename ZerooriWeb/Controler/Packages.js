﻿angular.module("ZerooriApp", ['ngCookies']).controller("Packages", function ($scope, $http, $cookies) {

    $scope.urlArray = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    $scope.Page = 'packages';

    $scope.CurrentURL = $scope.urlArray[0];
    $scope.PrevPage = '';
    $scope.SessionId = '';

    $scope.LoginStatusCaption = "SIGN IN";
    $scope.UserName = "Welcome";
    $scope.ISLogin = true;
    $scope.ISSignINStatus = false;
    $scope.ISSignOutStatus = false;
    $scope.EnablePostAds = false;
    $scope.Products = null;
    $scope.Otp = '';


    $scope.ShowOpt = true;
    $scope.SowSubcs = false;


    $scope.NavOne = 1;
    $scope.NavTwo = 2;
    $scope.NavThree = 3;
    $scope.NavFour = 4;
    $scope.NavFive = 5;

    $scope.NavOneVis = true;
    $scope.NavTwoVis = true;
    $scope.NavThreeVis = true;
    $scope.NavFourVis = true;
    $scope.NavFiveVis = true;
    $scope.isLoading = false;


    $scope.SilverMast ,
    $scope.GoldMast ,
    $scope.PlatinumMast,

    $scope.SilverCol = {};
    $scope.GoldCol = {};
    $scope.PlatinumCol = {};

    $scope.ViewData = {
        UserName: "",
        Email: "",
        Passwd:"",

        ZaBase: {
            SessionId: '',
            Fld: '',
            UserName: '',
            PKID : ''
        }
    }


    if ($scope.urlArray.length > 1) {
        $scope.PrevPage = $scope.urlArray[0].replace('url=', '');
    }

    $scope.SetStatus = function (ISSignIN) {

        if (ISSignIN) {
            $scope.LoginStatusCaption = "Sign Out";
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
        url: 'packages.ashx',
        params: {
            GetC: JSON.stringify($scope.ViewData)
        }
    }).then(function successCallback(response) {

        if ($scope.isValidSave(response)) {

            $scope.ZaKey = response.data.UserData.ZaBase.ZaKey;
            $scope.SessionId = $cookies.get($scope.ZaKey);
            $scope.ViewData.ZaBase.SessionId = $cookies.get($scope.ZaKey);
            $scope.LoadInit();
           


        }

    }, function errorCallback(response) {
        alert(response);
    });

    $scope.ShowPage = function (PageNum) {

        var PageNo = 1;

        if (PageNum == 'L') {

            PageNo = PageNo - 2;
        }
        else if (PageNum == 'R') {
            PageNo = PageNo + 1;
        }
        else {
            PageNo = PageNum
        }

        if (PageNo <= 1)
            PageNo = 1


        $scope.ViewData.PageNo = PageNo;
        $scope.LoadData();

    }



    $scope.LoadInit = function (Mode) {
        try {
            $http({
                method: 'POST',
                url: 'packages.ashx',
                params: {
                    Init: JSON.stringify($scope.ViewData)
                }
            }).then(function successCallback(response) {


                if ($scope.isValidSave(response)) {

                    $scope.isLoading = true;

                    if (response.data.UserData.FistNam != "") {
                        $scope.SetStatus(true);

                        $scope.UserName = "Hi, " + response.data.UserData.FistNam;
                        $scope.ViewData.FistNam = response.data.UserData.FistNam;
                    }
                    else
                        $scope.SetStatus(false);

                    $scope.SilverMast = response.data.SilverMast;
                    $scope.GoldMast = response.data.GoldMast;
                    $scope.PlatinumMast = response.data.PlatinumMast;
                    $scope.LoadData();

                    $scope.isLoading = false;
                } // User Login Mode

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

    $scope.LoadData = function () {

        try {
            $http({
                method: 'POST',
                url: 'packages.ashx',
                params: {
                    LoadData: JSON.stringify($scope.ViewData)
                }
            }).then(function successCallback(response) {

                if ($scope.isValidSave(response)) {
                    $scope.isLoading = true;
                    
                    $scope.SilverCol = response.data.SilverCol;
                    $scope.GoldCol = response.data.GoldCol;
                    $scope.PlatinumCol = response.data.PlatinumCol;

                    $scope.ViewData.ZaBase.UserName = '';
                    $scope.ViewData.ZaBase.PKID = '';
                  
                    $scope.isLoading = false;
                } // User Login Mode

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




    $scope.GetUserInfo = function (Mode) {

        if (($scope.SessionId != null && $scope.SessionId != "") || (Mode == "UL" && $scope.isValidLoginData())) {
            try {
                $http({
                    method: 'POST',
                    url: 'packages.ashx',
                    params: {
                        LoadUser: JSON.stringify($scope.ViewData)
                    }
                }).then(function successCallback(response) {

                    if ($scope.isValidSave(response) && response.data.UserData.ZaBase.SessionId != "") {

                        $scope.isLoading = true;
                        if (response.data.UserData.FistNam != "") {
                            $scope.SetStatus(true);

                            $scope.UserName = "Hi, " + response.data.UserData.FistNam;
                            $scope.ViewData.FistNam = response.data.UserData.FistNam;
                        }
                        else
                            $scope.SetStatus(false);


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

    $scope.ShowSubscribe = function (Type) {

        $('#myModal').modal('show');


        if (Type != "")
            $scope.ViewData.ZaBase.Fld = Type;


        if ($scope.ViewData.ZaBase.UserName != undefined && $scope.ViewData.ZaBase.UserName != "" && $scope.ViewData.ZaBase.UserName != null) {
            try {
                $http({
                    method: 'POST',
                    url: 'packages.ashx',
                    params: {
                        Subscrb: JSON.stringify($scope.ViewData)
                    }
                }).then(function successCallback(response) {

                    if ($scope.isValidSave(response)) {
                        $scope.Otp = response.data.Otp;
                        $scope.ShowOpt = false;
                        $scope.SowSubcs = true;
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

    $scope.Subscribe = function () {

        try {

            if ($scope.Otp == $scope.ViewData.ZaBase.PKID) {
                $http({
                    method: 'POST',
                    url: 'packages.ashx',
                    params: {
                        SubscrbA: JSON.stringify($scope.ViewData)
                    }
                }).then(function successCallback(response) {

                    if ($scope.isValidSave(response)) {
                        $scope.ShowOpt = true;
                        $scope.SowSubcs = false;

                        $scope.Otp = '';
                        $scope.ViewData.ZaBase.Fld = '';
                        $('#myModal').modal('hide');
                        alert('Subscribe Successfully');

                        $scope.navigate("index");

                    }
                }, function errorCallback(response) {
                    alert(response);
                });
            }
            else {
                alert('Invalid OTP');
            }
        }
        catch (err) {
            alert(err);
        }

        

    }


    $scope.SignOut = function () {

        if ($scope.ISLogin) {
            try {
                $http({
                    method: 'POST',
                    url: 'packages.ashx',
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

        if ($scope.ViewData.Email.trim() == "") {
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

        if (response.data.UserData.ZaBase != undefined
            && response.data.UserData.ZaBase.ErrorMsg != undefined
            && response.data.UserData.ZaBase.ErrorMsg.trim() != "") {

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