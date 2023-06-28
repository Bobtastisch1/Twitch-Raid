# Twitch-Raid

You want to raid on Twitch but you're unsure who you should raid? No worries! Here's a simple program called TwitchRaid that can help you out.

# Program Overview

TwitchRaid is a program that automatically checks all your followers on Twitch and creates a list of those who are currently live. From that list, one lucky streamer will be randomly chosen to receive the raid.

# Setup

Visit https://dev.twitch.tv/console and create a new application.
Set the application's redirect URI to http://localhost:3000 and make note of the generated Client ID and Client Secret.
To obtain an OAuth token with the required scopes, use the following link:


https://id.twitch.tv/oauth2/authorize?response_type=token&client_id={{client-id}}&redirect_uri={{redirect_uri}}&scope=moderator%3Aread%3Afollowers+channel%3Amanage%3Araids

- Replace {{client-id}} with your Client ID
- {{redirect_uri}} with http://localhost:3000.

When you click the link, Twitch will prompt you to log in with the account you wish to use for raiding (can be either your bot account or streaming account). After login, you will receive an Access Token in the Link URL. Save this token for later use.

# Installation

Download the program from the GitHub repository on the right Release get the **.msi** file. 
Extract the contents to a preferred location on your computer.
Inside the extracted folder, you will find an executable file with a cat logo.
Run the program by executing the file.

Upon running, the program will create **Init.txt**, **Ban.txt**, **Favorite.txt** files.

Close the program and open the Init.txt file.
Fill in the required details in the Init.txt file, including:
- ClientId: Your Twitch Client ID obtained from the Twitch Developer Console.
- ClientSecret: Your Twitch Client Secret obtained from the Twitch Developer Console.
- YourStreamerName: The name of your Twitch streamer account.
- oauth: The Access Token you obtained from the Twitch OAuth link.
- OnlyFavorite: True/False  If you only want to Raid your Friends or not **True** **False** Upercase 

After the **field:** double points there is a space
Save the changes made to the Init.txt file.

If you don't want to raid somebody, add their Twitch name in **lower case** example Bobtastisch2 => bobtastisch2

Favorite.txt

Favorite:

yushia

Ban.txt

Ban:

xqc
 
That's it! You should now have TwitchRaid set up and ready to use.

Happy raiding on Twitch!

