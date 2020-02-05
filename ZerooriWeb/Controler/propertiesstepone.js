angular.module("ZerooriApp", ['ngCookies']).controller("propertiesstepone", function ($scope, $http, $cookies) {
$scope.urlArray = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
$scope.Page = 'propertiesstepone';
$scope.CurrentURL = $scope.urlArray[0];
$scope.PrevPage = '';
$scope.SessionId = '';
$scope.LoginStatusCaption = "";
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
    
$http({
method: 'POST',
url: 'propertiesstepone.ashx',
params: {
GetC: JSON.stringify($scope.ViewData)
}
}).then(function successCallback(response) {
if ($scope.isValidSave(response)) {
$scope.ZaKey = response.data.UserData.ZaBase.ZaKey;
$scope.SessionId = $cookies.get($scope.ZaKey);
$scope.ViewData.ZaBase.SessionId = $cookies.get($scope.ZaKey);
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
url: 'propertiesstepone.ashx',
params: {
LoadUser: JSON.stringify($scope.ViewData)
}
}).then(function successCallback(response) {
if ($scope.isValidSave(response)) {
$scope.LoginStatusCaption ="Hi, "+ response.data.UserData.ZaBase.UserName;
}    // User Login Mode
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
$scope.ShowPwd = function (Name) {
if ('password' == $('#' + Name).attr('type')) {
$('#' + Name).prop('type', 'text');
} else {
$('#' + Name).prop('type', 'password');
}
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
