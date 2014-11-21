WFFM-SQL-Server-SaveToDatabase
==============================

Web Form For Marketeer's SQL Data Provider for Sitecore 7.5

With the release of Web Forms for Marketers 2.5 for Sitecore 7.5, SQL server is no longer supported. This project provides the ability to store and retrieve data which is stored in SQL server for version prior to 2.5.

With 7.5 WFFM only supports the use of xDB (MongoDB) to store form data that is submitted. MongoDB is a better choice for storing non-structured, but there are a number of websites that want to upgrade to 7.5 but they are not ready to use MongoDB and the intention of this Sitecore module is to allow them to continue with their existing SQL based database for storing WFFM data (i.e. SaveToDatabase action).
This is currently simple version that allows you to do the following
1)	Save data to an WFFM SQL database
2)	Retrieve all submitted data for a given form via API
In the future I intend to provide the ability to down load the data for a given form as an Excel spreadsheet from within the Sitecore client.

