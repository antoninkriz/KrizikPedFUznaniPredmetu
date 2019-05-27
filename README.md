# KrizikPedFUznaniPredmetu
##### _High-school seminary work - 2019_
##### _User-friendly Recognition of Completion of Courses for CUNI PedF_
___
# About
## Installation and requirements
### Rquirements:
- *Debian
  - tested on Linux Mint 18.04 but should work on any common distro
- *DotNet Core SDK*
  - rested with version 2.2.0
- *Docker*
  - tested with version 18.09.4
- *Python 3*
  - with following libraries: `uuid`, `urllib`, `lxml`
- *MySQL client*
  - testesd with version 14.14
- Networking:
  - `api.local` redirected to `127.0.0.1` (testeds using `hosts` file)
  - Ports:
    - Web: `8080` - application itself, `5000, 5050, 5051` - running api and services
    - RabbitMQ: `5672` - messaging queue itself, `15672` - RabbitMQ management
    - Database: `30006` - MySQL database port
### Installation
```sh
# requirements already satisfied
git clone https://github.com/antoninkriz/KrizikPedFUznaniPredmetu.git
cd KrizikPedfUznaniPredmetu/Tools/DockerDeploy
sudo ./netcore-publish.sh
````
## Structure
Whole project can be divided into following parts:
- **Back-end**
  - *KarolinkaUznani.Api* - Api containing only controllers and connection to RabbitMQ
  - *KarolinkaUznani.Services* - Services themselves
    - ... *.Auth* - Everything related to users - sign up, login, ...
    - ... *.Data* - Fetching data related to Karolinka only from the database
- **Front-end**
  - KarolinkaUznani.Api/*[ClientApp](https://github.com/antoninkriz/KrizikPedFUznaniPredmetu-ClientApp)* - Whole front-end client with styles and everything around
- **Database**
  - *Database/\*.sql* - SQL scripts to create and seed the database from
  - *KarolinkaUznani.Database* - contains only stored procedures
- **Tools**
  - *DockerDeploy* - script for one-click deployment of the whole project
  - *KarolinkaToSQL* - script parsing Karolinka into one huge SQL insert

### Choice of technologies
#### General
- *Docker* - for simple project deployment across other systems
- *RabbitMQ* - messaging queue for communication between API and Services
- *MySQL* - platform on which is the whole database based around
##### [Docker](https://github.com/docker/compose)
A great tool to eliminate "it works on my machine" phrase. Simple to setup with docker-compose and sounds great as a buzzword.
##### [RabbitMQ](https://github.com/rabbitmq)
Since RabbitMQ is the most used solution of its type, combined with [RawRabbit](https://github.com/pardahlman/RawRabbit) (.Net framework for communication with RabbitMQ server) and since it's also used at my workplace, the choice was simple.
##### [MySQL](https://github.com/mysql)
To make everything sound even cooler my first choice was MongoDB, but since the relational database is a better choice for this project, I stuck to MySQL which I already have more experience with. However, the whole structure of this project takes in mind possible usage of other types of databases.
#### Back-End
- *C# DotNet Core* - framework the whole project is based on
##### [C# DotNet Core](https://github.com/Microsoft/dotnet)
With my love for modern C#, its usage at my workplace, simple one-click creation of SPA (Single Page Application) web project and an already solid amount of experience, the choice was simple.
#### Front-End
- *Angular 7* - front-end TypeScript based web framework
- *SCSS* - CSS extension language
##### [Angular 7](https://github.com/angular/angular)
With already some experience with React, I wanted to try something new, its also used at my workplace and with its popularity in other job listings, it was a simple choice over VueJS. [TypeScript](https://github.com/Microsoft/TypeScript)s 
##### [SCSS](https://github.com/sass])
Pure CSS makes me ~~sucidal~~ unhappy to work with, especially on bigger projects, so [LESS](https://github.com/less) or SCSS was an obvious choice. For my purposes, both would do the job just fine, but SCSS was easier for me to integrate with Angular.
#### Tools
##### [Python 3](https://github.com/python/)
With a solid amount of libraries, Python was the perfect choice for something like a script parsing HTML from a website.
