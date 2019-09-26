# Trip Planner

#### _A web site were you can add and check list of your different trip you would like to plan ahead, Here User needs to create their profile and account and store their lists of trip data. With the help of different API the user could also check the weather, Hotels where they can stay, Restraunts and Dinn in places and get directions with the help of Maps.- Septemper 27, 2019_

#### _By **Saswati Patra**_

## Description

A web site were you can add and check list of your different trip you would like to plan ahead, Here User needs to create their profile and account and store their lists of trip data. With the help of different API the user could also check the weather, Hotels where they can stay, Restraunts and Dinn in places and get directions with the help of Maps.

## Setup/Installation Requirements

* This application requires MySQL.

1. Clone this repository:
    ```
    $ git clone https://github.com/saswatipatra/TripPlanner

    ```
2. Open the database context file (TripPlanner/Models/TripPlannerContext.cs) and replace `password=epicodus` with a string containing your MySQL password (ex: `"abcd123"`).

3. Log onto MySQL:
    ```
    $ mysql -u USERNAME -p PASSWORD
    ```
5. Navigate to the project directory (TripPlanner). Restore dependencies, update your local database, and run the API:
    ```
    $ dotnet restore
    $ dotnet ef database update
    $ dotnet run
    ```
## Using the API

### **Basic CRUD**
| Endpoint | HTTP Method | Description |
| :------------- | :------------- | :------------- |
| `api/Flight/` | GET |  Returns all the Flights |
| `api/Hotel/` | GET | Returns all the Hotels near the place|
| `api/Restruant` | GET |  Returns all the Restraunts nearby |
| `api/Weather/` | GET |  Returns that day Weather |
| `api/Maps/` | GET |  Returns a specific Map |

## Flow chart and Mock up Structure
![FlowChart](https://user-images.githubusercontent.com/51075552/65715071-3fde0b00-e051-11e9-852b-f988f4498099.jpg)
![HomePage](https://user-images.githubusercontent.com/51075552/65715277-a400cf00-e051-11e9-8a9b-88d82683f8ad.jpg)
![SignUp](https://user-images.githubusercontent.com/51075552/65715288-a95e1980-e051-11e9-83bc-1129e663c0b7.jpg)
![UserWelcomePage](https://user-images.githubusercontent.com/51075552/65715295-acf1a080-e051-11e9-95bb-f9d51861878e.jpg)
![AddTrip](https://user-images.githubusercontent.com/51075552/65715302-b3801800-e051-11e9-86af-a793ca5849f2.jpg)
![DetailPage](https://user-images.githubusercontent.com/51075552/65715320-baa72600-e051-11e9-8cda-a578c486bca1.jpg)

## Known Bugs
* No known bugs at this time.

## Technologies Used
* C#
* Asp.Net Core
* Css
* bootstrap
* Mysql Workbench

## Support and contact details

_Please contact  Saswati with questions and comments._

### License

*GNU GPLv3*

Copyright (c) 2019 **_Saswati Patra_**