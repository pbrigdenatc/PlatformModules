

jQuery(document).ready(function () {
    var proposalDialog = jQuery('.post-success-wrap').dialog({ modal: true, title: jQuery('.PostSuccessTitle').html(), minHeight: 50});
    

    jQuery('#post-success-close').live('click', function () {
        jQuery('.post-success-wrap').dialog('close');
    });
});