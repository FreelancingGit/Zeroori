angular.module("ZerooriApp", ['ngCookies']).controller("jobsHiringlisting", function ($scope, $http, $cookies) {

    $scope.urlArray = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    $scope.Page = 'jobs-hiring-listing';

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

    $scope.PageCount = [];
    $scope.CompnyJobCol = {};
    $scope.ReportypCol = {};
    $scope.IndstryCol = {};
    $scope.SelectedPage = 0;
    $scope.SelectedData = {
        PageNo: 1,       
        UserData:
        {
            FistNam: '',
            Email: '',
            Passwd:'',
            ZaBase: {
                SessionId: ''
            }
        },
        IndstryCol: {
        },
        Reportyp: {
            DisPlyMembr: '',
            ValMembr: ''
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

    $scope.SelectedOption = function (item) {
        console.log(item.Indstry);
        $scope.SelectedData.IndstryCol = JSON.parse(angular.toJson(item.Indstry));
        console.log($scope.SelectedData.IndstryCol);
        $scope.LoadData();
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
        url: 'jobsHiringlisting.ashx',
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

    $scope.ShowPage = function (PageNum) {

        var PageNo = $scope.SelectedPage == 0 ? 1 : $scope.SelectedPage;

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

        $scope.SelectedPage = PageNo;
        $scope.SelectedData.PageNo = PageNo;
        $scope.LoadData();

    }



    $scope.ShowItemDetail = function (CompanyJobMastId) {

        $cookies.put("MADID", CompanyJobMastId);
        $scope.navigate("jobs-hiring-detail");
    }

    $scope.showtemp = function () {
        if ($scope.SelectedData.Reportyp.ValMembr == 1 && $scope.Page == 'jobs-hiring-listing')
            $scope.navigate("jobs-wanted-listing");
       
	}

    $scope.LoadInit = function (Mode) {



        try {
            $http({
                method: 'POST',
                url: 'jobsHiringlisting.ashx',
                params: {
                    Init: JSON.stringify($scope.SelectedData)
                }
			}).then(function successCallback(response)
			{
				if ($scope.isValidSave(response)) {
					console.log(response.data);
                    $scope.isLoading = true;
                    if (response.data.UserData.FistNam != "") {
                        $scope.SetStatus(true);
                        $scope.SelectedData.UserData.ZaBase.SessionId = $cookies.get($scope.ZaKey);
                        $scope.UserName = "Hi, " + response.data.UserData.FistNam;
                        $scope.SelectedData.FistNam = response.data.UserData.FistNam;
                    }
                    else
                        $scope.SetStatus(false);
                    var PageNo = parseInt(response.data.PageNoCol[0].DisPlyMembr);
                    var TotalPages = response.data.PageNoCol[0].ValMembr;
                    var start = 1;
                    if (PageNo > 2) start = PageNo - 2
                    var total = 5;
                    if (TotalPages < 5) total = TotalPages;
                    $scope.PageCount = new Array(total);
                    for (var i = 1; i <= total; i++) {
                        $scope.PageCount[i - 1] = start;
                        start = start + 1;
                    }
                    $scope.CompnyJobCol = response.data.CompnyJobCol;
                    $scope.ReportypCol = response.data.ReportypCol;
                    $scope.IndstryCol = response.data.IndustryCol;
                    $scope.SelectedData.Reportyp = response.data.ReportypCol[0];
                   
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
                url: 'jobsHiringlisting.ashx',
                params: {
                    LoadData: JSON.stringify($scope.SelectedData)
                }
            }).then(function successCallback(response) {

                if ($scope.isValidSave(response)) {
                    $scope.isLoading = true;
                    if (response.data.PageNoCol.length > 0) {

                        $scope.EmpJobCol = response.data.EmpJobCol;

                        var PageNo = parseInt(response.data.PageNoCol[0].DisPlyMembr);
                        var TotalPages = response.data.PageNoCol[0].ValMembr;
                        $scope.CompnyJobCol = response.data.CompnyJobCol;
                        var start = 1;
                        if (PageNo > 2) start = PageNo - 2
                        var total = 5;
                        if (TotalPages < 5) total = TotalPages;
                        $scope.PageCount = new Array(total);
                        for (var i = 1; i <= total; i++) {
                            $scope.PageCount[i - 1] = start;
                            start = start + 1;
                        }
                    }
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
                    url: 'jobsHiringlisting.ashx',
                    params: {
                        SignOut: JSON.stringify($scope.SelectedData)
                    }
                }).then(function successCallback(response) {

                    if ($scope.isValidSave(response)) {
                        $scope.ViewData.FistNam = "";
                        $scope.ViewData.Passwd = "";
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
