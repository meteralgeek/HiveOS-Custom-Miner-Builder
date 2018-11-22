#!/usr/bin/env bash

export LD_LIBRARY_PATH=/hive/lib

cd `dirname $0`

[ -t 1 ] && . colors

#CUSTOM_MINER="xmrig-amd"
#. /hive-config/wallet.conf
#[[ -z $CUSTOM_MINER ]] && echo -e "${RED}No CUSTOM_MINER is set${NOCOLOR}" && exit 1
#. /hive/custom/$CUSTOM_MINER/h-manifest.conf

. h-manifest.conf

echo $CUSTOM_NAME
echo $CUSTOM_MINER_DIR
echo $CUSTOM_LOG_BASENAME
echo $CUSTOM_CONFIG_FILENAME

. /hive-config/wallet.conf

echo $CUSTOM_ALGO
echo $CUSTOM_TEMPLATE
echo $CUSTOM_URL
echo $CUSTOM_PASS
echo $CUSTOM_USER_CONFIG

[[ -z $CUSTOM_LOG_BASENAME ]] && echo -e "${RED}No CUSTOM_LOG_BASENAME is set${NOCOLOR}" && exit 1
[[ -z $CUSTOM_CONFIG_FILENAME ]] && echo -e "${RED}No CUSTOM_CONFIG_FILENAME is set${NOCOLOR}" && exit 1
[[ ! -f $CUSTOM_CONFIG_FILENAME ]] && echo -e "${RED}Custom config ${YELLOW}$CUSTOM_CONFIG_FILENAME${RED} is not found${NOCOLOR}" && exit 1

CUSTOM_LOG_BASEDIR="$CUSTOM_LOG_BASENAME"

[[ ! -d $CUSTOM_LOG_BASEDIR ]] && mkdir -p $CUSTOM_LOG_BASEDIR

 cd $CUSTOM_MINER_DIR

./$CUSTOM_NAME -a $CUSTOM_ALGO -o $CUSTOM_URL -u $CUSTOM_TEMPLATE -p $CUSTOM_PASS $CUSTOM_USER_CONFIG $@ 2>&1 | tee $CUSTOM_LOG_BASENAME/$CUSTOM_NAME.log
# ./$CUSTOM_NAME $CUSTOM_CONFIG_FILENAME $@ 2>&1 | tee $CUSTOM_LOG_BASENAME/$CUSTOM_NAME.log
