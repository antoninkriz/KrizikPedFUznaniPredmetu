#!/bin/bash
HOME=$(pwd)
USER=$(id -u -n)

sudo echo "---SUDO---"

echo "---PREPARE---"
cd $home
rm -rf ./bin
sudo rm -rf ./data
#rm -rf ./.config
#rm -rf ./.dotnet
#rm -rf ./.local
#rm -rf ./.npm
mkdir ./bin
mkdir ./data
mkdir ./data/db
mkdir ./data/dbinit

echo "---ASPNET---"
cd $HOME
cd ../../KarolinkaUznani
rm -rf ./*/bin
rm -rf ./*/obj
dotnet restore .
dotnet publish ./KarolinkaUznani.Api -c Release -o ../../Tools/DockerDeploy/bin/Api --no-restore
dotnet publish ./KarolinkaUznani.Services.Auth -c Release -o ../../Tools/DockerDeploy/bin/Services.Auth --no-restore
dotnet publish ./KarolinkaUznani.Services.Data -c Release -o ../../Tools/DockerDeploy/bin/Services.Data --no-restore

echo "---DATABASE---"
cd $HOME
cd ../KarolinkaToSQL
####################python3 main.py > ../../Database/Data.sql
cd $home
cd ../../KarolinkaUznani/KarolinkaUznani.Database/StoredProcedures
rm ../../../Database/Procedures.sql;
for f in */*.sql; do (cat "${f}"; echo) >> ../../../Database/Procedures.sql; done
cd $home
cp ../../Database/User.sql ./data/dbinit/User.sql
cp ../../Database/Schema.sql ./data/dbinit/Schema.sql
cp ../../Database/Data.sql ./data/dbinit/Data.sql
cp ../../Database/Procedures.sql ./data/dbinit/Procedures.sql
sed -i 's/`root`@`localhost`/`KarolinkaDb`@`localhost`/g' ./data/dbinit/Procedures.sql

echo "---DOCKER---"
cd $HOME
docker-compose up --detach --build

#echo "---DATA---"
#mysql -hlocalhost -P 30006 -uroot -pAsdf.1234 KarolinkaDb < ./data/dbinit/Schema.sql
#mysql -hlocalhost -O 30006 -uroot -pAsdf.1234 KarolinkaDb < ./data/dbinit/User.sql
#mysql -hlocalhost -P 30006 -uroot -pAsdf.1234 KarolinkaDb < ./data/dbinit/Procedures.sql
#mysql -hlocalhost -P 30006 -uroot -pAsdf.1234 KarolinkaDb < ./data/dbinit/Data.sql

echo "---CLEANUP---"
#rm -rf ./.config
#rm -rf ./.dotnet
#rm -rf ./.local
#rm -rf ./.npm
#rm -rf ./.nuget
