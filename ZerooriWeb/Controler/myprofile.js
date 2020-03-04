angular.module("ZerooriApp", ['ngCookies']).controller("myprofile", function ($scope, $http, $cookies) {
    $scope.urlArray = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    $scope.Page = 'my-profile';
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
        DOB: "",
        OldPasswd: "",
        Passwd: "",
        Cpasswd: "",
        UsrMastID: "",
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
    $(document).ready(function () {
        $("#myModal").on('hide.bs.modal', function () {
            if ($scope.ViewData.ZaBase.SessionId == undefined || $scope.ViewData.ZaBase.SessionId == null || $scope.ViewData.ZaBase.SessionId == '') {
                this.modal("show");
            }
        });
    });
    $http({
        method: 'POST',
        url: 'myprofile.ashx',
        params: {
            GetC: JSON.stringify($scope.ViewData)
        }
    }).then(function successCallback(response) {
        if ($scope.isValidSave(response)) {
            $scope.ZaKey = response.data.ZaBase.ZaKey;
            $scope.ViewData.Passwd = "";
            $scope.ViewData.ZaBase.SessionId = $cookies.get($scope.ZaKey);

            if ($scope.ViewData.ZaBase.SessionId == undefined || $scope.ViewData.ZaBase.SessionId == null || $scope.ViewData.ZaBase.SessionId == '') {
                $('#myModal').modal('show');
            }
            else {
                $('#myModal').modal('hide');
                $scope.GetUserInfo("FL");
            }
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
    $scope.GetUserInfo = function (Mode) {
        try {
            $http({
                method: 'POST',
                url: 'myprofile.ashx',
                params: {
                    LoadUser: JSON.stringify($scope.ViewData)
                }
            }).then(function successCallback(response) {
                if ($scope.isValidSave(response)) {
                    if (response.data.FistNam != undefined && response.data.FistNam != null && response.data.FistNam != "") {
                        $scope.SetStatus(true);
                        $scope.UserName = "Hi, " + response.data.ZaBase.UserName;
                        $scope.ViewData.Email = '';
                        $scope.ViewData.Passwd = '';
                        $scope.ViewData.FistNam = response.data.FistNam;
                        $scope.ViewData.LastNam = response.data.LastNam;
                        $scope.ViewData.Email = response.data.Email;
                        $scope.ViewData.Mob = response.data.Mob;
                        $scope.ViewData.Passwd = response.data.Passwd;
                        $scope.ViewData.Cpasswd = response.data.Cpasswd;
                        $scope.ViewData.OldPasswd = response.data.OldPasswd;
                        $scope.ViewData.UsrMastID = response.data.UsrMastID;
                        $scope.SessionId = response.data.ZaBase.SessionId;
                        $scope.ViewData.ZaBase.SessionId = response.data.ZaBase.SessionId;
                        $cookies.remove($scope.ZaKey);
                        $cookies.put($scope.ZaKey, $scope.SessionId);
                        $('#myModal').modal('hide');

                    }
                    else {
                        alert('Invalid username or password');
                    }
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
        if ($scope.EnablePostAds && $scope.ISSignINStatus) {
            $scope.navigate('postyouradd')
        }
    }
    $scope.DoSave = function () {
        try {
            if ($scope.isValidData()) {
                $http({
                    method: 'POST',
                    url: 'myprofile.ashx',
                    params: {
                        DoSave: JSON.stringify($scope.ViewData)
                    }
                }).then(function successCallback(response) {
                    if ($scope.isValidSave(response)) {
                        $scope.ViewData.FistNam = response.data.FistNam;
                        $scope.ViewData.LastNam = response.data.LastNam;
                        $scope.ViewData.Email = response.data.Email;
                        $scope.ViewData.Mob = response.data.Mob;
                        alert('Update Successfully');
                    }
                }, function errorCallback(response) {
                    alert(response);
                });
            }
        }
        catch (err) {
            alert(err);
        }
    }
    $scope.DoUpdatePwd = function () {
        try {
            if ($scope.isValidData()) {
                $http({
                    method: 'POST',
                    url: 'myprofile.ashx',
                    params: {
                        DoChangePwd: JSON.stringify($scope.ViewData)
                    }
                }).then(function successCallback(response) {
                    if ($scope.isValidSave(response)) {
                        $scope.ViewData.FistNam = response.data.FistNam;
                        $scope.ViewData.LastNam = response.data.LastNam;
                        $scope.ViewData.Email = response.data.Email;
                        $scope.ViewData.Mob = response.data.Mob;
                        alert('Update Successfully');
                    }
                }, function errorCallback(response) {
                    alert(response);
                });
            }
        }
        catch (err) {
            alert(err);
        }
    }
    $scope.isValidData = function () {
        if ($scope.ViewData.ZaBase.SessionId == undefined || $scope.ViewData.ZaBase.SessionId == null || $scope.ViewData.ZaBase.SessionId == '') {
            $('#myModal').modal('show');
            return false;
        }
        else if ($scope.ViewData.FistNam == '') {
            alert("Please Enter Fist Name");
            return false;
        }
        else if ($scope.ViewData.LastNam == '') {
            alert("Please Enter Last Name");
            return false;
        }
        else if ($scope.ViewData.Email == '') {
            alert("Please Enter Email Address");
            return false;
        }
        else if ($scope.ViewData.Mob == '') {
            alert("Please Enter Mobile Number");
            return false;
        }
        else if ($scope.ViewData.Passwd.trim() == "") {
            alert('Please Enter a valid Password');
            return false;
        }
        else if ($scope.ViewData.Cpasswd.trim() == "") {
            alert('Please Enter a Confirm Password');
            return false;
        }
        else if ($scope.ViewData.OldPasswd.trim() == "") {
            alert('Please Enter a Old Password');
            return false;
        }
        else if ($scope.ViewData.Cpasswd.trim() != $scope.ViewData.Passwd.trim()) {
            alert('Password Mismatch');
            return false;
        }
        return true;
    }
    $scope.SignIN = function () {
        if (!$scope.ISLogin) {
            $('#myModal').modal('show');
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
                    url: 'myprofile.ashx',
                    params: {
                        SignOut: JSON.stringify($scope.ViewData)
                    }
                }).then(function successCallback(response) {
                    if ($scope.isValidSave(response)) {
                        $scope.ViewData.FistNam = "";
                        $scope.ViewData.LastNam = "";
                        $scope.ViewData.Email = "";
                        $scope.ViewData.Mob = "";
                        $scope.ViewData.Passwd = "";
                        $scope.ViewData.Cpasswd = "";
                        $scope.ViewData.OldPasswd = "";
                        $scope.ViewData.UsrMastID = "";
                        $scope.SessionId = "";
                        $scope.ViewData.ZaBase.SessionId = "";
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
        if (response.data.ZaBase != undefined
            && response.data.ZaBase.ErrorMsg != undefined
            && response.data.ZaBase.ErrorMsg.trim() != "") {
            alert(response.data.ZaBase.ErrorMsg);
            return false;
        }
        return true
    }
    $scope.navigate = function (URL) {
        url = URL + '.html?url=' + $scope.Page;
        $(location).attr('href', url);
    };
})
