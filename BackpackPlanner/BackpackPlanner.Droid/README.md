# Test Doc Please Ignore

The following is necessary to fix Proguard failing:

* Install Android Studio
* mklink /j C:\android-sdk "<path to Android SDK>"
  * This should be from the Android Studio installation, not Visual Studio/Xamarin
   * C:\Users\<username>\AppData\Local\Android\sdk
  * Set ANDROID_HOME to C:\android-sdk
  * Add %ANDROID_HOME%\tools and %ANDROID_HOME%\platform_tools to the PATH
  * Set PROGUARD_HOME to %ANDROID_HOME%\tools\proguard
* Set Xamarin to use the SDK in C:\android-sdk in Visual Studio
* May need to build the Release build once with Visual Studio in order for everything to get setup for automatic builds
* The Jenkins slave MUST run as a non-system user who has a Xamarin account.
* AOT and LLVM need to be turned off on the Release build or it will fail to build
  * https://forums.xamarin.com/discussion/49180/android-xa5101-platform-library-directory-for-target-arm-and-api-level-23-was-not-found
* http://developer.android.com/training/permissions/index.html

# Elevations

* Cards: 2dp
* List top thingies: 3dp (TODO: this shadow doesn't work *sad face*)
* Toolbars: 4dp
* FAB: 15dp (setting app:borderWidth="0dp" on this doesn't seem to be useful)
* Navigation Drawer: 16dp

# Deployment

* https://developer.xamarin.com/guides/android/deployment,_testing,_and_metrics/publishing_an_application/part_1_-_preparing_an_application_for_release/
* https://developer.xamarin.com/guides/android/deployment,_testing,_and_metrics/publishing_an_application/part_2_-_signing_the_android_application_package/
  * This has a PowerShell example that is useful for working with Jenkins
