# library-api

### Set up a new ASP.NET Core project.
### Define models for Book and User with appropriate fields such as title, author, ISBN, publication year, name, email, password, etc.
### Set up Entity Framework Core and create a database context to handle interactions with the database.
### Implement CRUD operations for books and users using the database context. This includes creating, reading, updating, and deleting books and users.
Create, read, update, and delete operations can be implemented in your controllers or a separate service layer using the database context.

### Implement a simple user authentication mechanism using ASP.NET Core Identity or a custom solution. This includes registering new users, authenticating users with login/logout functionality, and updating user profiles.
### Implement book reservation functionality. Ensure that each user can reserve a maximum of 3 books at a time and prevent users from reserving books that are already reserved by someone else.
Add a reservation table to your database schema to track book reservations by users.
Implement the necessary logic to enforce the maximum of 3 reservations per user and prevent users from reserving already reserved books.

### Update the database schema:
Add a new table named "Reservations" to track book reservations.
The table should have columns for the reservation ID, book ID, user ID, and reservation date.
The book ID and user ID columns will serve as foreign keys to the respective tables.

### Implement the logic to enforce the maximum reservations per user and prevent reserving already reserved books:
In the book reservation API endpoint, check if the user has already reached the maximum reservation limit (3 reservations).
Verify if the book is already reserved by another user.
If the conditions are met, create a new reservation entry in the Reservations table.
