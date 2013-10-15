jQuery(document).ready(function () {
    var i = 0;
    jQuery('select[id$="HeardFrom"] > option').each(function () {
        this.text = jQuery('.heardFrom' + i).html();
        i++;
    });
});