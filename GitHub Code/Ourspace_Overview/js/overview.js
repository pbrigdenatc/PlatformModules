jQuery(document).ready(function () {
    jQuery('.tab-inactive, .pager-link, .AjaxLoadingTrigger').live('click', function () {
        

        if (jQuery(this).attr('disabled') != "disabled") {
            jQuery('.updatePanel').addClass('update-panel-loading');
            jQuery('.pager-wrapper').addClass('update-panel-loading');
        }

    });
});