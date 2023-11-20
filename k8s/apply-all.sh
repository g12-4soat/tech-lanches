#!/bin/bash

# For Unix-like operating systems (Linux Distros, Mac OS ...)
# /> chmod +x apply-all.sh

# Apply Namespace
kubectl apply -f techlanches-namespace.yaml

# Apply Secrets
kubectl apply -f techlanches-secrets.yaml

# Apply ConfigMap
kubectl apply -f techlanches-configmap.yaml

# Apply PV and PVC for SQL Server
kubectl apply -f techlanches-sql-pv.yaml
kubectl apply -f techlanches-sql-pvc.yaml

# Apply Deployments
kubectl apply -f techlanches-api-deployment.yaml
kubectl apply -f techlanches-worker-deployment.yaml

# Apply Services
kubectl apply -f techlanches-api-service.yaml
kubectl apply -f techlanches-worker-service.yaml

# Apply HPA
kubectl apply -f techlanches-api-hpa.yaml
kubectl apply -f techlanches-worker-hpa.yaml