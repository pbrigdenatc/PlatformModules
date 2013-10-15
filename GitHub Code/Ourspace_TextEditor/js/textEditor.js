// append after each Forum_PostBody

// triggered by replyButton

jQuery(document).ready(function () {
    jQuery('.replyButton').live('click', function () {
        jQuery('.textEditor-wrapper').hide();
        if (parseInt(jQuery('.currentUserId').html()) > 0) {
            var test = jQuery(this).closest('.Forum_PostBody');
            jQuery(this).closest('[class^=Forum_PostBody_Container]').append(jQuery('.textEditor-wrapper'));
            var postId = jQuery(this).next().html();
            var subject = jQuery(this).next().next().html();
            jQuery('.hidden-post-id').html(postId);
            jQuery('.hidden-post-subject').html(subject);
            jQuery('.textEditor-wrapper').fadeIn();
        }
        else {
            var proposalDialog = jQuery('.post-please-login-proposal').dialog({ modal: true, title: jQuery('.PostPleaseLoginTitle').html(), minHeight: 50 });
            jQuery('.proposal-cancel').live('click', function () {
                jQuery('.post-please-login-proposal').dialog('close');
            });
        }


        var postParent = jQuery(this).closest('[id^=tblPostBody]');
        var replyPostId = postParent.attr('id').replace('tblPostBody', '');
        jQuery('[id$=hdnfld_CurrentPostReply]').val(replyPostId);




    });
    if (jQuery('[id$=hdnfld_CurrentPostReply]').length > 0 && jQuery('[id$=hdnfld_CurrentPostReply]').val() != "") {
        var testingValue = jQuery('[id$=hdnfld_CurrentPostReply]').val();
        jQuery('.textEditor-wrapper').hide();
        jQuery('[id^=tblPostBody' + jQuery('[id$=hdnfld_CurrentPostReply]').val() + ']').closest('[class^=Forum_PostBody_Container]').append(jQuery('.textEditor-wrapper'));
        jQuery('.textEditor-wrapper').show();
    }

    jQuery('.proposalDescription').keyup(function () {
        limitChars('proposalDescription', 500, 'available-chars');
    })

    jQuery('.popup-login-btn').attr('href', jQuery('.logout-button').attr('href'));



    jQuery('.feedback-cancel').click(function () {
        jQuery('.textEditor-wrapper').fadeOut();
    });

    jQuery('.submit-proposal-btn-wrapper .action-button').live('click', function () {

        if (parseInt(jQuery('.currentUserId').html()) > 0) {
            jQuery('.textEditor-propose-solution').show();
            var proposalDialog = jQuery('.textEditor-propose-solution').dialog({ modal: true, width: '700px', title: jQuery('.ProposalDialogTitle').html() });
            proposalDialog.parent().appendTo(jQuery("form:first"));

            jQuery('.proposal-cancel').click(function () {
                jQuery('.textEditor-propose-solution').dialog('close');
            });



            jQuery('.textEditor-propose-solution').show();
        }
        else {
            var proposalDialog = jQuery('.post-please-login-proposal').dialog({ modal: true, title: jQuery('.PostPleaseLoginTitle').html(), minHeight: 50 });
            jQuery('.proposal-cancel').live('click', function () {
                jQuery('.post-please-login-proposal').dialog('close');
            });

        }

    }
    );

    jQuery('.already-voted').live('click', function () {
        var proposalDialog2 = jQuery('.lblAlreadyVoted').dialog({ modal: true, title: jQuery('.lblAlreadyVotedTitle').html(), minHeight: 50 });
        jQuery('.already-voted-close').live('click', function () {
            jQuery('.lblAlreadyVoted').dialog('close');
        });
    });

    jQuery('.please-log-in').live('click', function () {
        var proposalDialog3 = jQuery('.post-please-login-proposal').dialog({ modal: true, title: jQuery('.PostPleaseLoginTitle').html(), minHeight: 50 });
        jQuery('.proposal-cancel').live('click', function () {
            jQuery('.post-please-login-proposal').dialog('close');
        });
    });


    jQuery('.Forum_PostBody #spBody, .Forum_PostBody_Alt #spBody').each(function () {
        var postHtml = jQuery(this).html();
        postHtml = replaceURLWithHTMLLinks(postHtml);
        jQuery(this).html(postHtml);
    })



});

function replaceURLWithHTMLLinks(text) {
    var exp = /(\b(https?|ftp|file):\/\/[-A-Z0-9+&@#\/%?=~_|!:,.;]*[-A-Z0-9+&@#\/%=~_|])/ig;
    return text.replace(exp,"<a target='_blank' href='$1'>$1</a>"); 
}
function limitChars(textid, limit, infodiv) {
    var text = jQuery('.' + textid).val();
    var textlength = text.length;
    if (textlength > limit) {
        //jQuery('#' + infodiv).html('You cannot write more then ' + limit + ' characters!');
        jQuery('.' + textid).val(text.substr(0, limit));
        return false;
    }
    else {
        jQuery('#' + infodiv).html(limit - textlength);
        return true;
    }
}
 
