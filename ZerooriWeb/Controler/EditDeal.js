angular.module("ZerooriApp", ['ngCookies']).controller("EditDeal", function ($scope, $http, $cookies) {


    $scope.urlArray = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    $scope.Page = 'edit-deal';
    $scope.CurrentURL = $scope.urlArray[0];
    $scope.PrevPage = '';
    $scope.SessionId = '';
    $scope.LoginStatusCaption = "";
    $scope.ISLogin = true;
    $scope.ISSignINStatus = false;
    $scope.ISSignOutStatus = false;
    $scope.EnablePostAds = false;


    $scope.SelectedData = {

        PackDealMastId: "",
        DealMastId: "",
        DealName: "",
        Price: "",
        Descrptn: "",
        StartDt: "",
        EndDt: "",
        UserData: {
            ZaBase: {
                SessionId: ''
            }
        }
    }


    if ($scope.urlArray.length > 1) {
        $scope.PrevPage = $scope.urlArray[0].replace('url=', '');
    }

    $scope.SelectedData.PackDealMastId = $cookies.get("MYPROD");
    $http({
        method: 'POST',
        url: 'EditDeal.ashx',
        params: {
            GetC: JSON.stringify($scope.SelectedData)
        }
    }).then(function successCallback(response) {
        if ($scope.isValidSave(response)) {
            $scope.ZaKey = response.data.UserData.ZaBase.ZaKey;
            $scope.SessionId = $cookies.get($scope.ZaKey);
            $scope.SelectedData.UserData.ZaBase.SessionId = $cookies.get($scope.ZaKey);
            $scope.LoadInit();
        }
    }, function errorCallback(response) {
        alert(response);
        });


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


    $scope.LoadInit = function (Mode) {
        if (($scope.SessionId != null && $scope.SessionId != "")) {
            try {
                $http({
                    method: 'POST',
                    url: 'EditDeal.ashx',
                    params: {
                        LoadInit: JSON.stringify($scope.SelectedData)
                    }
                }).then(function successCallback(response) {

                    if ($scope.isValidSave(response)) {
                        $scope.UserName = "Hi, " + response.data.UserData.FistNam;
                    }    // User Login Mode

                    //$scope.SetSaveData(response)

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
    }

  
    $scope.SaveData = function () {
        if (($scope.SessionId != null && $scope.SessionId != "")) {
            try {
                
                $http({
                    method: 'POST',
                    url: 'EditDeal.ashx',
                    params: {
                        SaveData: JSON.stringify($scope.SelectedData)
                    }
                }).then(function successCallback(response) {

                    alert('Save Successfully');
                    $scope.SetSaveData(response)
                    
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
    }

    $scope.SetSaveData = function (response) {

        $scope.SelectedData.DealName = response.data.DealName;
        $scope.SelectedData.Price = response.data.Price;
        $scope.SelectedData.Descrptn = response.data.Descrptn;
        $scope.SelectedData.StartDt = response.data.StartDt;
        $scope.SelectedData.EndDt = response.data.EndDt;
    }




    $scope.isValidSave = function (response) {
        if (response.data.UserData.ZaBase != undefined
            && response.data.UserData.ZaBase.ErrorMsg != undefined
            && response.data.UserData.ZaBase.ErrorMsg.trim() != "") {

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
