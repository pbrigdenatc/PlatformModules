
jQuery(document).ready(function () {
    // Define what happens when the textbox loses focus
    // Add the watermark class and default text
    //    jQuery(".rgFilterBox").blur(function () {
    //        jQuery(this).filter(function () {
    //            // We only want this to apply if there's not
    //            // something actually entered
    //            return jQuery(this).val() == ""
    //        }).addClass("watermarkText").val("Search...");
    //    });
    var button = jQuery(".personSearchBtnWrapper").html();
    var parent = jQuery(".rgFilterBox").parent();
    jQuery(button).insertBefore(".rgFilterBox");


});