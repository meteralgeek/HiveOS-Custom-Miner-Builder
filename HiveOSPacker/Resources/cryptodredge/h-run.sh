#!/usr/bin/env bash

cd `dirname $0`

[ -t 1 ] && . colors

. h-manifest.conf
[[ `dpkg -s libc-ares2 2>/dev/null | grep -c "ok installed"` -eq 0 ]] && apt-get install -y libc-ares2


[[ -z $CUSTOM_LOG_BASENAME ]] && echo -e "${RED}No CUSTOM_LOG_BASENAME is set${NOCOLOR}" && exit 1
[[ -z $CUSTOM_CONFIG_FILENAME ]] && echo -e "${RED}No CUSTOM_CONFIG_FILENAME is set${NOCOLOR}" && exit 1
[[ ! -f $CUSTOM_CONFIG_FILENAME ]] && echo -e "${RED}Custom config ${YELLOW}$CUSTOM_CONFIG_FILENAME${RED} is not found${NOCOLOR}" && exit 1
CUSTOM_LOG_BASEDIR=`dirname "$CUSTOM_LOG_BASENAME"`
[[ ! -d $CUSTOM_LOG_BASEDIR ]] && mkdir -p $CUSTOM_LOG_BASEDIR

DRV_VERS=`nvidia-smi --help | head -n 1 | awk '{print $NF}' | sed 's/v//' | tr '.' ' ' | awk '{print $1}'`

#echo $DRV_VERS

echo -e -n "${GREEN}NVidia${NOCOLOR} driver ${GREEN}${DRV_VERS}${NOCOLOR}-series detected "
#if [ ${DRV_VERS} -ge 396 ]; then
  if [ ${DRV_VERS} -ge 410 ]; then
    echo -e "(${BCYAN}CUDA 10.0${NOCOLOR} compatible)"
    ln -fs CryptoDredge_c100 CryptoDredge
  else
    echo -e "(${BCYAN}CUDA 9.2${NOCOLOR} compatible)"
    ln -fs CryptoDredge_c92 CryptoDredge
  fi
#else
#   echo -e "(${BCYAN}CUDA 9.1${NOCOLOR} compatible)"
#   ln -fs CryptoDredge_c91 CryptoDredge
#fi

./$CUSTOM_NAME $(< $CUSTOM_CONFIG_FILENAME) --log $CUSTOM_LOG_BASENAME.log -b 127.0.0.1:${WEB_PORT}$@

