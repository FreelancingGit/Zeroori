angular.module("ZerooriApp", ['ngCookies']).controller("motorosstepthree", function ($scope, $http, $cookies) {

$scope.urlArray = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
$scope.Page = 'motorosstepthree';

$scope.CurrentURL = $scope.urlArray[0];
$scope.PrevPage = '';
$scope.SessionId = '';
$scope.LoginStatusCaption = "";
$scope.SignUPStats = true;
$scope.picFile = 'images/cameras.png';
$scope.LocationCol = {};


$scope.LoginStatusCaption = "SIGN IN";
$scope.UserName = "Welcome";
$scope.ISLogin = true;
$scope.ISSignINStatus = false;
$scope.ISSignOutStatus = false;
$scope.EnablePostAds = false;


$scope.ViewData = {
AddMotorsADMastID: "",
AdSeq: "",
PHNo: "",
Price: "",
Location: {},
UserData: {
ZaBase: {
SessionId: ''
}
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
$scope.UserName = "Welcome";
//$scope.EnablePostAds = false;
}
}

$scope.SetStatus(false);


   


$(function () { $("#upload_link1").on('click', function (e) { e.preventDefault(); $("#upload1:hidden").trigger('click'); }); });
$(function () { $("#upload_link2").on('click', function (e) { e.preventDefault(); $("#upload2:hidden").trigger('click'); }); });
$(function () { $("#upload_link3").on('click', function (e) { e.preventDefault(); $("#upload3:hidden").trigger('click'); }); });
$(function () { $("#upload_link4").on('click', function (e) { e.preventDefault(); $("#upload4:hidden").trigger('click'); }); });
$(function () { $("#upload_link5").on('click', function (e) { e.preventDefault(); $("#upload5:hidden").trigger('click'); }); });
$(function () { $("#upload_link6").on('click', function (e) { e.preventDefault(); $("#upload6:hidden").trigger('click'); }); });
$(function () { $("#upload_link7").on('click', function (e) { e.preventDefault(); $("#upload7:hidden").trigger('click'); }); });
$(function () { $("#upload_link8").on('click', function (e) { e.preventDefault(); $("#upload8:hidden").trigger('click'); }); });
$(function () { $("#upload_link9").on('click', function (e) { e.preventDefault(); $("#upload9:hidden").trigger('click'); }); });
$(function () { $("#upload_link10").on('click', function (e) { e.preventDefault(); $("#upload10:hidden").trigger('click'); }); });
$(function () { $("#upload_link11").on('click', function (e) { e.preventDefault(); $("#upload11:hidden").trigger('click'); }); });
$(function () { $("#upload_link12").on('click', function (e) { e.preventDefault(); $("#upload12:hidden").trigger('click'); }); });
$(function () { $("#upload_link13").on('click', function (e) { e.preventDefault(); $("#upload13:hidden").trigger('click'); }); });
$(function () { $("#upload_link14").on('click', function (e) { e.preventDefault(); $("#upload14:hidden").trigger('click'); }); });
$(function () { $("#upload_link15").on('click', function (e) { e.preventDefault(); $("#upload15:hidden").trigger('click'); }); });

$(document).ready(function () { $("#upload1").change(function () { var File = this.files; if (File && File[0]) { $scope.ReadImage(File[0], '#Imgupload1', "#upload1", 1); } }) })
$(document).ready(function () { $("#upload2").change(function () { var File = this.files; if (File && File[0]) { $scope.ReadImage(File[0], '#Imgupload2', "#upload2", 2); } }) })
$(document).ready(function () { $("#upload3").change(function () { var File = this.files; if (File && File[0]) { $scope.ReadImage(File[0], '#Imgupload3', "#upload3", 3); } }) })
$(document).ready(function () { $("#upload4").change(function () { var File = this.files; if (File && File[0]) { $scope.ReadImage(File[0], '#Imgupload4', "#upload4", 4); } }) })
$(document).ready(function () { $("#upload5").change(function () { var File = this.files; if (File && File[0]) { $scope.ReadImage(File[0], '#Imgupload5', "#upload5", 5); } }) })
$(document).ready(function () { $("#upload6").change(function () { var File = this.files; if (File && File[0]) { $scope.ReadImage(File[0], '#Imgupload6', "#upload6", 6); } }) })
$(document).ready(function () { $("#upload7").change(function () { var File = this.files; if (File && File[0]) { $scope.ReadImage(File[0], '#Imgupload7', "#upload7", 7); } }) })
$(document).ready(function () { $("#upload8").change(function () { var File = this.files; if (File && File[0]) { $scope.ReadImage(File[0], '#Imgupload8', "#upload8", 8); } }) })
$(document).ready(function () { $("#upload9").change(function () { var File = this.files; if (File && File[0]) { $scope.ReadImage(File[0], '#Imgupload9', "#upload9", 9); } }) })
$(document).ready(function () { $("#upload10").change(function () { var File = this.files; if (File && File[0]) { $scope.ReadImage(File[0], '#Imgupload10', "#upload10", 10); } }) })
$(document).ready(function () { $("#upload11").change(function () { var File = this.files; if (File && File[0]) { $scope.ReadImage(File[0], '#Imgupload11', "#upload11", 11); } }) })
$(document).ready(function () { $("#upload12").change(function () { var File = this.files; if (File && File[0]) { $scope.ReadImage(File[0], '#Imgupload12', "#upload12", 12); } }) })
$(document).ready(function () { $("#upload13").change(function () { var File = this.files; if (File && File[0]) { $scope.ReadImage(File[0], '#Imgupload13', "#upload13", 13); } }) })
$(document).ready(function () { $("#upload14").change(function () { var File = this.files; if (File && File[0]) { $scope.ReadImage(File[0], '#Imgupload14', "#upload14", 14); } }) })
$(document).ready(function () { $("#upload15").change(function () { var File = this.files; if (File && File[0]) { $scope.ReadImage(File[0], '#Imgupload15', "#upload15", 15); } }) })

$scope.RemoveImage = function (Seq) {
var ImageName = '#Imgupload' + Seq
$(ImageName).attr('src', 'images/camera.png');

$scope.ViewData.AdSeq = Seq;
$('#myModal').modal('show');

$http({
method: 'POST',
url: 'motorosstepthree.ashx',
params: {
RemoveImg: JSON.stringify($scope.ViewData)
}
}).then(function successCallback(response) {
$('#myModal').modal('hide');
if ($scope.isValidSave(response)) {

}

}, function errorCallback(response) {
alert(response);
$('#myModal').modal('hide');
});
}



$scope.ShowPwd = function (Name) {
if ('password' == $('#' + Name).attr('type')) {
$('#' + Name).prop('type', 'text');
} else {
$('#' + Name).prop('type', 'password');
}
}
    


$http({
method: 'POST',
url: 'motorosstepthree.ashx',
params: {
GetC: JSON.stringify($scope.ViewData)
}
}).then(function successCallback(response) {

if ($scope.isValidSave(response)) {
$scope.ZaKey = response.data.UserData.ZaBase.ZaKey;
$scope.SessionId = $cookies.get($scope.ZaKey);
             

$scope.ViewData.UserData.ZaBase.SessionId = $cookies.get($scope.ZaKey);
$scope.ViewData.AddMotorsADMastID = $cookies.get('FlI');

if ($scope.ViewData.UserData.ZaBase.SessionId == undefined) {
$scope.navigate('index')
}
else
$scope.Onload();
}
}, function errorCallback(response) {
alert(response);
});
     

$scope.Onload = function (Mode) {
try {
$http({
method: 'POST',
url: 'motorosstepthree.ashx',
params: {
Onload: JSON.stringify($scope.ViewData)
}
}).then(function successCallback(response) {


if ($scope.isValidSave(response)) {
                     
if (response.data.UserData != undefined && response.data.UserData.FistNam != "") {
$scope.SetStatus(true);
$scope.UserName = "Hi, " + response.data.UserData.FistNam;


$scope.ViewData.FistNam = response.data.UserData.FistNam;
}
else
$scope.SetStatus(false);


$scope.LocationCol = response.data.LocationCol;

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
$scope.ReadImage = function (file, ImgSrcNam, UploadFileName, Seq) {
$('#myModal').modal('show');
$scope.Uploadimage(UploadFileName, Seq, ImgSrcNam);
}


$scope.Uploadimage = function (UploadFileName, Seq, ImgSrcNam) {

var file = $(UploadFileName).get(0).files;
$scope.ViewData.AdSeq = Seq;

$http({
method: 'POST',
url: 'motorosstepthree.ashx',
data: file[0],
params: {
SaveImg: JSON.stringify($scope.ViewData)
}
}).then(function successCallback(response) {

if ($scope.isValidSave(response)) {
$('#myModal').modal('hide');

$.each(response.data.FileNames, function (name, value) {
var pos = value.ValMembr;
var impPath = value.DisPlyMembr

if (pos == 1) { $('#Imgupload1').attr('src', impPath); }
else if (pos == 2) { $('#Imgupload2').attr('src', impPath); }
else if (pos == 3) { $('#Imgupload3').attr('src', impPath); }
else if (pos == 4) { $('#Imgupload4').attr('src', impPath); }
else if (pos == 5) { $('#Imgupload5').attr('src', impPath); }
else if (pos == 6) { $('#Imgupload6').attr('src', impPath); }
else if (pos == 7) { $('#Imgupload7').attr('src', impPath); }
else if (pos == 8) { $('#Imgupload8').attr('src', impPath); }
else if (pos == 9) { $('#Imgupload9').attr('src', impPath); }
else if (pos == 10) { $('#Imgupload10').attr('src', impPath); }
else if (pos == 11) { $('#Imgupload11').attr('src', impPath); }
else if (pos == 12) { $('#Imgupload12').attr('src', impPath); }
else if (pos == 13) { $('#Imgupload13').attr('src', impPath); }
else if (pos == 14) { $('#Imgupload14').attr('src', impPath); }
else if (pos == 15) { $('#Imgupload15').attr('src', impPath); }
});
}
}, function errorCallback(response) {
alert(response);
$('#myModal').modal('hide');
});
};

$scope.UploadPrice = function ( ) {
if ($scope.ViewData.Price == "") {
alert ("Enter A Valid Price")
}
else {
$http({
method: 'POST',
url: 'motorosstepthree.ashx',
params: {
SaveP: JSON.stringify($scope.ViewData)
}
}).then(function successCallback(response) {
if ($scope.isValidSave(response)) {
$scope.navigate('index')
}
}, function errorCallback(response) {
alert(response);
});
}

};

$scope.isValidSave = function (response) {
if (response.data.UserData != undefined
&& response.data.UserData.ZaBase != undefined
&& response.data.UserData.ZaBase.ErrorMsg != undefined
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
   