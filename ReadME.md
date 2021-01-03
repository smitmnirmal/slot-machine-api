## Prerequisites

* .NET Core SDK 3.0 or later
* Visual Studio 2019 with the ASP.NET and web development workload
* Postman

## To run the project

* Clone this repo 

    ```
    git clone https://github.com/smitnirmal3597/slot-machine-api
    ```

* Run the project from Visual Studio 2019

* Open Postman

1. User Login
    * Select Post from request type dropdown
    * Enter url https://localhost:44392/api/user in request url input and pass UserName "temp" and Password "temp" in the form-data.
    ![Login](https://github.com/smitnirmal3597/slot-machine-api/blob/master/login.JPG?raw=true)
    * Copy the token form response body which we will use for authentication

2. Get User Balance
    * Select GET from request type dropdown
    * Enter url https://localhost:44392/api/slotmachine in request url input and inside the headers tab add "Authorization" inside key input and paste the token which we get from the previous request inside the value field by prepending "Bearer " to it
    ![Check Balance](https://github.com/smitnirmal3597/slot-machine-api/blob/master/userbalance.JPG?raw=true)

3. Place Bet
    * Select POST from request type dropdown
    * Enter url https://localhost:44392/api/slotmachine in request url input and inside the headers tab add "Authorization" inside key input and paste the token inside the value field by prepending "Bearer " to it
    * Add the amount value inside the form-data
    ![Place Bet](https://github.com/smitnirmal3597/slot-machine-api/blob/master/login.JPG?raw=true)