#!/usr/bin/env bash

. /hive-config/wallet.conf

API_TIMEOUT="15"

stats_raw=`echo '{"command":"summary+devs"}' | nc -w $API_TIMEOUT localhost 4028`
if [[ $? -ne 0 || -z $stats_raw ]]; then
	echo -e "${YELLOW}Failed to read $miner from localhost:4028${NOCOLOR}"
else
	khs=`echo $stats_raw | jq '.["summary"][0]["SUMMARY"][0]["KHS 5s"]'`
	stats=`echo $stats_raw | jq '{khs: [.devs[0].DEVS[]."KHS 5s"], temp: [.devs[0].DEVS[].Temperature], \
			fan: [.devs[0].DEVS[]."Fan Percent"], uptime: .summary[0].SUMMARY[0].Elapsed, algo: "'$CUSTOM_ALGO'"}'`
fi

	[[ -z $khs ]] && khs=0
	[[ -z $stats ]] && stats="null"
