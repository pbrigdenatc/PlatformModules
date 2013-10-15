

jQuery(document).ready(function () {
    var proposalDialog = jQuery('.proposal-success-wrap').dialog({ modal: true, title: jQuery('.ProposalSuccessTitle').html(), minHeight: 50});


    jQuery('#proposal-success-close').live('click', function () {
        jQuery('.proposal-success-wrap').dialog('close');
    });
});