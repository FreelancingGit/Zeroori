angular.module("ZerooriApp", ['ngCookies']).controller("forgotpassword", function ($scope, $http, $cookies) {
$scope.urlArray = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
$scope.Page = 'forgotpassword';
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
url: 'forgotpassword.ashx',
params: {
GetC: JSON.stringify($scope.ViewData)
}
}).then(function successCallback(response) {
if ($scope.isValidSave(response)) {
$scope.ZaKey = response.data.UserData.ZaBase.ZaKey;
$scope.SessionId = $cookies.get($scope.ZaKey);
$scope.ViewData.ZaBase.SessionId = $cookies.get($scope.ZaKey);
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
$scope.SendPwd = function () {
try {
if ($scope.IsValid()) {
$http({
method: 'POST',
url: 'forgotpassword.ashx',
params: {
SendPwd: JSON.stringify($scope.ViewData)
}
}).then(function successCallback(response) {
if ($scope.isValidSave(response)) {
$scope.navigate($scope.CurrentURL ); 
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
$scope.IsValid = function () {
if ($scope.ViewData.Email == undefined || $scope.ViewData.Email == null || $scope.ViewData.Email.trim() == "") {
alert('Please Enter Email Address');
return false;
}
return true
}
$scope.isValidSave = function (response) {
if (response.data.UserData == undefined
|| response.data.UserData.ZaBase == undefined
|| response.data.UserData.ZaBase.ErrorMsg == undefined
|| response.data.UserData.ZaBase.ErrorMsg.trim() != "") {
alert(response.data.UserData.ZaBase.ErrorMsg);
return false;
}
return true
}
$scope.navigate = function (URL) {
URL = URL.replace('url=', '');
URL = URL.replace('.html', '');
url = URL + '.html?url=' + $scope.Page;
$(location).attr('href', url);
};
})
