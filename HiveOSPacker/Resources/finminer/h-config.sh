#!/usr/bin/env bash
# This code is included in /hive/bin/custom function

. /hive/miners/custom/$CUSTOM_NAME/h-manifest.conf
log=/hive/miners/custom/$CUSTOM_NAME/test.log

[[ -z $CUSTOM_TEMPLATE ]] && echo -e "${YELLOW}CUSTOM_TEMPLATE is empty${NOCOLOR}" && return 1
[[ -z $CUSTOM_CONFIG_FILENAME ]] && echo -e "${RED}No CUSTOM_CONFIG_FILENAME is set${NOCOLOR}" && return 1

echo "wallet = ${CUSTOM_TEMPLATE}" | sed "s/%WORKER_NAME%/$WORKER_NAME/g" > $CUSTOM_CONFIG_FILENAME

[[ ! -z ${CUSTOM_ALGO} ]] && echo "algorithm = ${CUSTOM_ALGO}" >> $CUSTOM_CONFIG_FILENAME

[[ ! -z ${CUSTOM_PASS} ]] && echo "rigPassword = ${CUSTOM_PASS}" >> $CUSTOM_CONFIG_FILENAME

i=1
if [[ ! -z $CUSTOM_URL ]]; then
	if [[ $CUSTOM_URL = *"pool"* ]]; then
		echo $CUSTOM_URL >> $CUSTOM_CONFIG_FILENAME
	else
		for url in $CUSTOM_URL; do
			echo "pool$i = $url" >> $CUSTOM_CONFIG_FILENAME
			let "i = i + 1"
		done
	fi
fi

echo "mport = -$WEB_PORT" >> $CUSTOM_CONFIG_FILENAME

[[ -n $CUSTOM_USER_CONFIG ]] && echo ${CUSTOM_USER_CONFIG} | sed "s/%WORKER_NAME%/$WORKER_NAME/g" | sed 's/, /\n/g' | sed 's/,/\n/g' >> $CUSTOM_CONFIG_FILENAME

