#!/bin/bash

until mysqladmin ping -h db -u root -P 3306 --silent; do
  echo 'Waiting for MySQL to be ready...'
  sleep 2
done


echo 'Applying database migrations...'
cd /App
dotnet ef database update 
#--project /App/Contactly.csproj
#dotnet ef database update

echo 'Starting application...'
dotnet Contactly.dll