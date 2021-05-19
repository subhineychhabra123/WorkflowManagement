var MessageType = {
    Success: 1,
    Warning: 2,
    Error: 3
}
var PermissionArray = [];

function showAlert(message, messageType) {
    switch (messageType) {
        case MessageType.Success:
            toastr.success(message, 'Success !');
            break;
        case MessageType.Warning:
            toastr.warning(message, 'Warning !');
            break;
        case MessageType.Error:
            toastr.error(message, 'Error !');
            break;

    }
}
function showConfirmAlert(message, callbackResult) {
    bootbox.confirm({
        message: message,
        buttons: {
            'confirm': {
                label: 'ok',
                className: 'btn btn-theme btn-md'
            },
            'cancel': {
                label: 'cancel',
                className: 'btn btn-gray btn-md'
            }

        },
        callback: callbackResult
    });
    //bootbox.confirm(message, callback);
}

function showConfirmAlertFormOfYesNo(message, callbackResult) {
   
    bootbox.confirm({
        message: message,
        buttons: {
            'confirm': {
                label: 'Yes',
                className: 'btn btn-theme btn-md'
            },
            'cancel': {
                label: 'No',
                className: 'btn btn-gray btn-md'
            }

        },
        callback: callbackResult
    });
    //bootbox.confirm(message, callback);
}

function showNotificationBottomRight(title, message, type) {
    var shortCutFunction = type == '' ? 'success' : type;
    toastr.options = {
        closeButton: true,
        debug: false,
        progressBar: true,
        positionClass: 'toast-bottom-right',
        onclick: null
    };
    toastr.options.showDuration = '1000000000';
    toastr.options.hideDuration = '100000';
    toastr.options.timeOut = '10000000';
    toastr.options.extendedTimeOut = '1000';
    toastr.options.showEasing = 'swing';
    toastr.options.hideEasing = 'linear';
    toastr.options.showMethod = 'fadeIn';
    toastr.options.hideMethod = 'fadeOut';
    var msg = message == '' ? 'Success' : message;
    var title = title == '' ? '' : title;
    var $toast = toastr[shortCutFunction](msg, title);
}
function showGlobalProgressbar(show) {
    if (CommonFunctions.isBlockedUi != true && (show == undefined || show != false)) {
        $.blockUI({
            css: {
                border: 'none',
                padding: '15px',
                backgroundColor: '#fff',
                '-webkit-border-radius': '10px',
                '-moz-border-radius': '10px',
                opacity: .5,
                color: '#000'
            },
            bindEvents: false
        });
    }
    CommonFunctions.isBlockedUi = true;
    if (show != undefined && show == false) {
        if (CommonFunctions.isBlockedUi) {
            $.unblockUI();
            CommonFunctions.isBlockedUi = false;
        }
    }
}
function getDateFromDateJSON(objDate, dateformat) {
    if (objDate == null) {
        return '';
    }
    if (objDate != '') {
        if (String(objDate).indexOf('/Date(') == 0) {
            objDate = new Date(parseInt(objDate.replace(/\/Date\((.*?)\)\//gi, "$1")));
        }
        var date = new Date(objDate).format(dateformat);
        return date;
    }
    else {
        var date = new Date().format(dateformat);
        return date;
    }
}
var CommonFunctions = {
    isBlockedUi: false,
    showNotificationBottomRight: showNotificationBottomRight,
    showGlobalProgressbar: showGlobalProgressbar,
    showAlert: showAlert,
    showConfirmAlert: showConfirmAlert,
    showConfirmAlertFormOfYesNo: showConfirmAlertFormOfYesNo,
    getDateFromDateJSON: getDateFromDateJSON,
    ApplyPermission: ApplyPermission
}
$(document).ready(function () {
    var d = new Date();
    var v = d.getTime() + "|" + GetCookie('Permissions');
    PermissionArray = v.split('|')
    for (var i = 0; i < PermissionArray.length; i++) {
        {
            PermissionArray[i] = PermissionArray[i].toString().toLowerCase();
        }
    }
    ApplyPermission();

});
function ApplyPermission(selector) {
    if (selector != undefined) {
        $(selector + ' [data-permission]').each(function () {
            if ($.inArray($(this).attr('data-permission').toLowerCase(), PermissionArray) >= 0) {
                $(this).removeClass('permissionbased');
            }
        });
    }
    else {
        $('[data-permission]').each(function () {
            if ($.inArray($(this).attr('data-permission').toLowerCase(), PermissionArray) >= 0) {
                $(this).removeClass('permissionbased');
            }
        });

    }
}


//Code added for implementing permission on ui based on cookie
function GetCookie(cname) {

    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i].split(' ').join('');
        if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
    }
    return "";
}
(function ($) {

    //re-set all client validation given a jQuery selected form or child
    $.fn.resetValidation = function (resetForAll) {
        var elementFor = undefined;
        var $form = this.closest('form');

        if (resetForAll == false) {
            var elementId = this.attr('id');
            elementFor = $form.find('[data-valmsg-for=' + elementId + ']');
        } else {
            elementFor = $form.find("[data-valmsg-replace]");

            //reset unobtrusive validation summary, if it exists
            $form.find("[data-valmsg-summary=true]")
           .removeClass("validation-summary-errors")
           .addClass("validation-summary-valid")
           .find("ul").empty();
        }

        //reset jQuery Validate's internals
        // $form.validate().resetForm();

        //reset unobtrusive field level, if it exists
        elementFor
            .removeClass("field-validation-error")
            .addClass("field-validation-valid")
            .empty();

        return $form;
    };

    //reset a form given a jQuery selected form or a child
    //by default validation is also reset
    $.fn.formReset = function (resetValidation) {
        var $form = this.closest('form');

        $form[0].reset();

        if (resetValidation == undefined || resetValidation) {
            $form.resetValidation();
        }

        return $form;
    }
})(jQuery);
