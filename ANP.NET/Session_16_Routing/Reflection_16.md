#################### 
	Reflection 16		
####################

### 1. Lab ID and Derived Values

 Lab ID: 8

 MAX_YEAR: Formula: (8 % 4) + 1 = 0 + 1 = 1

 MIN_GPA: Formula: 2.5 + (8 % 3) × 0.5 = 2.5 + (2 * 0.5 ) = 3.5

 INTAKE_CODE: Formula: 8 % 3 = 2 ==> "itiC"

###################################################################################

### 2. What happens when the request is one above MAX_YEAR?

My MAX_YEAR is 1, so requesting "/students/top/2" is outside the allowed range. 
The framework first receives the request and starts to match:
	It checks the route constraints before executing the action and If the route doesn't match the constraints, the request is rejected and a 404 Not Found response is returned.

###################################################################################

### 3. Compare the built-in "int" constraint and my custom "intakecode" constraint

Both constraints are used by the routing system to decide whether a URL matches a route before the request reaches the controller.

The difference is that the built-in "int" constraint is provided by ASP.NET Core and only checks whether the route value is an integer.
But my "intakecode" constraint is a custom class that I wrote. 
It performs a case-insensitive comparison and only accepts my intake code ("itiC").

###################################################################################

### 4. Why is "/Students/About" unreachable?

Because the "About" action uses attribute routing.
It is only reachable through its explicitly defined route (`/about/ahmed`) "Attribute routing",
that default route and the conventioal routes can't be reached.