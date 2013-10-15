jQuery(document).ready(function () {
    jQuery(".datepicker").datepicker({ dateFormat: 'dd-mm-yy', changeMonth: true,
        changeYear: true, yearRange: "-100:+0"
        //onSelect: function (date) {
        //            // alert(date);
        //            jQuery('[id$="hdnfld_DOB"]').val(date);
        //            jQuery(".datepicker").datepicker("hide");
        //        }
    });

    //                          jQuery(".datepicker").live('click', function () {
    //                              jQuery(this).datepicker('destroy').datepicker({ dateFormat: 'dd-mm-yy', changeMonth: true,
    //                                  changeYear: true, yearRange: "-100:+0"
    //                              });
    //                          });
    jQuery(".calandar-btn").live('click', function () {
        jQuery(".datepicker").prependTo(jQuery(".calandar-btn").parent());
        jQuery(".datepicker").show();
        jQuery(".datepicker").datepicker("show");
        var lblAge = jQuery(".lbl_AgeValue").val();
        jQuery(".lbl_AgeValue").hide();
        //jQuery(".datepicker").val(jQuery(".lbl_AgeValue").html());
    });

    jQuery('#dashboard-menu a, .tab-inactive').click(function () {
        jQuery('.updatePanel').addClass('update-panel-loading');

    });
    jQuery('.tab-inactive').live('click',function () {
        jQuery('.updatePanel').addClass('update-panel-loading');

    });
});

function updateHdnDOB(datetext, inst) {
    inst.datepicker("hide");

    
    //
}

//jQuery('.datepicker[maxLength]').limitMaxlength();