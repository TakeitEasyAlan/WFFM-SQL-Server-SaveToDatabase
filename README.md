WFFM-SQL-Server-SaveToDatabase
==============================

Web Form For Marketeer's (WFFM) SQL Data Provider for Sitecore 7.5

This project provides the ability for WFFM 2.5 to store and retrieve data from the existing WFFM SQL database.

With the release of WFFM 2.5 (Sitecore 7.5) SQL Server for the storage of form data is no longer supported. 

WFFM 2.5 only supports the use of xDB (MongoDB) to store form data that is submitted, whilst I also believe MongoDB is a much better choice for storing non-structured, there are unfrotunatly a number of websites that want to upgrade to 7.5 but they are not ready to use MongoDB.

So the intention of this Sitecore module is to allow them to continue with their existing SQL based database (i.e. SaveToDatabase action).

This is currently simple version that allows you to do the following

1)	Save data to an WFFM SQL database
2)	Retrieve all submitted data for a given form via API

In the future I intend to provide the ability to down load the data for a given form as an Excel spreadsheet from within the Sitecore client.

