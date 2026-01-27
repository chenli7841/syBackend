# Deployment
1. On local machine:

`dotnet publish -c Release -r linux-x64 --self-contained true`

2. Copy everything from syBackend\EplusCore\bin\Release\net6.0\linux-x64\ to /opt/record/newEplusCore/

3. On remote server:

`cd /opt/record/newEplusCore/`
`screen ./WebUI --urls=http://localhost:5002`
