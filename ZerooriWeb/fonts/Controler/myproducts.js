angular.module("ZerooriApp", ['ngCookies']).controller("myproducts", function ($scope, $http, $cookies) {
$scope.urlArray = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
$scope.Page = 'my-products';
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
$scope.ViewData = {
FistNam: "",
LastNam: "",
Email: "",
Mob: "",
Passwd: "",
Cpasswd: "",
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
$scope.LoginStatusCaption ="Account";
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
url: 'myproducts.ashx',
params: {
GetC: JSON.stringify($scope.ViewData)
}
}).then(function successCallback(response) {
if ($scope.isValidSave(response)) {
$scope.ZaKey = response.data.UserData.ZaBase.ZaKey;
$scope.SessionId = $cookies.get($scope.ZaKey);
$scope.ViewData.UserData.ZaBase.SessionId = $cookies.get($scope.ZaKey);
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
try {
$http({
method: 'POST',
url: 'myproducts.ashx',
params: {
Init: JSON.stringify($scope.ViewData)
}
}).then(function successCallback(response) {
if ($scope.isValidSave(response)) {
if (response.data.UserData.FistNam != "") {
$scope.SetStatus(true);
$scope.UserName = "Hi, " + response.data.UserData.FistNam;
$scope.ViewData.FistNam = response.data.UserData.FistNam;
}
else
$scope.SetStatus(false);
$scope.ViewData.Email = '';
$scope.ViewData.Passwd = '';
$scope.BusinessCol = response.data.Business;
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
//manage-page.html
$scope.ShowBusiness = function (UsrBusinesMastId) {
$cookies.put("MADID", UsrBusinesMastId);
$scope.navigate("manage-page");
}
$scope.PostAdd = function () {
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
url: 'myproducts.ashx',
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
})
