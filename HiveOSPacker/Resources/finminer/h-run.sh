#!/usr/bin/env bash

#export LD_LIBRARY_PATH=/hive/xmr-stak/fireice-uk

cd `dirname $0`

[ -t 1 ] && . colors

#CUSTOM_MINER="xmrig-amd"
#. /hive-config/wallet.conf
#[[ -z $CUSTOM_MINER ]] && echo -e "${RED}No CUSTOM_MINER is set${NOCOLOR}" && exit 1
#. /hive/custom/$CUSTOM_MINER/h-manifest.conf

. h-manifest.conf

echo $CUSTOM_NAME
echo $CUSTOM_LOG_BASENAME
echo $CUSTOM_CONFIG_FILENAME

[[ -z $CUSTOM_LOG_BASENAME ]] && echo -e "${RED}No CUSTOM_LOG_BASENAME is set${NOCOLOR}" && exit 1
[[ -z $CUSTOM_CONFIG_FILENAME ]] && echo -e "${RED}No CUSTOM_CONFIG_FILENAME is set${NOCOLOR}" && exit 1
[[ ! -f $CUSTOM_CONFIG_FILENAME ]] && echo -e "${RED}Custom config ${YELLOW}$CUSTOM_CONFIG_FILENAME${RED} is not found${NOCOLOR}" && exit 1

CUSTOM_LOG_BASEDIR="$CUSTOM_LOG_BASENAME/logs"

[[ ! -d $CUSTOM_LOG_BASEDIR ]] && mkdir -p $CUSTOM_LOG_BASEDIR
[[ -d $CUSTOM_LOG_BASENAME ]] && cp -f /hive/custom/finminer/finminer $CUSTOM_LOG_BASENAME/finminer && cp -f /hive/custom/finminer/kernel.dat $CUSTOM_LOG_BASENAME/kernel.dat && cp -f $CUSTOM_CONFIG_FILENAME $CUSTOM_LOG_BASENAME/config.ini

cd $CUSTOM_LOG_BASENAME

./finminer $CUSTOM_CONFIG_FILENAME$@
#/hive/custom/$CUSTOM_NAME/$CUSTOM_CONFIG_FILENAME$@
#./xmrig-amd $(< /hive/custom/$CUSTOM_NAME/$CUSTOM_NAME.conf) $@ 2>&1 | tee $CUSTOM_LOG_BASENAME.log

