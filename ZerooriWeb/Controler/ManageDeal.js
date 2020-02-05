angular.module("ZerooriApp", ['ngCookies']).controller("ManageDeal", function ($scope, $http, $cookies) {


    $scope.urlArray = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    $scope.Page = 'manage-deal';
    $scope.CurrentURL = $scope.urlArray[0];
    $scope.PrevPage = '';
    $scope.SessionId = '';
    $scope.LoginStatusCaption = "";
    $scope.ISLogin = true;
    $scope.ISSignINStatus = false;
    $scope.ISSignOutStatus = false;
    $scope.EnablePostAds = false;

    $scope.Dealcol = {};

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

    $scope.SelectedData.PackDealMastID = $cookies.get("MYPROD");

    $http({
        method: 'POST',
        url: 'ManageDeal.ashx',
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
                    url: 'ManageDeal.ashx',
                    params: {
                        LoadInitM: JSON.stringify($scope.SelectedData)
                    }
                }).then(function successCallback(response) {

                    if ($scope.isValidSave(response)) {
                        $scope.UserName = "Hi, " + response.data.UserData.FistNam;
                    }    // User Login Mode

                    $scope.Dealcol = response.data.Dealcol;

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

   

    $scope.DeleteDeal = function (DealMastId) {
        $scope.SelectedData.DealMastID = DealMastId
        if (($scope.SessionId != null && $scope.SessionId != "" && DealMastId != null)) {
            try {
                $http({
                    method: 'POST',
                    url: 'ManageDeal.ashx',
                    params: {
                        DeleteDeal: JSON.stringify($scope.SelectedData)
                    }
                }).then(function successCallback(response) {

                    alert('Delete Successfully');
                   
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

    $scope.LoadDeal = function (DealMastId) {

        $scope.SelectedData.DealMastID = DealMastId

       

        if (($scope.SessionId != null && $scope.SessionId != "" && DealMastId != null)) {
            try {
                $http({
                    method: 'POST',
                    url: 'ManageDeal.ashx',
                    params: {
                        LoadInitDeal: JSON.stringify($scope.SelectedData)
                    }
                }).then(function successCallback(response) {
                    
                    $cookies.put("DEALID", DealMastId);
                    $scope.navigate("add-deal");


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

   
    $scope.SetDealData = function (response) {

        $scope.SelectedData.DealName = response.data.DealName;
        $scope.SelectedData.Price = response.data.Price;
        $scope.SelectedData.Description = response.data.Description;
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
