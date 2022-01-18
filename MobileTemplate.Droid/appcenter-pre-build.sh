#!/usr/bin/env bash

ANDROID_MAINACTIVITY_FILE=${APPCENTER_SOURCE_DIRECTORY}/MObileTemplate.Droid/Activities/MainActivity.cs
ANDROID_SPLASHACTIVITY_FILE=${APPCENTER_SOURCE_DIRECTORY}/MObileTemplate.Droid/Activities/SplashScreen.cs
ANDROID_MANIFEST_FILE=$APPCENTER_SOURCE_DIRECTORY/MObileTemplate.Droid/Properties/AndroidManifest.xml
INFO_PLIST_FILE=$APPCENTER_SOURCE_DIRECTORY/MObileTemplate.iOS/Info.plist

source $APPCENTER_SOURCE_DIRECTORY/PreBuildScripts/appcenter-pre-build-common.sh

load_environment_file

update_environment_constants

print_pre_build_result

##### UPDATE APP NAME
echo "Changing the App display name on Android to: ${APPLICATION_NAME_VALUE}"
sed -i '' "s#Label = \"@string/AppName\"#Label = \"${APPLICATION_NAME_VALUE}\"#" ${ANDROID_MAINACTIVITY_FILE}
sed -i '' "s#Label = \"@string/AppName\"#Label = \"${APPLICATION_NAME_VALUE}\"#" ${ANDROID_SPLASHACTIVITY_FILE}

VERSION_NUMBER=$((VERSION_CODE_SHIFT + APPCENTER_BUILD_ID))

##### PRINT MAINACTIVITY AND SPLASHACTIVITY
echo "${ANDROID_MAINACTIVITY_FILE} content:"
cat ${ANDROID_MAINACTIVITY_FILE}
echo "${ANDROID_SPLASHACTIVITY_FILE} content:"
cat ${ANDROID_SPLASHACTIVITY_FILE}

#### UPDATE PACKAGE NAME AND PRINT ANDROIDMANIFES AND INFOPLIST
if [ -e "$ANDROID_MANIFEST_FILE" ]
then
    echo "Updating package name to $PACKAGE_NAME_VALUE in AndroidManifest.xml"
    sed -i '' 's/package="[^"]*"/package="'$PACKAGE_NAME_VALUE'"/' $ANDROID_MANIFEST_FILE

	echo "Updating app name to $APPLICATION_NAME_VALUE in AndroidManifest.xml"
	sed -i '' "s#label=\"@string/AppName\"#label=\"${APPLICATION_NAME_VALUE}\"#" ${ANDROID_MANIFEST_FILE}

    echo "Updating version number to $VERSION_CODE in AndroidManifest.xml"
    sed -i "" 's/android:versionName="[^"]*"/android:versionName="'${VERSION_NUMBER_PREFIX}${VERSION_NUMBER}'"/' ${ANDROID_MANIFEST_FILE}


    echo "File content:"
    cat $ANDROID_MANIFEST_FILE
fi

if [ -e "$INFO_PLIST_FILE" ]
then
    echo "Updating package name to $PACKAGE_NAME_VALUE in Info.plist"
    plutil -replace CFBundleIdentifier -string $PACKAGE_NAME_VALUE $INFO_PLIST_FILE

    echo "File content:"
    cat $INFO_PLIST_FILE
fi