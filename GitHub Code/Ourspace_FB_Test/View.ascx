<%@ Control language="C#" Inherits="DotNetNuke.Modules.Ourspace_FB_Test.View" AutoEventWireup="false"  Codebehind="View.ascx.cs" %>
test


<script>
//    window.fbAsyncInit = function () {
//        FB.init({ appId: '177630498963174', status: true, cookie: true,
//            xfbml: true
//        });
//    };
//    (function () {
//        var e = document.createElement('script'); e.async = true;
//        e.src = document.location.protocol +
//      '//connect.facebook.net/en_US/all.js';
//        document.getElementById('fb-root').appendChild(e);
//    } ());
</script>


<div id="fb-root"></div>
<script src="http://connect.facebook.net/en_US/all.js"></script>
<script>
    FB.init({
        appId: '177630498963174',
        status: true, // check login status
        cookie: true, // enable cookies to allow the server to access the session
        xfbml: true, // parse XFBML
        channelURL: 'http://pc39.atc.gr/ourspace/Portals/0/channel.html', // channel.html file
        oauth: true // enable OAuth 2.0
    });
</script>
<script>
//FB.api('/me', function(response) {
//  alert(response.name);
    //});
//    FB.getLoginStatus(function (response) {
//        if (response.authResponse) {
//            alert('logged in and connected user');
//            // logged in and connected user, someone you know
//        } else {
//            alert('no user session available');
//            // no user session available, someone you dont know
//        }
    //    });



//    FB.ui(
//  {
//      method: 'feed',
//      attachment: {
//          name: 'JSSDK',
//          caption: 'The Facebook JavaScript SDK',
//          description: (
//        'A small JavaScript library that allows you to harness ' +
//        'the power of Facebook, bringing the user\'s identity, ' +
//        'social graph and distribution power to your site.'
//      ),
//          href: 'http://fbrell.com/'
//      },
//      action_links: [
//      { text: 'fbrell', href: 'http://fbrell.com/' }
//    ]
//  },
//  function (response) {
//      if (response && response.post_id) {
//          alert('Post was published.');
//      } else {
//          alert('Post was not published.');
//      }
//  }
    //);
   // FB.api('/me', function (response2) {
    //    alert(response2.Name);
   // });
        FB.getLoginStatus(function (response) {
            if (response.authResponse) {
                //alert('logged in and connected user');
                // logged in and connected user, someone you know

                //alert(response.authResponse.userID);

            } else {
                alert('no user session available');
                // no user session available, someone you dont know
            }
        });
        var user_id = '100002495509310';
        var query = FB.Data.query('select name, uid from user where uid={0}',
                           user_id);
        query.wait(function (rows) {
            //document.getElementById('name').innerHTML =
            //alert('Your name is ' + rows[0].name);
            document.getElementById('name').innerHTML = 'Your name is ' + rows[0].name;
        });

//         FB.ui({
//          method: 'send',
//          name: 'Test',
//          link: 'http://www.nytimes.com/2011/06/15/arts/people-argue-just-to-win-scholars-assert.html',
        //          });

        var params = {};
        params['message'] = 'Message';
        params['name'] = 'Name';
        params['description'] = 'Description';
        params['link'] = 'http://www.google.com';
        params['picture'] = 'http://summer-mourning.zoocha.com/uploads/thumb.png';
        params['caption'] = 'Caption';

        FB.api('/me/feed', 'post', params, function (response) {
            if (!response || response.error) {
                //alert('Error occured');
            } else {
                alert('Published to stream - you might want to delete it now!');
            }
        });


//    FB.api('/me', function (response) {
//        alert(response.name);
//    });
      

  //alert('Post was published.');
</script>
<script>
//    function displayUser(user) {
//        var userName = document.getElementById('userName');
//        var greetingText = document.createTextNode('Greetings, '
//         + user.name + '.');
//        userName.appendChild(greetingText);
//    }

//    var appID = '177630498963174';
//    if (window.location.hash.length == 0) {
//        var path = 'https://www.facebook.com/dialog/oauth?';
//        var queryParams = ['client_id=' + appID,
//        //'redirect_uri=' + window.location,
//        'redirect_uri=' + window.location,
//     'response_type=token'];
//        var query = queryParams.join('&');
//        var url = path + query;
//       window.open(url);
//    } else {
//        var accessToken = window.location.hash.substring(1);
//        var path = "https://graph.facebook.com/me?";
//        var queryParams = [accessToken, 'callback=displayUser'];
//        var query = queryParams.join('&');
//        var url = path + query;

//        // use jsonp to call the graph
//        var script = document.createElement('script');
//        script.src = url;
//        document.body.appendChild(script);
//    }
   </script> 
   <p id="userName"></p> 
<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
<div id="name"></div>