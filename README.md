git clone https://github.com/sgebb/bad.git

cd bad/src

dotnet build

dotnet run

dotnet test

runs on: https://localhost:7111/swagger/index.html (on my machine...)

requires .NET 6.0 SDK

If you're not able to run localdb (probably because you're using Mac), check slack at #testworkshop-2022 for instructions on how to target a database running in azure