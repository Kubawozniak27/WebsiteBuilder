var JqueryDataTableHelper = (function () {
    $.extend(true, $.fn.dataTable.defaults, {
        language: {
            url: '/Scripts/datatables/Polish.json'
        },
        bFilter: true,
        sDom: "rtpli",
        serverSide: true,
        ajax: {
            type: "POST",
            contentType: AjaxHelper.jQueryContentTypes.Json,
        },
        processing: true,
        autoWidth: true,
    });

    var defaults = {
        sDomWithGlobalSearch: "<'dt-toolbar'<f>>tpli",
        sDomWithExportButtons: '<"html5buttons"B>lTfgtp',
        sDomWithExportButtonsWithoutSearch: '<"html5buttons"B>lTgtp'
    }
    var renderDateColumn = function (date) {
        return moment(date).format("YYYY-MM-DD");
    };

    var renderDateTimeColumn = function (date) {
        if (!date)
            return null;
        return moment(date).format("YYYY-MM-DD HH:mm");
    };

    var renderCheckbox = function (checked) {
        var checkboxInput = "<input type='checkbox'";

        if (checked) {
            checkboxInput += " checked='checked'";
        }

        checkboxInput += " disabled='disabled' />";

        return checkboxInput;
    }

    var renderLinkColumn = function (link, cssClass, text) {
        return TextHelper.format("<a href='{0}' class='{1}'>{2}</a>", link, cssClass, text);
    }

    var renderButtonColumn = function (buttonName, butonCssClass, value, displayText, loadingText) {
        var button = "<button type='button'";

        if (buttonName) {
            button += " name='" + buttonName + "'";
        }

        if (butonCssClass) {
            button += " class='" + butonCssClass + "'";
        }

        if (loadingText) {
            button += " data-loading-text='" + loadingText + "'";
        }

        if (value) {
            button += "value='" + value + "'";
        }

        button += "> " + displayText;

        button += "</button>";

        return button;
    }

    function applyColumnFiltersToCriteria(criteria, dataTablesCritieria, columnMapping) {
        $.each(dataTablesCritieria.columns,
            function (index, column) {
                if (column.search.value) {
                    if (columnMapping && columnMapping[column.data]) {
                        criteria[columnMapping[column.data]] = column.search.value;
                    } else {
                        criteria[column.data] = column.search.value;
                    }
                }
            });
    };

    function createDataTable(tableSelector, tableDefinition) {
        var defaultDefinition = {
            sDom: 'rtpli'
        };

        var definitionWithDefaults = $.extend(true, defaultDefinition, tableDefinition);

        return $(tableSelector).DataTable(definitionWithDefaults);
    };

    function initColumnFiltering(dataTableSelector, dataTable) {
        var searchColumn = function (input) {
            dataTable
                .column($(input).closest('th').index() + ':visible')
                .search($(input).val());
        };
        $(dataTableSelector + ' thead th input[type=text]').each(function (index, input) {
            $(input).clearSearch({
                callback: function () {
                    dataTable.draw();
                }
            });

            $(input).on('keyup change', function () {
                searchColumn(input);
            });

            $(input).on('change', function () {
                searchColumn(input);
                dataTable.draw();
            });

            FormHelper.attachEnterPressedHandler(input,
                function () {
                    dataTable.draw();
                });
        });

        $(dataTableSelector + ' thead th select').each(function (index, input) {
            $(input).on('change', function () {
                searchColumn(input);
                dataTable.draw();
            });
        });
    };

    function doubleClick(tableSelector,table, callback) {

        $(tableSelector + ' tbody').on('dblclick', 'tr', function () {
            var data = table.row(this).data();
            callback(data);
        });
    }

    function enableRowSingleSelect(tableSelector, table, rowSelectedCallback, rowUnselectedCallback) {
        $(tableSelector + ' tbody').on('click', 'tr', function () {
            var id = table.row(this).id();
            if ($(this).hasClass('selected')) {
                $(this).removeClass('selected');
                if (rowUnselectedCallback) {
                    rowUnselectedCallback(id);
                }
            }
            else {
                var selectedRow = $(tableSelector + ' tr.selected');
                if (selectedRow.length > 0) {
                    selectedRow.removeClass('selected');
                    if (rowUnselectedCallback) {
                        rowUnselectedCallback(id);
                    }
                }
                $(this).addClass('selected');
                if (rowSelectedCallback) {
                    var data = table.row(this).data();
                    rowSelectedCallback(id, data);
                }
            }
        });
    };

    function getSelectedRowId(table) {
        var ids = table.rows({ selected: true }).ids();
        if (ids.length > 0)
            return ids[0];

        return null;
    };


    return {
        renderDateColumn: renderDateColumn,
        renderDateTimeColumn: renderDateTimeColumn,
        renderCheckbox: renderCheckbox,
        renderLinkColumn: renderLinkColumn,
        renderButtonColumn: renderButtonColumn,
        applyColumnFiltersToCriteria: applyColumnFiltersToCriteria,
        createDataTable: createDataTable,
        initColumnFiltering: initColumnFiltering,
        defaults: defaults,
        getSelectedRowId: getSelectedRowId,
        enableRowSingleSelect: enableRowSingleSelect,
        doubleClick: doubleClick
    }
})();