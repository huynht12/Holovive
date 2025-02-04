# HoloVive
Observe a VR session from the same room using a HoloLens!

This project uses Unity's cloud based Multiplayer service.  After opening the project
 go to Window > Services to open the Services tab.  It is important to mention you 
 must be logged into your Unity cloud account within the Unity Editor. (ie. not working offline)
 
![HoloLens Build Settings](HoloViveObserver/Screenshots/services_tab.png)

Click the *New link...* button in the Services tab. Select your organization name 
(username). Click *Create* button to create a cloud project.  Next click the
Multiplayer service to enable.  Click *Go to dashboard* In a browser if you are
 logged into your Unity account a *Max players per room" field needs to be set as
 2 or more and click save. Finally click _Refresh Configuration_ and you should
 now see a list of your current configuration settings.
 
## Building the HoloLens app
If you are planning on building for the HoloLens and running the Vive from the same
 machine it is important to build for the HoloLens
first.

Open the File -> Build Settings menu.

Make sure *Windows Store* is selected. If it isn't, select it and click
*Switch Platform*.

Then click *Build* and select the existing *App* folder. This will export
a Visual Studio project. Yes, you have to do this each time you make changes
within Unity and want to deploy to HoloLens. If you *just* make code changes,
you can skip the Build Settings step and build right from Visual Studio.

Within the *App* folder, open the *Vive Observer.sln* file. Do not be confused
by the HoloViveObserver.sln file in the parent folder, that solution has no
projects.

Once within Visual Studio 2017, select *Release* and target *x86*, then choose
whether you want to deploy to an actual HoloLens or the emulator.  

![HoloLens Build Settings](HoloViveObserver/Screenshots/SteamVR_Settings_Do_Not_Press.png)

NOTE: When choosing the UWP Build Type as *D3D* the SteamVR_Settings window may pop up.  Simply
leave this window up, DO NOT click *Accept All Settings* or *Ignore All*.  If you do this
the project will not function properly on the HoloLens.

## Running the Vive (SteamVR) app
Open the File -> Build Settings menu.

Make sure *PC, Mac, Linux Standalone* is selected. If it isn't, select it and click
*Switch Platform*.

On the SteamVR_Settings window click *Accept All*

![HoloLens Build Settings](HoloViveObserver/Screenshots/SteamVR_Settings.png)

Now, hit the play button at the top of the Unity window.

## Building the HoloLens app after *Accept All*
Make sure your build settings look like this:
![HoloLens Build Settings](HoloViveObserver/Screenshots/build_settings_hololens.png)

Also make sure your color space at the player settings is Gamma.
![HoloLens Player Settings](HoloViveObserver/Screenshots/gama.jpg)

