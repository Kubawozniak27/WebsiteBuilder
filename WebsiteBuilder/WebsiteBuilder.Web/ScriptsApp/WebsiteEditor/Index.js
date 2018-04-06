var WebsiteEditor = WebsiteEditor || {};

WebsiteEditor.Index = (function () {
    var selectors = {
        selectColorDiv: '#selectColorDiv',
        selectColor: '#selectColor',
        backgroundColorBtn: '#changeBackgroundColorBtn',
        websiteContainer: '#website_container',
        saveBtn: '#save-website-btn',
        addImageBtn: '#add-image-btn',
        grid: '#grid',
        addImageSrcHiddenInput: '#add-image-src-hidden-input',
        addImageIdHiddenInput: '#add-image-id-hidden-input',
        selectImageLabel: '#select-image-label',
        addTextBtn: '#add-text-btn',
        websiteIdHidden: '#WebsiteId',
        deleteWidget: '.deleteWidget',
        imageId: '.imageId',
        textId: '.textId',
        textIdError: '#textIdError',
        imageIdError: '#imageIdError',
        selectImage: '#imageContainer img',
        addNavigationBar: '#add-navigation-bar',
        navigationBar: '#navigation-bar',
        editTextarea: '.editWidget',
        selectFontSize: '#selectFontSize',
        selectFontWeight: '#selectFontWeight',
        selectFontStyle: '#selectFontStyle',
        selectFontFamily: '#selectFontFamily',
        changeFontColorBtn: '#changeFontColorBtn',
        addNavbarElementBtn: '#addNavbarElementBtn',
        nameNavigation: '.nameNavigation',
        selectSections: '#selectSections',
        addNavigationListItem: '.addNavigationListItem',
        addNavigationElement: '.addNavigationElement',
        deleteNavbar: '.deleteNavbar',
        radiobuttonForNavbarStyles: '.radio',
        errorForImageLabel: '#errorForImage',
        uploadImageBtn: '#uploadImageBtn',
        uploadImageForm: '#uploadImageForm',
        deleteMessege: '.deleteMessege'
    }

    var websiteElementType = {
        text: 1,
        image: 2
    }

    var fontWeightType = {
        normal: 1,
        bold: 2
    }

    var fontStyleType = {
        normal: 1,
        italic: 2
    }

    var navigationBarColor = {
        "black-bg": 1,
        "blue-bg": 2,
        "gray-bg": 3,
        "white-bg": 4
    }

    var fontFamilyType = {
        "Arial": 1,
        "'Century Gothic'": 2,
        "'Copperplate Gothic Light'": 3,
        "Georgia": 4,
        "Impact": 5,
        "'Lucida Console'": 6,
        "'Tahoma'": 7,
        "'Times New Roman'": 8,
        "'Trebuchet MS'": 9,
        "'Verdana'": 10
    };

    var grid;
    var items;
    var itemsToDelelete = [];
    var selectedTextAreaForEdit = null;
    var selectorWidgetId = [];
    var navigationArray = [];
    var isDeleteNavigationArrayFromServer = false;
    var navigationArrayFromServer = [];
    var navigationBarStyles = null;
    var websiteColorTmp = "";
    var selectedImage = null;

    $(function () {
        init();
    });

    function init() {
        initGridStack();
        disabledColorPickerForFontColor();
        initDraggableForAddNavigationElement();

        $(selectors.deleteMessege).click(function () {
            $(".errorForUploadImage").hide();
            $('.errorForAddTextOrImage').hide();
            $('.errorForAddTextOrImageRequired').hide();
        });
        $(selectors.uploadImageForm).submit(function (event) {
            var file = $("#file-upload").prop('files');
            if (file.length > 0) {
                return;
            }
            else {
                $(".errorForUploadImage").show();
                event.preventDefault();
            }
        });
        $(selectors.saveBtn).click(saveWebsite);
        $(selectors.addTextBtn).click(addNewWidgetForText);
        $(selectors.addImageBtn).click(addNewWidgetForImage);
        $(selectors.addNavigationBar).click(function () {
            $(selectors.navigationBar).show();

            var value = $('input:radio[name=optradio]:checked').val();
            $('input:radio[name=optradio]').attr('disabled', 'disabled');
            $(selectors.addNavigationBar).attr('disabled', 'disabled');
            navigationBarStyles = stringOFNavigationBar(value);
            $('nav').removeClass();
            $('nav').addClass('navbar');
            $(selectors.navigationBar).find('.navbar').addClass(navigationBarStyles);
            initNavigationContainer();
        });
        $(selectors.selectImage).click(function () {
            selectImage(this);
        });
        $(selectors.selectFontSize).change(function () {
            selectFontSize(this);
        });
        $(selectors.selectFontFamily).change(function () {
            selectFontFamily(this);
        });
        $(selectors.selectFontStyle).change(function () {
            selectFontStyle(this);
        });
        $(selectors.selectFontWeight).change(function () {
            selectFontWeight(this);
        });
        $(selectors.addNavbarElementBtn).click(function () {
            addNavbarElement();
        });
        $(selectors.nameNavigation).keyup(function () {
            var nameNavigation = $(this).val();
            var selectSectionId = $(selectors.selectSections).val();
            if (nameNavigation && selectSectionId) {
                $(selectors.addNavbarElementBtn).removeAttr('disabled');
            }
            else {
                $(selectors.addNavbarElementBtn).attr('disabled', 'disabled');
            }
        });

        $('input:radio[name=optradio]').change(function () {
            var value = $('input:radio[name=optradio]:checked').val();
            navigationBarStyles = stringOFNavigationBar(value);
            $(this).addClass(navigationBarStyles);

        });
    }

    function initDraggableForAddNavigationElement() {
        $(selectors.addNavigationElement).draggable();
    }
    function addNavbarElement() {
        var navigationName = $(selectors.nameNavigation).val();
        var selectedWidgetId = $(selectors.selectSections).val();
        var sectionName = selectorWidgetId[selectedWidgetId];

        var object = {
            NavigationName: navigationName,
            SectionName: sectionName
        };

        navigationArray.push(object);
        selectorWidgetId.splice(selectedWidgetId, 1);


        var navigationBar = $(selectors.navigationBar);
        var lastElementUl = navigationBar.find("li:last-child").remove();
        var navigationBarLi = navigationBar.find("ul")
            .append($('<li>')
                .append($('<a>').attr('href', '#' + sectionName).addClass(navigationBarStyles)
                    .append($('<span>').attr('class', 'tab')
                        .append(navigationName)
                    )));

        var navigationLast = navigationBar.find("ul")
            .append($('<li>').css({ 'fontSize': '20px', 'padding-top': '10px', 'color': 'red' }).attr('class', 'addNavigationListItem')
                .append($('<i>').attr('class', 'fa fa-plus')));

        $(selectors.nameNavigation).val(null);
        $(selectors.addNavigationElement).hide();
        initNavigationContainer();
        initDropdownSections();
    }
    function initNavigationContainer() {

        $(selectors.addNavigationListItem).click(function () {

            $(selectors.addNavigationElement).show();
            var selectSectionId = $(selectors.selectSections).val();
            if (selectSectionId) {
                $(selectors.addNavbarElementBtn).removeAttr('disabled');
            }
            else {
                $(selectors.addNavbarElementBtn).attr('disabled', 'disabled');
            }
        });

        $(selectors.deleteNavbar).click(function () {
            deleteNavbar();
        });

        $('.deleteNavigationForm').click(function () {
            $(selectors.addNavigationElement).hide();
        });
    }
    function deleteNavbar() {
        $.each(navigationArray, function (index, value) {
            selectorWidgetId.push(value.SectionName);
        });
        isDeleteNavigationArrayFromServer = true;
        navigationArray = [];
        var navigationBar = $(selectors.navigationBar);
        navigationBar.find("li").remove();
        var navigationLast = navigationBar.find("ul")
            .append($('<li>').css({ 'fontSize': '20px', 'padding-top': '10px', 'color': 'red' }).attr('class', 'addNavigationListItem')
                .append($('<i>').attr('class', 'fa fa-plus')));
        $(selectors.addNavigationElement).hide();
        $('input:radio[name=optradio]').removeAttr('disabled');
        $(selectors.addNavigationBar).removeAttr('disabled');
        navigationBar.hide();
        initDropdownSections();
    }
    function initDropdownSections() {
        $(selectors.selectSections).empty();
        $.each(selectorWidgetId, function (key, value) {
            $(selectors.selectSections)
                .append($("<option></option>")
                    .attr("value", key)
                    .text(value));
        });
    }
    function selectFontFamily(context) {
        var changeValue = parseInt($(context).val());
        var textArea = $("#" + selectedTextAreaForEdit);
        var fontFamilyValue = stringOfFontFamilyType(changeValue);
        $(textArea).css({ "font-family": fontFamilyValue });
    }
    function selectFontSize(context) {
        var changeValue = $(context).val();
        var textArea = $("#" + selectedTextAreaForEdit);
        $(textArea).css('font-size', changeValue + "px");
    }
    function selectFontStyle(context) {
        var changeValue = parseInt($(context).val());
        var textArea = $("#" + selectedTextAreaForEdit);
        var fontWeightValue = stringOfFontStyleType(changeValue);
        $(textArea).css("font-style", fontWeightValue);
    }
    function selectFontWeight(context) {
        var changeValue = parseInt($(context).val());
        var textArea = $("#" + selectedTextAreaForEdit);
        var fontWeightValue = stringOfFontWeightType(changeValue);
        $(textArea).css("font-weight", fontWeightValue);
    }
    function selectImage(image) {
        $(selectedImage).css('outline', '1px solid black');
        var imageSrc = $(image).attr('src');
        var imageId = $(image).attr('value');
        $(image).css('outline', '1px solid red');
        $(selectors.selectImageLabel).hide();
        $(selectors.addImageBtn).show();
        $(selectors.addImageBtn).css({ "margin-bottom": "10px", "margin-top": "10px" });
        $(selectors.imageId).show();
        $(selectors.addImageSrcHiddenInput).val(imageSrc);
        $(selectors.addImageIdHiddenInput).val(imageId);
        selectedImage = image;
    }
    function addNewWidgetForText() {
        var textSelectorId = $(selectors.textId).val();
        var isSelectorUnique = checkUniqueWidgetId(textSelectorId);

        if (!textSelectorId) {
            $('.errorForAddTextOrImageRequired').show();
            return;
        }

        if (isSelectorUnique == false) {
            $('.errorForAddTextOrImage').show();
            return;
        }

        $(selectors.textId).val(null);
        $(selectors.textIdError).empty();

        addNewWidget(websiteElementType.text, null, null, textSelectorId);
    }
    function addNewWidgetForImage() {
        var imageSrc = $(selectors.addImageSrcHiddenInput).val();
        var imageId = $(selectors.addImageIdHiddenInput).val();
        var imageSelectorId = $(selectors.imageId).val();

        var isSelectorUnique = checkUniqueWidgetId(imageSelectorId);

        if (!imageSelectorId) {
            $('.errorForAddTextOrImageRequired').show();
            return;
        }

        if (isSelectorUnique == false) {
            $('.errorForAddTextOrImage').show();
            return;
        }
        $(selectors.imageIdError).empty();
        $(selectors.imageId).val(null);



        addNewWidget(websiteElementType.image, imageSrc, imageId, imageSelectorId);
    }
    function checkUniqueWidgetId(element) {
        return !(selectorWidgetId.indexOf(element) > -1);
    }
    function addNewWidget(websiteElement, imageSrc, imageId, selectorId) {

        selectorWidgetId.push(selectorId);
        items = getWidgets();

        var maxY = getMaxY();

        var widgetMax = items.find(x => x.y == maxY);

        if (maxY != null) {
            maxY = maxY + widgetMax.height + 1;
        }
        else {
            maxY = 0;
        }

        var widgetDefinition;
        var node;

        if (websiteElement == websiteElementType.image) {
            widgetDefinition = '<div><div class="grid-stack-item-content" style="border:2px dashed black;display:flex;"><img src="' + imageSrc + '" value="' + imageId + '" id="' + selectorId + '" style = "overflow:hidden;height:auto;width:auto;" /> <div class="deleteWidget">&times;</div></div ></div > ';
            node = {
                x: 0,
                y: maxY,
                width: 3,
                height: 2
            };
        }
        else if (websiteElement == websiteElementType.text) {
            widgetDefinition = '<div><div class="grid-stack-item-content" style="border:2px dashed black;display:flex;"><textarea style="overflow:hidden;max-height:100%;" id="' + selectorId + '" ></textarea><span class="editWidget"><i class="fa fa-pencil" aria-hidden="true"></i></span><div class="deleteWidget">&times;</div></div></div>';
            node = {
                x: 0,
                y: maxY,
                width: 12,
                height: 1
            }

        }

        grid.addWidget($(widgetDefinition), node.x, node.y, node.width, node.height);

        $('textarea').css('background-color', '' + websiteColorTmp);

        initActionWidget();

        return false;
    }
    function initNavigationBar(data) {
        if (data.length > 0) {
            var navigationBar = $(selectors.navigationBar);
            $(selectors.navigationBar).find('nav').addClass(data[0].NavigationBarStyle);
            var lastElementUl = navigationBar.find("li:last-child").remove();
            $.each(data, function (index, value) {
                var object = {
                    NavigationId: value.NavigationId,
                    SectionName: value.SectionName,
                    NavigationName: value.NavigationName
                };

                navigationArray.push(object);
                var selectorWidgetIdTmp = getSelectorWidgetIdFromArray(value.SectionName);
                selectorWidgetId.splice(selectorWidgetIdTmp, 1);


                var navigationBarLi = navigationBar.find("ul")
                    .append($('<li>').attr('id', value.NavigationId)
                        .append($('<a>').attr('href', '#' + value.SectionName)
                            .append($('<span>').attr('class', 'tab')
                                .append(value.NavigationName)
                            )));
            });
            $(selectors.navigationBar).show();

            var navigationLast = navigationBar.find("ul")
                .append($('<li>').css({ 'fontSize': '20px', 'padding-top': '10px', 'color': 'red' }).attr('class', 'addNavigationListItem')
                    .append($('<i>').attr('class', 'fa fa-plus')));

            $(selectors.nameNavigation).val(null);
            $(selectors.addNavigationElement).hide();
            initNavigationContainer();
            initDropdownSections();
        }
    }
    function initGridStack() {
        var websiteId = $(selectors.websiteIdHidden).val();

        AjaxHelper.get("/WebsiteEditor/GetWebsiteContent", { id: websiteId }, function (data) {
            websiteColorTmp = data.WebsiteColor;
            items = data;
            var navigationBars = data.NavigationBars;
            navigationArrayFromServer = data.NavigationBars;

            if (navigationBars.length > 0) {
                $(selectors.addNavigationBar).attr('disabled', 'disabled');
                $('input:radio[name=optradio]').attr('disabled', 'disabled');
            }

            debugger;

            var options = {
            };
            $(selectors.grid).gridstack(options);
            $(selectors.grid).each(function () {
                grid = $(this).data('gridstack');
                _.each(items.WebsiteContents, function (node) {
                    selectorWidgetId.push(node.SelectorId);

                    var fontSize = node.FontSize;
                    var fontWeight = stringOfFontWeightType(node.FontWeight);
                    var fontFamily = stringOfFontFamilyType(node.FontFamily);
                    var fontStyle = stringOfFontStyleType(node.FontStyle);
                    var fontColor = node.FontColor;

                    var styleForFont = 'font-weight:' + fontWeight + ';font-style:' + fontStyle + ';font-family:' + fontFamily + ';font-size:' + fontSize + 'px;' + 'color:' + fontColor + ';';

                    if (node.WebsiteElementType == websiteElementType.image) {
                        node.ImageSrc = node.ImageSrc.replace("~", "");
                        grid.addWidget($('<div><div class="grid-stack-item-content" style="border:3px dashed black;display:flex;"><img src="' + node.ImageSrc + '" style="overflow:hidden;height:auto;width:auto;" id="' + node.SelectorId + '"  /><div class="deleteWidget" value="' + node.Id + '">&times;</div> <input type = "hidden" value="' + node.Id + '" /></div></div>'),
                            node.X, node.Y, node.Width, node.Height)
                    }
                    else if (node.WebsiteElementType == websiteElementType.text) {
                        grid.addWidget($('<div><div class="grid-stack-item-content" style="border:3px dashed black;display:flex;"><textarea style="overflow:hidden;max-height:100%;' + styleForFont + '" id="' + node.SelectorId + '" >' + node.Text + '</textarea><span class="editWidget"><i class="fa fa-pencil" aria-hidden="true"></i></span><div class="deleteWidget" value="' + node.Id + '">&times;</div> <input type = "hidden" value="' + node.Id + '" /></div></div>'),
                            node.X, node.Y, node.Width, node.Height)
                    }

                }, this);

                return false;
            });
            $('textarea').css('background-color', '' + websiteColorTmp);
            initActionWidget(navigationBars);
            initNavigationBar(navigationBars);
        })
    }
    function initActionWidget(navigationBars) {
        initDeleteWidget();
        initEditTextArea();
        initDropdownSections();
        initColorPicker();
    }
    function initDeleteWidget() {
        $(selectors.deleteWidget).click(function () {
            var widgetId;
            var context = this;
            var widgetTypeTmp = $(this).prev();
            var widgetType = $(widgetTypeTmp).prop('tagName') == "IMG" ? websiteElementType.image : websiteElementType.text;
            var item = $(this).parent().parent();
            var id = $(this).next().val();
            var coordinateX = item.data('gs-x');
            var coordinateY = item.data('gs-y');
            var height = item.data('gs-height');
            var width = item.data('gs-width');

            if (id) {
                var websiteToDelete = {};
                websiteToDelete.Id = id;
                websiteToDelete.WebsiteElementType = widgetType;
                itemsToDelelete.push(websiteToDelete);
            }
            getWidgetsAsync();
            getWidgetIdFromArrayAndDelete(context);

            console.log(navigationArray);
            console.log(selectorWidgetId);
            debugger;
            function getWidgetsAsync() {
                setTimeout(function () {
                    items = getWidgets();
                    debugger;
                }, 100);
            }
            function getWidgetIdFromArrayAndDelete(context) {
                setTimeout(function () {
                    widget = getWidgetIdFromArray(coordinateX, coordinateY);
                    deleteWidget(widget, context);
                    debugger;
                }, 200);
            }
            function deleteWidget(widget, ciaobella) {
                setTimeout(function () {
                    if (widget.Index > -1) {

                        //delete widget from array
                        items.splice(widget.Index, 1);
                        var getElementFromNavigationArray = null;
                        var elementFromNavigationArray = null;
                        if (navigationArray.length > 0) {
                            var getElementFromNavigationArray = function () {
                                for (var i = 0, len = navigationArray.length; i < len; i++) {
                                    if (navigationArray[i].SectionName === widget.Widget.selectorId) {
                                        return {
                                            Index: i,
                                            Navigation: navigationArray[i]
                                        };
                                    }
                                }
                                return null;
                            }
                            elementFromNavigationArray = getElementFromNavigationArray();

                            if (elementFromNavigationArray) {
                                navigationArray.splice(elementFromNavigationArray.Index, 1);

                                if (elementFromNavigationArray.Navigation.NavigationId) {
                                    isDeleteNavigationArrayFromServer = true;
                                    navigationArrayFromServer.push(elementFromNavigationArray.Navigation);
                                }
                                //delete element list from navigation bar


                                $(selectors.navigationBar).find("a[href='#" + widget.Widget.selectorId + "']").closest('li').remove();
                            }
                        }
                        console.log(selectorWidgetId);
                        console.log(navigationArray);

                        var getElementFromSectionArray = function () {
                            for (var i = 0, len = selectorWidgetId.length; i < len; i++) {
                                if (selectorWidgetId[i].SectionName === widget.Widget.selectorId) {
                                    return i;
                                }
                            }
                            return null;
                        }

                        var elementFromSectionArray = getElementFromSectionArray();
                        debugger;
                        if (elementFromSectionArray) {
                            selectorWidgetId.splice(elementFromSectionArray, 1);
                        }
                        

                        console.log(selectorWidgetId);
                        console.log(navigationArray);
                        

                        //delete widget from grid
                        let deleteWidget = $(context).closest(".grid-stack-item");
                        grid.removeWidget(deleteWidget);

                        initDropdownSections();
                    }
                }, 250);
            }
        });
    }
    function initEditTextArea() {
        $(selectors.editTextarea).click(function () {
            if (selectedTextAreaForEdit != null) {
                $("#" + selectedTextAreaForEdit).closest('.grid-stack-item-content').css('border', '3px dashed black');

            }

            var textArea = $(this).siblings('textarea');
            textArea.closest('.grid-stack-item-content').css('border', '3px dashed red');
            var textareaId = textArea.attr('id');
            var textColor = textArea.css('color');
            var textFontSize = textArea.css('font-size');
            var textFontFamily = textArea.css('font-family');
            var textFontWeight = textArea.css('font-weight');
            var textFontStyle = textArea.css('font-style');

            textFontSize = parseInt(textFontSize.replace("px", ""));
            textFontStyle = textFontStyle === "normal" ? fontStyleType.normal : fontStyleType.italic;
            textFontWeight = textFontWeight === 400 ? fontWeightType.normal : fontWeightType.bold;
            textFontFamily = fontFamilyType[textFontFamily] == null ? fontFamilyType.Arial : fontFamilyType[textFontFamily];
            textColor = convertRGBtoHex(textColor);

            $(selectors.selectFontFamily).val(textFontFamily);
            $(selectors.selectFontSize).val(textFontSize);
            $(selectors.selectFontWeight).val(textFontWeight);
            $(selectors.selectFontStyle).val(textFontStyle);

            $(selectors.changeFontColorBtn).find('input[type="text"]').val(textColor);
            selectedTextAreaForEdit = textareaId;

            initColorPickerForFontColor();
            $(selectors.selectFontFamily).removeAttr('disabled');
            $(selectors.selectFontSize).removeAttr('disabled');
            $(selectors.selectFontWeight).removeAttr('disabled');
            $(selectors.selectFontStyle).removeAttr('disabled');
        });
    }
    function convertRGBtoHex(rgb) {
        rgb = rgb.match(/^rgba?[\s+]?\([\s+]?(\d+)[\s+]?,[\s+]?(\d+)[\s+]?,[\s+]?(\d+)[\s+]?/i);
        return (rgb && rgb.length === 4) ? "#" +
            ("0" + parseInt(rgb[1], 10).toString(16)).slice(-2) +
            ("0" + parseInt(rgb[2], 10).toString(16)).slice(-2) +
            ("0" + parseInt(rgb[3], 10).toString(16)).slice(-2) : '';
    }
    function getWidgetIdFromArray(coordinateX, coordinateY) {
        var result;
        items.filter(function (e, index) {
            if (e.x == coordinateX && e.y == coordinateY) {
                result = {
                    Index: index,
                    Widget: e
                };
                return true;
            }
        });
        return result;
    }
    function getSelectorWidgetIdFromArray(sectionName) {
        var result;
        selectorWidgetId.filter(function (e, index) {
            if (e == sectionName) {
                result = index;
                return true;
            }
        });
        return result;
    }
    function getWidgets() {
        return _.map($('.grid-stack > .grid-stack-item:visible'), function (el) {
            el = $(el);
            var textareaValue = null;
            var imageSrc = imageSrc = el.find('img').attr("src");
            var imageId = null;
            var elementId = el.find('input[type="hidden"]').val();
            var node = el.data('_gridstack_node');
            var selectorId = null;


            var fontSizeText = null;
            var fontWeightText = null;
            var fontColorText = null;
            var fontStyleText = null;
            var fontFamilyText = null;
            var fontColorText = null;

            var type = imageSrc != null ? websiteElementType.image : websiteElementType.text;

            if (type == websiteElementType.text) {
                var fontSizeTextTmp = el.find('textarea').css('fontSize');
                textareaValue = el.find('textarea').val();
                selectorId = el.find('textarea').attr('id');

                fontSizeText = parseInt(fontSizeTextTmp.replace("px", ""));


                fontStyleText = el.find('textarea').css('font-style');
                fontStyleText = fontStyleText === "normal" ? fontStyleType.normal : fontStyleType.italic;

                fontWeightText = el.find('textarea').css('font-weight');
                fontWeightText = fontWeightText === 400 ? fontWeightType.normal : fontWeightType.bold;

                fontFamilyText = el.find('textarea').css('font-family');
                fontFamilyText = fontFamilyType[fontFamilyText];

                fontColorText = convertRGBtoHex(el.find('textarea').css('color'));
            }
            else {
                selectorId = el.find('img').attr('id');

                imageId = el.find('img').attr("value");
            }

            return {
                x: node.x,
                y: node.y,
                width: node.width,
                height: node.height,
                text: textareaValue,
                imageId: imageId,
                imageSrc: imageSrc,
                id: elementId,
                selectorId: selectorId,
                websiteElementType: type,
                fontSize: fontSizeText,
                fontStyle: fontStyleText,
                fontWeight: fontWeightText,
                fontFamily: fontFamilyText,
                fontColor: fontColorText
            }
        }, this);
    }
    function getMaxY() {
        if (items.length != 0) {
            return items.reduce((max, p) => p.y > max ? p.y : max, items[0].y);
        }
        return null;
    }
    function stringOfFontFamilyType(value) {
        for (var k in fontFamilyType) if (fontFamilyType[k] == value) return k;
        return null;
    }
    function stringOFNavigationBar(value) {
        for (var k in navigationBarColor) if (navigationBarColor[k] == value) return k;
        return null;
    }
    function stringOfFontStyleType(value) {
        for (var k in fontStyleType) if (fontStyleType[k] == value) return k;
        return null;
    }
    function stringOfFontWeightType(value) {
        for (var k in fontWeightType) if (fontWeightType[k] == value) return k;
        return null;
    }
    function saveWebsite() {
        var websiteId = $(selectors.websiteIdHidden).val();
        var widgets = getWidgets();
        var website = {
            WebsiteId: websiteId,
            WebsiteColor: websiteColorTmp,
            WebsiteContents: widgets,
            WidgetsToRemove: itemsToDelelete,
            NavigationBars: navigationArray,
            NavigationBarsToRemove: navigationArrayFromServer,
            IsDeleteNavigationArrayFromServer: isDeleteNavigationArrayFromServer,
            NavigationBarStyle: navigationBarStyles
        };
        AjaxHelper.postAndHandleErrors("/WebsiteEditor/SaveWebsite", { request: website }, null, function () {
            window.location.href = "/Website/Index";
        });

    }
    function initColorPickerForFontColor() {
        $(selectors.changeFontColorBtn).colorpicker('enable');
        $(selectors.changeFontColorBtn).colorpicker({
            color: 'Zmien kolor tła',
            colorSelectors: {
                'red': '#FF0000',
                'orange': '#FFA500',
                'yellow': '#ffff00',
                'limegreen': '#32cd32',
                'green': '#00ff00',
                'blue': '#0000ff '
            }
        }
        ).on('changeColor', function (e) {
            var colorHex = e.color.toString('hex');
            var textArea = $("#" + selectedTextAreaForEdit);
            $(textArea).css('color', colorHex);
        });
    }
    function disabledColorPickerForFontColor() {
        $(selectors.changeFontColorBtn).colorpicker('disable');
    }
    function initColorPicker() {
        $(selectors.backgroundColorBtn).colorpicker(
            {
                "color": "" + websiteColorTmp,
                colorSelectors: {
                    'red': '#FF0000',
                    'orange': '#FFA500',
                    'yellow': '#ffff00',
                    'limegreen': '#32cd32',
                    'green': '#00ff00',
                    'blue': '#0000ff '
                }
            }
        ).on('changeColor', function (e) {

            $(selectors.backgroundColorBtn).val();
            websiteColorTmp = e.color.toString('hex');
            $('textarea').css('background-color', '' + websiteColorTmp);
            $(selectors.websiteContainer).css("background-color", e.color.toString('hex'));
        });
    }
})();