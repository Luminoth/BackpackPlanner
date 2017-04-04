# Test Doc Please Ignore

The following is necessary to fix Proguard failing:

* Ensure that Jenkins is running as a local user, not the system user!
* Uninstall any 32-bit or pre-1.8 JDKs and install 64-bit JDK 1.8
  * Set JAVA_HOME and add %JAVA_HOME%\bin to the PATH
* Install Android Studio
* mklink /j C:\android-sdk "<path to Android SDK>"
  * This should be from the Android Studio installation, not Visual Studio/Xamarin
   * C:\Users\<username>\AppData\Local\Android\sdk
  * Set ANDROID_HOME to C:\android-sdk
  * Add %ANDROID_HOME%\tools and %ANDROID_HOME%\platform-tools to the PATH
  * Set PROGUARD_HOME to %ANDROID_HOME%\tools\proguard
    ** This should be the most current proguard, so that might need to be downloaded and copied over
      *** Even for debug builds, this should be the latest proguard
      *** The android specific proguard files (proguard-*.txt) should be copied from the SDK proguard to the latest proguard
  * Add %PROGUARD_HOME%\bin to the PATH
* Set Xamarin to use the SDK in C:\android-sdk in Visual Studio (in Tools -> Options -> Xamarin)
* Set Xamarin to use the JDK 1.8 in Visual Studio (in Tools -> Options -> Xamarin)
* Set the Visual Studio Cross Platform Android SDK to be C:\android-sdk (in Tools -> Options -> Cross Platform)
* Set the Visual Studio Cross Platform JDK to be 1.8 (in Tools -> Options -> Cross Platform)
* On top of this, Jenkins should set /p:AndroidSdkDirectory=C:\android-sdk when building to ensure the correct SDK is used
* May need to build the Release build once with Visual Studio in order for everything to get setup for automatic builds
* The Jenkins slave MUST run as a non-system user who has a Xamarin account.
* AOT and LLVM need to be turned off on the Release build or it will fail to build
  * https://forums.xamarin.com/discussion/49180/android-xa5101-platform-library-directory-for-target-arm-and-api-level-23-was-not-found

# Misc

* http://developer.android.com/training/permissions/index.html
* https://theconfuzedsourcecode.wordpress.com/2017/01/03/is-your-visual-studio-stuck-at-emulator-is-already-running/

# Icons

* 36dp

# Deployment

* https://developer.xamarin.com/guides/android/deployment,_testing,_and_metrics/publishing_an_application/part_1_-_preparing_an_application_for_release/
* https://developer.xamarin.com/guides/android/deployment,_testing,_and_metrics/publishing_an_application/part_2_-_signing_the_android_application_package/
  * This has a PowerShell example that is useful for working with Jenkins

# Pulling the database from a debug install

* adb shell
  * run-as com.energonsoftware.BackpackPlanner
  * cd /data/user/0/com.energonsoftware.BackpackPlanner/files/
  * chmod 644 BackpackPlanner.db
  * cp BackpackPlanner.db /mnt/sdcard
  * chmod 600 BackpackPlanner.db
  * exit
  * exit
* adb pull /mnt/sdcard/BackpackPlanner.db