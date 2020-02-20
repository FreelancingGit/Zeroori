angular.module("ZerooriApp", ['ngCookies']).controller("ManagePage", function ($scope, $http, $cookies) {


    $scope.urlArray = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    $scope.Page = 'manage-page';
    $scope.CurrentURL = $scope.urlArray[0];
    $scope.PrevPage = '';
    $scope.SessionId = '';
    $scope.LoginStatusCaption = "";
    $scope.ISLogin = true;
    $scope.ISSignINStatus = false;
    $scope.ISSignOutStatus = false;
	$scope.EnablePostAds = false;
	$scope.CategoriesCol = {};
	$scope.LocationCol = {};
	$scope.BannerImage = '';
	$scope.CompanyLogo = '';

    $scope.SelectedData = {
        BusinessName: "",
        URL: "",
        BannerImage: "",
        CompanyLogo: "",
        Facebook: "",
        Instagram: "",
		Twitter: "",
		Category:{},
        PhoneNo: "",
        Email: "",
        Website: "",
		LocationData: {},
		Location:'',
        Description: "",
        DealMastId: "",
        UserData: {
            ZaBase: {
                SessionId: ''
            }
        }
    };

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
            $scope.LoginStatusCaption = "Sign Out";
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

    function readURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#Imgupload1').attr('src', e.target.result);
            }

            reader.readAsDataURL(input.files[0]);
        }
    }
    $("#upload1").change(function () {
        readURL(this);
    });

    function readURL2(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#Imgupload2').attr('src', e.target.result);
            }

            reader.readAsDataURL(input.files[0]);
        }
    }
    $("#upload2").change(function () {
        readURL2(this);
    });



    $scope.PostAdd = function () {
        if (!$scope.ISLogin) {
            $('#myModal').modal('show');
        }
        else {
            $scope.navigate('postyouradd')
        }
    }
    $scope.SelectedData.DealMastID = $cookies.get("MYPROD");
    $http({
        method: 'POST',
        url: 'ManagePage.ashx',
        params: {
            GetC: JSON.stringify($scope.SelectedData)
        }
    }).then(function successCallback(response) {
        if ($scope.isValidSave(response)) {
            $scope.ZaKey = response.data.UserData.ZaBase.ZaKey;
            $scope.SessionId = $cookies.get($scope.ZaKey);
            $scope.SelectedData.UserData.ZaBase.SessionId = $cookies.get($scope.ZaKey);
            $scope.LoadInit();
        }
    }, function errorCallback(response) {
        alert(response);
        });


    $scope.GetUserInfo = function (Mode) {
        if (($scope.SessionId != null && $scope.SessionId != "") || (Mode == "UL" && $scope.isValidLoginData())) {
            try {
                $http({
                    method: 'POST',
                    url: 'ManagePage.ashx',
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
							$scope.navigate('postyouradd');
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

    $scope.ShowPwd = function (Name) {
        if ('password' == $('#' + Name).attr('type')) {
            $('#' + Name).prop('type', 'text');
        } else {
            $('#' + Name).prop('type', 'password');
        }
    }

    $scope.LoadInit = function (Mode) {
        if (($scope.SessionId != null && $scope.SessionId != "")) {
            try {
                $http({
                    method: 'POST',
                    url: 'ManagePage.ashx',
                    params: {
                        LoadInit: JSON.stringify($scope.SelectedData)
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
                    } // User Login Mode

					$scope.SetSaveData(response);

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

	$scope.isValidInputs = function ()
	{
		var flag = true;
		if ($scope.SelectedData.BusinessName == '' || $scope.SelectedData.BusinessName == null) {
			flag = false;
			alert("Please give your bussines name");
			
		}
		else if ($scope.SelectedData.Category.ValMembr == -1) {
			flag = false;
			alert("Please Select category");

		}
		else if ($scope.SelectedData.PhoneNo == '' || $scope.SelectedData.PhoneNo == null) {
			flag = false;
			alert("Please enter phone number");

		}
		else if ($scope.SelectedData.Email == '' || $scope.SelectedData.Email == null) {
			flag = false;
			alert("Please enter Email");

		}
		return flag;
	};

	$scope.SaveData = function ()
	{
		if ($scope.isValidInputs()) {

			if (($scope.SessionId != null && $scope.SessionId != "")) {
				try {
					$scope.SelectedData.location = $scope.SelectedData.locationData.PlaceName;
					var file = $('#upload1').get(0).files;

					$http({
						method: 'POST',
						url: 'ManagePage.ashx',
						data: file[0],
						params: {
							SaveData: JSON.stringify($scope.SelectedData)
						}
					}).then(function successCallback(response) {

						$scope.SaveLogo();
						alert('Save Successfully');
						$(location).attr('href','my-products.html');
						//$scope.SetSaveData(response);


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
    }

    $scope.SaveLogo = function () {
        if (($scope.SessionId != null && $scope.SessionId != "")) {
            try {

                var file = $('#upload2').get(0).files;
                $http({
                    method: 'POST',
                    url: 'ManagePage.ashx',
                    data: file[0],
                    params: {
                        SaveLogo: JSON.stringify($scope.SelectedData)
                    }
                }).then(function successCallback(response) {


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

	$scope.SetSaveData = function (response)
	{

		$scope.SelectedData.BusinessName = response.data.BusinessName;
		$scope.SelectedData.URL = response.data.URL;

		$scope.SelectedData.BannerImage = response.data.BannerImage;
		$scope.SelectedData.CompanyLogo = response.data.CompanyLogo;
		$scope.SelectedData.Facebook = response.data.Facebook;
		$scope.SelectedData.Instagram = response.data.Instagram;
		$scope.SelectedData.Twitter = response.data.Twitter;
		$scope.SelectedData.PhoneNo = response.data.PhoneNo;
		$scope.SelectedData.Email = response.data.Email;
		$scope.SelectedData.Website = response.data.Website;
		$scope.SelectedData.Location = response.data.Location;
		$scope.SelectedData.Description = response.data.Description;
		$scope.CategoriesCol = response.data.CategoriesCol;
		$scope.SelectedData.Category = response.data.CategoriesCol[0];
		$scope.LocationCol = response.data.LocationCol;
		$scope.SelectedData.locationData = response.data.LocationCol[0];
	};
    
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
})
