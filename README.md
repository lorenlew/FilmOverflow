Film Overflow
-----------------------------------------------------------------------------------------------------------------------

Specification:

Develop the system for online purchase of tickets in cinemas.
The system must have 4-level access layer.


-For anonymous users there is available movie list, seance list, tickets' prices, movies' details and movies' reviews browsing.


-There must be an ability for registered users to estimate movies, write reviews, reserve and buy tickets.
There must be provided an ability to view free/sold/reserved seats for a certain seance,
personal user's profile, purchased tickets.


-Moderators must have an ability to view list with users, manage movies, seances, access of users.


-Administrators must have an ability to view list with users,
manage roles and access of users and, in particular, moderators,
change the list of cinemas.

Administrators and moderators have an ability to create custom flexible hall's schemas for cinemas.

In the application have to be implemented: 
- email confirmation for registration of users,
- two-way (client/server) validation for all CUD operations,
- signalR support for operations with ticket reservation/purchase.

-----------------------------------------------------------------------------------------------------------------------
Technology stack: ASP.NET MVC 5 (razor), MS SQL, javascript (knockout.js, jquery.js, bootstrap.js),
html5, css3 (twitter bootstrap), SignalR, ninject, markdown.
