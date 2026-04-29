//Find employees with salary greater than 30,000

using System.Collections.Concurrent;

SELECT * 
FROM Employee
WHERE Salary > 30000;


//Find employees with salary between 20,000 and 35,000
SELECT *
FROM Employee
WHERE Salary BETWEEN 20000 AND 35000;


// Nth Highest Salary (Window Function)
SELECT * FROM
(
    SELECT EMPLOYEE_Id, NAME, SALARY, DENSE_RANK() OVER (ORDER BY SALARY DESC) AS SalaryRank
   )t
    WHERE SalaryRank = 3;


// Departmentwise Salary (Window Function)
SELECT * FROM
    (
    SELECT EMPLOYEE_Id, Name, Salary, Dept_Id,
    ROW_NUMBER() OVER (PARTITION BY ORDER BY SALARY DESC) AS DEPT_Sal
    )t
    WHERE DEPT_Sal = 3; 