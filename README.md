# TwitchRaid

You want to raid on Twitch and don't really know who you should raid.

Here is a simple program that checks all your followers on Twitch and gets those that are live in a list.

From that list, one is randomly chosen and will get the raid.

All with just one button.


#Setup

Go to https://dev.twitch.tv/console and create an Application on http://localhost:3000
Save the Client-Id and Client-Secret

For Loging in you need a oauth Token from Twitch with the right scopse thats the link for it
https://id.twitch.tv/oauth2/authorize?response_type=token&client_id={{client-id}}&redirect_uri={{redirect_uri}}&scope=moderator%3Aread%3Afollowers+channel%3Amanage%3Araids
replace the client-id with your Client-Id
and redirect_uri with the [loca](http://localhost:3000)http://localhost:3000

Twitch will asked you to Login in the the Account you use for the Raid can be the Bot Account or your Streaming Account.
At the link there will be the Access_Token save.

#Installing the Programm
Get it from Github in the zip File copy it somewhere on your Pc after that.
There should be the Exe File with a Cat Logo on it.
Start the programm it will create the Init.txt file and the Ban.txt file.
The Programm will close and you should fill in the Init file your Saveed Client-id etc.
[
ClientId: 
ClientSecret: 
YourStreamerName: 
oauth: 
]

