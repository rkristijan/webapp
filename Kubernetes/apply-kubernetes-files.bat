@echo off

kubectl apply -f namespace.yaml

kubectl apply -f volumeclaim.yaml

kubectl apply -f secrets.yaml

kubectl apply -f mssql_service.yaml

kubectl apply -f mssql_deploy.yaml

kubectl apply -f appservice.yaml

kubectl apply -f appdeploy.yaml

kubectl apply -f ingress.yaml

kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.8.1/deploy/static/provider/cloud/deploy.yaml 
pause