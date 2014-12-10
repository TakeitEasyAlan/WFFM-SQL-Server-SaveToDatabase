WFFM-SQL-Server-SaveToDatabase
==============================

Sitecore 7.5 - Web Forms for Marketers (WFFM) 2.5 - SQL Data Provider (SaveToDatabase)

This project provides the ability to store and retrieve data from the existing WFFM SQL database, as with the release of WFFM 2.5 (Sitecore 7.5 version) SQL Server is no longer supported. 

WFFM 2.5 only supports the use of xDB (MongoDB) to store submitted form data, whilst I also believe MongoDB is a much better choice for storing non-structured, there are unfortunately a number of websites that want to upgrade to 7.5 but they are not ready to use MongoDB.

So the intention of this Sitecore module is to allow them to continue with their existing SQL based database (i.e. SaveToDatabase action).

This is currently simple version that allows you to do the following

1)	Save data to a WFFM SQL database

2)	Retrieve all submitted data for a given form via API

3)  Provide the ability to download the data for a given form as an Excel spreadsheet from within the Sitecore client.

Documetation: http://blog.coates.dk/2014/11/22/sql-provider-for-web-forms-for-marketers-wffm-2-5-sitecore-7-5



