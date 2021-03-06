﻿angular.module("ZerooriApp", ['ngCookies']).controller("motorlist", function ($scope, $http, $cookies) {
$scope.urlArray = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
$scope.Page = 'motorlist';
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
Brand: {},
Model: {},
FuelType: {},
BodyType: {},
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
$scope.SortByCol = {};
$scope.LocationCol = {};
$scope.BrandCol = {};
$scope.ModelCol = {};
$scope.FuelTypeCol = {};
$scope.BodyTypeCol = {};
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
url: 'motorlist.ashx',
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
$scope.ShowPwd = function (Name) {
if ('password' == $('#' + Name).attr('type')) {
$('#' + Name).prop('type', 'text');
} else {
$('#' + Name).prop('type', 'password');
}
}
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
url: 'motorlist.ashx',
params: {
Init: JSON.stringify($scope.ViewData)
}
}).then(function successCallback(response) {
if ($scope.isValidSave(response)) {
$scope.isLoading = true;
if (response.data.UserData != undefined && response.data.UserData.FistNam != "") {
$scope.SetStatus(true);
$scope.UserName = "Hi, " + response.data.UserData.FistNam;
$scope.ViewData.FistNam = response.data.UserData.FistNam;
}
else
$scope.SetStatus(false);
$scope.ViewData.Email = '';
$scope.ViewData.Passwd = '';
$scope.MotorDataCol = response.data.MotorDataCol;
$scope.SortByCol = response.data.SortByCol;
$scope.LocationCol = response.data.LocationCol;
$scope.BrandCol = response.data.BrandCol;
$scope.ModelCol = response.data.ModelCol;
$scope.FuelTypeCol = response.data.FuelTypeCol;
$scope.BodyTypeCol = response.data.BodyTypeCol;
$scope.ColorCol = response.data.ColorCol;
$scope.SelectedData.FuelType = response.data.FuelTypeCol[0];
$scope.SelectedData.BodyType = response.data.BodyTypeCol[0];
$scope.SelectedData.Color = response.data.ColorCol[0];
$scope.SelectedData.Model = response.data.ModelCol[0];
$scope.SelectedData.SortBy = response.data.SortByCol[0];
$scope.SelectedData.Location = response.data.LocationCol[0];
var PageNo = parseInt(response.data.PageNoCol[0].DisPlyMembr);
var TotalPages = response.data.PageNoCol[0].ValMembr;
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
$scope.ShowItemDetail = function (MotorsAdMastId) {
$cookies.put("MADID", MotorsAdMastId);
$scope.navigate("motorlistdetail");
}
$scope.LoadData = function () {
try {
$http({
method: 'POST',
url: 'motorlist.ashx',
params: {
LoadData: JSON.stringify($scope.SelectedData)
}
}).then(function successCallback(response) {
if ($scope.isValidSave(response)) {
$scope.isLoading = true;
$scope.MotorDataCol = response.data.MotorDataCol;
if (response.data.PageNoCol.length > 0) {
var PageNo = parseInt(response.data.PageNoCol[0].DisPlyMembr);
var TotalPages = response.data.PageNoCol[0].ValMembr;
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
$scope.$watch('SelectedData.Category.PropSpecDtlId', function (newValue, oldValue) {
//if (!$scope.isLoading && newValue != "")
//$scope.LoadData();
});
$scope.GetUserInfo = function (Mode) {
if (($scope.SessionId != null && $scope.SessionId != "") || (Mode == "UL" && $scope.isValidLoginData())) {
try {
$http({
method: 'POST',
url: 'motorlist.ashx',
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
url: 'motorlist.ashx',
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
if (response.data.UserData != undefined
&& response.data.UserData.ErrorMsg != undefined
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
