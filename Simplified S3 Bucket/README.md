## SIMPLIFIED S3 BUCKET
Create a simplified S3 bucket using k8s. The following criteria should be met.
- The user should be able to post a file to the system from outside kubernetes
- It is expected that the user only posts files with unique names
- No versioning If the user posts a file with the same name, the previous copy of the file should be overwritten
- The files should only be on emptyDir ephemeral storage. Databases, PVs or Mapped drives should not be used to store the files.
- There should always be 3 copies of the files stored on 3 separate pods.
- The user should be able to perform a read-after-write action with a 5 second delay. This means that as a user I should be able to post a file, wait for 5 seconds and Get the file without failure using the same end point from outside the k8s. I just have to change the verb on the endpoint and provide valid parameters and content. The system can return the file from any of the copies, but the user should not be required to change the url to access from different types of copies.
- Only txt file is to be expected as inputs. No images or non txt files will be used to test. The file size will be 1kb or less.