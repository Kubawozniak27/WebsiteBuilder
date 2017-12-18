var CorrectionDefinition = CorrectionDefinition || {};

CorrectionDefinition.Index = (function () {

    var selectors = {
        websiteTable: '#website-table',
        websiteEditorBtn: '#websiteEditorBtn',
        showWebsiteBtn: '#showWebsiteBtn',
        exportWebsiteToHtmlFileBtn: '#exportWebsiteToHtmlFileBtn',
        selectedWebsiteHiddenInput:'#selectedWebsite'
    };

    var table;
    var selectedWebsite;

    $(function () {
        init();
    });

    function init() {
        createTable();
    }

    function createTable() {
        $.getJSON("/Website/GetWebsitesPagedTable",
            function (json) {
                
                table = $(selectors.websiteTable).DataTable({
                    data: json,
                    rowId: 'WebsiteId',
                    columns: [
                        { data: 'WebsiteId' },
                        { data: 'Name' },
                        { data: 'PublishDateShortDate' },
                        { data: 'EditDateShortDate' }
                    ],
                    select: {
                        style: 'single'
                    }
                });

                enableRowSingleSelect(selectors.websiteTable, table,
                    function (id, data) {
                        selectedWebsite = id;

                        $(selectors.exportWebsiteToHtmlFileBtn).attr('disabled', false);
                        $(selectors.selectedWebsiteHiddenInput).val(id);

                        $(selectors.showWebsiteBtn).attr('disabled', false);
                        $(selectors.showWebsiteBtn).attr('href', '/Website/PreviewWebsite/' + id);

                        $(selectors.websiteEditorBtn).attr('disabled', false);
                        $(selectors.websiteEditorBtn).attr('href', '/WebsiteEditor/Index/' + id);

                    },
                    function () {
                        selectedWebsite = null;
                        $(selectors.exportWebsiteToHtmlFileBtn).attr('disabled', true);
                        $(selectors.showWebsiteBtn).attr('disabled', true);
                        $(selectors.websiteEditorBtn).attr('disabled', true);
                    });

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
})();