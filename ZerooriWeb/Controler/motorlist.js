angular.module("ZerooriApp", ['ngCookies']).controller("motorlist", function ($scope, $http, $cookies) {
    $scope.urlArray = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    $scope.Page = 'motorlist';
    $scope.CurrentURL = $scope.urlArray[0];
    $scope.PrevPage = '';
    $scope.SessionId = '';
    $scope.LoginStatus = "SIGN IN";
    $scope.SignUPStats = true;
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
    $http({
        method: 'POST',
        url: 'motorlist.ashx',
        params: {
            GetC: JSON.stringify($scope.ViewData)
        }
    }).then(function successCallback(response) {
        if ($scope.isValidSave(response)) {
            $scope.ZaKey = response.data.UserData.ZaBase.ZaKey;
            $scope.SessionId = $cookies.get($scope.ZaKey);
            $scope.ViewData.ZaBase.SessionId = $cookies.get($scope.ZaKey);
            $scope.GetUserInfo("LO");

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
    $scope.PostAdd = function () {
        if ($scope.SignUPStats) {
            $('#myModal').modal('show');
            $scope.EnablePostAds = true;
        }
        else {
            $scope.GetUserInfo('UL');
            if ($scope.SignUPStats)
                $scope.navigate('postyouradd')
            else
                alert('Please Login');
        }
    }
    $scope.GetUserInfo = function (Mode) {
        if ($scope.SessionId != null && $scope.SessionId != "") {
            try {
                $http({
                    method: 'POST',
                    url: 'motorlist.ashx',
                    params: {
                        LoadUser: JSON.stringify($scope.ViewData)
                    }
                }).then(function successCallback(response) {
                    if ($scope.isValidSave(response) && response.data.UserData.ZaBase.SessionId != "") {
                        $scope.LoginStatus = response.data.UserData.ZaBase.UserName;
                        $scope.SignUPStats = false;
                        $scope.ViewData.Email = '';
                        $scope.ViewData.Passwd = '';
                        $scope.ViewData.FistNam = response.data.UserData.ZaBase.UserName;
                        $scope.SessionId = response.data.UserData.ZaBase.SessionId;
                        $scope.ViewData.ZaBase.SessionId = response.data.UserData.ZaBase.SessionId;
                        $cookies.remove($scope.ZaKey);
                        $cookies.put($scope.ZaKey, $scope.SessionId);
                    }
                    // User Login Mode
                    else if (Mode == "UL" && response.data.UserData.ZaBase.SessionId == "") {
                        alert('Invalid User Name Or password');
                    }
                }, function errorCallback(response) {
                    alert(response);
                });
            }
            catch (err) {
                alert(err);
            }
        }
        if ($scope.EnablePostAds) {
            if ($scope.SignUPStats)
                $scope.navigate('postyouradd')
            else
                alert('Please Login');
        }
    }
    $scope.SignIN = function () {
        if (!$scope.ISLogin) {
            $('#myModal').modal('show');
        }
    }
    $scope.SignOut = function () {
        if (!$scope.SignUPStats) {
            try {
                $http({
                    method: 'POST',
                    url: 'motorlist.ashx',
                    params: {
                        SignOut: JSON.stringify($scope.ViewData)
                    }
                }).then(function successCallback(response) {
                    if ($scope.isValidSave(response)) {
                        $scope.LoginStatus = "Sign IN";
                        $scope.SignUPStats = true;
                        $scope.ViewData.FistNam = "";
                        $scope.SessionId = "";
                        $cookies.remove($scope.ZaKey);
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

    $scope.isValidSave = function (response) {
        if (response.data.UserData.ZaBase.ErrorMsg.trim() != "") {
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
