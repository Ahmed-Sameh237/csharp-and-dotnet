# Reflection_19

## 1. My Lab Values

- Lab ID: 8
- MIN_GRADE_LAB: 1.0 + (8 mod 4) × 0.5 ==> 8 mod 4 = 0     ==> 1.0 + (0 × 0.5) = 1.0
- COURSE_COUNT: (8 mod 3) + 2 ==> 8 mod 3 = 2              ==> 2 + 2 = 4

======================================================================================================

## 2. Three Places Enrollment Data Can Be Rejected

Enrollment data can be rejected in three places before it reaches the database:

1. Client-side validation in the browser prevents invalid values from being submitted.
2. Server-side validation using "ModelState" and validation attributes like [Range] checks the submitted data after it reaches the controller.
3. The database can reject data that violates constraints such as the unique index on (StudentId, CourseId).

My Part D change belongs to the server-side validation because I changed the [Range] attribute on the "Grade" property.

======================================================================================================


## 3. Why a Foreign Key Alone Isn't Enough

- Using only a foreign key between "Student" and "Course" does not work because one student can take many courses, 
and one course can have many students. 
- The "Enrollment" entity fixes this by acting as the linking table and also storing additional information such as "EnrollmentDate" and "Grade".

======================================================================================================


## 4. Choosing a Delete Behavior

If a future "Assignment" belongs to a "Course", I would use Cascade delete. 
When a course is deleted, its assignments should also be deleted because they cannot exist without their course
,which keeps the database consistent.
