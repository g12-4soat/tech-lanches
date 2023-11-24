#!/bin/bash

# For Unix-like operating systems (Linux Distros, Mac OS ...)
# /> chmod +x apply-all.sh

# Apply all files (including subdirectories)
kubectl apply -f . --recursive