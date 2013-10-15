

jQuery(document).ready(function () {

    jQuery('.microprofile-flag').live('click', function () {
        jQuery("#change-language").dialog({
            resizable: false,
            width: 500,
            modal: true,
            closeOnEscape: false,
            title: 'Please select your preferred language',
            open: function (event, ui) {
                jQuery(this).parent().children().children('.ui-dialog-titlebar-close').hide();
                jQuery('.language-change-object .selected').click(function () {
                    var date = new Date();
                    date.setTime(date.getTime() + (3650 * 24 * 60 * 60 * 1000));
                    jQuery.cookie('ourspaceLanguage', '1', { path: "/", expires: date });
                    jQuery("#change-language").dialog("close");
                })
            }
        });
    });
    



});