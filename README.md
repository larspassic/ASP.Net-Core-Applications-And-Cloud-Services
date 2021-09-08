# ASP.Net Core Applications And Cloud Services
 Repository for class work


### Website Project
[For my website project I built a website called **The Learning Center**.](https://github.com/larspassic/ASP.Net-Core-Applications-And-Cloud-Services/tree/main/Homework/The%20Learning%20Center) The goal of this project was to create a small education website using ASP.NET Core MVC.

#### #1 - Index
This was the index page. 
![picture](https://github.com/larspassic/Creating-Client-Applications-Using-.NET-Core/blob/main/Homework/CarTracker/cartracker.png?raw=true)

#### #2 - Register for an account
This was the page that allows the user to enroll in a new account. I did not use the default ASP.NET identity solution, I used a custom one.

#### #3 - Log in
This was the page where the user could log in. I had a few challenges trying to get the [Authorize] attribute to successfully redirect the user to the log in page.

#### #4 - Class list
This was the screen to display the information on all of the available classes. The user does not need to be authenticated to see this page.

#### #5 - Enroll in a class
This page allows the user to register for a class. The user needs to be authenticated for enrollment to work correctly, so the user must be logged in to visit this page.

#### #6 - Enrolled classes
This page allows a currently logged in user to view the classes that they are currently enrolled in. In order to view all of this data in one screen, I needed to create a custom model, which combines data elements from two models. The **ClassModel** contains all of the detailed information about the classes themselves, like Name and Description and Price. The **EnrollModel** contains the relationship between a student, and the class they are enrolled in. I built a custom model called UserEnrolledClassesViewModel that brings many of these elements together so that they can be displayed together in a single view.


### REST service project
