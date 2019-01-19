#!/usr/bin/env bash
# This code is included in /hive/bin/custom function


conf="${CUSTOM_USER_CONFIG}"


[[ -z $CUSTOM_CONFIG_FILENAME ]] && echo -e "${RED}No CUSTOM_CONFIG_FILENAME is set${NOCOLOR}" && return 1
echo "$conf" > $CUSTOM_CONFIG_FILENAME

