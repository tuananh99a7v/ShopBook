﻿function AjaxPostAsync(url, objAjax, successCallback) {
    AjaxRequestClientAPI("POST", url, objAjax, successCallback, true, "json");
}
function AjaxGetAsync(url, objAjax, successCallback) {
    AjaxRequestClientAPI("GET", url, objAjax, successCallback, true, "json");
}
function AjaxPutAsync(url, objAjax, successCallback) {
    AjaxRequestClientAPI("PUT", url, objAjax, successCallback, true, "json");
}
function AjaxDeleteAsync(url, objAjax, successCallback) {
    AjaxRequestClientAPI("DELETE", url, objAjax, successCallback, true, "json");
}
function AjaxPost(url, objAjax, successCallback) {
    AjaxRequestClientAPI("POST", url, objAjax, successCallback, false, "json");
}
function AjaxGet(url, objAjax, successCallback) {
    AjaxRequestClientAPI("GET", url, objAjax, successCallback, false, "json");
}
function AjaxPut(url, objAjax, successCallback) {
    AjaxRequestClientAPI("PUT", url, objAjax, successCallback, false, "json");
}
function AjaxDelete(url, objAjax, successCallback) {
    AjaxRequestClientAPI("DELETE", url, objAjax, successCallback, false, "json");
}
function AjaxRequestClientAPI(type, url, objAjax, successCallback, async, dataType) {
    var parameters = undefined, divLoading = undefined;
    if (objAjax !== undefined && objAjax !== null) {
        parameters = objAjax.parameters;
        divLoading = objAjax.divLoading;
    }
    if (divLoading !== undefined && divLoading !== "")
        $(divLoading).loading("toggle");
    $.ajax({
        type: type === undefined ? "POST" : type,
        url: url === undefined ? "" : url,
        contentType: "application/json; charset=utf-8",
        data: parameters === undefined ? "" : parameters,
        dataType: dataType === undefined ? "json" : dataType,
        async: async === undefined ? true : async,
        crossDomain: true,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            xhr.setRequestHeader("Authorization", "Bearer " + GetCookie("access_token"));
        },
        success: function (data) {
            if (data.data !== undefined)
                data = data.data;
            if (divLoading !== undefined && divLoading !== "")
                $(":loading").loading("stop");
            if (successCallback !== undefined && typeof successCallback === "function") successCallback(data);
        },
        error: function (xhr, textStatus) {
            if (divLoading !== undefined && divLoading !== "")
                $(":loading").loading("stop");
            if (xhr.status === 400 || xhr.status === 401) {
                console.log("StatusText: " + xhr.statusText + ", Message: " + xhr.responseJSON.Message);
                SwalNotifyError(xhr.responseJSON.Message);
                return;
            } else {
                var message = xhr.responseText !== undefined ? xhr.responseText : textStatus;
                console.log(message);
                SwalNotifyError("Có lỗi xảy ra. Xin vui lòng thử lại sau hoặc thông báo với quản trị. Chi tiết: F12");
                return;
            }
        }
    });
}
function AjaxLoadPartial(url, divLoading, successCallback) {
    AjaxRequestPartial(url, divLoading, successCallback);
}
function AjaxPostLoadPartial(url, parameters, divLoading, successCallback) {
    AjaxRequestPartial(url, divLoading, successCallback, "POST", parameters);
}
function AjaxRequestPartial(url, divLoading, successCallback, type, parameters) {
    if (divLoading !== undefined && divLoading !== "")
        $(divLoading).loading("toggle");
    $.ajax({
        type: type === undefined ? "GET" : type,
        url: url === undefined ? "" : url,
        contentType: "application/json; charset=utf-8",
        data: parameters === undefined ? "" : parameters,
        async: true,
        success: function (data) {
            if (divLoading !== undefined && divLoading !== "")
                $(":loading").loading("stop");
            successCallback(data);
        },
        error: function (xhr, textStatus) {
            if (divLoading !== undefined && divLoading !== "")
                $(":loading").loading("stop");
            if (xhr.status === 400 || xhr.status === 401) {
                console.log("StatusText: " + xhr.statusText + ", Message: " + xhr.responseJSON.Message);
                SwalNotifyError(xhr.responseJSON.Message);
                return;
            } else {
                var message = xhr.responseText !== undefined ? xhr.responseText : textStatus;
                console.log(message);
                SwalNotifyError("Có lỗi xảy ra. Xin vui lòng thử lại sau hoặc thông báo với quản trị. Chi tiết: F12");
                return;
            }
        }
    });
}

function AjaxFile(url, objAjax, successCallback) {
    $.ajax({
        url: url,
        type: 'POST',
        contentType: false,
        processData: false,
        data: objAjax.parameters,
        success: function (response) {
            successCallback(response);
        },
        error: function (xhr, textStatus, errorThrown) {
            if (xhr.status === 400 || xhr.status === 401) {
                console.log("StatusText: " + xhr.statusText + ", Message: " + xhr.responseJSON.Message);
                SwalNotifyError(xhr.responseJSON.Message);
                return;
            } else {
                var message = xhr.responseText !== undefined ? xhr.responseText : textStatus;
                console.log(message);
                SwalNotifyError("Có lỗi xảy ra. Xin vui lòng thử lại sau hoặc thông báo với quản trị. Chi tiết: F12");
                return;
            }
        }
    });
}
/*SetCookie*/
function SetCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + exdays * 24 * 60 * 60 * 1000);
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}
/*GetCookie*/
function GetCookie(cname) {
    if (document.cookie === "") return "";
    var name = cname + "=";
    try {
        var ca = document.cookie.split(";");
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) === " ") {
                c = c.substring(1);
            }
            if (c.indexOf(name) === 0) {
                return c.substring(name.length, c.length);
            }
        }
        return "";
    } catch (e) {
        return "";
    }
}
/*CreateURL*/
function CreateURL(prefix, objValue) {
    if (prefix === undefined) return "";
    if (objValue === undefined) return prefix;
    var url = prefix + "?";
    for (var prop in objValue) {
        if (objValue.hasOwnProperty(prop)) {
            url += prop + "=" + objValue[prop] + "&";
        }
    }
    return (url + "*").replace("&*", "");
}
/*Phân trang quản trị*/
function PaginationPageAdmin(totalPage, prefixUrl, objValue, functionLoadResult) {
    PaginationPageAdminMain("gg-pagination-footer", "table-body", totalPage, prefixUrl, objValue, functionLoadResult);
}
function PaginationPageAdminMain(idpagination, idResult, totalPage, prefixUrl, objValue, functionLoadResult) {
    if (totalPage === 0) totalPage = 1;
    $("#" + idpagination).twbsPagination({
        totalPages: totalPage,
        visiblePages: 4,
        first: '<span data-toggle="tooltip" data-placement="top" data-original-title="Trang đầu tiên"><i data-feather="skip-back"></i></span>',
        last: '<span data-toggle="tooltip" data-placement="top" data-original-title="Trang cuối cùng"><i data-feather="skip-forward"></i></span>',
        prev: '<span data-toggle="tooltip" data-placement="top" data-original-title="Trang trước"><i data-feather="chevron-left"></i></span>',
        next: '<span data-toggle="tooltip" data-placement="top" data-original-title="Trang sau"><i data-feather="chevron-right"></i></span>',
        onPageClick: function (event, page) {
            AjaxLoadPartial(CreateURL(prefixUrl, objValue) + page, undefined,
                function (html) {
                    $("#" + idResult).html(html);
                    window.feather.replace();
                    window.LoadSkill();
                    if (functionLoadResult !== undefined)
                        functionLoadResult();
                });
        }
    });
}

function LoadPaginationPageInfoLog(totalPage, urlLoadContent) {
    $('#gg-pagination-footer-log').twbsPagination({
        totalPages: totalPage,
        visiblePages: 4,
        first: '<span data-toggle="tooltip" data-placement="top" data-original-title="Trang đầu tiên"><i data-feather="skip-back"></i></span>',
        last: '<span data-toggle="tooltip" data-placement="top" data-original-title="Trang cuối cùng"><i data-feather="skip-forward"></i></span>',
        prev: '<span data-toggle="tooltip" data-placement="top" data-original-title="Trang trước"><i data-feather="chevron-left"></i></span>',
        next: '<span data-toggle="tooltip" data-placement="top" data-original-title="Trang sau"><i data-feather="chevron-right"></i></span>',
        onPageClick: function (event, page) {
            AjaxLoadPartial(urlLoadContent + page, undefined,
                function (html) {
                    $("#table-body-log").html(html);
                    window.feather.replace();
                });
        }
    });
}

function LoadPaginationPageInfo(totalPage, urlLoadContent) {
    if (totalPage === 0)
        totalPage = 1;
    $('#gg-pagination-footer').twbsPagination({
        totalPages: totalPage,
        visiblePages: 4,
        first: '<span data-toggle="tooltip" data-placement="top" data-original-title="Trang đầu tiên"><i data-feather="skip-back"></i></span>',
        last: '<span data-toggle="tooltip" data-placement="top" data-original-title="Trang cuối cùng"><i data-feather="skip-forward"></i></span>',
        prev: '<span data-toggle="tooltip" data-placement="top" data-original-title="Trang trước"><i data-feather="chevron-left"></i></span>',
        next: '<span data-toggle="tooltip" data-placement="top" data-original-title="Trang sau"><i data-feather="chevron-right"></i></span>',
        onPageClick: function (event, page) {
            AjaxLoadPartial(urlLoadContent + page, undefined,
                function (html) {
                    $("#table-body").html(html);
                    window.feather.replace();
                });
        }
    });
}
/**
 * Phân trang hệ thống
 * @param {any} idPagination ID triển khai phân trang
 * @param {any} totalPage    Tổng số trang
 * @param {any} urlLoadContent URL lấy nội dung khi có sự thay đổi trang(page), lưu ý "page=" phải ở cuối URL để hứng page
 * @param {any} idLoadContent  ID để load nội dung khi có sự thay đổi trang(page)
 */

function PaginationExamOnline(idPagination, totalPage, urlLoadContent, idLoadContent, functionLoadResult) {
    $("#" + idPagination).twbsPagination({
        totalPages: totalPage,
        visiblePages: 4,
        first: '<span data-toggle="tooltip" data-placement="top" data-original-title="Trang đầu tiên"><i data-feather="skip-back"></i></span>',
        last: '<span data-toggle="tooltip" data-placement="top" data-original-title="Trang cuối cùng"><i data-feather="skip-forward"></i></span>',
        prev: '<span data-toggle="tooltip" data-placement="top" data-original-title="Trang trước"><i data-feather="chevron-left"></i></span>',
        next: '<span data-toggle="tooltip" data-placement="top" data-original-title="Trang sau"><i data-feather="chevron-right"></i></span>',
        onPageClick: function (event, page) {
            if (window.isPaginationFirst && page === 1) { window.isPaginationFirst = false; return; }
            AjaxLoadPartial(urlLoadContent + page, "body", function (html) {
                $("#" + idLoadContent).html(html);
                window.feather.replace();
                if (functionLoadResult !== undefined)
                    functionLoadResult();
            });
        }
    });
}
/*
 * Thẻ Tag
 */
function LoadSelect2Tag(divId, url, value, objAjax, prefixId, prefixName) {
    if (value === undefined) value = "";
    var data = [];
    AjaxGet(url, objAjax,
        function (result) {
            for (var i = 0; i < result.length; i++) {
                data.push({ id: result[i][prefixId], text: result[i][prefixName] });
            }
            $("#" + divId).select2({
                createSearchChoice: function (term, data) {
                    if ($(data).filter(function () {
                        return this.text.localeCompare(term) === 0;
                    }).length === 0) {
                        return { id: term, text: term };
                    }
                    return { id: term, text: term };
                },
                multiple: true,
                data: data
            });
            $("#" + divId).val(value).trigger("change");
        });
}
function AutoSetTextSelect2(divIdGet, divIdSet) {
    $("#" + divIdGet).on("change", function () {
        $("#" + divIdSet).val(GetTextSelectedSelect2(divIdGet).join().trim());
    });
}
function GetTextSelectedSelect2(divId) {
    var array = [];
    var result = $("#" + divId).select2("data");
    for (var i = 0; i < result.length; i++) {
        array.push(" " + result[i].text);
    }
    return array;
}
function GetTextSelectedChosen(divId) {
    var options = $("#" + divId + " option:selected");

    var values = $.map(options, function (option) {
        return option.text;
    });
    return values.join().trim();
}
/*Thực hiện kiểm tra UserName*/
function IsSpaceString(value) {
    var spaceChars = " ";
    for (var i = 0; i < value.length; i++) {
        if (spaceChars.indexOf(value[i]) >= 0)
            return true;
    }
    return false;
}
function Is_String(value) {
    var spaceChars = "_";
    for (var i = 0; i < value.length; i++) {
        if (spaceChars.indexOf(value[i]) >= 0)
            return true;
    }
    return false;
}
function IsInvalidUserName(value) {
    if (Is_String(value))
        value = value.replace(/_/g, "");
    var pattern = /^[a-zA-Z0-9]+$/;
    if (pattern.test(value)) {
        return true;
    }
    else return false;
}
/*
 *FormatMoney
 */
String.prototype.reverse = function () { return this.split("").reverse().join(""); }
function FormatMoney(input) {
    var x = input.value;
    x = x.replace(/,/g, ""); // Strip out all commas
    x = x.reverse();
    x = x.replace(/.../g, function (e) {
        return e + ",";
    }); // Insert new commas
    x = x.reverse();
    x = x.replace(/^,/, ""); // Remove leading comma
    input.value = x;
}
/*
*********************************ShowMessage*************************************
*/
function ShowMessage(content, title, width) {
    $(document.body).append("<div id='DialogID' style='display: none'><p id='DialogContent'></p></div>");
    if (title === undefined || title === "")
        title = "Thông báo";
    if (width === undefined)
        width = 550;
    $("#DialogContent").html(content);
    $("#DialogID").dialog({
        title: title,
        autoOpen: true,
        resizable: false,
        modal: true,
        height: 'auto',
        overflow: 'auto',
        maxHeight: $(window).height() * 0.9,
        width: width,
        buttons: [{
            text: "Đóng",
            'class': "btn dialog-close",
            click: function () {
                $(this).dialog("close");
            }
        }]
    });
}
function ShowMessageDiv(divID, title, width) {
    if (title === undefined || title === "")
        title = "Thông báo";
    if (width === undefined)
        width = 550;
    $("#" + divID).dialog({
        title: title,
        autoOpen: true,
        resizable: false,
        modal: true,
        height: "auto",
        overflow: "auto",
        position: { my: "top", at: "top+50" },
        maxHeight: $(window).height() * 0.9,
        width: width,
        buttons: [{
            text: "Đóng",
            'class': "btn dialog-close",
            click: function () {
                $(this).dialog("close");
            }
        }]
    });
}
function ShowPartial(url, title, width, id) {
    if (id === undefined)
        id = "Loga" + parseInt(Math.random() * 10000);
    $(document.body).append("<div id='DialogPartial" + id + "' style='display: none'></div>");
    if (title === undefined || title === "")
        title = "Thông báo";
    if (width === undefined)
        width = 550;
    $("#DialogPartial" + id).dialog({
        title: title,
        autoOpen: true,
        resizable: false,
        modal: true,
        height: "auto",
        overflow: "auto",
        position: { my: "top", at: "top+50" },
        maxHeight: $(window).height() * 0.9,
        width: width,
        open: function (event, ui) {
            $(this).load(url);
        },
        buttons: [{
            text: "Đóng",
            'class': "btn dialog-close",
            click: function () {
                $(this).dialog("close");
            }
        }]
    });
}
//function CoreConfirm(text, title, width, textConfirm, functionConfirm) {
//    $(document.body).append("<div id='DialogConfirm' style='display: none'><p>" + text + "</p></div>");
//    $("#DialogConfirm").dialog({
//        title: title,
//        autoOpen: true,
//        resizable: false,
//        modal: true,
//        height: 'auto',
//        overflow: 'auto',
//        maxHeight: $(window).height() * 0.9,
//        width: width,
//        buttons: [{
//            text: textConfirm,
//            'class': "btn btn-primary",
//            click: function () {
//                functionConfirm();
//                $(this).dialog("close");
//            }
//        },
//        {
//            text: "Đóng",
//            'class': "btn dialog-close",
//            click: function () {
//                $(this).dialog("close");
//            }
//        }]
//    });
//}
//function Delete(id, label) {
//    $(document.body).append("<div id='DialogDeleteID' style='display: none'><p id='DialogDeleteContent'></p></div>");
//    $("#DialogDeleteContent").html("Bạn có chắc chắn muốn xóa <b>" + label + "</b> ?");
//    $("#DialogDeleteID").dialog({
//        title: "Xóa thông tin",
//        autoOpen: true,
//        resizable: false,
//        modal: true,
//        height: 'auto',
//        overflow: 'auto',
//        maxHeight: $(window).height() * 0.9,
//        width: 500,
//        buttons: [{
//            text: "Đồng ý",
//            'class': "btn btn-primary",
//            click: function () {
//                ComfirmDelete(id);
//                $(this).dialog("close");
//            }
//        },
//        {
//            text: "Đóng",
//            'class': "btn dialog-close",
//            click: function () {
//                $(this).dialog("close");
//            }
//        }]
//    });
//}
//function DeleteRole(id, label, roleName) {
//    $(document.body).append("<div id='DialogDeleteRoleID' style='display: none'><p id='DialogDeleteRoleContent'></p></div>");
//    $("#DialogDeleteRoleContent").html("Bạn có chắc chắn muốn xóa quyền " + roleName + " của <b>" + label + "</b> ?");
//    $('#DialogDeleteRoleID').dialog({
//        title: "Xóa quyền",
//        autoOpen: true,
//        resizable: false,
//        modal: true,
//        height: 'auto',
//        overflow: 'auto',
//        maxHeight: $(window).height() * 0.9,
//        width: 500,
//        buttons: [{
//            text: "Đồng ý",
//            'class': "btn btn-primary",
//            click: function () {
//                ComfirmDeleteRole(id, roleName);
//                $(this).dialog("close");
//            }
//        },
//        {
//            text: "Đóng",
//            'class': "btn dialog-close",
//            click: function () {
//                $(this).dialog("close");
//            }
//        }]
//    });
//}
//function ResetPassword(id, label) {
//    $(document.body).append("<div id='DialogResetPasswordID' style='display: none'><p id='DialogResetPasswordContent'></p></div>");
//    $("#DialogResetPasswordContent").html("Bạn có chắc chắn muốn reset mật khẩu của <b>" + label + "</b> ?");
//    $('#DialogResetPasswordID').dialog({
//        title: "Reset mật khẩu",
//        autoOpen: true,
//        resizable: false,
//        modal: true,
//        height: 'auto',
//        overflow: 'auto',
//        maxHeight: $(window).height() * 0.9,
//        width: 500,
//        buttons: [{
//            text: "Đồng ý",
//            'class': "btn btn-primary",
//            click: function () {
//                ComfirmResetPassword(id);
//                $(this).dialog("close");
//            }
//        },
//        {
//            text: "Đóng",
//            'class': "btn dialog-close",
//            click: function () {
//                $(this).dialog("close");
//            }
//        }]
//    });
//}
//function Confirm(id, label, title, funcConfirm) {
//    $(document.body).append("<div id='DialogConfirmID' style='display: none'><p id='DialogConfirmContent'></p></div>");
//    $("#DialogConfirmContent").html(label);
//    $('#DialogConfirmID').dialog({
//        title: title,
//        autoOpen: true,
//        resizable: false,
//        modal: true,
//        height: 'auto',
//        overflow: 'auto',
//        maxHeight: $(window).height() * 0.9,
//        width: 500,
//        buttons: [{
//            text: "Đồng ý",
//            'class': "btn btn-primary",
//            click: function () {
//                funcConfirm(id);
//                $(this).dialog("close");
//            }
//        },
//        {
//            text: "Đóng",
//            'class': "btn dialog-close",
//            click: function () {
//                $(this).dialog("close");
//            }
//        }]
//    });
//}
//function ShowDialogInsertOrUpdate(idDialog, functUpdate, pWidth) {
//    ShowForm(idDialog, "Cập nhật thông tin", pWidth, functUpdate);
//}
//function ShowForm(idDialog, pTitle, pWidth, functUpdate) {
//    if (pWidth === 'undefined' || pWidth === null)
//        pWidth = 550;
//    $("#" + idDialog).dialog({
//        title: pTitle,
//        autoOpen: true,
//        resizable: false,
//        modal: true,
//        overflow: 'auto',
//        height: 'auto',
//        position: { my: 'top', at: 'top+50' },
//        maxHeight: $(window).height() * 0.9,
//        width: pWidth,
//        minHeight: 10,
//        buttons: [{
//            text: "Cập nhật",
//            'class': "btn btn-primary btn-callapi",
//            click: function () {
//                functUpdate(this);
//            }
//        },
//        {
//            text: "Đóng",
//            'class': "btn dialog-close",
//            click: function () {
//                $(this).dialog("close");
//            }
//        }]
//    });
//}
//function ShowFormExportExcel(idDialog, pTitle, pWidth, functUpdate) {
//    if (pWidth === undefined || pWidth === 0)
//        pWidth = 550;
//    $("#" + idDialog).dialog({
//        title: pTitle,
//        autoOpen: true,
//        resizable: false,
//        modal: true,
//        overflow: 'auto',
//        height: 'auto',
//        position: { my: 'top', at: 'top+50' },
//        maxHeight: $(window).height() * 0.9,
//        width: pWidth,
//        minHeight: 10,
//        buttons: [{
//            text: "Lưu cấu hình và Xuất Excel",
//            'class': "btn btn-primary btn-callapi",
//            click: function () {
//                functUpdate(this);
//            }
//        },
//        {
//            text: "Đóng",
//            'class': "btn dialog-close",
//            click: function () {
//                $(this).dialog("close");
//            }
//        }]
//    });
//}
//function ShowFormTemp(idDialog, pTitle, pWidth, functSaveTemp, functSave) {
//    if (pWidth === 'undefined' || pWidth === null)
//        pWidth = 550;
//    $("#" + idDialog).dialog({
//        title: pTitle,
//        autoOpen: true,
//        resizable: false,
//        modal: true,
//        overflow: 'auto',
//        height: 'auto',
//        position: { my: 'top', at: 'top+50' },
//        maxHeight: $(window).height() * 0.9,
//        width: pWidth,
//        minHeight: 10,
//        buttons: [{
//            text: "Lưu chính thức",
//            'class': "btn btn-primary",
//            click: function () {
//                functSave(this);
//            }
//        },
//        {
//            text: "Lưu nháp",
//            'class': "btn btn-danger",
//            click: function () {
//                functSaveTemp(this);
//            }
//        },
//        {
//            text: "Đóng",
//            'class': "btn dialog-close",
//            click: function () {
//                $(this).dialog("close");
//            }
//        }]
//    });
//}
//function ShowDialogContent(id, label, title, content, funct) {
//    $(document.body).append("<div id='DialogID' style='display: none'><p id='DialogContent'></p></div>");
//    $("#DialogContent").html(content + "<b>" + label + "</b> ?");
//    $('#DialogID').dialog({
//        title: title,
//        autoOpen: true,
//        resizable: false,
//        modal: true,
//        height: 'auto',
//        overflow: 'auto',
//        maxHeight: $(window).height() * 0.9,
//        width: 500,
//        buttons: [{
//            text: "Đồng ý",
//            'class': "btn btn-primary",
//            click: function () {
//                funct(this);
//                $(this).dialog("close");
//            }
//        },
//        {
//            text: "Đóng",
//            'class': "btn dialog-close",
//            click: function () {
//                $(this).dialog("close");
//            }
//        }]
//    });
//}
/*
 ***********************LoadCombobox***********************
 */
//divID: thẻ div cần nhét combobox
//url: Đường dẫn
//isAll: Có lấy bộ chọn tất cả hay không
//isChosen: Có sử dụng Chosen không
//valueSelect: Giá trị mặc định cho bản ghi được chọn
//valueMultiple: Giá trị kiểu Multiple select
//prefixID: Tiền tố ID
//prefixName: Tiền tố Name
function LoadCombobox(divId, url, isAll, isChosen, valueSelect, valueMultiple, objAjax, prefixId, prefixName, maxHeight, textAll, customRender) {
    if (isAll === undefined) isAll = false;
    if (isChosen === undefined) isChosen = false;
    if (maxHeight === undefined) maxHeight = 150;
    var htmlText = "";

    var parameters = undefined, divLoading = undefined;
    if (objAjax !== undefined && objAjax !== null) {
        parameters = objAjax.parameters;
        divLoading = objAjax.divLoading;
    }
    if (divLoading !== undefined && divLoading !== "")
        $(divLoading).loading("toggle");
    $.ajax({
        processData: false,
        type: "GET",
        url: url,
        contentType: "application/json; charset=utf-8",
        data: parameters === undefined ? "" : parameters,
        dataType: "json",
        async: false,
        crossDomain: true,
        beforeSend: function (xhr) {
            xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            xhr.setRequestHeader("Authorization", "Bearer " + GetCookie("access_token"));
        },
        success: function (data) {
            if (data.data !== undefined)
                data = data.data;
            if (divLoading !== undefined && divLoading !== "")
                $(":loading").loading("stop");

            if (isAll) {
                if (textAll === undefined)
                    htmlText = '<option selected="selected" value= "0">Tất cả</option>';
                else
                    htmlText = '<option selected="selected" value= "0">' + textAll + '</option>';
            }
            if (customRender != undefined) {
                customRender(data);
            }
            else {
                for (var i = 0; i < data.length; i++) {
                    htmlText += "<option value='" + data[i][prefixId] + "'>" + data[i][prefixName] + "</option>";
                }
            }
            $("#" + divId).html(htmlText);
            if (isChosen) {
                $("#" + divId).chosen({ no_results_text: "Không tìm thấy kết quả: " });
                $("#" + divId + "_chosen .chosen-drop ul.chosen-results").css("max-height", maxHeight + "px");
                $("#" + divId).trigger("chosen:updated");
                if (valueSelect !== undefined)
                    $("#" + divId).val(valueSelect).trigger("chosen:updated");
                if (valueMultiple !== undefined)
                    $("#" + divId).val(JSON.parse("[" + valueMultiple + "]")).trigger("chosen:updated");
            }
            else {
                if (valueSelect === undefined) return;
                $("#" + divId).val(valueSelect);
            }
        },
        error: function (xhr, textStatus) {
            if (divLoading !== undefined && divLoading !== "")
                $(":loading").loading("stop");
            if (xhr.status === 400 || xhr.status === 401) {
                console.log("StatusText: " + xhr.statusText + ", Message: " + xhr.responseJSON.Message);
                SwalNotifyError(xhr.responseJSON.Message);
                return;
            } else {
                var message = xhr.responseText !== undefined ? xhr.responseText : textStatus;
                console.log(message);
                SwalNotifyError("Có lỗi xảy ra. Xin vui lòng thử lại sau hoặc thông báo với quản trị. Chi tiết: F12");
                return;
            }
        }
    });
}
function LoadComboboxAsync(divId, url, isAll, isChosen, valueSelect, valueMultiple, objAjax, prefixId, prefixName, maxHeight) {
    if (isAll === undefined) isAll = false;
    if (isChosen === undefined) isChosen = false;
    if (maxHeight === undefined) maxHeight = 150;
    var htmlText = "";
    AjaxGetAsync(url, objAjax,
        function (result) {
            if (isAll)
                htmlText = '<option selected="selected" value= "0">Tất cả</option>';
            for (var i = 0; i < result.length; i++) {
                htmlText += "<option value=" + result[i][prefixId] + ">" + result[i][prefixName] + "</option>";
            }
            $("#" + divId).html(htmlText);
            if (isChosen) {
                $("#" + divId).chosen({ no_results_text: "Không tìm thấy kết quả: " });
                $("#" + divId + "_chosen .chosen-drop ul.chosen-results").css("max-height", maxHeight + "px");
                $("#" + divId).trigger("chosen:updated");
                if (valueSelect !== undefined)
                    $("#" + divId).val(parseInt(valueSelect)).trigger("chosen:updated");
                if (valueMultiple !== undefined)
                    $("#" + divId).val(JSON.parse("[" + valueMultiple + "]")).trigger("chosen:updated");
            }
            else {
                if (valueSelect === undefined) return;
                $("#" + divId).val(parseInt(valueSelect));
            }
        });
}
function LoadComboboxPost(divID, url, isAll, isChosen, valueSelect, valueMultiple, objAjax, prefixID, prefixName) {
    if (isAll === undefined) isAll = false;
    if (isChosen === undefined) isChosen = false;
    var htmlText = "";
    AjaxPost(url, objAjax,
        function (result) {
            if (isAll)
                htmlText = '<option selected="selected" value= "0">Tất cả</option>';
            for (var i = 0; i < result.length; i++) {
                htmlText += "<option value=" + result[i][prefixID] + ">" + result[i][prefixName] + "</option>";
            }
            $("#" + divID).html(htmlText);
            if (isChosen) {
                $("#" + divID).chosen({ no_results_text: "Không tìm thấy kết quả: " });
                $("#" + divID + "_chosen .chosen-drop ul.chosen-results").css("max-height", "150px");
                $("#" + divID).trigger("chosen:updated");
                if (valueSelect !== undefined)
                    $("#" + divID).val(parseInt(valueSelect)).trigger('chosen:updated');
                if (valueMultiple !== undefined)
                    $("#" + divID).val(JSON.parse("[" + valueMultiple + "]")).trigger('chosen:updated');
            }
            else {
                if (valueSelect === undefined) return;
                $("#" + divID).val(parseInt(valueSelect));
            }
        });
}
//Người dùng
function LoadComboboxUser(divID, isAll, isChosen, valueSelect, valueMultiple, objAjax) {
    if (isAll === undefined) isAll = false;
    if (isChosen === undefined) isChosen = false;
    var htmlText = "";
    AjaxGet("/api/user/user_select", objAjax,
        function (result) {
            if (isAll)
                htmlText = "<option selected='selected' value= '0'>Tất cả</option>";
            for (var i = 0; i < result.length; i++) {
                htmlText += "<option value='" + result[i].Id + "'>" + result[i].UserName + "</option>";
            }
            $("#" + divID).html(htmlText);
            if (isChosen) {
                $(".chosen-select").chosen({ no_results_text: "Không tìm thấy kết quả: " });
                //$("#" + divID + "_chosen .chosen-drop ul.chosen-results").css("max-height", "150px");
                $("#" + divID).trigger("chosen:updated");
                if (valueSelect !== undefined)
                    $("#" + divID).val(parseInt(valueSelect)).trigger("chosen:updated");
                if (valueMultiple !== undefined)
                    $("#" + divID).val(JSON.parse("[" + valueMultiple + "]")).trigger("chosen:updated");
            }
            else {
                if (valueSelect === undefined) return;
                $("#" + divID).val(parseInt(valueSelect));
            }
        });
}
//Hàm thực thi Datepicker
function ShowDatepicker(strId) {
    $.datepicker.regional['vi'] = {
        closeText: 'Đóng',
        prevText: '&#x3c;Trước',
        nextText: 'Tiếp&#x3e;',
        currentText: 'Hôm nay',
        monthNames: ['Tháng Một', 'Tháng Hai', 'Tháng Ba', 'Tháng Tư', 'Tháng Năm', 'Tháng Sáu',
            'Tháng Bảy', 'Tháng Tám', 'Tháng Chín', 'Tháng Mười', 'Tháng Mười Một', 'Tháng Mười Hai'],
        monthNamesShort: ['Tháng 1', 'Tháng 2', 'Tháng 3', 'Tháng 4', 'Tháng 5', 'Tháng 6',
            'Tháng 7', 'Tháng 8', 'Tháng 9', 'Tháng 10', 'Tháng 11', 'Tháng 12'],
        dayNames: ['Chủ Nhật', 'Thứ Hai', 'Thứ Ba', 'Thứ Tư', 'Thứ Năm', 'Thứ Sáu', 'Thứ Bảy'],
        dayNamesShort: ['CN', 'T2', 'T3', 'T4', 'T5', 'T6', 'T7'],
        dayNamesMin: ['CN', 'T2', 'T3', 'T4', 'T5', 'T6', 'T7'],
        weekHeader: 'Tu',
        dateFormat: 'dd/mm/yy',
        firstDay: 0,
        isRTL: false,
        showMonthAfterYear: false,
        yearSuffix: ''
    };
    $.datepicker.setDefaults($.datepicker.regional['vi']);
    $(strId).datepicker({ showWeek: true, firstDay: 1, constrainInput: false });
}

//*************************************************************************
//****************************summernote**********************************
//**************************************************************************
function Summernote(strId, height) {
    if (height === undefined) height = 100;
    $(strId).summernote({
        lang: 'vi-VN',
        focus: true,
        height: height,
        tabsize: 2,
        toolbar: [
            ['style', ['bold', 'italic', 'underline', 'clear']],
            ['font', ['strikethrough', 'superscript', 'subscript']],
            ['fontsize', ['fontsize']],
            ['color', ['color']],
            ['para', ['ul', 'ol', 'paragraph']],
            ['insert', ['link', 'picture', 'video']],
            ['height', ['height']],
            ['view', ['fullscreen', 'codeview']]
        ],
        callbacks: {
            onImageUpload: function (image) {
                UploadImage(image[0], strId);
            }
        }
    });
}
function UploadImage(image, id) {
    var data = new FormData();
    data.append("image", image);
    $.ajax({
        data: data,
        type: "POST",
        url: "/Home/UploadFileSummerNote",
        cache: false,
        processData: false,
        contentType: false,
        success: function (response) {
            var data = response.data;
            if (response.code === 200)
                $(id).summernote("insertImage", data);
            else
                ShowMessage(data, "Thông báo lỗi", 550);
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log("Error: " + xhr.responseText);
            ShowMessage("Có lỗi xảy ra. Xin vui lòng thử lại sau hoặc thông báo với quản trị.", "Thông báo lỗi", 550);
        }
    });
}
//Hàm làm tròn số: 3.2222222 -> 3.22
function MathRound(x) {
    return Math.round(parseFloat(x) * 1000) / 1000;
}
function MathPercent(iNumber, iTotal) {
    if (iTotal === 0) return 0;
    return parseFloat((iNumber * 100 / iTotal).toFixed(1));
}
//VD: "ABC" -> 'ABC'
function ReplaceJS(str) {
    return str.replace(/\"/g, '\'');
}
function ConvertStringMoneyToNumber(number) {
    return number.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,") + " VNĐ";
}
function DateTimeNow() {
    return $.datepicker.formatDate('dd/mm/yy', new Date());
}
//"2018-07-20T12:30:00" -> 01-08-2017 12:30
function ConvertDateSql(datetime) {
    var arrDate = datetime.substring(0, 10).split('-');
    var arrTime = datetime.substring(11, 16);
    return arrDate[2] + '/' + arrDate[1] + '/' + arrDate[0] + " " + arrTime;
}
//"2018-07-20T12:30:00" -> 01/08/2017
function ConvertDateSqlToDateString(datetime) {
    var arrDate = datetime.substring(0, 10).split('-');
    return arrDate[2] + '/' + arrDate[1] + '/' + arrDate[0];
}
//"2018-07-20T12:30:00" -> 12:30
function ConvertDateSqlToTimeString(datetime) {
    var arrTime = datetime.substring(11, 16);
    return arrTime;
}
//"20/07/2018 12:30" -> 2018-07-20 12:30
function CheckDateTime(datetime) {
    try {
        var dateTimeArr = datetime.trim().split(" ");
        var arrDate = dateTimeArr[0].split('/');
        var arrTime = dateTimeArr[1].split(':');
        if (arrDate[0] > 31 || arrDate[1] > 12 || arrDate[2] > 2099 || arrTime[0] > 24 || arrTime[1] > 60)
            return false;
        else
            return true;
    } catch (e) {
        return false;
    }
}
//"20/07/2018 12:30" -> 2018-07-20 12:30
function ConvertDateTime(datetime) {
    var arrDate = datetime.substring(0, 10).split('/');
    var arrTime = datetime.substring(11, 16);
    return arrDate[2] + '-' + arrDate[1] + '-' + arrDate[0] + " " + arrTime;
}
//18/07/2018 => 2018/07/18
function FormatDate(strDate) {
    var arrBirthDate = strDate.split('/');
    return arrBirthDate[2] + '/' + arrBirthDate[1] + '/' + arrBirthDate[0];
}
//07/18/2018 => 2018/07/18
function FormatDateMMddyyyy(strDate) {
    var arrBirthDate = strDate.split('/');
    return arrBirthDate[2] + '/' + arrBirthDate[0] + '/' + arrBirthDate[1];
}
function ConvertStringToDate(strDate) {//dd/MM/YYYY
    var arrBirthDate = strDate.split('/');
    return new Date(arrBirthDate[2] + '/' + arrBirthDate[1] + '/' + arrBirthDate[0]);
}
function ConvertStringToDateTime(strDateTime) {//dd/MM/YYYY hh:mm
    var arr = strDateTime.trim().split(' ');
    var arrBirthDate = arr[0].split('/');
    var arrTime = arr[1].split(':');
    return new Date(arrBirthDate[2], arrBirthDate[1], arrBirthDate[0], arrTime[0], arrTime[1]);
}
function ConvertDate(strDate) {//dd/MM/YYYY
    var arrBirthDate = strDate.split('/');
    return arrBirthDate[2] + '/' + arrBirthDate[1] + '/' + arrBirthDate[0];
}
function ConvertDateFormat(date) {//dd/MM/YYYY
    var day = date.getDate();       // yields date
    var month = date.getMonth() + 1;    // yields month (add one as '.getMonth()' is zero indexed)
    var year = date.getFullYear();  // yields year
    var hour = date.getHours();     // yields hours
    var minute = date.getMinutes(); // yields minutes
    var second = date.getSeconds(); // yields seconds

    // After this construct a string with the above results as below
    return month + "/" + day + "/" + year + " " + hour + ':' + minute + ':' + second;
}
function ConvertFulltimeToStr(fulltime) {
    var arrFull = fulltime.split('T');
    var arrDate = arrFull[0].substring(0, 10).split('-');
    var strDate = arrDate[2] + '/' + arrDate[1] + '/' + arrDate[0];
    var strTime = arrFull[1].substring(0, 8);
    return strTime + " " + strDate;
}
function CheckNumber(value) {
    return $.isNumeric(value);
}
function isNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}
//VD 1000 -> 1,000
function FormatMoneyVi(iNumber) {
    return iNumber.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,");
}

//VD 1,000 -> 1000
function ConvertMoney(strMoney) {
    if (strMoney === "" || strMoney === undefined || strMoney === null)
        return 0;
    else
        return parseFloat(strMoney.replace(/,/g, ""));
}

function ForamatUsd(value) {
    var formatter = new Intl.NumberFormat('en-US', {
        style: 'currency',
        currency: 'USD',
    });
    return formatter.format(value).replace("$", "");
}
function CheckMoney(strMoney) {
    if (strMoney === "" || strMoney === undefined || strMoney === null)
        return true;
    else
        return CheckNumber(strMoney.replace(/,/g, ""));
}
function EncodeBase64(str) {
    var base64 = { _keyStr: "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=", encode: function (e) { var t = ""; var n, r, i, s, o, u, a; var f = 0; e = base64._utf8_encode(e); while (f < e.length) { n = e.charCodeAt(f++); r = e.charCodeAt(f++); i = e.charCodeAt(f++); s = n >> 2; o = (n & 3) << 4 | r >> 4; u = (r & 15) << 2 | i >> 6; a = i & 63; if (isNaN(r)) { u = a = 64; } else if (isNaN(i)) { a = 64 } t = t + this._keyStr.charAt(s) + this._keyStr.charAt(o) + this._keyStr.charAt(u) + this._keyStr.charAt(a) } return t }, decode: function (e) { var t = ""; var n, r, i; var s, o, u, a; var f = 0; e = e.replace(/[^A-Za-z0-9\+\/\=]/g, ""); while (f < e.length) { s = this._keyStr.indexOf(e.charAt(f++)); o = this._keyStr.indexOf(e.charAt(f++)); u = this._keyStr.indexOf(e.charAt(f++)); a = this._keyStr.indexOf(e.charAt(f++)); n = s << 2 | o >> 4; r = (o & 15) << 4 | u >> 2; i = (u & 3) << 6 | a; t = t + String.fromCharCode(n); if (u !== 64) { t = t + String.fromCharCode(r); } if (a !== 64) { t = t + String.fromCharCode(i); } } t = base64._utf8_decode(t); return t; }, _utf8_encode: function (e) { e = e.replace(/\r\n/g, "\n"); var t = ""; for (var n = 0; n < e.length; n++) { var r = e.charCodeAt(n); if (r < 128) { t += String.fromCharCode(r) } else if (r > 127 && r < 2048) { t += String.fromCharCode(r >> 6 | 192); t += String.fromCharCode(r & 63 | 128) } else { t += String.fromCharCode(r >> 12 | 224); t += String.fromCharCode(r >> 6 & 63 | 128); t += String.fromCharCode(r & 63 | 128); } } return t; }, _utf8_decode: function (e) { var t = ""; var n = 0; var r = c1 = c2 = 0; while (n < e.length) { r = e.charCodeAt(n); if (r < 128) { t += String.fromCharCode(r); n++; } else if (r > 191 && r < 224) { c2 = e.charCodeAt(n + 1); t += String.fromCharCode((r & 31) << 6 | c2 & 63); n += 2 } else { c2 = e.charCodeAt(n + 1); c3 = e.charCodeAt(n + 2); t += String.fromCharCode((r & 15) << 12 | (c2 & 63) << 6 | c3 & 63); n += 3; } } return t; } };
    // Encode the String
    return base64.encode(str);
}
function DecodeBase64(str) {
    var base64 = { _keyStr: "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=", encode: function (e) { var t = ""; var n, r, i, s, o, u, a; var f = 0; e = base64._utf8_encode(e); while (f < e.length) { n = e.charCodeAt(f++); r = e.charCodeAt(f++); i = e.charCodeAt(f++); s = n >> 2; o = (n & 3) << 4 | r >> 4; u = (r & 15) << 2 | i >> 6; a = i & 63; if (isNaN(r)) { u = a = 64; } else if (isNaN(i)) { a = 64 } t = t + this._keyStr.charAt(s) + this._keyStr.charAt(o) + this._keyStr.charAt(u) + this._keyStr.charAt(a) } return t }, decode: function (e) { var t = ""; var n, r, i; var s, o, u, a; var f = 0; e = e.replace(/[^A-Za-z0-9\+\/\=]/g, ""); while (f < e.length) { s = this._keyStr.indexOf(e.charAt(f++)); o = this._keyStr.indexOf(e.charAt(f++)); u = this._keyStr.indexOf(e.charAt(f++)); a = this._keyStr.indexOf(e.charAt(f++)); n = s << 2 | o >> 4; r = (o & 15) << 4 | u >> 2; i = (u & 3) << 6 | a; t = t + String.fromCharCode(n); if (u !== 64) { t = t + String.fromCharCode(r); } if (a !== 64) { t = t + String.fromCharCode(i); } } t = base64._utf8_decode(t); return t; }, _utf8_encode: function (e) { e = e.replace(/\r\n/g, "\n"); var t = ""; for (var n = 0; n < e.length; n++) { var r = e.charCodeAt(n); if (r < 128) { t += String.fromCharCode(r) } else if (r > 127 && r < 2048) { t += String.fromCharCode(r >> 6 | 192); t += String.fromCharCode(r & 63 | 128) } else { t += String.fromCharCode(r >> 12 | 224); t += String.fromCharCode(r >> 6 & 63 | 128); t += String.fromCharCode(r & 63 | 128); } } return t; }, _utf8_decode: function (e) { var t = ""; var n = 0; var r = c1 = c2 = 0; while (n < e.length) { r = e.charCodeAt(n); if (r < 128) { t += String.fromCharCode(r); n++; } else if (r > 191 && r < 224) { c2 = e.charCodeAt(n + 1); t += String.fromCharCode((r & 31) << 6 | c2 & 63); n += 2 } else { c2 = e.charCodeAt(n + 1); c3 = e.charCodeAt(n + 2); t += String.fromCharCode((r & 15) << 12 | (c2 & 63) << 6 | c3 & 63); n += 3; } } return t; } };
    // Decode the String
    return base64.decode(str);
}
//Hàm kiểm tra số container
function CheckContainerNumber(containerNumber) {
    if (containerNumber.length !== 11) {
        SwalNotifyError("Container '" + containerNumber + "' chưa đủ 11 ký tự. Kiểm tra lại!");
        return false;
    }
    var prefix = containerNumber.slice(0, 4);
    var serial = containerNumber.slice(4, 10);
    var end = containerNumber.slice(10, 11);
    if (prefix.charAt(3).toUpperCase() !== "U") {
        SwalNotifyError("Ký tự cuối cùng của Tiếp đầu ngữ thường là chữ 'U'. Kiểm tra lại!");
        return false;
    }
    if (prefix.match(/[^a-z]/i)) {
        SwalNotifyError("Tiếp đầu ngữ chỉ có thể là chữ cái!");
        return false;
    }
    if (isNaN(Number(serial))) {
        SwalNotifyError("Số Sê-ri '" + serial + "' không hợp lệ. Kiểm tra lại.");
        return false;
    }
    containerNumber = containerNumber.toUpperCase();
    var contArray = new Array;
    for (var i = 0; i < 10; i++) {
        contArray[i] = containerNumber.charAt(i);
    }
    for (var j = 0; j < 4; j++) {
        var tiepdaungu = contArray[j];
        var alphabet = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ';
        var so = [
            10, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 34, 35, 36, 37, 38
        ];
        var pos = alphabet.search(tiepdaungu);
        contArray[j] = so[pos];
    }
    var tongso = 0;
    var sokiemtra;
    for (var k = 0; k < 10; k++) {
        contArray[k] = contArray[k] * Math.pow(2, k);
        tongso = tongso + contArray[k];
    }
    sokiemtra = tongso % 11;
    if (sokiemtra === 10) {
        sokiemtra = 0;
    }
    if (parseInt(end) !== sokiemtra) {
        SwalNotifyError("Số Container '" + containerNumber + "' không đúng. Vui lòng kiểm tra lại!");
        return false;
    }
    return true;
}
function copyToClipboard(text) {
    var dummy = document.createElement("textarea");
    document.body.appendChild(dummy);
    dummy.value = text;
    dummy.select();
    document.execCommand("copy");
    document.body.removeChild(dummy);
}
function isMobileDevice() {
    return (typeof window.orientation !== "undefined") || (navigator.userAgent.indexOf('IEMobile') !== -1);
}
function GetTimeFloat(dateTime) {
    if (dateTime.getMinutes() >= 30)
        return parseFloat(dateTime.getHours() + ".5");
    return dateTime.getHours();
}

function IsVietnameseString(value) {
    var vietnameseChars = "ÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂưăạảấầẩẫậắằẳẵặẹẻẽềềểỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ";
    for (var i = 0; i < value.length; i++) {
        if (vietnameseChars.indexOf(value[i]) >= 0)
            return true;
    }
    return false;
}
function IsSpaceString(value) {
    var spaceChars = " ";
    for (var i = 0; i < value.length; i++) {
        if (spaceChars.indexOf(value[i]) >= 0)
            return true;
    }
    return false;
}
//Kiểm tra tên đăng nhập
function IsInvalidUser(value) {
    var pattern = /^[a-zA-Z0-9]+$/;
    if (pattern.test(value)) {
        return true;
    }
    else return false;
}

function FormatVietnameseNumber(value) { // format value ở dạng số mà code hiểu sang dạng Việt Nam
    if (isNaN(value)) return false;

    var pointParts = value.toString().split(".");

    value = "";
    var first = pointParts[0];
    while (first.length > 0) {
        var i = first.length - 3;
        if (i < 0) i = 0;
        if (value.length > 0) value = "." + value;
        value = first.substring(i) + value;
        first = first.substring(0, i);
    }
    if (pointParts.length > 1) value = value + "," + pointParts[1];

    return value;
}
// Kiểm tra số điện thoại có đúng không
function checkPhoneNumber(phone) {
    var flag = false;
    phone = phone.replace('(+84)', '0');
    phone = phone.replace('+84', '0');
    phone = phone.replace('0084', '0');
    phone = phone.replace(/ /g, '');
    if (phone !== '') {
        var firstNumber = phone.substring(0, 2);
        if ((firstNumber === '09' || firstNumber === '08' || firstNumber === '03' || firstNumber === '07' || firstNumber === '05') && phone.length === 10) {
            if (phone.match(/^\d{10}/)) {
                flag = true;
            }
        } else if (firstNumber === '01' && phone.length === 11) {
            if (phone.match(/^\d{11}/)) {
                flag = true;
            }
        }
    }
    return flag;
}

function GetTextSelect2(id, prefix) {
    if (prefix === undefined) prefix = ", ";
    var text = "";
    var selected = $("#" + id).select2("data");
    for (var i = 0; i <= selected.length - 1; i++) {
        text += selected[i].text + prefix;
    }

    return (text + prefix).replace(prefix + prefix, "");
}
function CheckTypeInput(value) {
    return !(value === "" || value == null);
}