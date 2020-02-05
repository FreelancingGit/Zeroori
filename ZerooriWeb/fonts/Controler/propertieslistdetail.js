angular.module("ZerooriApp", ['ngCookies']).controller("propertieslistdetail", function ($scope, $http, $cookies) {
    $scope.urlArray = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    $scope.Page = 'propertieslistdetail';
    $scope.CurrentURL = $scope.urlArray[0];
    $scope.PrevPage = '';
    $scope.SessionId = '';
    $scope.LoginStatusCaption = "SIGN IN";
    $scope.ISLogin = true;
    $scope.ISSignINStatus = false;
    $scope.ISSignOutStatus = false;
    $scope.EnablePostAds = false;
    $scope.Products = null;
    $scope.UserName = "Welcome";
    $scope.MADID = "";

    $scope.PropData = {
        PropTitle: '',
        CrtdDt: '',
        BedRoom: '',
        BathRoom: '',
        Size: '',
        IsFurnished: '',
        Appartment: '',
        RentIsPaid: '',
        ListedBy: '',
        Category: '',
        City: '',
        UsrEmail: '',
        UsrPhno: '',
        PropDescription: '',
        PlaceName: '',
        Price: '',
        PropAdMastId: '',
        FileNames: {
            DisPlyMembr: ''
        },
        UserData: {
            ZaBase: {
                SessionId: ''
            }
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
        url: 'Propslistdetail.ashx',
        params: {
            GetC: JSON.stringify($scope.PropData)
        }
    }).then(function successCallback(response) {
        if ($scope.isValidSave(response)) {
            $scope.ZaKey = response.data.UserData.ZaBase.ZaKey;
            $scope.SessionId = $cookies.get($scope.ZaKey);

            $scope.PropData.UserData.ZaBase.SessionId = $cookies.get($scope.ZaKey);
            $scope.MADID = $cookies.get('PropID');

            if ($scope.MADID != undefined
                && $scope.MADID != null) {
                $scope.LoadInit();
            }
        }
    }, function errorCallback(response) {
        alert(response);
    });
    $scope.LoadInit = function () {
        $scope.PropData.PropAdMastId = $scope.MADID;
        try {
            $http({
                method: 'POST',
                url: 'Propslistdetail.ashx',
                params: {
                    Init: JSON.stringify($scope.PropData)
                }
            }).then(function successCallback(response) {
                if ($scope.isValidSave(response)) {
                   
                    if (response.data.UserData != undefined && response.data.UserData.FistNam != "") {
                        $scope.SetStatus(true);
                        $scope.UserName = "Hi, " + response.data.UserData.FistNam;
                    }
                    else
                        $scope.SetStatus(false);
                     
                    $scope.PropData = response.data;

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
    $scope.SignIN = function () {
        if (!$scope.ISLogin) {
            $('#myModal').modal('show');
        }
    }

    $scope.ShowPwd = function (Name) {
        if ('password' == $('#' + Name).attr('type')) {
            $('#' + Name).prop('type', 'text');
        } else {
            $('#' + Name).prop('type', 'password');
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
    $scope.SignOut = function () {
        if ($scope.ISLogin) {
            try {
                $http({
                    method: 'POST',
                    url: 'Propslistdetail.ashx',
                    params: {
                        SignOut: JSON.stringify($scope.MotorData)
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
    $scope.isValidSave = function (response) {
        if (response.data.UserData.ZaBase == undefined
            && response.data.UserData.ZaBase.ErrorMsg == undefined
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
