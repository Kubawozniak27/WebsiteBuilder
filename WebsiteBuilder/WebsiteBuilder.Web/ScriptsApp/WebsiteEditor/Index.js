//var WebsiteEditor = WebsiteEditor || {};

//WebsiteEditor.Index = (function () {

//    var selectors = {
//        selectColorDiv: '#selectColorDiv',
//        selectColor: '#selectColor',
//        backgroundColorBtn: '#changeBackgroundColorBtn',
//        websiteContainer: '#website_container',
//        backgroundColorHidden: '#backgroundColorHidden',
//        saveBtn: '#save-website-btn',
//        grid: '#grid'
//    }

//    var grid;
//    var items;

//    $(function () {
//        init();
//    });



//    function init() {
//        initColorPicker();
//        initGridStack();
//        $(selectors.saveBtn).click(saveWebsite);

//        $("#add-text-btn").unbind('click').bind('click', function (e) {
//            addNewWidget();
//        });
//    }

//    function initGridStack() {
//        AjaxHelper.get("/WebsiteEditor/GetWebsiteContent", { id: 1 }, function (data) {
//            items = data;
//            var options = {
//            };
//            $(selectors.grid).gridstack(options);

//            $(selectors.grid).each(function () {
//                grid = $(this).data('gridstack');

//                _.each(items.Texts, function (node) {
//                    debugger;
//                    grid.addWidget($('<div><div class="grid-stack-item-content" style="border:2px dashed black;display:flex;"><textarea style="overflow:hidden;max-height:100%;">' + node.Text + '</textarea> <input type = "hidden" value="' + node.Id + '" /></div></div>'),
//                        node.X, node.Y, node.Width, node.Height)

//                }, this);
//                return false;
//            });
//        })
//    }

//    function getWidgets() {
//        return _.map($('.grid-stack > .grid-stack-item:visible'), function (el) {
//            el = $(el);
//            var textareaValue = el.find('textarea').val();
//            var textId = el.find('input[type="hidden"]').val();
//            var node = el.data('_gridstack_node');
//            return {
//                x: node.x,
//                y: node.y,
//                width: node.width,
//                height: node.height,
//                text: textareaValue,
//                id: textId
//            }
//        }, this);
//    }

//    function getMaxY() {
//        if (items.length != 0) {
//            return items.reduce((max, p) => p.y > max ? p.y : max, items[0].y);
//        }
//        return null;
//    }

//    function addNewWidget() {
//        items = getWidgets();

//        var maxY = getMaxY();
//        debugger;
//        if (maxY != null) {
//            maxY = maxY + 1;
//        }
//        else {
//            maxY = 0;
//        }
//        var node = {
//            x: 0,
//            y: maxY,
//            width: 12,
//            height: 1
//        };

//        grid.addWidget($('<div><div class="grid-stack-item-content" style="border:2px dashed black;display:flex;"><textarea style="overflow:hidden;max-height:100%;"></textarea></div></div>'),
//            node.x, node.y, node.width, node.height);
//        return false;
//    };

//    function saveWebsite() {
//        var websiteColor = $(selectors.backgroundColorHidden).val();

//        var widgets = getWidgets();
//        var website = {
//            WebsiteId: 1,
//            WebsiteColor: websiteColor,
//            Texts: widgets
//        };

//        AjaxHelper.postAndHandleErrors("/WebsiteEditor/SaveWebsite", { request: website }, null, function () {
//            window.location.href = "/Website/Index";
//        });

//    }

//    function initColorPicker() {
//        $(selectors.backgroundColorBtn).colorpicker(
//            {
//                color: 'Zmien kolor tła',
//                colorSelectors: {
//                    'red': '#FF0000',
//                    'orange': '#FFA500',
//                    'yellow': '#ffff00',
//                    'limegreen': '#32cd32',
//                    'green': '#00ff00',
//                    'blue': '#0000ff '
//                }
//            }
//        ).on('changeColor', function (e) {
//            $(selectors.backgroundColorBtn).val('Zmień kolor tła');
//            $(selectors.backgroundColorHidden).val(e.color.toString('hex'));
//            $(selectors.websiteContainer).css("background-color", e.color.toString('hex'));
//        });
//    }
    
//})();