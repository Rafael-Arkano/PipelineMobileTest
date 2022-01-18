#!/usr/bin/env bash

CONSTANTS_ENVIRONMENT_FILE=$APPCENTER_SOURCE_DIRECTORY/MobileTemplate.Core/Constants.cs
DEV_ENVIRONMENT_FILE=$APPCENTER_SOURCE_DIRECTORY/PreBuildScripts/appcenter-pre-build-dev-environment.sh
TEST_ENVIRONMENT_FILE=$APPCENTER_SOURCE_DIRECTORY/PreBuildScripts/appcenter-pre-build-test-environment.sh
PRODUCTION_ENVIRONMENT_FILE=$APPCENTER_SOURCE_DIRECTORY/PreBuildScripts/appcenter-pre-build-prod-environment.sh

load_environment_file () {
   	if [ "$APPCENTER_BRANCH" == "release/develop" ];
	then
		echo "Updating enviroment to DEV"
		cat $DEV_ENVIRONMENT_FILE
		source $DEV_ENVIRONMENT_FILE
	elif [ "$APPCENTER_BRANCH" == "release/test" ];
	then
		echo "Updating enviroment to TEST"
		cat $TEST_ENVIRONMENT_FILE
		source $TEST_ENVIRONMENT_FILE
	elif [ "$APPCENTER_BRANCH" == "master" ];
	then
		echo "Updating enviroment to PRODUCTION"
		cat $PRODUCTION_ENVIRONMENT_FILE
		source $PRODUCTION_ENVIRONMENT_FILE
	fi
}

update_environment_constants () {
	echo "Updating enviroment constants"

	echo "Updating environment name to $ENVIRONMENT_NAME_VALUE in $CONSTANTS_ENVIRONMENT_FILE"
	sed -i '' 's/ENVIRONMENT_NAME_PROPERTY/'$ENVIRONMENT_NAME_VALUE'/' $CONSTANTS_ENVIRONMENT_FILE

	echo "Updating web api host to $WEB_API_HOST_VALUE in $CONSTANTS_ENVIRONMENT_FILE"
	sed -i '' 's#WEB_API_HOST_PROPERTY#'$WEB_API_HOST_VALUE'#' $CONSTANTS_ENVIRONMENT_FILE	
	
	echo "Updating web api key to $WEB_API_KEY in $CONSTANTS_ENVIRONMENT_FILE"
	sed -i '' 's#WEB_API_KEY_PROPERTY#'$WEB_API_KEY'#' $CONSTANTS_ENVIRONMENT_FILE

	echo "Updating AppCenter Droid to $APP_CENTER_DROID_VALUE in $CONSTANTS_ENVIRONMENT_FILE"
	sed -i '' 's/APP_CENTER_DROID_PROPERTY/'$APP_CENTER_DROID_VALUE'/' $CONSTANTS_ENVIRONMENT_FILE	

	echo "Updating AppCenter iOS to $APP_CENTER_IOS_VALUE in $CONSTANTS_ENVIRONMENT_FILE"
	sed -i '' 's/APP_CENTER_IOS_PROPERTY/'$APP_CENTER_IOS_VALUE'/' $CONSTANTS_ENVIRONMENT_FILE
}

print_pre_build_result () {
   	# Printing Constants file
	echo "$CONSTANTS_ENVIRONMENT_FILE content:"
	cat $CONSTANTS_ENVIRONMENT_FILE
}