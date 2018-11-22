#!/usr/bin/env bash

. h-manifest.conf


./$CUSTOM_NAME $(< /hive/custom/$CUSTOM_NAME/$CUSTOM_NAME.conf)