# Document template system

ASP.Net Core application for storing, creating and using document templates. 
User interface is covered with Angular 7 CLI, which contacts C# server retrieving data from MySQL database.

Basic Users Functionalities

As an Administrator you can:
* Activate new users accounts,
* Delete existing ones,
* Change template state between active and inactive, 
* Accept new template for usage,
* Change template owner,
* Edit user data and create user access levels.

As an Editor you can:
* Create new templates, 
* Edit ones that you are owner of, 
* Cede ownership of template to another user.

As an User you can:
* Pick a template document from given list,
* Fill template with your data,
* Print/export prepared document to PDF file,
* Edit you account data.
