## INSTRUCTIONS
Using the sample signalr chat application [here](https://docs.microsoft.com/en-us/aspnet/signalr/overview/getting-started/tutorial-getting-started-with-signalr) create a container.<br>
Deploy this container as a k8s deployment with 1 replica which should be accessible from outside the k8s cluster.<br>
Create another application that scales the deployment from 1 replica to 2 replicas when there are 10 clients connected to the first pod<br>

## Notes
The system will not be tested for more than 2 replicas.