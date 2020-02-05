angular.module("ZerooriApp", ['ngCookies']).controller("SignUP", function ($scope, $http, $cookies) {
$scope.urlArray = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
$scope.Page = 'Signup';
$scope.CurrentURL = $scope.urlArray[0];
$scope.PrevPage = 'index';
$scope.SessionId = '';
$scope.ViewData = {
FistNam: "",
LastNam: '',
Email: '',
Mob: "",
AcceptAgrement: false,
Passwd: "",
Cpasswd: "",
ZaBase: {
SessionId: ''
}
}
$scope.ShowPwd = function (Name) {
if ('password' == $('#' + Name).attr('type')) {
$('#' + Name).prop('type', 'text');
} else {
$('#' + Name).prop('type', 'password');
}
}
if ($scope.urlArray[0].indexOf("url=") != -1) {
$scope.PrevPage = $scope.urlArray[0].replace('url=', '');
}
$(document).ready(function () {
$("#myModal").on('hide.bs.modal', function () {
this.modal("show");
});
});
$scope.SaveLogin = function () {
try {
if ($scope.IsValid()) {
$http({
method: 'POST',
url: 'signup.ashx',
params: {
SaveOrder: JSON.stringify($scope.ViewData)
}
}).then(function successCallback(response) {
if ($scope.isValidSave(response)) {
$scope.ViewData.FistNam = response.data.ZaBase.UserName;
$scope.SessionId = response.data.ZaBase.SessionId;
$scope.ViewData.ZaBase.SessionId = response.data.ZaBase.SessionId;
$scope.ZaKey = response.data.ZaBase.ZaKey;
$cookies.remove($scope.ZaKey);
$('#myModal').modal('show');
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
$scope.OtpVerfy = function () {
try {
if ($scope.IsValid()) {
$http({
method: 'POST',
url: 'signup.ashx',
params: {
VeiFyOtp: JSON.stringify($scope.ViewData)
}
}).then(function successCallback(response) {
if ($scope.isValidSave(response)) {
$scope.ViewData.FistNam = response.data.ZaBase.UserName;
$scope.SessionId = response.data.ZaBase.SessionId;
$scope.ZaKey = response.data.ZaBase.ZaKey;
$cookies.remove($scope.ZaKey);
$cookies.put($scope.ZaKey, $scope.SessionId);
$scope.navigate('');
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
if (!$scope.ViewData.AcceptAgrement) {
alert('Please accept the terms & conditions');
return false;
}
else if ($scope.ViewData.FistNam.trim() == "") {
alert('Please Enter First Name');
return false;
}
else if ($scope.ViewData.LastNam.trim() == "") {
alert('Please Enter Last Name');
return false;
}
else if ($scope.ViewData.Email == undefined || $scope.ViewData.Email == null || $scope.ViewData.Email.trim() == "") {
alert('Please Enter Email Address');
return false;
}
         
else if ($scope.ViewData.Mob  == undefined || $scope.ViewData.Mob  == null  ) {
alert('Please Enter Mobile No');
return false;
}
//else if (!$scope.pattern.test($scope.ViewData.Email)) {
//    alert('not a valid e-mail address');
//}​
else if ($scope.ViewData.Passwd.trim() == "") {
alert('Please Enter a valid Password');
return false;
}
else if ($scope.ViewData.Cpasswd.trim() == "") {
alert('Please Enter a Confirm Password');
return false;
}
else if ($scope.ViewData.Cpasswd.trim() != $scope.ViewData.Passwd.trim()) {
alert('Password Mismatch');
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
if (URL == '') {
URL = $scope.PrevPage;
}
url = URL + '.html?url=' + $scope.Page;
$(location).attr('href', url);
};
})
