jQuery(document).ready(function () {

    jQuery("#search input").keyup(function (event) {
        if (event.keyCode == 13) {
            jQuery("#search a").click();
        }

    });
   var searchText = jQuery(".searchWatermarkText").html();
   jQuery("#search input").watermark(searchText, "watermark");
   jQuery(".rgFilterBox").watermark(searchText, "watermark");

}
);