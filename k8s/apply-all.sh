#!/bin/bash

# For Unix-like operating systems (Linux Distros, Mac OS ...)
# /> chmod +x apply-all.sh

# Apply all files (including subdirectories)
kubectl apply -f metrics.yaml
kubectl apply -f techlanches-namespace.yaml
kubectl apply -f techlanches-secrets.yaml
kubectl apply -f hpas/techlanches-api-hpa.yaml
kubectl apply -f ./deployments/techlanches-sql-deployment.yaml
kubectl apply -f ./deployments/techlanches-api-deployment.yaml
kubectl apply -f ./deployments/techlanches-worker-deployment.yaml