
$.extend($.fn.dataTableExt.oStdClasses, {
    sFilterInput: "form-control",
    sLengthSelect: "form-control"
});

$.extend($.fn.dataTable.defaults.oLanguage, {
    sSearch: "",
    sLengthMenu: "_MENU_",
    sSearchPlaceholder: "Search..."
});

var talondatatable_renderbuttons = function (row, defs) {
    var toReturn = "";
    for (var i = 0; i < defs.length; i++) {
        var $dummy = $("<div>");
        var def = defs[i];
        var $btn = $('<button>').addClass('btn btn-xs');
        if (def.Type != null)
            $btn.addClass('btn-' + def.Type);
        else
            $btn.addClass('btn-default');
        $btn.attr("onclick", "talondatatable_firebuttonevent(this, " + def.Method + ")");
        if (def.Icon != null)
            $("<i>").addClass('fa fa-fw fa-' + def.Icon).appendTo($btn);
        if (def.Title != null && !def.IconOnly)
            $("<span>").html(def.Title).appendTo($btn);
        $btn.appendTo($dummy);
        toReturn += $dummy.html();
    }
    return toReturn;
}

var talondatatable_firebuttonevent = function (el, method) {
    var tr = $(el).closest('tr');
    var tb = $(el).closest('table').data('datatable');
    var d = tb.row(tr).data();
    method.call(el, d);
}