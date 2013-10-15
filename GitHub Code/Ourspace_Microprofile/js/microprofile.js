// Load the SDK Asynchronously


var respCache;
var totalRenderedUsers = 0;
var fbRowHeight = 50;
var currentPageScroll = 1;
var FRIENDS_PAGE_SIZE = 15;
var APP_ID = '177630498963174';
var friendIDs = [];
var availableTags = [];
jQuery(document).ready(function () {
    console.log("Loading FB SDK...");
    (function (d) {
        var js, id = 'facebook-jssdk'; if (d.getElementById(id)) { return; }
        js = d.createElement('script'); js.id = id; js.async = true;
        js.src = "//connect.facebook.net/en_US/all.js";
        d.getElementsByTagName('head')[0].appendChild(js);
    } (document));
    console.log("Starting...");
    window.fbAsyncInit = function () {
        console.log("Initiliazing facebook sdk...");
        FB.init({
            appId: APP_ID,
            channelURL: 'http://localhost/ourspace', // Channel File
            status: true, // check login status
            cookie: true, // enable cookies to allow the server to access the session
            oauth: true, // enable OAuth 2.0
            xfbml: true  // parse XFBML
        });

        var userFacebookId = "";
        var resp;
        results = "some stuff";


        var shareUrl = $('.personalSharingUrl').html();


        jQuery('.fb-invitation-wrap .action-button').live('click', function () {
            FB.ui({
                method: 'send',
                name: 'Join the discussion on the OurSpace platform!',
                link: shareUrl,
                display: 'popup',
                to: jQuery(this).attr('name')
            },
            function (response) {
                if (response) {
                    facebookShareSuccess();
                } else {
                    facebookShareFailure();
                }
            }
            );

        });



        jQuery('.facebook-send').live('click', function () {
            FB.ui({
                method: 'send',
                name: 'Join the discussion on the OurSpace platform!',
                link: shareUrl,
                display: 'popup'
            },
           function (response) {
               if (response) {
                   facebookShareSuccess();
               } else {
                   facebookShareFailure();
               }
           }
          );
        });












        console.log("Getting login status...");
        FB.getLoginStatus(function (response) {
            if (response.authResponse) {
                console.log("AuthResponse " + response.authResponse);
                FB.api('/me', function (response) {

                    if (response.id == jQuery('.userFacebookId').html()) {
                        userFacebookId = response.id;
                        var imglink = "http://graph.facebook.com/" + response.id + "/picture";
                        jQuery('.profilePicture').attr('src', imglink);
                    }
                });
                if (jQuery('.userFacebookId').html() != "0" && jQuery('.friends-entire-wrapper').length) {

                    FB.api('/me/friends/?fields=installed,name,link,gender,first_name,last_name', function (response2) {
                        //alert(response.data);
                        console.log("Rendering users that are on Ourspace...");
                        resp = response2.data;
                        respCache = resp;
                        var size = resp.length;
                        var MAX_IMAGES = 6;
                        var currentImage = 1;
                        for (var key in resp) {
                            if (currentImage <= 6) {
                                if (resp[key].installed != null) {
                                    var imglink2 = "http://graph.facebook.com/" + resp[key].id + "/picture";
                                    var newImg = "<img src='" + imglink2 + "' alt='' />";
                                    jQuery('.facebook-output').html(jQuery('.facebook-output').html() + newImg);
                                    console.log("User rendered " + currentImage);
                                    currentImage++;

                                }
                            }
                            availableTags.push(resp[key].first_name + " " + resp[key].last_name);
                            friendIDs.push(resp[key].id);
                        }




                        $("#tags").autocomplete({
                            source: availableTags
                        });
                        $("#tags").bind("autocompleteselect", function (event, ui) {
                            var user = ui.item.value;
                            var userPositionInArray = jQuery.inArray(user, availableTags);
                            renderFriend(userPositionInArray);
                            currentPageScroll = 1;

                        });

                        renderFriends(resp, 1, FRIENDS_PAGE_SIZE);
                        if (jQuery('#facebook-friends').length > 0) {
                            renderFriendsMiniFriends(resp);
                        }
                        jQuery(".pager-link").live('click', function () {
                            jQuery('#ourspaceFriends-pager-wrap').empty();
                            jQuery('.user-row').remove();
                            renderFriends(respCache, jQuery(this).html(), FRIENDS_PAGE_SIZE);
                        });
                    }

                );
                    // logged in and connected user, someone you know
                }
            } else {
                if (jQuery('.userFacebookId').html() != "0") {
                    jQuery('.notLoggedOnFacebook').show(); // no user session available, someone you dont know
                    jQuery('.notLoggedOnFacebook').removeClass('hidden');

                    // Friends module
                    jQuery('.friend-table-loading').removeClass('friend-table-loading');
                    jQuery('#more-fb-friends').hide();

                    jQuery('.not-logged-in-fb').show();
                    jQuery('.not-logged-in-fb').removeClass('hidden');
                }

            }
        });


    };


    jQuery(document).ready(function () {
        var pageguide = tl.pg.init();

        jQuery('#more-fb-friends').live('click', function () {
            scrolled();
        });

        jQuery('.pointsAndLevelsInfo').live('click', function () {
            var proposalDialog = jQuery('#pointsAndLevelsDesc').dialog({ modal: true, minHeight: 20, minWidth: 400, title: jQuery('.pointsAndLevelsTitle').html(), buttons: { "Ok": function () { $(this).dialog("close"); } } });
        });





    });



});


function scrolled(e) {
        renderFriends(respCache, currentPageScroll, FRIENDS_PAGE_SIZE);
}


function friendsGoToPage(resp) {
    jQuery('#ourspaceFriends-pager-wrap').append("length " + resp.length);


}

function renderFriendsMiniFriends(resp) {
    var counting = 0;
    for (var key in resp) {
        //currentItem
       
            var imglink2 = "http://graph.facebook.com/" + resp[key].id + "/picture";
            var newImg = "<img src='" + imglink2 + "' alt='' title='"+ resp[key].name +"' />";
            jQuery('#facebook-friends').append(newImg);
            counting++;
            if (counting > 17) {
                break;
            }

        }


    }


    function renderFriend(key) {
        var isAlt = false;
        var imglink2 = "http://graph.facebook.com/" + respCache[key].id + "/picture";
        var newImg = "<img src='" + imglink2 + "' alt='' />";
        var tdClass = "friend-td";
        var onOurspaceHtml = "";
        var testingInstalled = respCache[key].installed;
        if (respCache[key].installed != undefined) {
            onOurspaceHtml = respCache[key].first_name + " " + jQuery('.js_IsOnOurspace').html() + "!";
        }
        else {

            var fbname = respCache[key].first_name;

            if (fbname.length > 15) {
                fbname = fbname.substring(0, 14) + "..";
            }
            onOurspaceHtml = "<span class='fb-user-status'>" + respCache[key].first_name + " " + jQuery('.js_IsNotOnOurspace').html() + "</span><div class='fb-invitation-wrap'><a class='action-button' name='" + respCache[key].id + "'>" + jQuery('.js_Invite').html() + " " + fbname + "</a></div>";

        }
        if (isAlt) {
            jQuery('#friend-table').html('<tr class="user-row"><td class="friend-td fb-cell-1 alt-item"><div class="userWrap">' + newImg + '<a href="' + respCache[key].link + '"><span class="friends-name-lbl">' + respCache[key].name + '</span></a><div class="fb-gender">' + jQuery('.js_Gender').html() + ' <span class="fb-' + respCache[key].gender + '"></span></div></div></td><td class="friend-td alt-item"><div class="fb-info">' + onOurspaceHtml + '</div></td></tr>');
        }
        else {
            jQuery('#friend-table').html('<tr class="user-row"><td class="friend-td fb-cell-1"><div class="userWrap">' + newImg + '<a href="' + respCache[key].link + '"><span class="friends-name-lbl">' + respCache[key].name + '</span></a><div class="fb-gender">' + jQuery('.js_Gender').html() + ' <span class="fb-' + respCache[key].gender + '"></span></div></div></td><td class="friend-td"><div  class="fb-info">' + onOurspaceHtml + '</div></td></tr>');
        }
        totalRenderedUsers++;
    }



function renderFriends(resp, page, pageSize) {

    console.log("Rendering friends...");
    if (page == 1) {
        jQuery('#friend-table').html("");
    }
    var isAlt = false;
    var currentItem = 0;
    var currentPageItem = 0;
    var pageCounter = 1;
    var itemToStartFrom = pageSize * page - pageSize;
    for (var key in resp) {
        //currentItem
        if (currentItem >= itemToStartFrom && currentItem < itemToStartFrom + pageSize)//jQuery('.facebook-output').html(jQuery('.facebook-output').html() + resp[key].id);
        {
            var imglink2 = "http://graph.facebook.com/" + resp[key].id + "/picture";
            var newImg = "<img src='" + imglink2 + "' alt='' />";
            var tdClass = "friend-td";
            var onOurspaceHtml = "";
            var testingInstalled = resp[key].installed;
            if (resp[key].installed != undefined) {
                onOurspaceHtml = resp[key].first_name + " " + jQuery('.js_IsOnOurspace').html() + "!";
            }
            else {

                var fbname = resp[key].first_name;

                if (fbname.length > 15) {
                    fbname = fbname.substring(0, 14) + "..";
                }
                onOurspaceHtml = "<span class='fb-user-status'>" + resp[key].first_name + " " + jQuery('.js_IsNotOnOurspace').html() + "</span><div class='fb-invitation-wrap'><a class='action-button' name='" + resp[key].id + "'>" + jQuery('.js_Invite').html() + " " + fbname + "</a></div>";
               
            }
            if (isAlt) {
                jQuery('#friend-table').append('<tr class="user-row"><td class="friend-td fb-cell-1 alt-item"><div class="userWrap">' + newImg + '<a href="' + resp[key].link + '"><span class="friends-name-lbl">' + resp[key].name + '</span></a><div class="fb-gender">'+ jQuery('.js_Gender').html() +' <span class="fb-' + resp[key].gender + '"></span></div></div></td><td class="friend-td alt-item"><div class="fb-info">' + onOurspaceHtml + '</div></td></tr>');
            }
            else {
                jQuery('#friend-table').append('<tr class="user-row"><td class="friend-td fb-cell-1"><div class="userWrap">' + newImg + '<a href="' + resp[key].link + '"><span class="friends-name-lbl">' + resp[key].name + '</span></a><div class="fb-gender">'+jQuery('.js_Gender').html()+' <span class="fb-' + resp[key].gender + '"></span></div></div></td><td class="friend-td"><div  class="fb-info">' + onOurspaceHtml + '</div></td></tr>');
            }
            totalRenderedUsers++;

        }
        if (currentPageItem == pageSize) {
            pageCounter++;
            currentPageItem = 0;
        }
        currentPageItem++;
        isAlt = !isAlt;
        //k++;
        currentItem++;

    }
        
    if(currentPageScroll > ((respCache.length / FRIENDS_PAGE_SIZE))) {
      jQuery('#more-fb-friends').hide();
    }
currentPageScroll++;
    jQuery('.pager-link[name="' + page + '"]').addClass('pager-link-active');
    jQuery('.friend-table-loading').removeClass('friend-table-loading');
    console.log("Rendering friends...Done");
}


function facebookShareSuccess() {

    var proposalDialog = jQuery('.post-success-wrap').dialog({ modal: true, minHeight: 20, title: jQuery('.js_Success').html(), buttons: { "Ok": function () { $(this).dialog("close"); } } });

}

function facebookShareFailure() {

    var proposalDialogFailed = jQuery('.post-failure-wrap').dialog({ modal: true, minHeight: 20, title: jQuery('.js_InvitationNotSent').html(), buttons: { "Ok": function () { $(this).dialog("close"); } } });

}


    
