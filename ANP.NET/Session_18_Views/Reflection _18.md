# Reflection 18

## 1. Lab ID, CHIP_YEAR and CHIP_LABEL

* Lab ID: 8
* CHIP_YEAR: (8 % 4) + 1 = (0) + 1 = 1
* CHIP_LABEL: Since CHIP_YEAR is not 4, the label is "Year 1".

==============================================================================================

## 2. Similar duplication from previous sessions

Part B removed duplicated table-row markup by moving it into a partial view so it exists in only one place.
A similar situation happened in the previous sessions with routing. Instead of hard-coding URLs throughout the views, Tag Helpers (`asp-controller`, `asp-action`, and `asp-route-id`) generate the URLs from the routing configuration. 
This removes duplicated route strings and ensures that changing the route configuration updates every generated link automatically.

==============================================================================================

## 3. Common pattern shared by the route constraint, validation attribute, and tag helper

All three extend the ASP.NET Core framework by creating a custom class that implements or inherits from a framework type, 
and the framework automatically discovers and executes it at the appropriate time.
The route constraint runs during routing, the validation attribute runs during model validation, and the tag helper runs while Razor renders a view
,allowing custom behavior without changing controller code.

==============================================================================================

## 4. Rule vs. label

The lecture's <gpa-badge> contains a rule because it decides which GPA values belong to each badge category and chooses the appropriate styling.

My <year-chip> mainly contains a label because it displays the academic year and highlights my assigned CHIP_YEAR.

I would be much more concerned about duplicating the GPA badge logic because business rules are more likely to change.
	Keeping the rule in one Tag Helper ensures every view stays consistent and only needs to be updated in one place.
