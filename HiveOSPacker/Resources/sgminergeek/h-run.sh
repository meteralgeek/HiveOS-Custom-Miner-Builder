#!/usr/bin/env bash

#Ubuntu 18.04 compat
[[ -e /usr/lib/x86_64-linux-gnu/libcurl-compat.so.3.0.0 ]] && export LD_PRELOAD=libcurl-compat.so.3.0.0

export GPU_MAX_HEAP_SIZE=100
export GPU_USE_SYNC_OBJECTS=1
export GPU_SINGLE_ALLOC_PERCENT=100
export GPU_MAX_ALLOC_PERCENT=100


cd `dirname $0`

#remove core dumps
rm *.bin > /dev/null 2>&1

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
SGMINER_GM_ALGO="$CUSTOM_ALGO"

[[ ! -d $CUSTOM_LOG_BASEDIR ]] && mkdir -p $CUSTOM_LOG_BASEDIR

 cd $CUSTOM_MINER_DIR


./$CUSTOM_NAME -k $SGMINER_GM_ALGO -o $CUSTOM_URL -u $CUSTOM_TEMPLATE -p $CUSTOM_PASS $CUSTOM_USER_CONFIG --api-listen --api-allow W:127.0.0.1
#./$CUSTOM_NAME -k $CUSTOM_ALGO -o $CUSTOM_URL -u $CUSTOM_TEMPLATE -p $CUSTOM_PASS $CUSTOM_USER_CONFIG --api-listen $@ 2>&1 | tee $CUSTOM_LOG_BASENAME/$CUSTOM_NAME.log
# ./$CUSTOM_NAME $CUSTOM_CONFIG_FILENAME $@ 2>&1 | tee $CUSTOM_LOG_BASENAME/$CUSTOM_NAME.log
