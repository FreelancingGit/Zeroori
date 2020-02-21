angular.module("ZerooriApp", ['ngCookies']).controller("frelncwantedlisting", function ($scope, $http, $cookies,$filter) {

    $scope.urlArray = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    $scope.Page = 'frelnc-wanted-listing';

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

    $scope.EmpJobCol = {};
    $scope.ReportypCol = {};
    $scope.IndstryCol = {};
    $scope.PageCount = [];
    $scope.SelectedData = {
        PageNo: 1,
        Reportyp: {
            DisPlyMembr: '',
            ValMembr: ''
        },
        UserData:
        {
            FistNam: '',
            ZaBase: {
                SessionId: ''
            }
        },
        IndstryCol: {
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
        url: 'frelncwantedlisting.ashx',
        params: {
            GetC: JSON.stringify($scope.SelectedData)
        }
    }).then(function successCallback(response) {

        if ($scope.isValidSave(response)) {

            $scope.ZaKey = response.data.UserData.ZaBase.ZaKey;
            $scope.SessionId = $cookies.get($scope.ZaKey);
            $scope.SelectedData.UserData.ZaBase.SessionId = $cookies.get($scope.ZaKey);
            $scope.LoadInit();
            $scope.query = $cookies.get('searchID');
            $scope.search();
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


        $scope.SelectedData.PageNo = PageNo;
        $scope.LoadData();
    }
    
    $scope.ShowItemDetail = function (EmpjobmastId) {

        $cookies.put("MADID", EmpjobmastId);
        $scope.navigate("frelnc-wanted-listing-detail");
    }

    $scope.showtemp = function () {
        if ($scope.SelectedData.Reportyp.ValMembr == 0 && $scope.Page == 'frelnc-wanted-listing')
            $scope.navigate("frelnc-hiring-listing");

    }

    $scope.LoadInit = function (Mode) {
        
        try {
            $http({
                method: 'POST',
                url: 'frelncwantedlisting.ashx',
                params: {
                    Init: JSON.stringify($scope.SelectedData)
                }
            }).then(function successCallback(response) {


                if ($scope.isValidSave(response)) {
                    $scope.isLoading = true;
                    if (response.data.UserData.FistNam != "") {
                        $scope.SetStatus(true);
                        $scope.SelectedData.UserData.ZaBase.SessionId = $cookies.get($scope.ZaKey);
                        $scope.UserName = "Hi, " + response.data.UserData.FistNam;
                        $scope.SelectedData.UserData.FistNam = response.data.UserData.FistNam;
                    }
                    else
                        $scope.SetStatus(false);

                   
                    $scope.EmpJobCol = response.data.EmpJobCol;
                    $scope.ReportypCol = response.data.ReportypCol;
                    $scope.IndstryCol = response.data.IndustryCol;
                    console.log(response.data.IndustryCol);
                    $scope.SelectedData.Reportyp = response.data.ReportypCol[1];
                    $scope.filterdata();
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
                url: 'frelncwantedlisting.ashx',
                params: {
                    LoadData: JSON.stringify($scope.SelectedData)
                }
            }).then(function successCallback(response) {

                if ($scope.isValidSave(response)) {
                    $scope.isLoading = true;


                    if (response.data.PageNoCol.length > 0) {

                        $scope.EmpJobCol = response.data.EmpJobCol;
                        $scope.filterdata();
                        var start = 1;
                        if (PageNo > 2) start = PageNo - 2
                        var total = 5;
                        if (TotalPages < 5) total = TotalPages;
                        $scope.PageCount = new Array(total);
                        for (var i = 1; i <= total; i++) {
                            $scope.PageCount[i - 1] = start;
                            start = start + 1;
                        }
                        $scope.isLoading = false;

                    } // User Login Mode
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

    $scope.GetUserInfo = function (Mode) {

        if (($scope.SessionId != null && $scope.SessionId != "") || (Mode == "UL" && $scope.isValidLoginData())) {
            try {
                $http({
                    method: 'POST',
                    url: 'frelncwantedlisting.ashx',
                    params: {
                        LoadUser: JSON.stringify($scope.SelectedData)
                    }
                }).then(function successCallback(response) {

                    if ($scope.isValidSave(response) && response.data.UserData.ZaBase.SessionId != "") {

                        $scope.isLoading = true;
                        if (response.data.UserData.FistNam != "") {
                            $scope.SetStatus(true);

                            $scope.UserName = "Hi, " + response.data.UserData.FistNam;
                            $scope.SelectedData.FistNam = response.data.UserData.FistNam;
                        }
                        else
                            $scope.SetStatus(false);


                        //$scope.ViewData.Email = '';
                        //$scope.ViewData.Passwd = '';
                        //$scope.ViewData.FistNam = response.data.UserData.ZaBase.UserName;
                        $scope.SessionId = response.data.UserData.ZaBase.SessionId;
                        $scope.SelectedData.ZaBase.SessionId = response.data.UserData.ZaBase.SessionId;

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

    $scope.SignOut = function () {

        if ($scope.ISLogin) {
            try {
                $http({
                    method: 'POST',
                    url: 'frelncwantedlisting.ashx',
                    params: {
                        SignOut: JSON.stringify($scope.SelectedData)
                    }
                }).then(function successCallback(response) {

                    if ($scope.isValidSave(response)) {
                        //$scope.ViewData.FistNam = "";
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

        if ($scope.SelectedData.Email.trim() == "") {
            alert('Please Enter Email Address');
            return false;
        }
        if ($scope.SelectedData.Passwd.trim() == "") {
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

    //new code for search
    $scope.filterdata = function () {
        // init
        $scope.filteredItems = [];
        $scope.groupedItems = [];
        $scope.itemsPerPage = 6;
        $scope.pagedItems = [];
        $scope.currentPage = 0;
        $scope.items = $scope.EmpJobCol;

        var searchMatch = function (haystack, needle) {
            if (!needle) {
                return true;
            }
            console.log(haystack);
            return haystack.toString().toLowerCase().indexOf(needle.toLowerCase()) !== -1;
        };

        // init the filtered items
        $scope.search = function () {
            $scope.filteredItems = $filter('filter')($scope.items, function (item) {
                for (var attr in item) {
                    if (searchMatch(item[attr], $scope.query))
                        return true;
                }
                return false;
            });

            $scope.currentPage = 0;
            // now group by pages
            $scope.groupToPages();
        };

        // calculate page in place
        $scope.groupToPages = function () {
            $scope.pagedItems = [];

            for (var i = 0; i < $scope.filteredItems.length; i++) {
                if (i % $scope.itemsPerPage === 0) {
                    $scope.pagedItems[Math.floor(i / $scope.itemsPerPage)] = [$scope.filteredItems[i]];
                } else {
                    $scope.pagedItems[Math.floor(i / $scope.itemsPerPage)].push($scope.filteredItems[i]);
                }
            }
        };

        $scope.range = function (start, end) {
            var ret = [];
            if (!end) {
                end = start;
                start = 0;
            }
            for (var i = start; i < end; i++) {
                ret.push(i);
            }
            return ret;
        };

        $scope.prevPage = function () {
            if ($scope.currentPage > 0) {
                $scope.currentPage--;
            }
        };

        $scope.nextPage = function () {
            if ($scope.currentPage < $scope.pagedItems.length - 1) {
                $scope.currentPage++;
            }
        };

        $scope.setPage = function () {
            $scope.currentPage = this.n;
        };

        // functions have been describe process the data for display
        $scope.search();


    };
})
