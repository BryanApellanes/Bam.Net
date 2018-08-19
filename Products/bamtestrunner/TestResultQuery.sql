-- \\integration\c$\bam\data\Test\SysData\Databases\Bam.Net.Automation.Testing.Data.Dao.Repository.TestingRepository_Testing.sqlite
SELECT td.AssemblyFullName, td.MethodName, td.Description, te.Passed, te.Tag, te.Exception, te.Created, te.Stacktrace 
FROM Testexecution te 
JOIN Testdefinition td on te.TestDefinitionId = td.Id
WHERE 
Passed = 0 AND 
te.Tag = '{Tag}'
ORDER BY te.Created DESC