Developer: Jordan Castling-Bolt
Student Number: ST10114132
Date: 29/05/2023
PROG7311 POE Part 2

***************************************
Farm Central Stock Management Prototype
***************************************

Welcome!, this is a prototype for a Farm Central stock management system.

1. Getting Started: 
	- To get started, you will need a version of visual studio installed. 
	- Once you have installed visual studio, you will need to open the file 'PROG_Part2' and find the file name 'PROGPart2.sln', double click the visual studio solution to open it.
	- Once you have opened the solution, you will need to build the solution. To do this, click on the 'Build' tab at the top of the screen, and click 'Build Solution'.
	- Once the solution has been built, you will need to run the solution. To do this, click on the 'Debug' tab at the top of the screen, and click 'Start Debugging'.
	- This will open up a browser tab showing the Landing page of the prototype, click the 'Login' button below 'Getting started' and you will be rediredcted to the 'Login' page.
	- Once you are on the 'Login' page, you will need to login to the system. To do this, you will need to enter the following credentials as either an Employee or Farmer:
	-Employee:
		- Username: 1011
		- Password: Password
	-Farmer:
		- Username: 1012/1111/1114/5432/7474 (pick one)
		- Password: Password
2. Employee features:
	- Once you have logged in as an Employee, you will be able to view all farmers in the Database and can edit, get details, or delete them.
		- An Employee is also able to add a farmer to the database by clicking the 'Add a Farmer' button.
		- The Employee should create a unique ID for the new farmer and add their personal details accordingly.
		- An Employee is also able to view all products in the database by clicking the 'Farmer Products' button.
			- This will redirect the Employee to the 'Farmer Products' page where they can select a farmer by their ID
			  and after clicking the 'Search' button, all products added by that farmer will be displayed.
			- An Employee can filter the results by selecting a start date and end date.
			- Or they can filter by product type by using the search box
		- An employee will also be able to logout of the system by clicking 'Farm Central Employee' at the top of the page.
3. Farmer features:
	- Once you have logged in as a farmer, you will be able to view all products that the logged in farmer added to the system.
		- A farmer is able to edit, get details, or delete products that they have added to the system.
		- A farmer can create a new product by clicking the 'Create New Product' button.
			- This will redirect the farmer to the 'Create Product' page where they will be instructed to add the relevant information accordingly.
		- A farmer will also be able to logout of the system by clicking 'Farm Central Farmer' at the top of the page. 

4. To view data in database:	
	- To view the data in the database, you will need to open the 'Server Explorer' tab on the left hand side of the screen.
	- Once you have opened the 'Server Explorer' tab, you will need to expand the 'Data Connections' tab and then expand the 'FarmCentralDBEntities1 (PROG_Part2)' tab.
	- Once you have expanded the 'FarmCentralDBEntities1 (PROG_Part2)' tab, you will need to expand the 'Tables' tab.
	- Once you have expanded the 'Tables' tab, you will be able to view all the tables in the database, and by right clicking on a table and selecting the 'Show Table Data', you will be able to view the data in that table.
