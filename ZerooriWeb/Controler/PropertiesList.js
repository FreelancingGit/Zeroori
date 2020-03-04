﻿angular.module("ZerooriApp", ['ngCookies']).controller("PropertiesList", function ($scope, $http, $cookies,$filter) {
    $scope.urlArray = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    $scope.Page = 'propertieslist';
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
    $scope.SelectedData = {
        Category: {},
        Location: {},
        SortBy: {},
        PageNo: 1
    }
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
    $scope.PropDataCol = {};
    $scope.CatagoryCol = {};
    $scope.SortByCol = {};
    $scope.LocationCol = {};
    $scope.SelectedPage = 0;
    $scope.PageCount = [];
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
    $scope.SortPropertiesList = function () {
        console.log($scope.SelectedData.SortBy);
        $scope.LoadData();
    };
    $scope.FilterLocation = function () {
        console.log($scope.SelectedData.Location)
        $scope.LoadData();
    };
    $scope.SelectedOption = function (item) {
        console.log(item.Category);
        $scope.SelectedData.Category = JSON.parse(angular.toJson(item.Category));
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
        url: 'PropertiesList.ashx',
        params: {
            GetC: JSON.stringify($scope.ViewData)
        }
    }).then(function successCallback(response) {
        if ($scope.isValidSave(response)) {
            $scope.ZaKey = response.data.UserData.ZaBase.ZaKey;
            $scope.SessionId = $cookies.get($scope.ZaKey);
            $scope.ViewData.ZaBase.SessionId = $cookies.get($scope.ZaKey);
            $scope.LoadInit();
            $scope.query = $cookies.get('searchID');
            $scope.search();
        }
    }, function errorCallback(response) {
        alert(response);
    });
    $scope.ShowPage = function (PageNum) {
        var PageNo = $scope.SelectedPage == 0 ? 1 : $scope.SelectedPage;
        if (PageNum == 'L') {
            PageNo = PageNo - 1;
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
        try {
            $http({
                method: 'POST',
                url: 'PropertiesList.ashx',
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
                    $scope.ViewData.Email = '';
                    $scope.ViewData.Passwd = '';
                    $scope.PropDataCol = response.data.PropDataCol;
                    $scope.CatagoryCol = response.data.CatagoryCol;
                    $scope.SortByCol = response.data.SortByCol;
                    $scope.LocationCol = response.data.LocationCol;

                    $scope.filterdata();
                    $scope.SelectedData.SortBy = response.data.SortByCol[0];
                    $scope.SelectedData.Location = response.data.LocationCol[0];
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
                    $scope.NavOne = PageNo + 0;
                    if (PageNo + 1 <= TotalPages) {
                        $scope.NavTwo = PageNo + 1;
                        $scope.NavTwoVis = true;
                    } else {
                        $scope.NavTwo = '';
                        $scope.NavTwoVis = false;
                    }
                    if (PageNo + 2 <= TotalPages) {
                        $scope.NavThree = PageNo + 2;
                        $scope.NavThreeVis = true;
                    } else {
                        $scope.NavThree = '';
                        $scope.NavThreeVis = false;
                    }
                    if (PageNo + 3 <= TotalPages) {
                        $scope.NavFour = PageNo + 3;
                        $scope.NavFourVis = true;
                    }
                    else {
                        $scope.NavFour = '';
                        $scope.NavFourVis = false;
                    }
                    if (PageNo + 4 <= TotalPages) {
                        $scope.NavFive = PageNo + 4;
                        $scope.NavFiveVis = true;
                    }
                    else {
                        $scope.NavFive = '';
                        $scope.NavFiveVis = false;
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
                url: 'PropertiesList.ashx',
                params: {
                    LoadData: JSON.stringify($scope.SelectedData)
                }
            }).then(function successCallback(response) {
                if ($scope.isValidSave(response)) {
                    $scope.isLoading = true;
                    $scope.PropDataCol = response.data.PropDataCol;
                    if (response.data.PageNoCol.length > 0) {
                        var PageNo = parseInt(response.data.PageNoCol[0].DisPlyMembr);
                        var TotalPages = response.data.PageNoCol[0].ValMembr;
                        var start = 1;
                        if (PageNo > 2) start = PageNo - 2
                        var total = 5;
                        if (TotalPages < 5) total = TotalPages;
                        $scope.filterdata();
                        $scope.PageCount = new Array(total);
                        for (var i = 1; i <= total; i++) {
                            $scope.PageCount[i - 1] = start;
                            start = start + 1;
                        }
                        $scope.NavOne = PageNo + 0;
                        if (PageNo + 1 <= TotalPages) {
                            $scope.NavTwo = PageNo + 1;
                            $scope.NavTwoVis = true;
                        } else {
                            $scope.NavTwo = '';
                            $scope.NavTwoVis = false;
                        }
                        if (PageNo + 2 <= TotalPages) {
                            $scope.NavThree = PageNo + 2;
                            $scope.NavThreeVis = true;
                        } else {
                            $scope.NavThree = '';
                            $scope.NavThreeVis = false;
                        }
                        if (PageNo + 3 <= TotalPages) {
                            $scope.NavFour = PageNo + 3;
                            $scope.NavFourVis = true;
                        }
                        else {
                            $scope.NavFour = '';
                            $scope.NavFourVis = false;
                        }
                        if (PageNo + 4 <= TotalPages) {
                            $scope.NavFive = PageNo + 4;
                            $scope.NavFiveVis = true;
                        }
                        else {
                            $scope.NavFive = '';
                            $scope.NavFiveVis = false;
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
    $scope.ShowItemDetail = function (PropAdMastId) {
        $cookies.put("PropID", PropAdMastId);
        $scope.navigate("propertieslistdetail");
    }

    $scope.$watch('FilterData.Category.PropSpecDtlId', function (newValue, oldValue) {
        if (!$scope.isLoading && newValue != "")
            $scope.LoadData();
    });

    $scope.GetUserInfo = function (Mode) {
        if (($scope.SessionId != null && $scope.SessionId != "") || (Mode == "UL" && $scope.isValidLoginData())) {
            try {
                $http({
                    method: 'POST',
                    url: 'PropertiesList.ashx',
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
                    url: 'PropertiesList.ashx',
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

    //new code for search
    $scope.filterdata = function () {
        // init
        $scope.filteredItems = [];
        $scope.groupedItems = [];
        $scope.itemsPerPage = 6;
        $scope.pagedItems = [];
        $scope.currentPage = 0;
        $scope.items = $scope.PropDataCol;

        var searchMatch = function (haystack, needle) {
            if (!needle) {
                return true;
            }
            console.log(haystack);
            $cookies.remove('searchID');
            return haystack.toLowerCase().indexOf(needle.toLowerCase()) !== -1;
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
            var last = end > (start + 5) ? (start + 5) : end;
            start = last <= 5 ? 0 : start;
            for (var i = start; i < last; i++) {
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
