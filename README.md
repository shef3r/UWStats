<p align=center><img width=150 src="https://github.com/shef3r/StatifyUWP/blob/master/Assets/Square44x44Logo.targetsize-256.png?raw=true"></p>
<h2 align=center>Statify</h2>
<p align=center>Here's your Spotify companion on Windows! Check all your stats easily and quickly.</p>

// rebrand soon
### Usage
#### Preparation
To use the app, you need to get a client ID. To do that,
- Go to [this website](https://developer.spotify.com/dashboard)
- Create an app:
    - Click the "create" button
    - Type whatever you want into the first 3 fields
    - In the "Redirect URIs" field, paste in "http://localhost:5543/callback"
    - Check "Web API" at the bottom
    - Agree to the terms
    - Click Save
- Click the "Settings" button at the top right of the page
- Save the "Client ID" value somewhere
#### Setup
- Launch the app
- Paste in your Client ID in the field
- Log in to Spotify **with the same account you used to create the app previously**


### Installation
#### Prebuilt
- Go to the Releases tab and download both the `.msixbundle` nad `.cer` files.
- Make sure you have App Installer on your machine (Alternative methods of installation or "debloated" machines are not supported.)
- Open the `.cer` file, install in for either all users and choose "Trusted people" as the certificate store (manually!)
- Open and `.msixbundle` file with App Installer and click "Install"
- Done!
#### From source
- Open the repo in Visual Studio (in my case, 2022 Community)
    - make sure you have the UWP workload and Windows 11 10.0.22621.0 SDK installed.
- Click F5 or choose "Local Machine" in the options on top.
- Done!

### Contributions
I allow bugfixes as PRs with no problem, but if you want to add a feature, create an issue.
