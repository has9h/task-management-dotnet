# Task Management App 

*This repository hosts the backend, the frontend repository, built using Angular, can be found [here](https://github.com/has9h/task-management-angular)*

Tech Stack built on the .NET Core Framework, using Web APIs.

- Angular 13.3.1
- .NET Core 5.0.406 
- PostgreSQL 10.2
- Bootstrap CSS 5.1.3

## Objectives

- [x] Users can add new Tasks
- [x] Users can edit existing Tasks
- [x] Users can delete Tasks
- [ ] Users can sort by Task Title, Date Created, Date Updated
- [ ] Users can search by Task Title
- [x] Users should be able to set status
- [x] Users should be able to set progress

## Constraints

- [x] Use REST APIs

## Additional Requirements

- [x] Use Bootstrap for responsiveness
    - *Not fully implemented*
- [ ] Use sockets to synchronize updates across tabs
- [ ] CI/CD Deployment
- [ ] Test Codes


## Architecture and Takeaways

- Understanding of the C# and .NET ecosystems were required for backend
- Structuring controllers, repositories, data classes, models, and configuration files
- Middleware ordering, HTTP pipelines, and routing
- Dependency injection and service method life cycles
- Entity framework core and working with PostgreSQL
- SignalR was not explored

Application was developed and manually tested locally. The backend is a web API application. The entrypoint of the application is defined as standard, under the `Program` class. The `Startup` class registers all the necessary services and configures all the middlewares for the application.  Primary keys of the database are not shown.

Schemas for the structure of the database are defined under the `Model` directory. The `status` Enum is handled in the model directory, by creating a separate value data type and defining the appropriate auto-property accessors. This is bound to a set of radio buttons in the frontend. The `progress` percentage variable is bound to a bootstrap slider, split into quartiles.

The `AspNetCore.JsonPatch` package was used to implement `PATCH` requests. Only the backend implementation exists; they can be tested using Postman or any other 3rd party API testing platform. 

Dependency injection allows the application to be loosely coupled and create an abstraction for the data access layer: The `Repository` directory is used to store the interface and the corresponding implementation that is used by the controller. Routes and their corresponding response methods are defined under `Controller`. 

Migrations were made sequentially, and can be tracked under the `Migrations` directory. The backend application was incrementally built and tested, starting with the `title` and `description` fields, and incorporating dates, progress and status after each successful testing of the APIs.  

The default Swagger service is kept as is provided by default with Web API application in Visual Studio.

The Angular frontend makes API calls to the backend and renders the responses. 

There is no currently implemented security or authentication layer. On startup, a query retrieves and renders all tasks from the database on the front page. Tasks can be edited and deleted. There are separate pages for adding and editing tasks. Deleting tasks refreshes the page and sorts the serial ID columns. Creating tasks creates the same time stamp for both the `Date Created` and `Date Updated` fields in the database. Updating the tasks updates the `Date Updated` value to the time of update automatically. 

A component level C4 model diagram for the system architecture is given below

![component-level-c4-model](https://user-images.githubusercontent.com/25825681/161847838-01e8ee35-da85-469a-8b58-3639a16dad0c.png)

### UI

The following images show the landing page, the add and the edit pages respectively

![localhost_4200_](https://user-images.githubusercontent.com/25825681/161848066-3dfd2432-6484-4339-bd6f-4d6a81bdd145.png)

![localhost_4200_add](https://user-images.githubusercontent.com/25825681/161847994-7535424d-e80f-4d57-ab92-f9e48c6b7e7b.png)

![localhost_4200_edit_37](https://user-images.githubusercontent.com/25825681/161848053-f42c500f-a2e2-41c3-b44a-596d0fdbefab.png)


## Optimization
- *Frontend:* HTML code was not DRY across components. Major scope for optimization.
- *Frontend:* Database queries can be reduced
- *Backend:* Automappers can be used for cleaner code between repository and controllers
- *Backend:* HTTP responses can be made better

### Known bugs

- Setting the progress slider to `Complete` should automatically set it to `100`.
