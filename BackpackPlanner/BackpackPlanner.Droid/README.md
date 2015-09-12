# Test Doc Please Ignore

This is necessary to fix Proguard failing:

mklink /j C:\android-sdk "C:\Program Files (x86)\Android\android-sdk"
Set Xamarin to use the SDK in C:\android-sdk in Visual Studio

Also, the Jenkins slave MUST run as a non-system user who has a Xamarin account.

Also AOT and LLVM need to be turned off on the Release build or it will fail to build:

https://forums.xamarin.com/discussion/49180/android-xa5101-platform-library-directory-for-target-arm-and-api-level-23-was-not-found

# Elevations

* Cards: 2dp
* List top thingies: 3dp (TODO: this shadow doesn't work *sad face*)
* Toolbars: 4dp
* FAB: 15dp (setting app:borderWidth="0dp" on this doesn't seem to be useful)
* Navigation Drawer: 16dp
