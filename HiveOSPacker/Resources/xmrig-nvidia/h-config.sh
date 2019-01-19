#!/usr/bin/env bash
# This code is included in /hive/bin/custom function

case ${CUSTOM_ALGO} in
	"cryptonight-lite-v7" )
		algo="--algo=cryptonight-lite"
	;;
	"" )
		algo=""
	;;
	* )
	algo="--algo=${CUSTOM_ALGO}"
	;;
esac

[[ -z $CUSTOM_TEMPLATE ]] && echo -e "${YELLOW}CUSTOM_TEMPLATE is empty${NOCOLOR}" && return 1
[[ -z $CUSTOM_URL ]] && echo -e "${YELLOW}CUSTOM_URL is empty${NOCOLOR}" && return 1

local pool=`head -n 1 <<< "$CUSTOM_URL"`

conf="-o ${pool} -u ${CUSTOM_TEMPLATE} -p ${CUSTOM_PASS} $algo ${CUSTOM_USER_CONFIG}"

#replace tpl values in whole file
[[ ! -z $WORKER_NAME ]] && conf=$(sed "s/%WORKER_NAME%/$WORKER_NAME/g" <<< "$conf") #|| echo "${RED}WORKER_NAME not set${NOCOLOR}"

[[ -z $CUSTOM_CONFIG_FILENAME ]] && echo -e "${RED}No CUSTOM_CONFIG_FILENAME is set${NOCOLOR}" && return 1
echo "$conf" > $CUSTOM_CONFIG_FILENAME

