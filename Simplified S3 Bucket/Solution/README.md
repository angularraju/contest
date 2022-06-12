# Deployment Details
Below are the required softwares to be install before start installation locally
- dotnet/sdk:6.0
- Docker Desktop
- Kubernetes
- Helm
<br>
## Installation Instructions/Steps
- Clone this repo locally
- Go to path "contest\Simplified S3 Bucket\Solution\SimplifiedS3Bucket" and build docker image
  <br>for example command - docker build -t simplifieds3bucket -f Dockerfile .
- Tag and push docker image to GitHub repo by referring below example commands
  <br>docker tag simplifieds3bucket rajumakadenice/simplifieds3bucket
  <br>docker push rajumakadenice/simplifieds3bucket
- To create Persistent Volume Claim in kubernetes which is one time activity. <br>Go to the path "contest\Simplified S3 Bucket\Solution\" and run the helm chart by command -  
 helm install s3bucket-infra .\s3bucket-infra-chart\ 
- For deployment of application, Go to path "contest\Simplified S3 Bucket\Solution\simplifieds3bucket-deployment-chart" and update **image** tag with above image build in **Values.yaml.**
<br>Run Helm chart by command - 
<br>helm install fileupload .\simplifieds3bucket-deployment-chart\
- After successful installation application is accessible by url http://localhost:30005
<br>
## Note
- If you want to uninstall application then uninstall Persistent Volume Claim as well. 
- for re-deployment again follow same steps.