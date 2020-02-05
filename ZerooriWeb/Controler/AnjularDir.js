var Days = new Array('Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday');
var Months = new Array('January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December');
var ZerooriApp = angular.module('ZerooriApp', []);



ZerooriApp.factory('vstutils', ['$http', function ($http) {
    var vstutils = {};
    vstutils.isEmpty = function (v) { return (v == null || $.trim(v).length == 0); }
    vstutils.isUndefined = function (v) { return (v == undefined || v == null); }
    vstutils.zeroFill = function (num, length) {
        var num = String(num);
        length = parseInt(length) || 2;
        while (num.length < length) num = "0" + num;
        return num;
    }


    vstutils.formatDate = function (d, f) {
        if (d == undefined || d == null) return '';
        return f.replace(/(yyyy|mmmm|mmm|mm|dddd|ddd|dd|hh|h|nn|n|ss|tt)/gi,
            function (v) {
                switch (v.toLowerCase()) {
                    case 'yyyy': return d.getFullYear();
                    case 'mmmm': return Months[d.getMonth()];
                    case 'mmm': return Months[d.getMonth()].substr(0, 3);
                    case 'mm': return vstutils.zeroFill((d.getMonth() + 1), 2);
                    case 'dddd': return Days[d.getDay()];
                    case 'ddd': return Days[d.getDay()].substr(0, 3);
                    case 'dd': return vstutils.zeroFill(d.getDate(), 2);
                    case 'hh': return vstutils.zeroFill(((h = d.getHours() % 12) ? h : 12), 2);
                    case 'h': return (h = d.getHours() % 12) ? h : 12;
                    case 'nn': return vstutils.zeroFill(d.getMinutes(), 2);
                    case 'n': return d.getMinutes();
                    case 'ss': return vstutils.zeroFill(d.getSeconds(), 2);
                    case 'tt': return d.getHours() < 12 ? 'AM' : 'PM';
                }
            });
    }
    return vstutils;
}]);


ZerooriApp.directive('vstUploader', function ($parse, vstutils) {
    return {
        restrict: 'A',
        template: '<div class="input-group" style="position: relative;">' +
            '<input type="text" class="form-control input-sm" id="txt_rc_book" placeholder="( Browse File )" readonly="">' +
            '<div class="input-group-btn">' +
            '<button type="button" class="btn btn-primary btn-sm">&nbsp;<span class="fa fa-search" aria-hidden="true"></span></button>' +
            '<a browser href="#" class="btn btn-primary btn-sm" target="_blank">&nbsp;<span class="fa fa-eye" aria-hidden="true"></span></a>' +
            '<button type="button" class="btn btn-primary btn-sm">&nbsp;<span class="fa fa-close" aria-hidden="true"></span></button>' +
            '</div>' +
            '</div>',
        link: function (scope, element, attrs, controllers) {
            var element = $(element);
            var property = $parse(attrs.vstUploader);
            var input = $('<input type="file" style="display:none;" accept="image/*, .mp4"/>');
            var clear = function () {
                try {
                    property.assign(scope, null);
                    element.find('input').attr('placeholder', '( Browse File )');
                    element.find('input').val('');
                } catch (ex) { }
            };

            element.css({ 'position': 'relative' });
            element.find('input[type=text]').attr('placeholder', '( Browse File )').prop('readonly', 'readonly');

            element.find('.fa.fa-search').closest('.btn').click(function () {
                try {
                    if (vstutils.isEmpty(element.find('input').val()) == false) {
                        alertify.alert("Please remove attached file!");
                        return;
                    }
                    input.click();
                } catch (ex) { }
            });
            element.find('.fa.fa-close').closest('.btn').click(function () {
                try {
                    scope.$apply(function () { clear(); });
                } catch (ex) { }
            });
            scope.$watch(property, function (v) {
                try {
                    if (v !== undefined && v !== null) {
                        element.find('input').val(v.LocalName || '');
                        element.find('a[browser]').attr('href', '../docs/' + (v.ServerName || ''));
                    } else { input.val(''); }
                    if (v === undefined || v === null || vstutils.isEmpty(v.LocalName) || vstutils.isEmpty(v.ServerName)) {
                        clear();
                    }
                } catch (ex) { }
            });
            input.change(function (e) {
                var files = (e.target || {}).files;
                if (files !== undefined && files !== null && files.length > 0) {
                    var file = files[0];
                    if (file !== undefined && file !== null) {
                        if (file.size / (1024 * 1024) > 10) {
                            alert('You cannot continue, Maximum allowed size is 10 MB.');
                            return;
                        }

                        var form = new FormData();
                        var xhr = new XMLHttpRequest;

                        form.append('filename', file.name);
                        form.append('filedata', file);

                        xhr.upload.onprogress = function (e) {
                            scope.$apply(function () {
                                var percentCompleted;
                                if (e.lengthComputable) {
                                    percentCompleted = Math.round(e.loaded / e.total * 100);
                                    if (percentCompleted < 1) {
                                        element.find('input').attr('placeholder', 'Uploading...');
                                    } else if (percentCompleted == 100) {
                                        element.find('input').attr('placeholder', 'Saving...');
                                    } else {
                                        element.find('input').attr('placeholder', 'Uploading... (' + percentCompleted + '%)');
                                    }
                                }
                            });
                        };
                        xhr.onreadystatechange = function () {
                            if (xhr.readyState == 4) {
                                scope.$apply(function () {
                                    if (vstutils.isEmpty(xhr.responseText)) {
                                        property.assign(scope, { LocalName: '', ServerName: '' });
                                        element.find('input').attr('placeholder', '( Browse File )');
                                        element.find('input').val('');
                                    }
                                    else {
                                        property.assign(scope, { LocalName: file.name, ServerName: xhr.responseText });
                                        element.find('a[browser]').attr('href', '../docs/' + (xhr.responseText || ''));
                                        element.find('input').val(file.name);
                                    }
                                    scope.dowait(false);
                                });
                            }
                        }
                        xhr.upload.onload = function (e) {
                            scope.$apply(function () {
                                element.find('input').attr('placeholder', '( Browse File )');
                            });
                        };

                        xhr.open('POST', 'VSTERPUploadHandler.ashx');
                        xhr.send(form);
                        scope.dowait(true);
                    }
                }
            });
        }
    };
});
Number.prototype.zeroFill = function (length) {
    var num = String(this);
    length = parseInt(length) || 2;
    while (num.length < length) num = "0" + num;
    return num;
};
Date.prototype.format = function (f) {
    if (!this.valueOf()) return '';
    var d = this;
    return f.replace(/(yyyy|mmmm|mmm|mm|dddd|ddd|dd|hh|nn|ss|a\/p)/gi,
        function (v) {
            switch (v.toLowerCase()) {
                case 'yyyy': return d.getFullYear();
                case 'mmmm': return Months[d.getMonth()];
                case 'mmm': return Months[d.getMonth()].substr(0, 3);
                case 'mm': return (d.getMonth() + 1).zeroFill(2);
                case 'dddd': return Days[d.getDay()];
                case 'ddd': return Days[d.getDay()].substr(0, 3);
                case 'dd': return d.getDate().zeroFill(2);
                case 'hh': return ((h = d.getHours() % 12) ? h : 12).zeroFill(2);
                case 'nn': return d.getMinutes().zeroFill(2);
                case 'ss': return d.getSeconds().zeroFill(2);
                case 'a/p': return d.getHours() < 12 ? 'a' : 'p';
            }
        }
    );
};