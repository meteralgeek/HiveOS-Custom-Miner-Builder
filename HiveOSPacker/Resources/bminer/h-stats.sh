#!/usr/bin/env bash

#######################
# MAIN script body
#######################


[ -t 1 ] && . colors

. /hive/custom/bminer/h-manifest.conf


	#@see https://www.bminer.me/references/
	stats_raw=`curl --connect-timeout 2 --max-time $API_TIMEOUT --silent --noproxy '*' http://${WEB_HOST}:${WEB_PORT}/api/status`
	if [[ $? -ne 0 || -z $stats_raw ]]; then
		echo -e "${YELLOW}Failed to read $miner from ${WEB_HOST}:{$WEB_PORT}${NOCOLOR}"
	else
	#fucking bminer sorts it's keys as numerics, not natual, e.g. "1", "10", "11", "2", fix that with sed hack by replacing "1": with "01":
	stats_raw=$(echo "$stats_raw" | sed -E 's/"([0-9])":\s*\{/"0\1":\{/g' | jq -c --sort-keys .) #"
	
	khs=`echo $stats_raw | jq '.miners[].solver.solution_rate' | awk '{s+=$1} END {print s/1000}'` #"
	
	local hs=`echo $stats_raw | jq '.miners[].solver.solution_rate' | awk '{ printf("%f ",$1/1000) }'` #"
	local uptime=$(( `date +%s` - $(jq '.start_time' <<< "$stats_raw") ))
	algo=`echo $stats_raw | jq -c '[.algorithm]' | sed 's/\[\"//g; s/\"\]//g'`
	[[ $algo == "zhash" ]] && algo="equihash 144/5"

	stats=$(jq -c --arg uptime "$uptime" \
				--arg algo "$algo" \
				--argjson hs "`echo ${hs[@]} | tr " " "\n" | jq -cs '.'`" \
				'{$hs,
						temp: [.miners[].device.temperature], fan: [.miners[].device.fan_speed], $uptime, $algo,
						ar: [.stratum.accepted_shares, .stratum.rejected_shares]}' <<< "$stats_raw")
	fi

	[[ -z $khs ]] && khs=0
	[[ -z $stats ]] && stats="null"

