#!/usr/bin/env bash

#######################
# Functions
#######################


get_cards_hashes(){
	# hs is global
	hs=''
	for (( i=0; i < ${GPU_COUNT_NVIDIA}; i++ )); do
		#local MHS=$(cat /var/log/miner/${CUSTOM_MINER}/${CUSTOM_MINER}.log | grep -a "Cycles/s" | tail -n 1 | cut -d " " -f10 | awk '{printf "%.6f",$1}') | jq -n '$MHS/$GPU_COUNT' | awk '{printf "%.6f",$0}'
		local MHS=`cat /var/log/miner/${CUSTOM_MINER}/${CUSTOM_MINER}.log | grep -a "Cycles/s" | tail -n 1 | cut -d " " -f10 | awk '{printf $1/6}'`
		hs[$i]=`echo $MHS | awk '{ printf $0 }'`
	done
}

get_nvidia_cards_temp(){
	echo $(jq -c "[.temp$nvidia_indexes_array]" <<< $gpu_stats)
}

get_nvidia_cards_fan(){
	echo $(jq -c "[.fan$nvidia_indexes_array]" <<< $gpu_stats)
}

get_miner_uptime(){
	local tmp=$(ps -p `pgrep $CUSTOM_NAME` -o lstart=)
	local start=$(date +%s -d "$tmp")
        local now=$(date +%s)
        echo $((now - start))
}

get_total_hashes(){
        # khs is global
        local Total=`cat /var/log/miner/${CUSTOM_MINER}/${CUSTOM_MINER}.log | grep -a "Cycles/s" | tail -n 1 | cut -d " " -f10 | awk '{printf $1/1000}'`
        echo $Total
}

get_log_time_diff(){
        local getLastLogTime=`tail -n 100 /var/log/miner/${CUSTOM_MINER}/${CUSTOM_MINER}.log | grep -a "Cycles/s" | tail -n 1 | awk {'print $1,$2'} | sed 's/[][]//g'`
        local logTime=`date --date="$getLastLogTime" +%s`
        local curTime=`date +%s`
        echo `expr $curTime - $logTime`
}

#######################
# MAIN script body
#######################

. /hive/miners/custom/$CUSTOM_MINER/h-manifest.conf
local LOG_NAME="$CUSTOM_LOG_BASENAME.log"

[[ -z $GPU_COUNT_NVIDIA ]] &&
	GPU_COUNT_NVIDIA=`gpu-detect NVIDIA`



# Calc log freshness
local diffTime=110
local maxDelay=120

# If log is fresh the calc miner stats or set to null if not
if [ "$diffTime" -lt "$maxDelay" ]; then
	local hs=
	get_cards_hashes					# hashes array
	local hs_units='hs'				# hashes utits
	local temp=$(get_nvidia_cards_temp)	# cards temp
	local fan=$(get_nvidia_cards_fan)	# cards fan
	local uptime=$(get_miner_uptime)	# miner uptime
	local algo="cuckoo"		# algo

	# A/R shares by pool
	#local ac=$(get_miner_shares_ac)
	#local rj=$(get_miner_shares_rj)
#	echo ac: $ac
#	echo rj: $rj

	# make JSON
	stats=$(jq -nc \
				--argjson hs "`echo ${hs[@]} | tr " " "\n" | jq -cs '.'`" \
				--arg hs_units "$hs_units" \
				--argjson temp "$temp" \
				--argjson fan "$fan" \
				--arg uptime "$uptime" \
				--arg algo "cuckoo" \
				'{$hs, $hs_units, $temp, $fan, $uptime, $algo}')
	# total hashrate in khs
	khs=$(get_total_hashes)
else
	stats=""
	khs=0
fi

# debug output
#echo temp:  $temp
#echo fan:   $fan
#echo stats: $stats
#echo khs:   $khs
#echo diff: $diffTime
#echo uptime: $uptime
