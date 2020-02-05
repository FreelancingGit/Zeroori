angular.module("ZerooriApp", ['ngCookies']).controller("classifiedsstepone", function ($scope, $http, $cookies) {
$scope.urlArray = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
$scope.Page = 'classifiedsstepone';
$scope.CurrentURL = $scope.urlArray[0];
$scope.PrevPage = '';
$scope.SessionId = '';
$scope.LoginStatusCaption = "SIGN IN";
$scope.ISLogin = true;
$scope.ISSignINStatus = false;
$scope.ISSignOutStatus = false;
$scope.EnablePostAds = false;
$scope.SelectedData = {
Description: '',
Title: '',
Category: {},
SubCategory: {},
Age: {},
Usage: {},
Condition: {},
Warranty: {},
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
$(document).ready(function () {
$("#myModal").on('hide.bs.modal', function () {
if ($scope.ViewData.ZaBase == null || $scope.ViewData.ZaBase.SessionId == null) {
this.modal("show");
}
});
});
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
//$scope.EnablePostAds = false;
}
}
$scope.SetStatus(false);
$http({
method: 'POST',
url: 'classifiedsstepone.ashx',
params: {
GetC: JSON.stringify($scope.ViewData)
}
}).then(function successCallback(response) {
if ($scope.isValidSave(response)) {
$scope.ZaKey = response.data.UserData.ZaBase.ZaKey;
$scope.SessionId = $cookies.get($scope.ZaKey);
$scope.ViewData.ZaBase.SessionId = $cookies.get($scope.ZaKey);
if ($scope.ViewData.ZaBase.SessionId == null) {
$('#myModal').modal('show');
}
else
$scope.GetUserInfo("FL");
}
}, function errorCallback(response) {
alert(response);
});
$scope.GetUserInfo = function (Mode) {
if (($scope.SessionId != null && $scope.SessionId != "") || (Mode == "UL" && $scope.isValidLoginData())) {
try {
$http({
method: 'POST',
url: 'classifiedsstepone.ashx',
params: {
LoadUser: JSON.stringify($scope.ViewData)
}
}).then(function successCallback(response) {
if ($scope.isValidSave(response) && response.data.UserData.ZaBase.SessionId != "") {
$scope.SetStatus(true);
$scope.LoginStatusCaption = "Hi, " + response.data.UserData.ZaBase.UserName;
$scope.ViewData.Email = '';
$scope.ViewData.Passwd = '';
$scope.ViewData.FistNam = response.data.UserData.ZaBase.UserName;
$scope.SessionId = response.data.UserData.ZaBase.SessionId;
$scope.ViewData.ZaBase.SessionId = response.data.UserData.ZaBase.SessionId;
$cookies.remove($scope.ZaKey);
$cookies.put($scope.ZaKey, $scope.SessionId);
//$('#myModal').modal("hide");
$scope.ViewData.CategoryCol = response.data.CategoryCol;
$scope.ViewData.SubCategoryCol = response.data.SubCategoryCol;
$scope.ViewData.AgeCol = response.data.AgeCol;
$scope.ViewData.UsageCol = response.data.UsageCol;
$scope.ViewData.ConditionCol = response.data.ConditionCol;
$scope.ViewData.WarrantyCol = response.data.WarrantyCol;
$scope.SelectedData.Category = response.data.CategoryCol[0];
$scope.SelectedData.SubCategory = response.data.SubCategoryCol[0];
$scope.SelectedData.Age = response.data.AgeCol[0];
$scope.SelectedData.Usage = response.data.UsageCol[0];
$scope.SelectedData.Condition = response.data.ConditionCol[0];
$scope.SelectedData.Warranty = response.data.WarrantyCol[0];
$('#myModal').modal('hide');
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
}
$scope.SignIN = function () {
if (!$scope.ISLogin) {
$('#myModal').modal('show');
}
}
$scope.SignOut = function () {
if ($scope.ISLogin) {
try {
$http({
method: 'POST',
url: 'classifiedsstepone.ashx',
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
$scope.ShowPwd = function (Name) {
if ('password' == $('#' + Name).attr('type')) {
$('#' + Name).prop('type', 'text');
} else {
$('#' + Name).prop('type', 'password');
}
}
$scope.DoSave = function () {
try {
if ($scope.isValidData()) {
$scope.SelectedData.UserData.ZaBase.SessionId = $scope.ViewData.ZaBase.SessionId;
$http({
method: 'POST',
url: 'classifiedsstepone.ashx',
params: {
DoSave: JSON.stringify($scope.SelectedData)
}
}).then(function successCallback(response) {
if ($scope.isValidSave(response) && response.data.UserData.ZaBase.SessionId != "") {
$cookies.remove($scope.ZaKey);
$cookies.put($scope.ZaKey, $scope.SessionId);
$cookies.put('FlI', response.data.ClasifdADMastID);
$scope.navigate('classifiedssteptwo');
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
if ($scope.SelectedData.Category.ClasifdSpecDtlId == -1) {
alert("Please Select Category");
return false;
}
//else if ($scope.SelectedData.SubCategory.ClasifdSpecDtlId == -1) {
//    alert("Please Select Bath Room");
//    return false;
//} 
else if ($scope.SelectedData.Age.ClasifdSpecDtlId == -1) {
alert("Please Select Age");
return false;
}
else if ($scope.SelectedData.Usage.ClasifdSpecDtlId == -1) {
alert("Please Select Usage");
return false;
}
else if ($scope.SelectedData.Condition.ClasifdSpecDtlId == -1) {
alert("Please Select Condition");
return false;
}
else if ($scope.SelectedData.Warranty.ClasifdSpecDtlId == -1) {
alert("Please Select Warranty");
return false;
}
else if ($scope.SelectedData.Description == '') {
alert("Please Enter Description");
return false;
}
else if ($scope.SelectedData.Title == '') {
alert("Please Enter Title");
return false;
}
return true;
}
$scope.navigate = function (URL) {
url = URL + '.html?url=' + $scope.Page;
$(location).attr('href', url);
};
})
