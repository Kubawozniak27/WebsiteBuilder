var WebsiteEditor = WebsiteEditor || {};

WebsiteEditor.Index = (function () {

    var selectors = {
        selectColorDiv: '#selectColorDiv',
        selectColor: '#selectColor',
        backgroundColorBtn: '#changeBackgroundColorBtn',
        websiteContainer: '#website_container',
        backgroundColorHidden: '#backgroundColorHidden'
    }



    $(function () {      
        init();
    });

    function init() {
        initPanelClick();
        initColorPicker();
    }

    function initColorPicker() {
        $(selectors.backgroundColorBtn).colorpicker(
            {
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
            $(selectors.backgroundColorBtn).val('Zmień kolor tła');
            $(selectors.backgroundColorHidden).val(e.color.toString('hex'));
            $(selectors.websiteContainer).css("background-color", e.color.toString('hex'));
        });
    }

    function initPanelClick() {
        $(document).on('click', '.panel-heading span.clickable', function (e) {
            var $this = $(this);
            if (!$this.hasClass('panel-collapsed')) {
                $this.parents('.panel').find('.panel-body').slideUp();
                $this.addClass('panel-collapsed');
                $this.find('i').removeClass('glyphicon-minus').addClass('glyphicon-plus');
            } else {
                $this.parents('.panel').find('.panel-body').slideDown();
                $this.removeClass('panel-collapsed');
                $this.find('i').removeClass('glyphicon-plus').addClass('glyphicon-minus');
            }
        });
        $(document).on('click', '.panel div.clickable', function (e) {
            var $this = $(this);
            if (!$this.hasClass('panel-collapsed')) {
                $this.parents('.panel').find('.panel-body').slideUp();
                $this.addClass('panel-collapsed');
                $this.find('i').removeClass('glyphicon-minus').addClass('glyphicon-plus');
            } else {
                $this.parents('.panel').find('.panel-body').slideDown();
                $this.removeClass('panel-collapsed');
                $this.find('i').removeClass('glyphicon-plus').addClass('glyphicon-minus');
            }
        });
    }


})();