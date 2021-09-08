# ASP.Net Core Applications And Cloud Services
This is my repository for class work!


## Website Project
[For my website project I built a small ASP.NET Core MVC website called **The Learning Center**.](https://github.com/larspassic/ASP.Net-Core-Applications-And-Cloud-Services/tree/main/Homework/The%20Learning%20Center) 

The website uses a simple SQL Express database with 3 tables: 
- **Class** to store information about classes
- **User** to store information about users
- **UserClass** to store the relationship of a user being registered for a class.

#### #1 - Index
The index page serves as the starting point for the website. The navigation bar at the top-right corner of the page changes to display a user's name if they are signed in.
![picture](https://github.com/larspassic/ASP.Net-Core-Applications-And-Cloud-Services/blob/main/Homework/The%20Learning%20Center/Index.png?raw=true)

#### #2 - Register for an account
This page allows the user to enroll in a new account, with some basic user input validation. I did not use the default ASP.NET identity solution, I used a custom one.
![picture](https://github.com/larspassic/ASP.Net-Core-Applications-And-Cloud-Services/blob/main/Homework/The%20Learning%20Center/Register%20account.png?raw=true)

#### #3 - Log in
This page allows an existing user to log in. I had a few challenges trying to get the [Authorize] attribute to successfully redirect the user to this log in page.
![picture](https://github.com/larspassic/ASP.Net-Core-Applications-And-Cloud-Services/blob/main/Homework/The%20Learning%20Center/Log%20in.png?raw=true)

#### #4 - Class list
This was the screen to display the information on all of the available classes. The user does not need to be authenticated to see this page. All of the class information in the table is pulled from the SQL express database.
![picture](https://github.com/larspassic/ASP.Net-Core-Applications-And-Cloud-Services/blob/main/Homework/The%20Learning%20Center/Class%20list.png?raw=true)

#### #5 - Enroll in a class
This page allows the user to register for a class. The user needs to be authenticated for enrollment to work correctly, so the user must be logged in to visit this page.
![picture](https://github.com/larspassic/ASP.Net-Core-Applications-And-Cloud-Services/blob/main/Homework/The%20Learning%20Center/Enroll%20in%20class.png?raw=true)

#### #6 - Enrolled classes
This page allows a currently logged in user to view the classes that they are currently enrolled in. In order to view all of this data in one screen, I needed to create a custom model, which combines data elements from two models. The **ClassModel** contains all of the detailed information about the classes themselves, like Name and Description and Price. The **EnrollModel** contains the relationship between a student, and the class they are enrolled in. I built a custom model called UserEnrolledClassesViewModel that brings many of these elements together so that they can be displayed together in a single view.
![picture](https://github.com/larspassic/ASP.Net-Core-Applications-And-Cloud-Services/blob/main/Homework/The%20Learning%20Center/Enrolled%20classes.png?raw=true)


## REST service project

Currently in progress!
