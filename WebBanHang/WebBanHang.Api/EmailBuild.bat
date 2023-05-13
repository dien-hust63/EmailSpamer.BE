dotnet restore
dotnet publish -c Release

del /S bin\Release\net6.0\publish\*.pdb

docker build -t dien2khust/emailspamer:0.0.0.1 .

docker push dien2khust/emailspamer:0.0.0.1