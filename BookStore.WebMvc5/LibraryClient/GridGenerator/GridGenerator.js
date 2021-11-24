function GGPagination(ggObject, isPaginationToHeader) {
    $(ggObject.PaginationId).twbsPagination('destroy');
    if (ggObject.TotalRow <= 0)
        return;

    $(ggObject.PaginationIdFooter + (isPaginationToHeader != undefined ? ',' + ggObject.PaginationIdHeader : '')).twbsPagination({
        totalPages: Math.ceil(ggObject.TotalRow / ggObject.PageSize),
        startPage: ggObject.CurrentPage,
        first: '<span data-toggle="popover" data-trigger="hover" data-placement="top" data-content="Trang đầu tiên" class="glyphicon glyphicon-step-backward"></span>',
        last: '<span data-toggle="popover" data-trigger="hover" data-placement="top" data-content="Trang cuối cùng" class="glyphicon glyphicon-step-forward"></span>',
        prev: '<span data-toggle="popover" data-trigger="hover" data-placement="top" data-content="Trang trước" class="glyphicon glyphicon-chevron-left"></span>',
        next: '<span data-toggle="popover" data-trigger="hover" data-placement="top" data-content="Trang sau" class="glyphicon glyphicon-chevron-right"></span>',
        onPageClick: function (event, page) {
            if (page != ggObject.CurrentPage) eval($(ggObject.TableId).data("fcr") + "(" + ggObject.PageSize + "," + page + ",undefined)");
        }
    });

    if (isPaginationToHeader == undefined) $('.gg-table-header').remove();
};
$(document).on('change', '.gg-pagesize', function (e) { 
    eval($($(this).data("ggtableid")).data("fcr") + "(" + $(this).val() + ",undefined,undefined)");
});
$(document).on('click', '.gg-table th', function (e) {
    if ($(this).data("sort") != null) eval($($(this).data("ggtableid")).data("fcr") + "(" + $($(this).data("ggpagesizeid")).val() + ", 1,'" + $(this).data("sort") + "')");
});