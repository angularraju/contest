## SIMPLIFIED S3 BUCKET
Create a simplified S3 bucket using k8s. The following criteria should be met.
- The user should be able to post a file to the system from outside kubernetes
- It is expected that the user only posts files with unique names
- If the user posts a file with the same name, the previous copy of the file should be overwritten.
- There should always be 3 copies of the files stored on the system.
- There should be 3 replicas of the service running behind a cluster IP service.
- The user should be able to perform a read-after-write action with a 5 second delay. This means that as a user I should be able to post a file, wait for 5 seconds and Get the file without failure using the same end point, but different HTTP verbs and parameters from outside the k8s. 
- Only txt file is to be expected as inputs. No images or non txt files will be used to test. The file size will be 1kb or less.
- Files should be posted via Http requests
