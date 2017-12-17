var CorrectionDefinition = CorrectionDefinition || {};

CorrectionDefinition.Index = (function () {

    var selectors = {
        websiteTable: '#website-table',
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
                var tr;

                //Append each row to html table  
                for (var i = 0; i < json.length; i++) {
                    tr = $('<tr/>');
                    tr.append("<td>" + json[i].WebsiteId + "</td>");
                    tr.append("<td>" + json[i].Name + "</td>");
                    tr.append("<td>" + json[i].PublishDateShortDate + "</td>");
                    tr.append("<td>" + json[i].EditDateShortDate + "</td>");
                    $('table').append(tr);

                }
                $(selectors.websiteTable).DataTable();
            });  
    }
            


})();