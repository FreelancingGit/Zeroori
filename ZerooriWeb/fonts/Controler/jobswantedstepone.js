angular.module("ZerooriApp", ['ngCookies']).controller("jobswantedstepone", function ($scope, $http, $cookies) {
    $scope.urlArray = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    $scope.Page = 'jobs-wanted-step-one';
    $scope.CurrentURL = $scope.urlArray[0];
    $scope.PrevPage = '';
    $scope.SessionId = '';
    $scope.LoginStatusCaption = "SIGN IN";
    $scope.ISLogin = true;
    $scope.ISSignINStatus = false;
    $scope.ISSignOutStatus = false;
    $scope.EnablePostAds = false;
    $scope.ViewData = {
        NationalityCol: {},
        CurrentLocCol: {},
        VisaStatusCol: {},
        CarrierLevelCol: {},
        CurrentSalaryCol: {},
        WorkExperianceCol: {},
        EducationalLevelCol: {},
        CommitmentCol: {},
        IndustryCol: {},
        UserData: {
            ZaBase: {
                SessionId: ''
            }
        }
    };
    $scope.SelectedData = {
        FirstName: '',
        LastName: '',
        Gender: 'N',
        Title: '',
        Description: '',
        Mobile: '',
        Email: '',
        CurrentCompany: '',
        CurrentPos: '',
        NoticePeriod: '',
        Nationality: {},
        CurrentLoc: {},
        VisaStatus: {},
        CarrierLevel: {},
        CurrentSalary: {},
        WorkExperiance: {},
        EducationalLevel: {},
        Industry: {},
        Commitment: {},
        UserData: {
            ZaBase: {
                SessionId: ''
            }
        }
    }
    if ($scope.urlArray.length > 1) {
        $scope.PrevPage = $scope.urlArray[0].replace('url=', '');
    }
    $(document).ready(function () {
        $("#myModal").on('hide.bs.modal', function () {
            if ($scope.ViewData.UserData.ZaBase == null || $scope.ViewData.UserData.ZaBase.SessionId == null) {
                this.modal("show");
            }
        });
    });

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
        url: 'jobswantedstepone.ashx',
        params: {
            GetC: JSON.stringify($scope.ViewData)
        }
    }).then(function successCallback(response) {
        if ($scope.isValidSave(response)) {
            $scope.ZaKey = response.data.UserData.ZaBase.ZaKey;
            $scope.SessionId = $cookies.get($scope.ZaKey);
            $scope.ViewData.UserData.ZaBase.SessionId = $cookies.get($scope.ZaKey);
            if ($scope.ViewData.UserData.ZaBase.SessionId == null || $scope.ViewData.UserData.ZaBase.SessionId == undefined || $scope.ViewData.UserData.ZaBase.SessionId == '') {
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
                    url: 'jobswantedstepone.ashx',
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
                        $scope.ViewData.UserData.ZaBase.SessionId = response.data.UserData.ZaBase.SessionId;
                        $scope.SelectedData.UserData.ZaBase.SessionId = $scope.ViewData.UserData.ZaBase.SessionId;
                        $cookies.remove($scope.ZaKey);
                        $cookies.put($scope.ZaKey, $scope.SessionId);
                        //$('#myModal').modal("hide");
                        $scope.ViewData.NationalityCol = response.data.NationalityCol;
                        $scope.ViewData.CurrentLocCol = response.data.CurrentLocCol;
                        $scope.ViewData.VisaStatusCol = response.data.VisaStatusCol;
                        $scope.ViewData.CarrierLevelCol = response.data.CarrierLevelCol;
                        $scope.ViewData.CurrentSalaryCol = response.data.CurrentSalaryCol;
                        $scope.ViewData.WorkExperianceCol = response.data.WorkExperianceCol;
                        $scope.ViewData.EducationalLevelCol = response.data.EducationalLevelCol;
                        $scope.ViewData.CommitmentCol = response.data.CommitmentCol;
                        $scope.ViewData.IndustryCol = response.data.IndustryCol;
                        $scope.SelectedData.Nationality = response.data.NationalityCol[0];
                        $scope.SelectedData.CurrentLoc = response.data.CurrentLocCol[0];
                        $scope.SelectedData.VisaStatus = response.data.VisaStatusCol[0];
                        $scope.SelectedData.CarrierLevel = response.data.CarrierLevelCol[0];
                        $scope.SelectedData.CurrentSalary = response.data.CurrentSalaryCol[0];
                        $scope.SelectedData.WorkExperiance = response.data.WorkExperianceCol[0];
                        $scope.SelectedData.EducationalLevel = response.data.EducationalLevelCol[0];
                        $scope.SelectedData.Commitment = response.data.CommitmentCol[0];
                        $scope.SelectedData.Industry = response.data.IndustryCol[0];
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
                    url: 'jobswantedstepone.ashx',
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
    $scope.ShowPwd = function (Name) {
        if ('password' == $('#' + Name).attr('type')) {
            $('#' + Name).prop('type', 'text');
        } else {
            $('#' + Name).prop('type', 'password');
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
        if (response.data.UserData != undefined
            && response.data.UserData.ZaBase != undefined
            && response.data.UserData.ZaBase.ErrorMsg != undefined
            && response.data.UserData.ZaBase.ErrorMsg.trim() != "") {
            alert(response.data.UserData.ZaBase.ErrorMsg);
            return false;
        }
        return true
    }
    $scope.DoSave = function () {
        try {
            if ($scope.isValidData()) {

                var file = $('#upload1').get(0).files;


                $http({
                    method: 'POST',
                    url: 'jobswantedstepone.ashx',
                    data: file[0],
                    params: {
                        DoSave: JSON.stringify($scope.SelectedData)
                    }
                }).then(function successCallback(response) {
                    if ($scope.isValidSave(response) && response.data.UserData.ZaBase.SessionId != "") {

                        $scope.Uploadimage();

                        $scope.navigate('Index');
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
    $scope.Uploadimage = function () {
        var file2 = $('#exampleFormControlFile1').get(0).files;

        $http({
            method: 'POST',
            url: 'jobswantedstepone.ashx',
            data: file2[0],
            params: {
                SaveCV: JSON.stringify($scope.ViewData)
            }
        }).then(function successCallback(response) {
            if ($scope.isValidSave(response)) {
            }
        }, function errorCallback(response) {
            alert(response);
            $('#myModal').modal('hide');
        });
    };


    $scope.isValidData = function () {
        if ($scope.SelectedData.FirstName == '') {
            alert("Please Enter First Name");
            return false;
        }
        else if ($scope.SelectedData.LastName == '') {
            alert("Please Enter Last Name");
            return false;
        }
        else if ($scope.SelectedData.Title == '') {
            alert("Please Enter Title");
            return false;
        }
        else if ($scope.SelectedData.Gender == 'N') {
            alert("Please Select Gender");
            return false;
        }
        else if ($scope.SelectedData.Description == '') {
            alert("Please Enter Description");
            return false;
        }
        
        else if ($scope.SelectedData.Industry.EmpJobDtlId == -1) {
            alert("Please Select Nationality");
            return false;
        }
        else if ($scope.SelectedData.Mobile == undefined || $scope.SelectedData.Mobile == '') {
            alert("Please Enter Mobile");
            return false;
        }
        else if ($scope.SelectedData.Email == undefined || $scope.SelectedData.Email == '') {
            alert("Please Enter Email");
            return false;
        }
        else if ($scope.SelectedData.CurrentCompany == '') {
            alert("Please Enter Current Company");
            return false;
        }
        else if ($scope.SelectedData.CurrentPos == '') {
            alert("Please Enter Current Possition");
            return false;
        }
        else if ($scope.SelectedData.NoticePeriod == '') {
            alert("Please Enter Notice Period");
            return false;
        }
        else if ($scope.SelectedData.Nationality.EmpJobDtlId == -1) {
            alert("Please Select Nationality");
            return false;
        }
        else if ($scope.SelectedData.EducationalLevel.EmpJobDtlId == -1) {
            alert("Please Select Educational Level");
            return false;
        }
        else if ($scope.SelectedData.Commitment.EmpJobDtlId == -1) {
            alert("Please Select Commitment");
            return false;
        }
        else if ($scope.SelectedData.CurrentLoc.EmpJobDtlId == -1) {
            alert("Please Select Current Location");
            return false;
        }
        else if ($scope.SelectedData.VisaStatus.EmpJobDtlId == -1) {
            alert("Please Select Visa Status");
            return false;
        }
        else if ($scope.SelectedData.CarrierLevel.EmpJobDtlId == -1) {
            alert("Please Select Carrier Level");
            return false;
        }
        else if ($scope.SelectedData.CurrentSalary.EmpJobDtlId == -1) {
            alert("Please Select Current Salary");
            return false;
        }
        else if ($scope.SelectedData.WorkExperiance.EmpJobDtlId == -1) {
            alert("Please Select Work Experiance");
            return false;
        }
        return true;
    }
    $scope.navigate = function (URL) {
        url = URL + '.html?url=' + $scope.Page;
        $(location).attr('href', url);
    };
})
