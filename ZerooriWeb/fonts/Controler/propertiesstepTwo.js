angular.module("ZerooriApp", ['ngCookies']).controller("propertiesstepTwo", function ($scope, $http, $cookies) {
$scope.urlArray = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
$scope.Page = 'propertiesstepTwo';
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
Title : '',
Bedroom: {},
BathRoom: {},
Size: {},
Furnished: {},
ApartmentFor: {},
RentIsPaid: {},
ListedBy: {},
Category: {},
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
$scope.ShowPwd = function (Name) {
if ('password' == $('#' + Name).attr('type')) {
$('#' + Name).prop('type', 'text');
} else {
$('#' + Name).prop('type', 'password');
}
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
//$scope.EnablePostAds = false;
}
}
$scope.SetStatus(false);
$http({
method: 'POST',
url: 'propertiessteptwo.ashx',
params: {
GetC: JSON.stringify($scope.ViewData)
}
}).then(function successCallback(response) {
if ($scope.isValidSave(response)) {
$scope.ZaKey = response.data.UserData.ZaBase.ZaKey;
$scope.SessionId = $cookies.get($scope.ZaKey);
$scope.ViewData.ZaBase.SessionId = $cookies.get($scope.ZaKey);
if ($scope.ViewData.ZaBase.SessionId == undefined) {
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
url: 'propertiessteptwo.ashx',
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
$scope.ViewData.BedroomCol = response.data.BedroomCol;
$scope.ViewData.BathRoomCol = response.data.BathRoomCol;
$scope.ViewData.SizeCol = response.data.SizeCol;
$scope.ViewData.FurnishedCol = response.data.FurnishedCol;
$scope.ViewData.ApartmentForCol = response.data.ApartmentForCol;
$scope.ViewData.RentIsPaidCol = response.data.RentIsPaidCol;
$scope.ViewData.ListedByCol = response.data.ListedByCol;
$scope.ViewData.CategoryCol = response.data.CategoryCol;
                       
$scope.SelectedData.Bedroom = response.data.BedroomCol[0];
$scope.SelectedData.BathRoom = response.data.BathRoomCol[0];
$scope.SelectedData.Size = response.data.SizeCol[0];
$scope.SelectedData.Furnished = response.data.FurnishedCol[0];
$scope.SelectedData.ApartmentFor = response.data.ApartmentForCol[0];
$scope.SelectedData.RentIsPaid = response.data.RentIsPaidCol[0];
$scope.SelectedData.ListedBy = response.data.ListedByCol[0];
$scope.SelectedData.Category = response.data.CategoryCol[0];
                         
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
url: 'propertiessteptwo.ashx',
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
$scope.DoSave = function () {
try {
if ($scope.isValidData()) {
$scope.SelectedData.UserData.ZaBase.SessionId = $scope.ViewData.ZaBase.SessionId;
$http({
method: 'POST',
url: 'propertiessteptwo.ashx',
params: {
DoSave: JSON.stringify($scope.SelectedData)
}
}).then(function successCallback(response) {
if ($scope.isValidSave(response) && response.data.UserData.ZaBase.SessionId != "") {
$cookies.remove($scope.ZaKey);
$cookies.put($scope.ZaKey, $scope.SessionId);
$cookies.put('FlI', response.data.PropADMastID);
$scope.navigate('propertiesstepthree');
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
if ($scope.SelectedData.Title == '') {
alert("Please Enter Title");
return false;
}
else if ($scope.SelectedData.Bedroom.PropSpecDtlId == -1) {
alert("Please Select Bed Room");
return false;
}
else if ($scope.SelectedData.BathRoom.PropSpecDtlId == -1) {
alert("Please Select Bath Room");
return false;
}
else if ($scope.SelectedData.Size.PropSpecDtlId == -1) {
alert("Please Select Square Feet");
return false;
}
else if ($scope.SelectedData.Furnished.PropSpecDtlId == -1) {
alert("Please Select Furnished");
return false;
}
else if ($scope.SelectedData.ApartmentFor.PropSpecDtlId == -1) {
alert("Please Select Apartment For");
return false;
}
else if ($scope.SelectedData.RentIsPaid.PropSpecDtlId == -1) {
alert("Please Select Rent Or Paid");
return false;
}
else if ($scope.SelectedData.ListedBy.PropSpecDtlId == -1) {
alert("Please Select Listed By");
return false;
}
else if ($scope.SelectedData.Category.PropSpecDtlId == -1) {
alert("Please Select Category");
return false;
} 
else if ($scope.SelectedData.Description == '') {
alert("Please Enter Description");
return false;
}
         
return true;
        
}
   
     
$scope.navigate = function (URL) {
url = URL + '.html?url=' + $scope.Page;
$(location).attr('href', url);
};
})
 