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

    $scope.GetUserInfo = function (Mode) {

        if (($scope.SessionId != null && $scope.SessionId != "") || (Mode == "UL" && $scope.isValidLoginData())) {
            try {
                $http({
                    method: 'POST',
                    url: 'jobsHiringlisting.ashx',
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

                    $scope.ViewData.FistNam = "";
                    $scope.SessionId = "";
                    $cookies.remove($scope.ZaKey);
                    $scope.SetStatus(false);

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
