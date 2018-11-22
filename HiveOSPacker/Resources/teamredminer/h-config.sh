#!/usr/bin/env bash
# This code is included in /hive/bin/custom function

[[ -z $CUSTOM_TEMPLATE ]] && echo -e "${YELLOW}CUSTOM_TEMPLATE is empty${NOCOLOR}" && return 1
[[ -z $CUSTOM_URL ]] && echo -e "${YELLOW}CUSTOM_URL is empty${NOCOLOR}" && return 1

case ${CUSTOM_ALGO} in
	"cryptonight" )
		algo="cnv8"
	;;
	"cryptonight-v8" )
		algo="cnv8"
	;;
	"" )
		algo="lyra2z"
	;;
	* )
	algo="${CUSTOM_ALGO}"
	;;
esac

local pool=`head -n 1 <<< "$CUSTOM_URL"`
grep -q "://" <<< $pool
[[ $? -ne 0 ]] && pool="stratum+tcp://${pool}"

conf="-a $algo -o $pool -u ${CUSTOM_TEMPLATE} -p ${CUSTOM_PASS} ${CUSTOM_USER_CONFIG}"

#replace tpl values in whole file
#Don't remove until Hive 1 is gone
[[ ! -z $EWAL ]] && conf=$(sed "s/%EWAL%/$EWAL/g" <<< $conf) #|| echo "${RED}EWAL not set${NOCOLOR}"
[[ ! -z $DWAL ]] && conf=$(sed "s/%DWAL%/$DWAL/g" <<< $conf) #|| echo "${RED}DWAL not set${NOCOLOR}"
[[ ! -z $ZWAL ]] && conf=$(sed "s/%ZWAL%/$ZWAL/g" <<< $conf) #|| echo "${RED}ZWAL not set${NOCOLOR}"
[[ ! -z $EMAIL ]] && conf=$(sed "s/%EMAIL%/$EMAIL/g" <<< $conf)
[[ ! -z $WORKER_NAME ]] && conf=$(sed "s/%WORKER_NAME%/$WORKER_NAME/g" <<< "$conf") #|| echo "${RED}WORKER_NAME not set${NOCOLOR}"

[[ -z $CUSTOM_CONFIG_FILENAME ]] && echo -e "${RED}No CUSTOM_CONFIG_FILENAME is set${NOCOLOR}" && return 1
echo "$conf" > $CUSTOM_CONFIG_FILENAME

