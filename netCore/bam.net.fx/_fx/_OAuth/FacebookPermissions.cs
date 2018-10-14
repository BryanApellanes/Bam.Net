/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.OAuth
{
    public static class FacebookPermissions
    {
        /// <summary>
        ///  Enables your application to post content; comments; 
        ///  and likes to a user's stream and to the streams of the
        ///  user's friends. With this permission; you can publish
        ///  content to a user's feed at any time; without requiring
        ///  offline_access. However; please note that Facebook recommends
        ///  a user-initiated sharing model.
        /// </summary>
        public const string PublishStream = "publish_stream";

        /// <summary>
        /// Enables your application to create and modify events on the user's behalf
        /// </summary>
        public const string CreateEvent = "create_event";

        /// <summary>
        /// Enables your application to RSVP to events on the user's behalf 
        /// </summary>
        public const string RsvpEvent = "rsvp_event";

        /// <summary>
        /// Enables your application to send messages 
        /// to the user and respond to messages from the user via text message
        /// </summary>
        public const string Sms = "sms 	";

        /// <summary>
        /// Enables your application to perform authorized requests on behalf of 
        /// the user at any time. By default; most access tokens expire after a short
        /// time period to ensure applications only make requests on behalf of the user 
        /// when the are actively using the application. This permission makes the access
        /// token returned by our OAuth endpoint long-lived.
        /// </summary>
        public const string OfflineAccess = "offline_access";

        /// <summary>
        /// Enables your application to perform checkins on behalf of the user
        /// </summary>
        public const string PublishCheckins = "publish_checkins";

        /// <summary>
        /// Provides access to the "About Me" section of the profile in the about property
        /// </summary>
        public const string UserAboutMe = "user_about_me";
        public const string FriendsAboutMe = "friends_about_me";

        /// <summary>
        /// Provides access to the user's list of activities as the activities connection
        /// </summary>
        public const string UserActivities = "user_activities";
        public const string FriendsActivities = "friends_activities";


        /// <summary>
        /// Provides access to the birthday with year as the birthday_date property
        /// </summary>
        public const string UserBirthday = "user_birthday";
        public const string FriendsBirthday = "friends_birthday";

        /// <summary>
        /// Provides access to education history as the education property
        /// </summary>
        public const string UserEducationHistory = "user_education_history";
        public const string FriendsEducationHistory = "friends_education_history";

        /// <summary>
        /// Provides access to the list of events the user is attending as the events connection
        /// </summary>
        public const string UserEvents = "user_events";
        public const string FriendsEvents = "friends_events";

        /// <summary>
        /// Provides access to the list of groups the user is a member of as the groups connection
        /// </summary>
        public const string UserGroups = "user_groups";
        public const string FriendsGroups = "friends_groups";

        /// <summary>
        /// Provides access to the user's hometown in the hometown property
        /// </summary>
        public const string UserHomeTown = "user_hometown";
        public const string FriendsHometown = "friends_hometown";

        /// <summary>
        /// Provides access to the user's list of interests as the interests connection
        /// </summary>
        public const string UserInterests = "user_interests";
        public const string FriendsInterests = "friends_interests";

        /// <summary>
        /// Provides access to the list of all of the pages the user has liked as the likes connection
        /// </summary>
        public const string UserLikes = "user_likes";
        public const string FriendsLikes = "friends_likes";

        /// <summary>
        /// Provides access to the user's current location as the location property
        /// </summary>
        public const string UserLocation = "user_location";
        public const string FriendsLocation = "friends_locatin";

        /// <summary>
        ///	Provides access to the user's notes as the notes connection
        /// </summary>
        public const string UserNotes = "user_notes";
        public const string FriendsNotes = "friends_notes";

        /// <summary>
        ///	Provides access to the user's online/offline presence
        /// </summary>
        public const string UserOnlinePresence = "user_online_presence";
        public const string FriendsOnlinePresence = "friends_online_presence";

        /// <summary>
        /// Provides access to the photos the user has been tagged in as the photos connection
        /// </summary>
        public const string UserPhotoVideoTags = "user_photo_video_tags";
        public const string FriendsPhotoVideoTags = "friends_photo_video_tags";

        /// <summary>
        /// Provides access to the photos the user has uploaded
        /// </summary>
        public const string UserPhotos = "user_photos";
        public const string FriendsPhotos = "friends_photos";

        /// <summary>
        /// Provides access to the user's family and personal relationships and relationship status
        /// </summary>
        public const string UserRelationships = "user_relationships";
        public const string FriendsRelationships = "friends_relationships";

        /// <summary>
        /// Provides access to the user's relationship preferences
        /// </summary>
        public const string UserRelationshipDetails = "user_relationship_details";
        public const string FriendsRelationshipDetails = "friends_relationship_details";

        /// <summary>
        /// Provides access to the user's religious and political affiliations
        /// </summary>
        public const string UserReligionPolitics = "user_religion_politics";
        public const string FriendsReligionPolitics = "friends_religion_politics";

        /// <summary>
        /// Provides access to the user's most recent status message
        /// </summary>
        public const string UserStats = "user_status";
        public const string FriendsStatus = "friends_status";

        /// <summary>
        /// Provides access to the videos the user has uploaded
        /// </summary>
        public const string UserVideos = "user_videos";
        public const string FriendsVideos = "friends_videos";

        /// <summary>
        /// Provides access to the user's web site URL
        /// </summary>
        public const string UserWebsite = "user_website";
        public const string FriendsWebsite = "friends_website";

        /// <summary>
        /// Provides access to work history as the work property
        /// </summary>
        public const string UserWorkHistory = "user_work_history";
        public const string FriendsWorkHistory = "friends_work_history";

        /// <summary>
        /// Provides access to the user's primary email address in the email property. 
        /// Do not spam users. Your use of email must comply both with Facebook policies 
        /// and with the CAN-SPAM Act.
        /// </summary>
        public const string Email = "email";

        /// <summary>
        /// Provides read access to any friend lists the user created. 
        /// NOTE: All user's friends are provided as part of basic data; this extended 
        /// permission grants access to the lists of friends a user has created; and 
        /// should only be requested if your application utilizes lists of friends.
        /// </summary>
        public const string ReadFriendLists = "read_friendlists";

        /// <summary>
        /// Provides read access to the Insights data for pages; applications; and domains the user owns.
        /// </summary>
        public const string ReadInsights = "read_insights";

        /// <summary>
        /// Provides the ability to read from a user's Facebook Inbox.
        /// </summary>
        public const string ReadMailbox = "read_mailbox";

        /// <summary>
        /// Provides read access to the user's friend requests
        /// </summary>
        public const string ReadRequests = "read_requests";

        /// <summary>
        /// Provides access to all the posts in the user's News Feed and enables your application to perform searches against the user's News Feed
        /// </summary>
        public const string ReadStream = "read_stream";

        /// <summary>
        /// Provides applications that integrate with Facebook Chat the ability to log in users.
        /// </summary>
        public const string XmppLogin = "xmpp_login";

        /// <summary>
        /// Provides the ability to manage ads and call the Facebook Ads API on behalf of a user.
        /// </summary>
        public const string AdsManagement = "ads_management";

        /// <summary>
        /// Provides read access to the authorized user's check-ins or a friend's check-ins that the user can see.
        /// </summary>
        public const string UserCheckins = "user_checkins";
        public const string FriendsCheckins = "friends_checkins";
    }
}
