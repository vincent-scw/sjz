# Deploy to Azure Kubernetes service (AKS)

## Steps
- Follow the [tutorials](https://docs.microsoft.com/en-us/azure/aks/) to setup AKS
- Install Helm and Tiller
  - `helm init --service-account tiller --history-max 200`

### Setup
- Create Namespaces `kubectl apply -f ./namespaces.yaml`
- Add ServiceAccount via `kubectl apply -f ./service-account.yaml`
  - `tiller` is for Helm
  - `kubernetes-dashboard` is for Kubernetes Dashboard

### Prerequisite
- Deploy [Mongodb](https://mongodb.com/) to Kubernetes
  - Run `helm install stable/mongodb --name mongodb --namespace infra --set mongodbRootPassword={your-password}`
- Deploy [neo4j](https://neo4j.com/) to Kubernetes
  - Run `helm install stable/neo4j --name neo4j --namespace infra --set acceptLicenseAgreement=yes --set neo4jPassword={your-password} --set core.numberOfServers=1`
- Deploy [Nginx](https://www.nginx.com/) to Kubernetes
  - Create a static IP, reference to https://docs.microsoft.com/en-us/azure/aks/static-ip
  - Run `helm install stable/nginx-ingress --name nginx --namespace sjz --set controller.replicaCount=2 --set controller.service.loadBalancerIP={{StaticIP}}`
- Use TLS with Let's Encrypt 
  - Reference to https://docs.microsoft.com/en-us/azure/aks/ingress-static-ip