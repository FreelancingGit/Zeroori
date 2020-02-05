angular.module("ZerooriApp", ['ngCookies']).controller("myAddsJobList", function ($scope, $http, $cookies) {
	$scope.urlArray = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    $scope.Page = 'myAdds-jobslist';
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

    $scope.SelectedData = {
        JobType:"",
        JobMastCol: {},
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
        url: 'myAddsJobslist.ashx',
		params: {
			GetC: JSON.stringify($scope.SelectedData)
		}
	}).then(function successCallback(response) {
		if ($scope.isValidSave(response)) {
			$scope.ZaKey = response.data.UserData.ZaBase.ZaKey;
			$scope.SessionId = $cookies.get($scope.ZaKey);
			$scope.SelectedData.ZaBase.SessionId = $cookies.get($scope.ZaKey);
			$scope.LoadInit();
		}
	}, function errorCallback(response) {
		alert(response);
	});

    $scope.DeleteJobs = function (EmpJobMastId, JobType) {
        $scope.SelectedData.EmpJobMastId = EmpJobMastId;
        $scope.SelectedData.JobType = JobType;
        if ($scope.SessionId != null && $scope.SessionId != "" && EmpJobMastId != null) {
            try {
                $http({
                    method: 'POST',
                    url: 'myAddsJobslist.ashx',
                    params: {
                        DeleteJobJW: JSON.stringify($scope.SelectedData)
                    }
                }).then(function successCallback(response) {

                    alert('Delete Successfully');
                    $scope.LoadData();

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
                url: 'myAddsJobslist.ashx',
				params: {
					Init: JSON.stringify($scope.SelectedData)
				}
			}).then(function successCallback(response) {
				if ($scope.isValidSave(response)) {
					$scope.isLoading = true;
					if (response.data.UserData.FistNam != "") {
						$scope.SetStatus(true);
						$scope.UserName = "Hi, " + response.data.UserData.FistNam;
						$scope.SelectedData.FistNam = response.data.UserData.FistNam;
					}
					else
						$scope.SetStatus(false);
					$scope.SelectedData.Email = '';
					$scope.SelectedData.Passwd = '';
					
                    $scope.JobMastCol = response.data.JobMastCol;
					
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
    $scope.UpdateJobs = function (EmpJobMastId, JobType) {
        if ((EmpJobMastId != undefined || EmpJobMastId != null )&& JobType=='JW') {
            $cookies.put("JW", EmpJobMastId);
            $scope.navigate("jobs-wanted-step-one");
        }
        if ((EmpJobMastId != undefined || EmpJobMastId != null) && JobType == 'JH') {
            $cookies.put("JH", EmpJobMastId);
            $scope.navigate("jobs-hiring-step-one");
        }
        if ((EmpJobMastId != undefined || EmpJobMastId != null) && JobType == 'FW') {
            $cookies.put("FW", EmpJobMastId);
            $scope.navigate("frelnc-wanted-step-one");
        }
        if ((EmpJobMastId != undefined || EmpJobMastId != null) && JobType == 'FH') {
            $cookies.put("FH", EmpJobMastId);
            $scope.navigate("frelnc-hiring-step-one");
        }
	}
	$scope.LoadData = function () {
		try {
			$http({
				method: 'POST',
                url: 'myAddsJobslist.ashx',
				params: {
                    LoadData: JSON.stringify($scope.SelectedData)
				}
			}).then(function successCallback(response) {
                if ($scope.isValidSave(response)) {
                    $scope.isLoading = true;
                    if (response.data.UserData.FistNam != "") {
                        $scope.SetStatus(true);
                        $scope.UserName = "Hi, " + response.data.UserData.FistNam;
                        $scope.SelectedData.FistNam = response.data.UserData.FistNam;
                    }
                    else
                        $scope.SetStatus(false);
                    $scope.SelectedData.Email = '';
                    $scope.SelectedData.Passwd = '';

                    $scope.JobMastCol = response.data.JobMastCol;

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
	$scope.$watch('FilterData.Category.ClasifdSpecDtlId', function (newValue, oldValue) {
		if (!$scope.isLoading && newValue != "")
			$scope.LoadData();
	});
	$scope.GetUserInfo = function (Mode) {
		if (($scope.SessionId != null && $scope.SessionId != "") || (Mode == "UL" && $scope.isValidLoginData())) {
			try {
				$http({
					method: 'POST',
                    url: 'myAddsJobslist.ashx',
					params: {
						LoadUser: JSON.stringify($scope.SelectedData)
					}
				}).then(function successCallback(response) {
					if ($scope.isValidSave(response) && response.data.UserData.ZaBase.SessionId != "") {
						$scope.isLoading = true;
						if (response.data.UserData.FistNam != "") {
							$scope.SetStatus(true);
							$scope.UserName = "Hi, " + response.data.UserData.FistNam;
							$scope.SelectedData.FistNam = response.data.UserData.FistNam;
						}
						else
							$scope.SetStatus(false);
						$scope.SelectedData.Email = '';
						$scope.SelectedData.Passwd = '';
						$scope.SelectedData.FistNam = response.data.UserData.ZaBase.UserName;
						$scope.SessionId = response.data.UserData.ZaBase.SessionId;
						$scope.SelectedData.ZaBase.SessionId = response.data.UserData.ZaBase.SessionId;
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
                    url: 'myAddsJobslist.ashx',
					params: {
						SignOut: JSON.stringify($scope.SelectedData)
					}
				}).then(function successCallback(response) {
					if ($scope.isValidSave(response)) {
						$scope.SelectedData.FistNam = "";
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
		if ($scope.SelectedData.Email.trim() == "") {
			alert('Please Enter Email Address');
			return false;
		}
		if ($scope.SelectedData.Passwd.trim() == "") {
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
