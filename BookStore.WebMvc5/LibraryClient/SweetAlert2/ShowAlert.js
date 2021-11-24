﻿var objSwal = {
    title: "Thông báo",
    html: "Công cụ Shopee",
    width: null,
    footer: null,
    type: "",
    textConfirm: "Xác nhận",
    textCancel: "Đóng",
    timer: null,
    showCloseButton: false,
    showCancelButton: true,
    focusCancel: false,
    showConfirmButton: true,
    allowOutsideClick: true
};
function ResetSwalDefault() {
    objSwal.title = "Thông báo";
    objSwal.html = "Công cụ Shopee";
    objSwal.width = null;
    objSwal.footer = null;
    objSwal.type = "";
    objSwal.textConfirm = "Xác nhận";
    objSwal.textCancel = "Đóng";
    objSwal.timer = null;
    objSwal.showCloseButton = false;
    objSwal.showCancelButton = true;
    objSwal.focusCancel = false;
    objSwal.showConfirmButton = true;
    objSwal.allowOutsideClick = true;
}
function SwalMain(objSwal, FuncConfirm) {
    window.swal({
        title: objSwal.title,
        html: objSwal.html,
        width: objSwal.width,
        footer: objSwal.footer,
        type: objSwal.type,
        confirmButtonText: objSwal.textConfirm,
        cancelButtonText: objSwal.textCancel,
        timer: objSwal.timer,
        showCloseButton: objSwal.showCloseButton,
        showCancelButton: objSwal.showCancelButton,
        focusCancel: objSwal.focusCancel,
        showConfirmButton: objSwal.showConfirmButton,
        allowOutsideClick: objSwal.allowOutsideClick,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "rgb(170, 170, 170)",
        confirmButtonClass: "btn btn-success btn-callapi"
    }).then((result) => {
        if (result.value) {
            FuncConfirm();
        }
    });
}
//Thông báo góc màn hình
function SwalNotifyError(title) {
    SwalNotify(title, "error", 5000);
}
function SwalNotifyWarning(title) {
    SwalNotify(title, "warning", 5000);
}
function SwalNotifySuccess(title) {
    SwalNotify(title, "success", 5000);
}

function SwalNotify(title, type, timer) {
    const toast = window.swal.mixin({
        toast: true,
        position: "top-end",
        showConfirmButton: false,
        timer: timer === undefined ? 3000 : timer
    });
    toast({
        type: type === undefined ? "success" : type,
        title: title
    });
}
//Thông báo
function SwalMessage(html, title, width, type) {
    ResetSwalDefault();
    objSwal.html = html;
    objSwal.title = title === undefined ? "Thông báo" : title;
    objSwal.width = width === undefined ? null : width;
    objSwal.type = title === undefined ? "warning" : type;
    objSwal.showConfirmButton = false;
    objSwal.focusCancel = true;
    SwalMain(objSwal, null);
}
//Alert theo div
function SwalMessageDiv(DivID, FuncConfirm, title, width, type) {
    ResetSwalDefault();
    objSwal.html = $("#" + DivID).html();
    objSwal.title = title === undefined ? "Thông báo" : title;
    objSwal.width = width === undefined ? null : width;
    objSwal.type = type === undefined ? "question" : type;
    objSwal.allowOutsideClick = false;
    SwalMain(objSwal, FuncConfirm);
}
//Alert có nút Comfirm AlertConfirm
function SwalConfirm(html, FuncConfirm, title, width, type, textConfirm) {
    ResetSwalDefault();
    objSwal.html = html;
    objSwal.title = (title == undefined) ? "Thông báo" : title;
    objSwal.width = (width == undefined) ? null : width;
    objSwal.type = (type == undefined) ? "question" : type;
    objSwal.textConfirm = (textConfirm == undefined) ? "Xác nhận" : textConfirm;
    objSwal.allowOutsideClick = false;
    SwalMain(objSwal, FuncConfirm);
}
//Alert có 2 nút chức năng
function SwalTwoButton(html, funcConfirm1, funcConfirm2, textConfirm1, textConfirm2, title, width, type) {
    if (textConfirm2 === "")
        window.swal({
            title: title,
            html: html,
            width: width,
            footer: null,
            type: type === undefined ? "success" : type,
            confirmButtonText: textConfirm1,
            showCloseButton: false,
            showCancelButton: false,
            showConfirmButton: true,
            allowOutsideClick: false,
            confirmButtonColor: '#3085d6',
            confirmButtonClass: 'btn btn-success btn-callapi'
        }).then((result) => {
            if (result.value) {
                funcConfirm1();
            } else if (result.dismiss === window.swal.DismissReason.cancel) {
                funcConfirm2();
            }
        });
    else
        window.swal({
            title: title,
            html: html,
            width: width,
            footer: null,
            type: type === undefined ? "success" : type,
            confirmButtonText: textConfirm1,
            cancelButtonText: textConfirm2,
            showCloseButton: false,
            showCancelButton: true,
            showConfirmButton: true,
            allowOutsideClick: false,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonClass: 'btn btn-success btn-callapi',
            cancelButtonClass: 'btn btn-danger'
        }).then((result) => {
            if (result.value) {
                funcConfirm1();
            } else if (result.dismiss === window.swal.DismissReason.cancel) {
                funcConfirm2();
            }
        });
}
//Chức năng đăng nhập
function SwalLogin() {
    window.swal({
        type: "question",
        title: 'Đăng nhập hệ thống',
        html:
            '<input type="text" id="swal-username" class="swal2-input" placeholder="Tên đăng nhập">' +
            '<input type="password" id="swal-password" class="swal2-input" placeholder="Mật khẩu">',
        inputAttributes: {
            autocapitalize: 'off'
        },
        showCancelButton: true,
        allowOutsideClick: false,
        confirmButtonText: 'Đăng nhập',
        showLoaderOnConfirm: true,
        preConfirm: () => {
            var userName = document.getElementById('swal-username').value;
            var password = document.getElementById('swal-password').value;
            var loginVM = {
                UserName: userName,
                Password: password
            };
            console.log(loginVM);
            if (userName === "")
                window.swal.showValidationError("Các hạ chưa điền tên đăng nhập");
            else if (password === "")
                window.swal.showValidationError("Các hạ chưa điền mật khẩu");
            else
                return fetch("/api/user/login",
                    {
                        method: "POST",
                        headers: {
                            "Content-Type": "application/json; charset=utf-8"
                        },
                        body: JSON.stringify(loginVM)
                    }).then(response => {
                        if (!response.ok)
                            throw new Error("Đăng nhập thất bại. Xin vui lòng kiểm tra lại tài khoản.");
                        else
                            return response.json();
                    }).catch(error => {
                        window.swal.showValidationError("Đăng nhập thất bại. Xin vui lòng kiểm tra lại tài khoản.");
                    });
        },
        allowOutsideClick: () => !window.swal.isLoading()
    }).then((result) => {
        if (result.value.code === 200) {
            setCookie('LogaVNUser', JSON.stringify(result.value.data), 30);
            localStorage.setItem("access_token", result.value.data.Token);
            location.reload();
        }
    });
}

//Hàm thực hiện Show thông báo cảnh báo
function AlertMessSwal(sMess) {
    window.swal({
        title: "Thông báo",
        html: sMess,
        type: 'warning',
        animation: false,
        showCancelButton: true,
        showConfirmButton: false,
    });
}

function SwalReqAjax(title, text, FuncConfirm, textConfirm, textCancel, type, isClose, width, textSuccess) {
    window.swal({
        title: title,
        html: text,
        type: type,
        width: width,
        showCancelButton: true,
        showCloseButton: true,
        allowOutsideClick: false,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonClass: 'btn btn-success btn-callapi',
        cancelButtonClass: 'btn btn-danger',
        confirmButtonText: textConfirm,
        cancelButtonText: textCancel,
        showLoaderOnConfirm: true,
        preConfirm: () => {
            FuncConfirm();
        },
        allowOutsideClick: () => !window.swal.isLoading()
    }).then((result) => {
        if (result.value) {
            AlertMessSuccess(textSuccess);
        }
    });
}

function AlertMessSwalNotClose(sMess) {
    window.swal({
        title: "Thông báo",
        html: sMess,
        type: 'warning',
        allowOutsideClick: false,
        animation: false,
        showCancelButton: false,
        showConfirmButton: false
    });
}
function AlertMessSuccess(sMess) {
    window.swal({
        title: "Thông báo",
        html: sMess,
        type: 'success',
        animation: false,
        showCancelButton: false,
        showConfirmButton: true,
    });
}
function AlertConfirm(title, text, FuncConfirm, textConfirm, textCancel) {
    window.swal({
        title: title,
        html: text,
        type: 'warning',
        showCancelButton: true,
        showCloseButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: 'rgb(170, 170, 170)',
        confirmButtonText: textConfirm,
        cancelButtonText: textCancel
    }).then((result) => {
        if (result.value) {
            FuncConfirm();
        }
    });
}
function AlertConfirmTwoChoose(title, text, FuncConfirm, textConfirm, FuncCancel, textCancel) {
    window.swal({
        title: title,
        html: text,
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: textConfirm,
        cancelButtonText: textCancel,
        confirmButtonClass: 'btn btn-success btn-callapi',
        cancelButtonClass: 'btn btn-danger',
        buttonsStyling: false,
        reverseButtons: true
    }).then((result) => {
        if (result.value) {
            FuncConfirm();
        } else if (result.dismiss === window.swal.DismissReason.cancel) {
            FuncCancel();
        }
    });
}
function AlertSuccess(title, text, FuncConfirm, textConfirm, textCancel) {
    window.swal({
        title: title,
        html: text,
        type: 'success',
        showCancelButton: true,
        showCloseButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: 'rgb(170, 170, 170)',
        confirmButtonClass: 'btn btn-success btn-callapi',
        confirmButtonText: textConfirm,
        cancelButtonText: textCancel
    }).then((result) => {
        if (result.value) {
            FuncConfirm();
        }
    });
}
function CountLoadingNotClose(title, text, time, FuntionFinish) {
    window.swal({
        title: title,
        html: text,
        allowOutsideClick: false,
        timer: time * 1000,
        onOpen: () => {
            window.swal.showLoading();
        }
    }).then((result) => {
        if (result.dismiss === window.swal.DismissReason.timer) {
            FuntionFinish();
        }
    });
}
function SwalInput(title, textconfirmButton, inputPlaceholder, funcConfirm, funcSuccess) {
    window.Swal.fire({
        title: title,
        input: "text",
        inputPlaceholder: inputPlaceholder,
        width: 800,
        inputAttributes: {
            autocapitalize: "off"
        },
        showCancelButton: true,
        confirmButtonText: textconfirmButton,
        showLoaderOnConfirm: true,
        preConfirm: (data) => {
            funcConfirm(data);
        },
        allowOutsideClick: () => !window.Swal.isLoading()
    }).then((result) => {
        funcSuccess(result);
    });
}