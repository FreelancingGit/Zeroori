angular.module("ZerooriApp", ['ngCookies']).controller("motorssteptwo", function ($scope, $http, $cookies) {
    $scope.urlArray = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    $scope.Page = 'motorssteptwo';
    $scope.CurrentURL = $scope.urlArray[0];
    $scope.PrevPage = '';
    $scope.SessionId = '';
    $scope.LoginStatusCaption = "SIGN IN";
    $scope.ISLogin = true;
    $scope.ISSignINStatus = false;
    $scope.ISSignOutStatus = false;
    $scope.EnablePostAds = false;
    $scope.SelectedData = {
        MotorsADMastID:'',
        Description: '',
        Title: '',
        KiloMetrs: '',
        Year: {},
        Colour: {},
        Doors: {},
        Warranty: {},
        RegionalSpecs: {},
        Transmisson: {},
        BodyType: {},
        FuelType: {},
        Cylinders: {},
        SellerType: {},
        Extras: {},
        TechinalFeatures: {},
        HoursePower: {},
        Brand: {},
        Condition: {},
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
            SessionId: '',
            PKID: ''
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
            //$scope.EnablePostAds = false;
        }
    }
    $scope.SetStatus(false);
    $scope.ShowPwd = function (Name) {
        if ('password' == $('#' + Name).attr('type')) {
            $('#' + Name).prop('type', 'text');
        } else {
            $('#' + Name).prop('type', 'password');
        }
    }
    $http({
        method: 'POST',
        url: 'motorssteptwo.ashx',
        params: {
            GetC: JSON.stringify($scope.ViewData)
        }
    }).then(function successCallback(response) {
        if ($scope.isValidSave(response)) {
            $scope.ZaKey = response.data.UserData.ZaBase.ZaKey;
            $scope.SessionId = $cookies.get($scope.ZaKey);
            $scope.ViewData.ZaBase.SessionId = $cookies.get($scope.ZaKey);
            $scope.ViewData.ZaBase.PKID = $cookies.get('MOID');
            $scope.SelectedData.MotorsADMastID = $cookies.get('MOID');
            if ($scope.ViewData.ZaBase.SessionId == null) {
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
                    url: 'motorssteptwo.ashx',
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
                        $scope.ViewData.YearCol = response.data.YearCol;
                        $scope.ViewData.ColourCol = response.data.ColourCol;
                        $scope.ViewData.DoorsCol = response.data.DoorsCol;
                        $scope.ViewData.WarrantyCol = response.data.WarrantyCol;
                        $scope.ViewData.RegionalSpecsCol = response.data.RegionalSpecsCol;
                        $scope.ViewData.TransmissonCol = response.data.TransmissonCol;
                        $scope.ViewData.BodyTypeCol = response.data.BodyTypeCol;
                        $scope.ViewData.FuelTypeCol = response.data.FuelTypeCol;
                        $scope.ViewData.CylindersCol = response.data.CylindersCol;
                        $scope.ViewData.SellerTypeCol = response.data.SellerTypeCol;
                        $scope.ViewData.ExtrasCol = response.data.ExtrasCol;
                        $scope.ViewData.TechinalFeaturesCol = response.data.TechinalFeaturesCol;
                        $scope.ViewData.HoursePowerCol = response.data.HoursePowerCol;
                        $scope.ViewData.BrandCol = response.data.BrandCol;
                        $scope.ViewData.ConditionCol = response.data.ConditionCol;


                      

                        if (response.data.SelectedData.Year.MotorSpecDtlId != null ||
                            response.data.SelectedData.Year.MotorSpecDtlId != undefined) {

                            $scope.SelectedData.Year = response.data.SelectedData.Year;
                            $scope.SelectedData.Colour = response.data.SelectedData.Colour;
                            $scope.SelectedData.Doors = response.data.SelectedData.Doors;
                            $scope.SelectedData.Warranty = response.data.SelectedData.Warranty;
                            $scope.SelectedData.RegionalSpecs = response.data.SelectedData.RegionalSpecs;
                            $scope.SelectedData.Transmisson = response.data.SelectedData.Transmisson;
                            $scope.SelectedData.BodyType = response.data.SelectedData.BodyType;
                            $scope.SelectedData.FuelType = response.data.SelectedData.FuelType;
                            $scope.SelectedData.Cylinders = response.data.SelectedData.Cylinders;
                            $scope.SelectedData.SellerType = response.data.SelectedData.SellerType;
                            $scope.SelectedData.Extras = response.data.SelectedData.Extras;
                            $scope.SelectedData.TechinalFeatures = response.data.SelectedData.TechinalFeatures;
                            $scope.SelectedData.HoursePower = response.data.SelectedData.HoursePower;
                            $scope.SelectedData.Brand = response.data.SelectedData.Brand;
                            $scope.SelectedData.Condition = response.data.SelectedData.Condition;
                            $scope.SelectedData.KiloMetrs = response.data.SelectedData.KiloMetrs;
                            $scope.SelectedData.Title = response.data.SelectedData.Title;
                            $scope.SelectedData.Description = response.data.SelectedData.Description;
                            $scope.SelectedData.MotorsADMastID = response.data.SelectedData.MotorsADMastID;
                      
                        }
                        else {
                            $scope.SelectedData.Year = response.data.YearCol[0];
                            $scope.SelectedData.Colour = response.data.ColourCol[0];
                            $scope.SelectedData.Doors = response.data.DoorsCol[0];
                            $scope.SelectedData.Warranty = response.data.WarrantyCol[0];
                            $scope.SelectedData.RegionalSpecs = response.data.RegionalSpecsCol[0];
                            $scope.SelectedData.Transmisson = response.data.TransmissonCol[0];
                            $scope.SelectedData.BodyType = response.data.BodyTypeCol[0];
                            $scope.SelectedData.FuelType = response.data.FuelTypeCol[0];
                            $scope.SelectedData.Cylinders = response.data.CylindersCol[0];
                            $scope.SelectedData.SellerType = response.data.SellerTypeCol[0];
                            $scope.SelectedData.Extras = response.data.ExtrasCol[0];
                            $scope.SelectedData.TechinalFeatures = response.data.TechinalFeaturesCol[0];
                            $scope.SelectedData.HoursePower = response.data.HoursePowerCol[0];
                            $scope.SelectedData.Brand = response.data.BrandCol[0];
                            $scope.SelectedData.Condition = response.data.ConditionCol[0];

                        }


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
                    url: 'motorssteptwo.ashx',
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
                    url: 'motorssteptwo.ashx',
                    params: {
                        DoSave: JSON.stringify($scope.SelectedData)
                    }
                }).then(function successCallback(response) {
                    if ($scope.isValidSave(response) && response.data.UserData.ZaBase.SessionId != "") {
                        $cookies.remove($scope.ZaKey);

                        $cookies.put($scope.ZaKey, $scope.SessionId);
                        $cookies.put('MI', response.data.MotorsADMastID);
                        $scope.navigate('motorosstepthree');
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
        else if ($scope.SelectedData.Year.MotorSpecDtlId == -1) {
            alert("Please Select Year of Manufacture");
            return false;
        }
        else if ($scope.SelectedData.Brand.MotorSpecDtlId == -1) {
            alert("Please Select Brand");
            return false;
        }
        else if ($scope.SelectedData.Colour.MotorSpecDtlId == -1) {
            alert("Please Select Colour");
            return false;
        }
        else if ($scope.SelectedData.Doors.MotorSpecDtlId == -1) {
            alert("Please Select Doors");
            return false;
        }
        else if ($scope.SelectedData.Warranty.MotorSpecDtlId == -1) {
            alert("Please Select Warranty");
            return false;
        }
        else if ($scope.SelectedData.RegionalSpecs.MotorSpecDtlId == -1) {
            alert("Please Select Regional Specification");
            return false;
        }
        else if ($scope.SelectedData.Transmisson.MotorSpecDtlId == -1) {
            alert("Please Select Transmisson Type");
            return false;
        }
        else if ($scope.SelectedData.BodyType.MotorSpecDtlId == -1) {
            alert("Please Select Body Type");
            return false;
        }
        else if ($scope.SelectedData.FuelType.MotorSpecDtlId == -1) {
            alert("Please Select Fuel");
            return false;
        }
        else if ($scope.SelectedData.Cylinders.MotorSpecDtlId == -1) {
            alert("Please Select Cylinders");
            return false;
        }
        else if ($scope.SelectedData.SellerType.MotorSpecDtlId == -1) {
            alert("Please Select Seller type");
            return false;
        }
        else if ($scope.SelectedData.HoursePower.MotorSpecDtlId == -1) {
            alert("Please Select Hourse Power");
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
