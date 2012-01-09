ReleaseCandidateManager.exe -a create -v 2.0.0.1 -t SomeTitle -l "PG Releases" -s https://url-to-sharepoint-site
ReleaseCandidateManager.exe -a update -v 2.0.0.1 -l "PG Releases" -s https://url-to-sharepoint-site -u IntegrationTestsFailed
ReleaseCandidateManager.exe -a list -l "PG Releases" -s https://url-to-sharepoint-site