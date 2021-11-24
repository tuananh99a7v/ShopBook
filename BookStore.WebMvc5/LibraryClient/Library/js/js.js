function ShowMessage(content, title, width) {
    $(document.body).append("<div id='DialogID' style='display: none'><p id='DialogContent'></p></div>");
    if (title == 'undefined' || title == null)
        title = "Thông báo";
    if (width == 'undefined' || width == null)
        width = 550;
    $("#DialogContent").html(content);
    $('#DialogID').dialog({
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
    if (title == 'undefined' || title == null)
        title = "Thông báo";
    if (width == 'undefined' || width == null)
        width = 550;
    $('#' + divID).dialog({
        title: title,
        autoOpen: true,
        resizable: false,
        modal: true,
        height: 'auto',
        overflow: 'auto',
        position: { my: 'top', at: 'top+50' },
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
    if (id == undefined)
        id = "Loga" + parseInt(Math.random() * 10000);
    $(document.body).append("<div id='DialogPartial" + id + "' style='display: none'></div>");
    if (title === "undefined" || title == null)
        title = "Thông báo";
    if (width === "undefined" || width == null)
        width = 550;
    $('#DialogPartial' + id).dialog({
        title: title,
        autoOpen: true,
        resizable: false,
        modal: true,
        height: 'auto',
        overflow: 'auto',
        position: { my: 'top', at: 'top+50' },
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
function CoreConfirm(text, title, width, textConfirm, functionConfirm) {
    $(document.body).append("<div id='DialogConfirm' style='display: none'><p>" + text + "</p></div>");
    $('#DialogConfirm').dialog({
        title: title,
        autoOpen: true,
        resizable: false,
        modal: true,
        height: 'auto',
        overflow: 'auto',
        maxHeight: $(window).height() * 0.9,
        width: width,
        buttons: [{
                text: textConfirm,
                'class': "btn btn-primary",
                click: function () {
                    functionConfirm();
                    $(this).dialog("close");
                }
            },
            {
                text: "Đóng",
                'class': "btn dialog-close",
                click: function () {
                    $(this).dialog("close");
                }
            }]
    });
}
function Delete(id, label) {
    $(document.body).append("<div id='DialogDeleteID' style='display: none'><p id='DialogDeleteContent'></p></div>");
    $("#DialogDeleteContent").html("Bạn có chắc chắn muốn xóa <b>" + label + "</b> ?");
    $('#DialogDeleteID').dialog({
        title: "Xóa thông tin",
        autoOpen: true,
        resizable: false,
        modal: true,
        height: 'auto',
        overflow: 'auto',
        maxHeight: $(window).height() * 0.9,
        width: 500,
        buttons: [{
            text: "Đồng ý",
            'class': "btn btn-primary",
            click: function () {
                ComfirmDelete(id);
                $(this).dialog("close");
            }
        },
        {
            text: "Đóng",
            'class': "btn dialog-close",
            click: function () {
                $(this).dialog("close");
            }
        }]
    });
}
function DeleteRole(id, label, roleName) {
    $(document.body).append("<div id='DialogDeleteRoleID' style='display: none'><p id='DialogDeleteRoleContent'></p></div>");
    $("#DialogDeleteRoleContent").html("Bạn có chắc chắn muốn xóa quyền " + roleName + " của <b>" + label + "</b> ?");
    $('#DialogDeleteRoleID').dialog({
        title: "Xóa quyền",
        autoOpen: true,
        resizable: false,
        modal: true,
        height: 'auto',
        overflow: 'auto',
        maxHeight: $(window).height() * 0.9,
        width: 500,
        buttons: [{
            text: "Đồng ý",
            'class': "btn btn-primary",
            click: function () {
                ComfirmDeleteRole(id, roleName);
                $(this).dialog("close");
            }
        },
        {
            text: "Đóng",
            'class': "btn dialog-close",
            click: function () {
                $(this).dialog("close");
            }
        }]
    });
}
function ResetPassword(id, label) {
    $(document.body).append("<div id='DialogResetPasswordID' style='display: none'><p id='DialogResetPasswordContent'></p></div>");
    $("#DialogResetPasswordContent").html("Bạn có chắc chắn muốn reset mật khẩu của <b>" + label + "</b> ?");
    $('#DialogResetPasswordID').dialog({
        title: "Reset mật khẩu",
        autoOpen: true,
        resizable: false,
        modal: true,
        height: 'auto',
        overflow: 'auto',
        maxHeight: $(window).height() * 0.9,
        width: 500,
        buttons: [{
            text: "Đồng ý",
            'class': "btn btn-primary",
            click: function () {
                ComfirmResetPassword(id);
                $(this).dialog("close");
            }
        },
        {
            text: "Đóng",
            'class': "btn dialog-close",
            click: function () {
                $(this).dialog("close");
            }
        }]
    });
}
function Confirm(id, label, title, funcConfirm) {
    $(document.body).append("<div id='DialogConfirmID' style='display: none'><p id='DialogConfirmContent'></p></div>");
    $("#DialogConfirmContent").html(label);
    $('#DialogConfirmID').dialog({
        title: title,
        autoOpen: true,
        resizable: false,
        modal: true,
        height: 'auto',
        overflow: 'auto',
        maxHeight: $(window).height() * 0.9,
        width: 500,
        buttons: [{
            text: "Đồng ý",
            'class': "btn btn-primary",
            click: function () {
                funcConfirm(id);
                $(this).dialog("close");
            }
        },
        {
            text: "Đóng",
            'class': "btn dialog-close",
            click: function () {
                $(this).dialog("close");
            }
        }]
    });
}
function ShowDialogInsertOrUpdate(idDialog, functUpdate, pWidth) {
    ShowForm(idDialog, "Cập nhật thông tin", pWidth, functUpdate);
}
function ShowForm(idDialog, pTitle, pWidth, functUpdate) {
    if (pWidth === 'undefined' || pWidth == null)
        pWidth = 550;
    $("#" + idDialog).dialog({
        title: pTitle,
        autoOpen: true,
        resizable: false,
        modal: true,
        overflow: 'auto',
        height: 'auto',
        position: { my: 'top', at: 'top+50' },
        maxHeight: $(window).height() * 0.9,
        width: pWidth,
        minHeight: 10,
        buttons: [{
            text: "Cập nhật",
            'class': "btn btn-primary btn-callapi",
            click: function () {
                functUpdate(this);
            }
        },
        {
            text: "Đóng",
            'class': "btn dialog-close",
            click: function () {
                $(this).dialog("close");
            }
        }]
    });
}
function ShowFormExportExcel(idDialog, pTitle, pWidth, functUpdate) {
    if (pWidth === 'undefined' || pWidth == null)
        pWidth = 550;
    $("#" + idDialog).dialog({
        title: pTitle,
        autoOpen: true,
        resizable: false,
        modal: true,
        overflow: 'auto',
        height: 'auto',
        position: { my: 'top', at: 'top+50' },
        maxHeight: $(window).height() * 0.9,
        width: pWidth,
        minHeight: 10,
        buttons: [{
                text: "Lưu cấu hình và Xuất Excel",
                'class': "btn btn-primary btn-callapi",
                click: function () {
                    functUpdate(this);
                }
            },
            {
                text: "Đóng",
                'class': "btn dialog-close",
                click: function () {
                    $(this).dialog("close");
                }
            }]
    });
}
function ShowFormTemp(idDialog, pTitle, pWidth, functSaveTemp, functSave) {
    if (pWidth === 'undefined' || pWidth === null)
        pWidth = 550;
    $("#" + idDialog).dialog({
        title: pTitle,
        autoOpen: true,
        resizable: false,
        modal: true,
        overflow: 'auto',
        height: 'auto',
        position: { my: 'top', at: 'top+50' },
        maxHeight: $(window).height() * 0.9,
        width: pWidth,
        minHeight: 10,
        buttons: [{
            text: "Lưu chính thức",
            'class': "btn btn-primary",
            click: function () {
                functSave(this);
            }
        },
        {
            text: "Lưu nháp",
            'class': "btn btn-danger",
            click: function () {
                functSaveTemp(this);
            }
        },
        {
            text: "Đóng",
            'class': "btn dialog-close",
            click: function () {
                $(this).dialog("close");
            }
        }]
    });
}
function ShowDialogContent(id, label, title, content, funct) {
    $(document.body).append("<div id='DialogID' style='display: none'><p id='DialogContent'></p></div>");
    $("#DialogContent").html(content + "<b>" + label + "</b> ?");
    $('#DialogID').dialog({
        title: title,
        autoOpen: true,
        resizable: false,
        modal: true,
        height: 'auto',
        overflow: 'auto',
        maxHeight: $(window).height() * 0.9,
        width: 500,
        buttons: [{
            text: "Đồng ý",
            'class': "btn btn-primary",
            click: function () {
                funct(this);
                $(this).dialog("close");
            }
        },
        {
            text: "Đóng",
            'class': "btn dialog-close",
            click: function () {
                $(this).dialog("close");
            }
        }]
    });
}