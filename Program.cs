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
    SELECT Employee_Id, Name, Salary, Dept_Id,
    ROW_NUMBER() OVER (PARTITION BY Dept_Id ORDER BY SALARY DESC) AS Dept_Sal_Rank
    )t
WHERE Dept_Sal_Rank = 3;


// Delete Duplicate records(with ID)

    DELETE FROM Employees
	WHERE EmployeeId NOT IN 
	(
		SELECT FirstName, MIN(EmployeeId) As DuplicateData
		FROM Employees
		GROUP BY FirstName
	)

// Delete Duplicate records (without Id)
	WITH CTE AS 
	(
		SELECT FirstName, ROW_NUMBER() OVER (PARTITION BY FirstName ORDER BY FirstName) AS ROW_NUM
		FROM Employees
	)
		DELETE FROM CTE WHERE ROW_NUM > 1



// Find department-wise average salary

	SELECT DeptId, AVG(Salary) AS AvgSalary
	FROM Employee
	GROUP BY DeptId;


// Find department-wise total salary

	SELECT DeptId, SUM(Salary) AS TotalSalary
	FROM Employee
	GROUP BY DeptId;


// Find top 3 highest paid employees

	SELECT TOP 3 *
	FROM Employee
	ORDER BY Salary DESC;


// Find employees with same salary

	SELECT Salary, COUNT(*) AS Total
	FROM Employee
	GROUP BY Salary
	HAVING COUNT(*) > 1;



