angular.module("ZerooriApp", ['ngCookies']).controller("jobsHiringStepOne", function ($scope, $http, $cookies) {

    $scope.urlArray = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    $scope.Page = 'jobs-hiring-step-one';
	$scope.CurrentURL = $scope.urlArray[0];
    $scope.PrevPage = '';
    $scope.SessionId = '';
    $scope.ComJobId = '';

    $scope.LoginStatusCaption = "SIGN IN";
    $scope.UserName = "Welcome";
    $scope.ISLogin = true;
    $scope.ISSignINStatus = false;
    $scope.ISSignOutStatus = false;
    $scope.EnablePostAds = false;
    
    $scope.IndstryCol = {};
    $scope.CompnySizeCol = {};
    $scope.EmploymntTypeCol = {};
    $scope.MonthlySalaryCol = {};
    $scope.ExprnceCol = {};
    $scope.EductnLevlCol = {};
    $scope.ListedBycol = {};
	$scope.CareervLevelCol = {};
	$scope.locationCol = {};
	$scope.JobsCol = {};
    
    //$scope.ViewData = {
       
    //    UserData: {
    //        ZaBase: {
    //            SessionId: ''
    //        }
    //    }
    //};

    $scope.SelectedData = {

        CompnyJobMastId: -1,
        CompnyName: '',
        TradeLicns: '',
        ContctName: '',
        Phone: '',
        CompnyWebsit: '', 
        Addrs: '',
        DescrpnStepOne: '',
        
        CompnyLogoImg: '',
		JobTitle: '',
		JobTitleData: {},
        Neighbrhd: '',
		Location: {},
		DescrptnStepTwo: '',
		filename:'',
        
        Indstry: {},
        CompnySize: {},
        EmplymntTyp: {},
        MonthlySalary: {},
        EductnLvl: {},
        ListedBy: {},
        CareerLvl: {},
        Exprnce: {},

        UserData:
        {
            FistNam: '',
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

    $http({
        method: 'POST',
        url: 'jobsHiringStepOne.ashx',
        params: {
            GetC: JSON.stringify($scope.SelectedData)
        }
    }).then(function successCallback(response) {

        if ($scope.isValidSave(response)) {

            $scope.ZaKey = response.data.UserData.ZaBase.ZaKey;
            $scope.SessionId = $cookies.get($scope.ZaKey);
            $scope.SelectedData.UserData.ZaBase.SessionId = $cookies.get($scope.ZaKey);
            $scope.SelectedData.UserData.ZaBase.PKID = $cookies.get('JH');
            $scope.SelectedData.CompnyJobMastId = $cookies.get('JH');
            $scope.LoadInit();
        }

    }, function errorCallback(response) {
        alert(response);
    });

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


    
    $scope.LoadInit = function (Mode) {
        

        try {
            $http({
                method: 'POST',
                url: 'jobsHiringStepOne.ashx',
                params: {
                    Init: JSON.stringify($scope.SelectedData)
                }
			}).then(function successCallback(response)
			{
				
                if ($scope.isValidSave(response) && response.data.UserData.ZaBase.SessionId != "") {
                    $scope.SetStatus(true);
                    $scope.LoginStatusCaption = "Hi, " + response.data.UserData.ZaBase.UserName;
                    //$scope.ViewData.Email = '';
                    //$scope.ViewData.Passwd = '';
                    $scope.SelectedData.UserData.ZaBase.FistNam = response.data.UserData.ZaBase.UserName;
                    $scope.SessionId = response.data.UserData.ZaBase.SessionId;
                    $scope.SelectedData.UserData.ZaBase.SessionId = response.data.UserData.ZaBase.SessionId;
                    //$scope.SelectedData.UserData.ZaBase.SessionId = $scope.ViewData.UserData.ZaBase.SessionId;
                    $cookies.remove($scope.ZaKey);
                    $cookies.put($scope.ZaKey, $scope.SessionId);
                    
					$scope.IndstryCol = response.data.IndstryCol;
                    $scope.CompnySizeCol = response.data.CompnySizeCol;
                    $scope.EmploymntTypeCol = response.data.EmploymntTypeCol;
                    $scope.MonthlySalaryCol = response.data.MonthlySalaryCol;
                    $scope.ExprnceCol = response.data.ExprnceCol;
                    $scope.EductnLevlCol = response.data.EductnLevlCol;
                    $scope.ListedBycol = response.data.ListedBycol;
					$scope.CareervLevelCol = response.data.CareervLevelCol;
					$scope.SelectedData.CompnySize = response.data.CompnySizeCol;
					$scope.LocationCol = response.data.LocationCol;
					$scope.JobsCol = response.data.JobsCol;

                    
                    if (response.data.FrelncMast.CompnyName != undefined || response.data.FrelncMast.CompnyName != null) {
                        $scope.SelectedData.CompnyName = response.data.FrelncMast.CompnyName;
                        $scope.SelectedData.TradeLicns = response.data.FrelncMast.TradeLicns;
                        $scope.SelectedData.ContctName = response.data.FrelncMast.ContctName;
                        $scope.SelectedData.Phone = response.data.FrelncMast.Phone;
                        $scope.SelectedData.CompnyWebsit = response.data.FrelncMast.CompnyWebsit;
                        $scope.SelectedData.Addrs = response.data.FrelncMast.Addrs;
                        $scope.SelectedData.DescrpnStepOne = response.data.FrelncMast.DescrpnStepOne;
                        $scope.SelectedData.Indstry = response.data.FrelncMast.Indstry;
						$scope.SelectedData.CompnySize = response.data.FrelncMast.CompnySize;
						
                    }
                    else {
                        $scope.SelectedData.Indstry = response.data.IndstryCol[0];
                        $scope.SelectedData.CompnySize = response.data.CompnySizeCol[0];
                    }

                    if (response.data.FrelncMast.JobTitle != undefined || response.data.FrelncMast.JobTitle != null) {
                        $scope.SelectedData.JobTitle = response.data.FrelncMast.JobTitle;
                        $scope.SelectedData.Neighbrhd = response.data.FrelncMast.Neighbrhd;
                        $scope.SelectedData.Location = response.data.FrelncMast.Location;
						$scope.SelectedData.DescrptnStepTwo = response.data.FrelncMast.DescrptnStepTwo;
                        $scope.SelectedData.EmplymntTyp = response.data.FrelncMast.EmplymntTyp;
                        $scope.SelectedData.MonthlySalary = response.data.FrelncMast.MonthlySalary;
                        $scope.SelectedData.EductnLvl = response.data.FrelncMast.EductnLvl;
                        $scope.SelectedData.ListedBy = response.data.FrelncMast.ListedBy;
                        $scope.SelectedData.CareerLvl = response.data.FrelncMast.CareerLvl;
						$scope.SelectedData.Exprnce = response.data.FrelncMast.Exprnce;
						$scope.SelectedData.CompnySize = response.data.CompnySizeCol[0];
						$scope.SelectedData.Indstry = response.data.IndstryCol[0];
						$scope.SelectedData.EmplymntTyp = response.data.EmploymntTypeCol[0];
						$scope.SelectedData.MonthlySalary = response.data.MonthlySalaryCol[0];
						$scope.SelectedData.EductnLvl = response.data.EductnLevlCol[0];
						$scope.SelectedData.ListedBy = response.data.ListedBycol[0];
						$scope.SelectedData.CareerLvl = response.data.CareervLevelCol[0];
						$scope.SelectedData.Exprnce = response.data.ExprnceCol[0];
						$scope.SelectedData.Location = response.data.LocationCol[0];
						$scope.SelectedData.JobTitleData = response.data.JobsCol[0];
                    }
                    else {
                        $scope.SelectedData.EmplymntTyp = response.data.EmploymntTypeCol[0];
                        $scope.SelectedData.MonthlySalary = response.data.MonthlySalaryCol[0];
                        $scope.SelectedData.EductnLvl = response.data.EductnLevlCol[0];
                        $scope.SelectedData.ListedBy = response.data.ListedBycol[0];
                        $scope.SelectedData.CareerLvl = response.data.CareervLevelCol[0];
						$scope.SelectedData.Exprnce = response.data.ExprnceCol[0];
						$scope.SelectedData.Location = response.data.LocationCol[0];
						$scope.SelectedData.JobTitleData = response.data.JobsCol[0];
                    }
                    $('#myModal').modal('hide');
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

    $scope.LoadData = function () {

        try {
            $http({
                method: 'POST',
                url: 'jobsHiringStepOne.ashx',
                params: {
                    LoadData: JSON.stringify($scope.SelectedData)
                }
            }).then(function successCallback(response) {

                if ($scope.isValidSave(response)) {
                    $scope.isLoading = true;

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
	
    $scope.SaveData = function () {
        
        if ($scope.isValidData() && ($scope.SessionId != null && $scope.SessionId != ""))
        {
            try {

				var file = $('#upload1').get(0).files;
				console.log(file[0].name);
				$scope.SelectedData.filename = file[0].name;
                $http({
                    method: 'POST',
                    url: 'jobsHiringStepOne.ashx',
                    data: file[0],
                    params: {
                        SaveData: JSON.stringify($scope.SelectedData)
                    }
				}).then(function successCallback(response)
				{
                    if ($scope.isValidSave(response) ) {
                        $scope.SelectedData.CompnyJobMastId = response.data.CompnyJobMastId;
						$cookies.put("JobID", $scope.SelectedData.CompnyJobMastId);
						console.log($cookies.get("JobID"));

                       // alert('You are Successfully Posted');
                        $scope.navigate("jobs-hiring-step-two");
                        //$scope.SetSaveData(response)
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

    
    $scope.isValidData = function () {
        if ($scope.SelectedData.CompnyName == '') {
            alert("Please Enter Company Name");
            return false;
        }
        else if ($scope.SelectedData.ContctName == '') {
            alert("Please Enter Contact Name");
            return false;
        }
        else if ($scope.SelectedData.Phone == '') {
            alert("Please Enter Phone");
            return false;
        }
        else if ($scope.SelectedData.CompnyWebsit == '') {
            alert("Please Enter Company Website");
            return false;
        }
        else if ($scope.SelectedData.Addrs == '') {
            alert("Please Enter Address");
            return false;
        }
        else if ($scope.SelectedData.DescrpnStepOne == '') {
            alert("Please Enter Description");
            return false;
        }
        else if ($scope.SelectedData.Indstry.EmpJobDtlId == -1) {
            alert("Please Select Jobs");
            return false;
        }

        else if ($scope.SelectedData.CompnySize.EmpJobDtlId == -1) {
            alert("Please Select Company size");
            return false;
        }
        return true;
    }
    
	$scope.isValidDataStepTwo = function () {
		if ($scope.SelectedData.JobTitleData.EmpJobValue == '') {
			alert("Please select Job Title");
			return false;
		}
		else if ($scope.SelectedData.Neighbrhd == '') {
			alert("Please Enter Neighbourhood");
			return false;
		}
		else if ($scope.SelectedData.Location == '') {
			alert("Please Enter Location");
			return false;
		}
		else if ($scope.SelectedData.DescrptnStepTwo == '') {
			alert("Please Enter Description");
			return false;
		}

		else if ($scope.SelectedData.EmplymntTyp.EmpJobDtlId == -1) {
			alert("Please Select Employement Type");
			return false;
		}

		else if ($scope.SelectedData.EductnLvl.EmpJobDtlId == -1) {
			alert("Please Select Education Qualification");
			return false;
		}

		else if ($scope.SelectedData.CareerLvl.EmpJobDtlId == -1) {
			alert("Please Select Career Level");
			return false;
		}

		else if ($scope.SelectedData.Exprnce.EmpJobDtlId == -1) {
			alert("Please Select Experiance ");
			return false;
		}

		else if ($scope.SelectedData.ListedBy.EmpJobDtlId == -1) {
			alert("Please Select Listed By");
			return false;
		}

		else if ($scope.SelectedData.MonthlySalary.EmpJobDtlId == -1) {
			alert("Please Select Current Salary");
			return false;
		}
		return true;
	};

	

	$scope.SaveDataStepTwo = function ()
	{
		$scope.SelectedData.CompnyJobMastId = $cookies.get('JobID');
		console.log($cookies.get('JobID'));
		console.log($scope.isValidDataStepTwo());
		$scope.SelectedData.JobTitle = $scope.SelectedData.JobTitleData.EmpJobValue;
        
      //  if ($scope.isValidDataStepTwo() && ($scope.SessionId != null && $scope.SessionId != ""))
        try {
            $http({
                method: 'POST',
                url: 'jobsHiringStepOne.ashx',
                params: {
                    SaveData: JSON.stringify($scope.SelectedData)
                }
            }).then(function successCallback(response) {
                if ($scope.isValidSave(response)) {
                    $cookies.remove('JobID');
                    $cookies.remove('JH');
                    alert('You are Successfully Posted');

                    $scope.navigate('postyouradd')
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
    $scope.GetUserInfo = function (Mode) {

        if (($scope.SessionId != null && $scope.SessionId != "") || (Mode == "UL" && $scope.isValidLoginData())) {
            try {
                $http({
                    method: 'POST',
                    url: 'jobsHiringStepOne.ashx',
                    params: {
                        LoadUser: JSON.stringify($scope.SelectedData)
                    }
                }).then(function successCallback(response) {

                    if ($scope.isValidSave(response) && response.data.SelectedData.UserData.ZaBase.SessionId != "") {
                        $scope.SetStatus(true);
                        $scope.LoginStatusCaption = "Hi, " + response.data.SelectedData.UserData.ZaBase.UserName;
                        $scope.ViewData.Email = '';
                        $scope.ViewData.Passwd = '';
                        $scope.ViewData.FistNam = response.data.SelectedData.UserData.ZaBase.UserName;
                        $scope.SessionId = response.data.SelectedData.UserData.ZaBase.SessionId;
                        $scope.SelectedData.UserData.ZaBase.SessionId = response.data.SelectedData.UserData.ZaBase.SessionId;
                        //$scope.SelectedData.UserData.ZaBase.SessionId = $scope.SelectedData.UserData.ZaBase.SessionId;
                        $cookies.remove($scope.ZaKey);
                        $cookies.put($scope.ZaKey, $scope.SessionId);


                  
                        $('#myModal').modal("hide");

                        if ($scope.EnablePostAds && $scope.ISSignINStatus) {
                            $scope.navigate('postyouradd')
                        }

                    } // User Login Mode
                    else if (Mode == "UL" && response.data.SelectedData.UserData.ZaBase.SessionId == "") {
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
                    url: 'jobsHiringStepOne.ashx',
                    params: {
                        SignOut: JSON.stringify($scope.SelectedData)
                    }
                }).then(function successCallback(response) {

                    if ($scope.isValidSave(response)) {
                        //$scope.ViewData.FistNam = "";
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

        if ((response.data.UserData.ZaBase != undefined) &&
            (response.data.UserData.ZaBase.ErrorMsg != undefined) &&
            (response.data.UserData.ZaBase.ErrorMsg.trim() != "")) {
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
