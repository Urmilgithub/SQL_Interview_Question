//Find employees with salary greater than 30,000

using System.Collections.Concurrent;
using System.Xml.Linq;

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


// Find cumulative salary by department

	SELECT *,
	       SUM(Salary) OVER (PARTITION BY DeptId ORDER BY HireDate) AS CumulativeSalary
	FROM Employee;


-----------------------------------------------------------------------------------------
Self Joins
-----------------------------------------------------------------------------------------

// Find Employees with their Manager

	SELECT
	    e.name AS employee,
	    m.name AS manager
	FROM employees e
	LEFT JOIN employees m
	ON e.manager_id = m.emp_id;


// Show Only Employees Who Have Managers

	SELECT e.name, m.name
	FROM employees e
	JOIN employees m
	ON e.manager_id = m.emp_id;


// Count Employees Under Each Manager

	SELECT m.name AS manager, COUNT(e.emp_id) AS team_size
	FROM employees e
	JOIN employees m
	ON e.manager_id = m.emp_id
	GROUP BY m.name;


// Find Employees Reporting to Same Manager

	SELECT e1.name, e2.name, e1.manager_id
	FROM employees e1
	JOIN employees e2
	ON e1.manager_id = e2.manager_id
	AND e1.emp_id <> e2.emp_id;


// Find Top-Level Managers (No Manager)

	SELECT name
	FROM employees
	WHERE manager_id IS NULL;

----------------------------------------------------------------------------------------------------

// Employees with Salary > Average 

    SELECT *
	FROM employees
	WHERE salary > (SELECT AVG(salary) FROM employees);


// Update Salary (Increase by 10%)

	UPDATE employees
	SET salary = salary * 1.10;


// Find Duplicate Salaries

	SELECT salary
	FROM employees
	GROUP BY salary
	HAVING COUNT(*) > 1;
