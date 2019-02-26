# Document template system

## General info
Web application for storing and creating document templates. 

## Technologies
Project is created with:
* ASP.NET Core 2.0,
* Angular 7 CLI,
* MS SQL.

## Features
As an Administrator you can:
* Authorize a new user account,
* Delete existing user accounts,
* Change user state between active, blocked and suspended, 
* Accept a new document template for usage,
* Transfer ownership of template to another Editor,
* Edit the data of each user,
* Change template state between active/inactive,
* Manage data base.

As an Editor you can:
* Update your account data,
* Create a new template, 
* Edit only your own template, 
* Cede ownership of template to another Editor.

As a User you can:
* Update your account data,
* Pick a template from given list,
* Fill in the template,
* Print/export prepared document to PDF file.

## [ERD](https://drive.google.com/file/d/1NHWDXeRJNL_sWFSTiQWzgRamhUHoRkK5/view?usp=sharing)

## Sample Input:
SampleInput file contains sample template sent from Editor's panel. It is stored in data base as a html file. 
Diamond signs <> represents data which should be filled in later. In <> signs @ markup sign represents data that 
should be loaded from data base based on user opening the template, and # markup sign represents data that will
be collected by form sent to user to fill in.
