
//var translatedContent;
//jQuery(document).ready(function () {

//    jQuery('.Forum_Post_Body').parent().append(jQuery('#translator-ddl').html());

//    var translationsCount = jQuery('.Forum_Post_Body').length;
//    var originalContent = new Array(translationsCount);
//    translatedContent = new Array(translationsCount);
//    var i = 0;
//    var j = 0;
//    jQuery('.Forum_Post_Body').each(function () {
//        originalContent[i] = jQuery(this).html();
//        i++;
//    })

//    jQuery(".translateLanguage").live("change", function () {
//        if (jQuery(this).val() != "x") {
//            if (jQuery(this).val() == "-") {
//                if (originalContent.length != 0) {
//                    revertToOriginal(originalContent);
//                }
//            }
//            else {
//                jQuery("#translating-complete").hide();
//                jQuery('.translateLanguage').attr("disabled", true);

//                //translateContent(jQuery(this).val());
//                jQuery(this).prev().addClass("translating");
//                Translate2.translate(jQuery(this).prev().html(),
//               "en", jQuery(this).val(), "mycallback");

//            }
//        }
//     });  



////    jQuery(".translateLanguage").change(function (e) {
////        if (jQuery(this).val() != "x") {
////            if (jQuery(this).val() == "-") {
////                if (originalContent.length != 0) {
////                    revertToOriginal(originalContent);
////                }
////            }
////            else {
////                jQuery("#translating-complete").hide();
////                //translateContent(jQuery(this).val());
////                jQuery(this).prev().addClass("translating");
////                Translate2.translate(jQuery(this).val(),
////               "en", "es", "mycallback");

////            }
////        }
////    });



//});

//function translateContent(translateTo) {
//////    $('.Forum_Post_Body').translate(translateTo, function () {
//////        $("#translating-status").hide();
//////        $("#translating-complete").show();
////    //    });

//////bing appid 47D78E7A59A91431BD06A3D8D5496E6308634F46
////    //jQuery.translate.load("47D78E7A59A91431BD06A3D8D5496E6308634F46");
////    jQuery('.Forum_Post_Body').translate(translateTo, {
////        start: function () { jQuery("#translating-status").show(); },
////        complete: function () {
////            jQuery("#translating-status").hide();
////            jQuery("#translating-complete").show().delay(1000).fadeOut('slow');
////         }
//    //    });

//    var languageFrom = "en";
//    var languageTo = "es";
//    var text = "translate this.";
//    translate();
//   
////        window.mycallback = function (response) { alert(response); }

////        var s = document.createElement("script");
////        s.src = "http://api.microsofttranslator.com/V2/Ajax.svc/Translate?oncomplete=mycallback&appId=47D78E7A59A91431BD06A3D8D5496E6308634F46&from=" + languageFrom + "&to=" + languageTo + "&text=" + text;
////        document.getElementsByTagName("head")[0].appendChild(s);
////        var number = 2;




//}


//function translate1() {
//    document.getElementById('trans').innerHTML = "Translating... please wait";
//    var text = document.getElementById('ori').value;
//    //window.mycallback = function (response) {
//      //  document.getElementById('trans').innerHTML = response;
//   // }

//    var s = document.createElement("script");
//    s.src = "http://api.microsofttranslator.com/V2/Ajax.svc/Translate?oncomplete=mycallback&appId=47D78E7A59A91431BD06A3D8D5496E6308634F46&from=" + languageFrom + "&to=" + languageTo + "&text=" + text;
//    document.getElementsByTagName("head")[0].appendChild(s);
//}



//function revertToOriginal(originalContent) {
//    i = 0;
//        jQuery('.Forum_Post_Body').each(function () {
//            jQuery(this).html(originalContent[i]);
//            i++;
//        })
//        jQuery("#reverting-complete").show().delay(1000).fadeOut('slow');
//    }



//    var Translate2 = {
//        baseUrl: "http://api.microsofttranslator.com/V2/Ajax.svc/",
//        appId: "47D78E7A59A91431BD06A3D8D5496E6308634F46",
//        translate: function (text, from, to,
//                              callback) {
//            var s = document.createElement("script");
//            s.src = this.baseUrl + "/Translate";
//            s.src += "?oncomplete=" + callback;
//            s.src += "&appId=" + this.appId;
//            s.src += "&from" + from;
//            s.src += "&to=" + to;
//            s.src += "&text=" + text;
//            document.getElementsByTagName(
//              "head")[0].appendChild(s);

//        },
//        detect: function (text, from, to,
//                              callback) {
//            var s = document.createElement("script");
//            s.src = this.baseUrl + "/Detect";
//            s.src += "?oncomplete=" + callback;
//            s.src += "&appId=" + this.appId;
//            s.src += "&text=" + text;
//            document.getElementsByTagName(
//              "head")[0].appendChild(s);

//        }
//    }
//// var mycallback = function (result) {
////     translatedContent[j] = result;
////     //document.getElementById('trans').innerHTML += result;
////     
////     j--;
//    // };

//function mycallback(result) {
//    //alert(result);
//    jQuery('.translateLanguage').attr("disabled", false);
//     jQuery('.translating').html(result);
//     jQuery('.translating').removeClass('translating');
// }

jQuery(".BtnTranslatePost").live("click", function () {
    $(this).hide();
    $(this).next().removeClass('hidden');
}); 