var Website = Website || {};

Website.AddWebsite = (function () {

    var selectors = {
        addWebsiteForm: '#add-website-form',
        addWebsiteBtn: '#add-website-btn'
    };

    $(function () {
        init();
    });

    function init() {
        $(selectors.addWebsiteBtn).click(function () {
            var isValid = $(selectors.addWebsiteForm).valid();
            if (!isValid) {
                return;
            }

            var request = FormHelper.getFormValues(selectors.addWebsiteForm);

            AjaxHelper.postAndHandleErrors("/Website/AddWebsite", { websiteDto: request }, null, function () {
                window.location.href = "/Website/Index";
            });

        });
    }

})();