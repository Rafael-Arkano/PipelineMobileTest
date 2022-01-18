#!/usr/bin/env bash

IOS_INFO_PLIST_FILE=${APPCENTER_SOURCE_DIRECTORY}/MobileTemplate.iOS/Info.plist

source $APPCENTER_SOURCE_DIRECTORY/PreBuildScripts/appcenter-pre-build-common.sh

load_environment_file

update_environment_constants

print_pre_build_result

VERSION_NUMBER=$((VERSION_CODE_SHIFT + APPCENTER_BUILD_ID))

##### UPDATE APP NAME
echo "Changing the App display name on iOS to: ${APPLICATION_NAME_VALUE}"
#plutil -replace CFBundleName -string ${APPLICATION_NAME_VALUE} $IOS_INFO_PLIST_FILE
plutil -replace CFBundleDisplayName -string "${APPLICATION_NAME_VALUE}" $IOS_INFO_PLIST_FILE

##### UPDATE APP ID
echo "Changing the App identifier on iOS to: ${APPLICATION_ID_IOS_VALUE}"
plutil -replace CFBundleIdentifier -string ${PACKAGE_NAME_VALUE} $IOS_INFO_PLIST_FILE

echo "Updatting the build number"
plutil -replace CFBundleVersion -string "${VERSION_NUMBER_PREFIX}${VERSION_NUMBER}" $IOS_INFO_PLIST_FILE

echo "Updatting the version number"
plutil -replace CFBundleShortVersionString -string "${VERSION_NUMBER_PREFIX}${VERSION_NUMBER}" $IOS_INFO_PLIST_FILE

##### PRINT INFO.PLIST FILE
echo "${IOS_INFO_PLIST_FILE} content:"
cat ${IOS_INFO_PLIST_FILE}