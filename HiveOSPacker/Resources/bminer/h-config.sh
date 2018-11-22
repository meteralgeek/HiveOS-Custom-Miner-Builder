#!/usr/bin/env bash

[ -t 1 ] && . colors

log=/hive/custom/bminer/test.log

. /hive/custom/bminer/h-manifest.conf
[[ -z $CUSTOM_URL ]] && echo -e "${YELLOW}BMINER_URL is empty, skipping config generation${NOCOLOR}" && return 1
[[ -z $CUSTOM_TEMPLATE ]] && echo -e "${YELLOW}BMINER_TEMPLATE is empty, skipping config generation${NOCOLOR}" && return 1
conf="-api ${WEB_HOST}:${WEB_PORT} -max-temperature 82"
tpl=$CUSTOM_TEMPLATE
[[ ! -z $WORKER_NAME ]] && tpl=$(sed "s/%WORKER_NAME%/$WORKER_NAME/g" <<< $tpl) #|| echo "${RED}WORKER_NAME not set${NOCOLOR}"
tpl=$(sed 's/\//%2F/g; s/ /%20/g; s/@/%40/g' <<< $tpl)
[[ ! -z $CUSTOM_PASS ]] && tpl+=":$CUSTOM_PASS"

local pool=`head -n 1 <<< "$CUSTOM_URL"`
grep -q "://" <<< $pool
if [[ $? -ne 0 ]]; then #protocol not found
	case $CUSTOM_ALGO in
		"ethash" )
			uri="ethash://"
		;;
		"equihash 144/5" )
			uri="zhash://"
		;;
		"blake2s" )
			uri="blake2s://"
		;;
		"blake14r")
			uri="blake14r://"
		;;
		"tensority" )
			uri="tensority://"
		;;
		* )
			uri="stratum://"
		;;
	esac
	uri=$uri$tpl@$pool
else
	uri=$(sed "s/:\/\//:\/\/$tpl@/g" <<< $pool) #replace :// with username
fi
conf+=" -uri $uri"
[[ ! -z $CUSTOM_USER_CONFIG ]] && conf+=" $CUSTOM_USER_CONFIG"
echo "$conf" > $CUSTOM_CONFIG_FILENAME
